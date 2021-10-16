using Seshat.Application.Common.Interfaces;

namespace Seshat.Application.IntegrationTests.Mocks
{
    public class CurrentUserServiceMock : ICurrentUserService
    {
        public string? UserId { get; private set; }

        public void OverrideUserId(string? userId)
        {
            UserId = userId;
        }
    }
}