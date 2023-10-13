using System.Threading.Tasks;

namespace Application.FunctionalTests;

[TestFixture]
public abstract class BaseTestFixture
{
    [SetUp]
    public async Task TestSetUpAsync()
    {
        await ResetState();
    }
}
