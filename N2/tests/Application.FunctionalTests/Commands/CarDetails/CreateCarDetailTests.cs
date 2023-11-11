using System.Threading.Tasks;
using Application.CarDetails.Commands.CreateCarDetail;
using Application.Common.Exceptions;
using Domain.Entities;

namespace Application.FunctionalTests.Commands.CarDetails;

public class CreateCarDetailTests : BaseTestFixture
{
    [Test]
    public async Task RequestFromUnauthorizedUser_ThrowsForbiddenException()
    {
        await RunAsDefaultUserAsync();

        var command = new CreateCarDetailCommand
        {
            Name = "First Detail",
            Price = 200
        };

        var act = () => SendAsync(command);

        await act.Should().ThrowAsync<ForbiddenAccessException>();
    }

    [Test]
    public async Task CorrectArguments_ReturnsCreatedCarDetail()
    {
        await RunAsAdministratorAsync();

        var expectedEntity = new CarDetail
        {
            Name = "First Detail",
            Price = 200
        };

        var command = new CreateCarDetailCommand
        {
            Name = expectedEntity.Name,
            Price = expectedEntity.Price
        };

        var result = await SendAsync(command);

        result.Should().NotBeNull();
        result.Id.Should().BeGreaterThan(0);
        result.Name.Should().Be(expectedEntity.Name);
        result.Price.Should().Be(expectedEntity.Price);

        var carDetail = await FindAsync<CarDetail>(result.Id);
        carDetail.Should().NotBeNull();
        carDetail.Name.Should().Be(expectedEntity.Name);
        carDetail.Price.Should().Be(expectedEntity.Price);
    }
}
