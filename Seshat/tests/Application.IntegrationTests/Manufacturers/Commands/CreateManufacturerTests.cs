using System;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Seshat.Application.Common.Exceptions;
using Seshat.Application.IntegrationTests.Scenarios;
using Seshat.Application.Manufacturers.Commands.CreateManufacturer;
using Seshat.Application.Manufacturers.Models;
using Seshat.Domain.Entities;

namespace Seshat.Application.IntegrationTests.Manufacturers.Commands
{
    using static Testing; 
    
    public class CreateManufacturerTests : TestBase
    {
        [Test]
        public async Task ShouldRequireMinimumFields()
        {
            // Arrange
            var scenario = await GetScenarioBuilder()
                .BuildAsync();
            var command = new CreateManufacturerCommand(new ManufacturerInputModel());
            
            // Act & Assert
            await FluentActions.Invoking(() => scenario.SendAsync(command))
                .Should().ThrowAsync<ValidationException>();
        }

        [Test]
        public async Task ShouldCreateManufacturer()
        {
            // Arrange
            string userId = null!;
            var scenario = await GetScenarioBuilder()
                .RunAsDefaultUser(id => userId = id)
                .BuildAsync();
            var command = new CreateManufacturerCommand(
                new ManufacturerInputModel
                {
                    Name = Guid.NewGuid().ToString()
                });
            
            // Act
            var result = await scenario.SendAsync(command);
            var item = await scenario.GetPublicEntity<Manufacturer>(result.Id);
            
            // Assert
            result.Should().NotBeNull();
            result.Id.Should().NotBeNullOrWhiteSpace();
            result.Name.Should().Be(command.Model.Name);

            item.Name.Should().Be(command.Model.Name);
            item.PublicIdentifier.Should().Be(result.Id);
            item.CreatedBy.Should().Be(userId); 
            item.Created.Should().BeCloseTo(DateTime.Now, TimeSpan.FromSeconds(10));
            item.LastModified.Should().BeNull();
            item.LastModifiedBy.Should().BeNull();
        }

        [Test]
        public async Task ShouldRequireUniqueName()
        {
            // Arrange
            var scenario = await GetScenarioBuilder()
                .BuildAsync();
            var command = new CreateManufacturerCommand(
                new ManufacturerInputModel
                {
                    Name = Guid.NewGuid().ToString()
                });
            // Create the first manufacturer 
            await scenario.SendAsync(command);

            
            // Act & Assert
            // Then we try to create another with the same name
            await FluentActions.Invoking(() => scenario.SendAsync(command))
                .Should().ThrowAsync<ValidationException>();
        }
    }
}