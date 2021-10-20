using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Seshat.Application.IntegrationTests.Scenarios;
using Seshat.Application.Manufacturers.Commands.CreateManufacturer;
using Seshat.Application.Manufacturers.Models;
using Seshat.Application.Manufacturers.Queries.GetManufacturers;

namespace Seshat.Application.IntegrationTests.Manufacturers.Queries
{
    using static Testing;
    public class GetManufacturersTests : TestBase
    {
        [Test]
        public async Task ShouldGetList()
        {
            // Arrange
            var scenario = await GetScenarioBuilder()
                .BuildAsync();
            Func<Task<ManufacturerDto>> createManufacturer = 
                async () => await scenario.SendAsync(
                new CreateManufacturerCommand(
                    new ManufacturerInputModel
                    {
                        Name = Guid.NewGuid().ToString()
                    }));
            var manufacturers = Enumerable.Range(1, 10)
                .Select(async _ => await createManufacturer())
                .Select(r => r.Result)
                .ToList();
            var command = new GetManufacturersQuery();
            
            // Act
            var result = (await scenario.SendAsync(command)).ToList();
            
            // Assert
            result.Should().NotBeNullOrEmpty();
            result.Should().HaveCount(manufacturers.Count);
            result.Should().BeInAscendingOrder(r => r.Name);
        } 
    }
}