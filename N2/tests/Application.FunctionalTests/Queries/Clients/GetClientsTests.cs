using System.Threading.Tasks;
using Application.Clients.Queries.GetClients;
using Application.Common.Exceptions;
using Domain.Entities;

namespace Application.FunctionalTests.Queries.Clients;

public class GetClientsTests : BaseTestFixture
{
    [Test]
    public async Task RequestFromUnauthorizedUser_ThrowsForbiddenException()
    {
        await RunAsDefaultUserAsync();

        var query = new GetClientsQuery();

        var act = () => SendAsync(query);

        await act.Should().ThrowAsync<ForbiddenAccessException>();
    }

    [Test]
    public async Task NoClients_ReturnsEmptyClientsList()
    {
        await RunAsAdministratorAsync();

        var query = new GetClientsQuery();

        var result = await SendAsync(query);

        result.Should().BeEmpty();
    }

    [Test]
    public async Task ClientsExist_ReturnsClientsList()
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

        var query = new GetClientsQuery();

        var result = await SendAsync(query);

        result.Should().HaveCount(2).And.SatisfyRespectively(
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
    public async Task LimitSetToOne_ReturnsClientsListWithOneElement()
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

        var query = new GetClientsQuery
        {
            Limit = 1
        };

        var result = await SendAsync(query);

        result.Should().HaveCount(1).And.SatisfyRespectively(firstItem =>
        {
            firstItem.Name.Should().Be(expectedFirstEntity.Name);
            firstItem.Address.Should().Be(expectedFirstEntity.Address);
        });
    }

    [Test]
    public async Task SearchNameSet_ReturnsClientsListWithSearchedName()
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

        var query = new GetClientsQuery
        {
            Search = expectedFirstEntity.Name
        };

        var result = await SendAsync(query);

        result.Should().HaveCount(1).And.SatisfyRespectively(firstItem =>
        {
            firstItem.Name.Should().Be(expectedFirstEntity.Name);
            firstItem.Address.Should().Be(expectedFirstEntity.Address);
        });
    }
}
