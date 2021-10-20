using System;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Scenario;
using Seshat.Application.Printers.Commands.CreatePrinter;
using Seshat.Application.Printers.Models;

namespace Seshat.Application.IntegrationTests.Scenarios
{
    public static class PrinterScenarioExtensions
    {
        public static TScenarioBuilder AddPrinter<TScenarioBuilder>(
            this TScenarioBuilder scenarioBuilder,
            string manufacturerId,
            Action<PrinterDto> printerCallback)
            where TScenarioBuilder : IScenarioBuilder
        {
            return (TScenarioBuilder)scenarioBuilder
                .With(async scope =>
                {
                    var mediator = scope.ServiceProvider.GetService<IMediator>();
                    var command = new CreatePrinterCommand(
                        new PrinterInputModel
                        {
                            Model = Guid.NewGuid().ToString(),
                            ManufacturerId = manufacturerId
                        });

                    return await mediator!.Send(command);
                }, printer => printerCallback
                    .Invoke((PrinterDto) printer!));
        }
    }
}