using NUnit.Framework;

namespace Seshat.Application.IntegrationTests
{
    using static Testing;

    public class TestBase
    {
        [SetUp]
        public void TestSetUp()
        {
            ResetState();
        }
    }
}