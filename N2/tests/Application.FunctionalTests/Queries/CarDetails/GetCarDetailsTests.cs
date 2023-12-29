using System.Threading.Tasks;
using Application.CarDetails.Queries.GetCarDetails;
using Domain.Entities;

namespace Application.FunctionalTests.Queries.CarDetails;

public class GetCarDetailsTests : BaseTestFixture
{
    [Test]
    public async Task NoCarDetails_ReturnsEmptyCarDetailsList()
    {
        await RunAsDefaultUserAsync();

        var query = new GetCarDetailsQuery();

        var result = await SendAsync(query);

        result.Should().BeEmpty();
    }

    [Test]
    public async Task CarDetailsExist_ReturnsCarDetailsList()
    {
        await RunAsDefaultUserAsync();

        var expectedFirstEntity = new CarDetail
        {
            Name = "First Detail",
            Price = 200
        };

        var expectedSecondEntity = new CarDetail
        {
            Name = "Second Detail",
            Price = 400
        };

        await AddAsync(expectedFirstEntity);
        await AddAsync(expectedSecondEntity);

        var query = new GetCarDetailsQuery();

        var result = await SendAsync(query);

        result.Should().HaveCount(2).And.SatisfyRespectively(
            firstItem =>
            {
                firstItem.Name.Should().Be(expectedFirstEntity.Name);
                firstItem.Price.Should().Be(expectedFirstEntity.Price);
            },
            secondItem =>
            {
                secondItem.Name.Should().Be(expectedSecondEntity.Name);
                secondItem.Price.Should().Be(expectedSecondEntity.Price);
            });
    }

    [Test]
    public async Task LimitSetToOne_ReturnsCarDetailsListWithOneElement()
    {
        await RunAsDefaultUserAsync();

        var expectedFirstEntity = new CarDetail
        {
            Name = "First Detail",
            Price = 200
        };

        var expectedSecondEntity = new CarDetail
        {
            Name = "Second Detail",
            Price = 400
        };

        await AddAsync(expectedFirstEntity);
        await AddAsync(expectedSecondEntity);

        var query = new GetCarDetailsQuery
        {
            Limit = 1
        };

        var result = await SendAsync(query);

        result.Should().HaveCount(1).And.SatisfyRespectively(firstItem =>
        {
            firstItem.Name.Should().Be(expectedFirstEntity.Name);
            firstItem.Price.Should().Be(expectedFirstEntity.Price);
        });
    }

    [Test]
    public async Task SearchNameSet_ReturnsCarDetailsListWithSearchedName()
    {
        await RunAsDefaultUserAsync();

        var expectedFirstEntity = new CarDetail
        {
            Name = "First Detail",
            Price = 200
        };

        var expectedSecondEntity = new CarDetail
        {
            Name = "Second Detail",
            Price = 400
        };

        await AddAsync(expectedFirstEntity);
        await AddAsync(expectedSecondEntity);

        var query = new GetCarDetailsQuery
        {
            Search = expectedFirstEntity.Name
        };

        var result = await SendAsync(query);

        result.Should().HaveCount(1).And.SatisfyRespectively(firstItem =>
        {
            firstItem.Name.Should().Be(expectedFirstEntity.Name);
            firstItem.Price.Should().Be(expectedFirstEntity.Price);
        });
    }
}
