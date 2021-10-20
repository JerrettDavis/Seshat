using System;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Seshat.Application.Common.Exceptions;
using Seshat.Application.IntegrationTests.Scenarios;
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
            var scenario = await GetScenarioBuilder()
                .BuildAsync();
            var command = new CreatePrinterCommand(new PrinterInputModel());
            
            // Act & Assert
            await FluentActions.Invoking(() => scenario.SendAsync(command))
                .Should().ThrowAsync<ValidationException>();
        }
        
        [Test]
        public async Task ShouldCreatePrinter()
        {
            // Arrange
            ManufacturerDto manufacturer = null!;
            string userId = null!;
            var scenario = await GetScenarioBuilder()
                .RunAsDefaultUser(id => userId = id)
                .AddManufacturer(r => manufacturer = r)
                .BuildAsync();
            
            var command = new CreatePrinterCommand(
                new PrinterInputModel
                {
                    Model = Guid.NewGuid().ToString(),
                    ManufacturerId = manufacturer.Id
                });
            
            // Act
            var result = await scenario.SendAsync(command);
            var item = await scenario.GetPublicEntity<Printer>(result.Id);
            
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
            ManufacturerDto manufacturer = null!;
            var scenario = await GetScenarioBuilder()
                .RunAsDefaultUser()
                .AddManufacturer(r => manufacturer = r)
                .BuildAsync();
            var command = new CreatePrinterCommand(
                new PrinterInputModel
                {
                    Model = Guid.NewGuid().ToString(),
                    ManufacturerId = manufacturer.Id
                });
            
            // Create the first printer 
            await scenario.SendAsync(command);

            
            // Act & Assert
            // Then we try to create another with the same name
            await FluentActions.Invoking(() => scenario.SendAsync(command))
                .Should().ThrowAsync<ValidationException>();
        }
    }
}