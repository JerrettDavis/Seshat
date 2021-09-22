using System;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Seshat.Application.Common.Exceptions;
using Seshat.Application.Manufacturers.Commands.CreateManufacturer;
using Seshat.Application.Manufacturers.Models;
using Seshat.Application.Printers.Commands.CreatePrinter;
using Seshat.Application.Printers.Models;
using Seshat.Domain.Entities;

namespace Seshat.Application.IntegrationTests.Printers.Commands
{
    using static Testing; 
    
    public class CreatePrinterTests : TestBase
    {
        [Test]
        public async Task ShouldRequireMinimumFields()
        {
            // Arrange
            var command = new CreatePrinterCommand(new PrinterInputModel());
            
            // Act & Assert
            await FluentActions.Invoking(() => SendAsync(command))
                .Should().ThrowAsync<ValidationException>();
        }
        
        [Test]
        public async Task ShouldCreateManufacturer()
        {
            // Arrange
            var userId = await RunAsDefaultUserAsync();
            var manufacturer = await SendAsync(
                new CreateManufacturerCommand(
                    new ManufacturerInputModel
                    {
                        Name = Guid.NewGuid().ToString()
                    }));
            var command = new CreatePrinterCommand(
                new PrinterInputModel
                {
                    Model = Guid.NewGuid().ToString(),
                    ManufacturerId = manufacturer.Id
                });
            
            // Act
            var result = await SendAsync(command);
            var item = await GetPublicEntity<Printer>(result.Id);
            
            // Assert
            result.Should().NotBeNull();
            result.Id.Should().NotBeNullOrWhiteSpace();
            result.Model.Should().Be(command.Model.Model);
            result.Manufacturer.Id.Should().Be(manufacturer.Id);
            result.Manufacturer.Name.Should().Be(manufacturer.Name);

            item.Model.Should().Be(command.Model.Model);
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
            var manufacturer = await SendAsync(
                new CreateManufacturerCommand(
                    new ManufacturerInputModel
                    {
                        Name = Guid.NewGuid().ToString()
                    }));
            var command = new CreatePrinterCommand(
                new PrinterInputModel
                {
                    Model = Guid.NewGuid().ToString(),
                    ManufacturerId = manufacturer.Id
                });
            
            // Create the first printer 
            await SendAsync(command);

            
            // Act & Assert
            // Then we try to create another with the same name
            await FluentActions.Invoking(() => SendAsync(command))
                .Should().ThrowAsync<ValidationException>();
        }
    }
}