using System.Threading.Tasks;
using Application.Clients.Queries.GetClientsWithPagination;
using Application.Common.Exceptions;
using Domain.Entities;

namespace Application.FunctionalTests.Queries.Clients;

public class GetClientsWithPaginationTests : BaseTestFixture
{
    [Test]
    public async Task RequestFromUnauthorizedUser_ThrowsForbiddenException()
    {
        await RunAsDefaultUserAsync();

        var query = new GetClientsWithPaginationQuery();

        var act = () => SendAsync(query);

        await act.Should().ThrowAsync<ForbiddenAccessException>();
    }

    [Test]
    public async Task NoClients_ReturnsEmptyClientsList()
    {
        await RunAsAdministratorAsync();

        var query = new GetClientsWithPaginationQuery();

        var result = await SendAsync(query);

        result.Should().NotBeNull();
        result.TotalCount.Should().Be(0);
        result.TotalPages.Should().Be(0);
        result.PageNumber.Should().Be(1);
        result.HasNextPage.Should().BeFalse();
        result.HasPreviousPage.Should().BeFalse();
        result.Items.Should().BeEmpty();
    }

    [Test]
    public async Task ClientsExist_ReturnsClientsPaginatedList()
    {
        await RunAsAdministratorAsync();

        var expectedFirstEntity = new Client
        {
            Name = "First Name",
            Address = "Volgograd"
        };

        var expectedSecondEntity = new Client
        {
            Name = "Second Name",
            Address = "Moscow"
        };

        await AddAsync(expectedFirstEntity);
        await AddAsync(expectedSecondEntity);

        var query = new GetClientsWithPaginationQuery();

        var result = await SendAsync(query);

        result.Should().NotBeNull();
        result.TotalCount.Should().Be(2);
        result.TotalPages.Should().Be(1);
        result.PageNumber.Should().Be(1);
        result.HasNextPage.Should().BeFalse();
        result.HasPreviousPage.Should().BeFalse();
        result.Items.Should().HaveCount(2).And.SatisfyRespectively(
            firstItem =>
            {
                firstItem.Name.Should().Be(expectedFirstEntity.Name);
                firstItem.Address.Should().Be(expectedFirstEntity.Address);
            },
            secondItem =>
            {
                secondItem.Name.Should().Be(expectedSecondEntity.Name);
                secondItem.Address.Should().Be(expectedSecondEntity.Address);
            });
    }

    [Test]
    public async Task PageSizeSetToOne_ReturnsClientsPaginatedListWithOneElement()
    {
        await RunAsAdministratorAsync();

        var expectedFirstEntity = new Client
        {
            Name = "First Name",
            Address = "Volgograd"
        };

        var expectedSecondEntity = new Client
        {
            Name = "Second Name",
            Address = "Moscow"
        };

        await AddAsync(expectedFirstEntity);
        await AddAsync(expectedSecondEntity);

        var query = new GetClientsWithPaginationQuery
        {
            PageSize = 1
        };

        var result = await SendAsync(query);

        result.Should().NotBeNull();
        result.TotalCount.Should().Be(2);
        result.TotalPages.Should().Be(2);
        result.PageNumber.Should().Be(1);
        result.HasNextPage.Should().BeTrue();
        result.HasPreviousPage.Should().BeFalse();
        result.Items.Should().HaveCount(1).And.SatisfyRespectively(firstItem =>
        {
            firstItem.Name.Should().Be(expectedFirstEntity.Name);
            firstItem.Address.Should().Be(expectedFirstEntity.Address);
        });
    }

    [Test]
    public async Task PageSizeSetToOneAndPageNumberSetToTwo_ReturnsClientsPaginatedListWithOneElement()
    {
        await RunAsAdministratorAsync();

        var expectedFirstEntity = new Client
        {
            Name = "First Name",
            Address = "Volgograd"
        };

        var expectedSecondEntity = new Client
        {
            Name = "Second Name",
            Address = "Moscow"
        };

        await AddAsync(expectedFirstEntity);
        await AddAsync(expectedSecondEntity);

        var query = new GetClientsWithPaginationQuery
        {
            PageNumber = 2,
            PageSize = 1
        };

        var result = await SendAsync(query);

        result.Should().NotBeNull();
        result.TotalCount.Should().Be(2);
        result.TotalPages.Should().Be(2);
        result.PageNumber.Should().Be(2);
        result.HasNextPage.Should().BeFalse();
        result.HasPreviousPage.Should().BeTrue();
        result.Items.Should().HaveCount(1).And.SatisfyRespectively(firstItem =>
        {
            firstItem.Name.Should().Be(expectedSecondEntity.Name);
            firstItem.Address.Should().Be(expectedSecondEntity.Address);
        });
    }

    [Test]
    public async Task SearchNameSet_ReturnsClientsPaginatedListWithSearchedName()
    {
        await RunAsAdministratorAsync();

        var expectedFirstEntity = new Client
        {
            Name = "First Name",
            Address = "Volgograd"
        };

        var expectedSecondEntity = new Client
        {
            Name = "Second Name",
            Address = "Moscow"
        };

        await AddAsync(expectedFirstEntity);
        await AddAsync(expectedSecondEntity);

        var query = new GetClientsWithPaginationQuery
        {
            Search = expectedFirstEntity.Name
        };

        var result = await SendAsync(query);

        result.Should().NotBeNull();
        result.TotalCount.Should().Be(1);
        result.TotalPages.Should().Be(1);
        result.PageNumber.Should().Be(1);
        result.HasNextPage.Should().BeFalse();
        result.HasPreviousPage.Should().BeFalse();
        result.Items.Should().HaveCount(1).And.SatisfyRespectively(firstItem =>
        {
            firstItem.Name.Should().Be(expectedFirstEntity.Name);
            firstItem.Address.Should().Be(expectedFirstEntity.Address);
        });
    }
}
