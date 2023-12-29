using System.Threading.Tasks;
using Application.Clients.Commands.CreateClient;
using Application.Common.Exceptions;
using Domain.Entities;

namespace Application.FunctionalTests.Commands.Clients;

public class CreateClientTests : BaseTestFixture
{
    [Test]
    public async Task RequestFromUnauthorizedUser_ThrowsForbiddenException()
    {
        await RunAsDefaultUserAsync();

        var command = new CreateClientCommand
        {
            Name = "First Name",
            Address = "Volgograd"
        };

        var act = () => SendAsync(command);

        await act.Should().ThrowAsync<ForbiddenAccessException>();
    }

    [Test]
    public async Task CorrectArguments_ReturnsCreatedClient()
    {
        await RunAsAdministratorAsync();

        var expectedEntity = new Client
        {
            Name = "First Name",
            Address = "Volgograd"
        };

        var command = new CreateClientCommand
        {
            Name = expectedEntity.Name,
            Address = expectedEntity.Address
        };

        var result = await SendAsync(command);

        result.Should().NotBeNull();
        result.Id.Should().BeGreaterThan(0);
        result.Name.Should().Be(expectedEntity.Name);
        result.Address.Should().Be(expectedEntity.Address);

        var client = await FindAsync<Client>(result.Id);
        client.Should().NotBeNull();
        client.Name.Should().Be(expectedEntity.Name);
        client.Address.Should().Be(expectedEntity.Address);
    }
}
