using FluentAssertions;
using System;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace ReserbizAPP.IntegrationTests.Controllers
{
    public class TestControllerTest : BaseIntegrationTest
    {
        public TestControllerTest(ApiWebApplicationFactory fixture) : base(fixture)
        {

        }

        [Fact]
        public async Task GET_current_datetime()
        {
            await InitializeAuthorizationAndTestDataAsync();

            var response = await _client.GetAsync("api/test/getCurrentDateTime");
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var currenDateTime = Convert.ToDateTime(await response.Content.ReadAsStringAsync());
            currenDateTime.Should().Be(DateTime.Now);
        }
    }
}