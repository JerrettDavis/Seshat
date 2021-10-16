using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using Seshat.Application.Common.Behaviours;
using Seshat.Application.Common.Interfaces;
using Seshat.Application.Manufacturers.Commands.CreateManufacturer;
using Seshat.Application.Manufacturers.Models;

namespace Seshat.Application.UnitTests.Common.Behaviours
{
    public class RequestLoggerTests
    {
        private readonly Mock<ILogger<CreateManufacturerCommand>> _logger;
        private readonly Mock<ICurrentUserService> _currentUserService;
        private readonly Mock<IIdentityService> _identityService;


        public RequestLoggerTests()
        {
            _logger = new Mock<ILogger<CreateManufacturerCommand>>();

            _currentUserService = new Mock<ICurrentUserService>();

            _identityService = new Mock<IIdentityService>();
        }

        [Test]
        public async Task ShouldCallGetUserNameAsyncOnceIfAuthenticated()
        {
            _currentUserService.Setup(x => x.UserId).Returns("Administrator");

            var requestLogger = new LoggingBehaviour<CreateManufacturerCommand>(
                _logger.Object, 
                _currentUserService.Object,
                _identityService.Object);

            await requestLogger.Process(
                new CreateManufacturerCommand(
                    new ManufacturerInputModel { Name = Guid.NewGuid().ToString()}),
                new CancellationToken());

            _identityService.Verify(i => 
                i.GetUserNameAsync(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public async Task ShouldNotCallGetUserNameAsyncOnceIfUnauthenticated()
        {
            var requestLogger = new LoggingBehaviour<CreateManufacturerCommand>(
                _logger.Object, 
                _currentUserService.Object,
                _identityService.Object);

            await requestLogger.Process(
                new CreateManufacturerCommand(
                    new ManufacturerInputModel { Name = Guid.NewGuid().ToString()}),
                new CancellationToken());

            _identityService.Verify(i => i.GetUserNameAsync(null), Times.Never);
        }
    }
}