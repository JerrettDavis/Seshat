using System;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Seshat.Application.Common.Exceptions;
using Seshat.Application.IntegrationTests.Scenarios;
using Seshat.Application.Manufacturers.Models;
using Seshat.Application.Printers.Commands.UpdatePrinter;
using Seshat.Application.Printers.Models;

namespace Seshat.Application.IntegrationTests.Printers.Commands
{
    using static Testing;
    public class UpdatePrinterTests : TestBase
    {
        [Test]
        public async Task ShouldRequireMinimumFields()
        {
            // Arrange
            var scenario = await GetScenarioBuilder()
                .BuildAsync();
            var command = new UpdatePrinterCommand("", new PrinterInputModel());

            // Act & Assert
            await FluentActions.Invoking(() => scenario.SendAsync(command))
                .Should().ThrowAsync<ValidationException>();
        }

        [Test]
        public async Task ShouldRequireValidPrinter()
        {
            // Arrange
            ManufacturerDto manufacturer = null!;
            var scenario = await GetScenarioBuilder()
                .AddManufacturer(r => manufacturer = r)
                .BuildAsync();
            var command = new UpdatePrinterCommand("", new PrinterInputModel
            {
                Model = Guid.NewGuid().ToString(),
                ManufacturerId = manufacturer.Id
            });
            
            // Act & Assert
            await FluentActions.Invoking(() => scenario.SendAsync(command))
                .Should().ThrowAsync<ValidationException>();
        }
        
        
    }
}