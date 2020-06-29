using System.Threading.Tasks;
using NUnit.Framework;

namespace CleanArchitecture.IntegrationTest
{
  using static SetupTestEnvironment;
  public class TestBase
  {
    [SetUp]
    public async Task SetUp()
    {
      await ResetState();
    }
  }
}