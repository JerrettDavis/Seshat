using System;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Scenario;
using Seshat.Application.Manufacturers.Commands.CreateManufacturer;
using Seshat.Application.Manufacturers.Models;

namespace Seshat.Application.IntegrationTests.Scenarios
{
    public static class ManufacturerScenarioExtensions
    {
        public static TScenarioBuilder AddManufacturer<TScenarioBuilder>(
            this TScenarioBuilder scenarioBuilder,
            Action<ManufacturerDto>? manufacturerCallback = null)
            where TScenarioBuilder : IScenarioBuilder
        {
            return (TScenarioBuilder)scenarioBuilder
                .With(async scope =>
                {
                    var mediator = scope.ServiceProvider.GetService<IMediator>();
                    var command = new CreateManufacturerCommand(
                        new ManufacturerInputModel
                        {
                            Name = Guid.NewGuid().ToString()
                        });

                    return await mediator!.Send(command);
                }, manufacturer => manufacturerCallback?
                    .Invoke((ManufacturerDto) manufacturer!));
        }
    }
}