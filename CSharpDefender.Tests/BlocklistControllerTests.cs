using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using CSharpDefender.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace CSharpDefender.Tests
{
    public class BlocklistControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public BlocklistControllerTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task AddBlockedIp_WithValidIp_ReturnsSuccessAndBlocksIp()
        {
            // Arrange
            var client = _factory.CreateClient();
            var ipToBlock = "1.2.3.4";
            var payload = new { ip = ipToBlock };

            // Act
            var postResponse = await client.PostAsJsonAsync("/api/blocklist", payload);

            // Assert
            postResponse.EnsureSuccessStatusCode(); // Status Code 200-299

            var getResponse = await client.GetAsync("/api/blocklist");
            getResponse.EnsureSuccessStatusCode();
            var blockedIps = await getResponse.Content.ReadFromJsonAsync<string[]>();

            Assert.Contains(ipToBlock, blockedIps);
        }
    }
}