using System;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Seshat.Application.Common.Exceptions;
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
            var command = new CreateManufacturerCommand(new ManufacturerInputModel());
            
            // Act & Assert
            await FluentActions.Invoking(() => SendAsync(command))
                .Should().ThrowAsync<ValidationException>();
        }

        [Test]
        public async Task ShouldCreateManufacturer()
        {
            // Arrange
            var userId = await RunAsDefaultUserAsync();
            var command = new CreateManufacturerCommand(
                new ManufacturerInputModel
                {
                    Name = Guid.NewGuid().ToString()
                });
            
            // Act
            var result = await SendAsync(command);
            var item = await GetPublicEntity<Manufacturer>(result.Id);
            
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
            var command = new CreateManufacturerCommand(
                new ManufacturerInputModel
                {
                    Name = Guid.NewGuid().ToString()
                });
            // Create the first manufacturer 
            await SendAsync(command);

            
            // Act & Assert
            // Then we try to create another with the same name
            await FluentActions.Invoking(() => SendAsync(command))
                .Should().ThrowAsync<ValidationException>();
        }
    }
}