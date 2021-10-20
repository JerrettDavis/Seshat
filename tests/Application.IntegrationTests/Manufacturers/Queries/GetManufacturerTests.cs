using System;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Seshat.Application.Common.Exceptions;
using Seshat.Application.IntegrationTests.Scenarios;
using Seshat.Application.Manufacturers.Commands.CreateManufacturer;
using Seshat.Application.Manufacturers.Models;
using Seshat.Application.Manufacturers.Queries.GetManufacturer;

namespace Seshat.Application.IntegrationTests.Manufacturers.Queries
{
    using static Testing;
    public class GetManufacturerTests : TestBase
    {
        [Test]
        public async Task ShouldGetManufacturer()
        {
            // Arrange
            var scenario = await GetScenarioBuilder()
                .BuildAsync();
            var manufacturer = await scenario.SendAsync(new CreateManufacturerCommand(
                new ManufacturerInputModel
                {
                    Name = Guid.NewGuid().ToString()
                }));
            var query = new GetManufacturerQuery(manufacturer.Id);
            
            // Act
            var result = await scenario.SendAsync(query);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(manufacturer.Id);
            result.Name.Should().Be(manufacturer.Name);
        }

        [Test]
        public async Task ShouldRequireMinimumFields()
        {
            // Arrange
            var scenario = await GetScenarioBuilder()
                .BuildAsync();
            var query = new GetManufacturerQuery("");
            
            // Act & Assert
            await FluentActions.Invoking(() => scenario.SendAsync(query))
                .Should().ThrowAsync<ValidationException>();
        }
        
        [Test]
        public async Task ShouldRequireValidManufacturer()
        {
            // Arrange
            var scenario = await GetScenarioBuilder()
                .BuildAsync();
            var query = new GetManufacturerQuery(Guid.NewGuid().ToString());
            
            // Act & Assert
            await FluentActions.Invoking(() => scenario.SendAsync(query))
                .Should().ThrowAsync<ValidationException>();
        }
    }
}