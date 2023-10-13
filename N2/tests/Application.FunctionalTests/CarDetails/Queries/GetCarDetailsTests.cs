using System.Threading.Tasks;
using Application.CarDetails.Queries.GetCarDetails;
using Domain.Entities;

namespace Application.FunctionalTests.CarDetails.Queries;

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

        var expectedEntity = new CarDetail
        {
            Name = "detail",
            Price = 200
        };

        await AddAsync(expectedEntity);

        var query = new GetCarDetailsQuery();

        var result = await SendAsync(query);

        result.Should().HaveCount(1);
        result.Should().SatisfyRespectively(item =>
        {
            item.Name.Should().Be(expectedEntity.Name);
            item.Price.Should().Be(expectedEntity.Price);
        });
    }
}
