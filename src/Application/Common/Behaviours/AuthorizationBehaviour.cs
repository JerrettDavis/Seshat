using Seshat.Application.Common.Exceptions;
using Seshat.Application.Common.Interfaces;
using Seshat.Application.Common.Security;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Seshat.Application.Common.Behaviours
{
    public class AuthorizationBehaviour<TRequest, TResponse> : 
        IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IIdentityService _identityService;

        public AuthorizationBehaviour(
            ICurrentUserService currentUserService,
            IIdentityService identityService)
        {
            _currentUserService = currentUserService;
            _identityService = identityService;
        }

        public async Task<TResponse> Handle(
            TRequest request, 
            CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            var authorizeAttributes = request.GetType()
                .GetCustomAttributes<AuthorizeAttribute>()
                .ToList();

            // No attribute, continue on.
            if (!authorizeAttributes.Any()) 
                return await next();
            
            // Must be authenticated user
            if (_currentUserService.UserId == null)
                throw new UnauthorizedAccessException();

            // Role-based authorization
            var authorizeAttributesWithRoles = authorizeAttributes
                .Where(a => !string.IsNullOrWhiteSpace(a.Roles))
                .ToList();

            await ValidateUserRoles(authorizeAttributesWithRoles);

            // Policy-based authorization
            var authorizeAttributesWithPolicies = authorizeAttributes
                    .Where(a => !string.IsNullOrWhiteSpace(a.Policy))
                    .ToList();

            await ValidateUserPolicies(authorizeAttributesWithPolicies);

            // User is authorized / authorization not required
            return await next();
        }

        private async Task ValidateUserRoles(
            IList<AuthorizeAttribute> attributes)
        {
            if (!attributes.Any()) return;
            
            foreach (var roles in attributes.Select(a => a.Roles.Split(',')))
            {
                var authorized = false;
                foreach (var role in roles)
                {
                    var isInRole = await _identityService
                        .IsInRoleAsync(_currentUserService.UserId!, role.Trim());
                        
                    if (isInRole)
                    {
                        authorized = true;
                        break;
                    }
                }

                // Must be a member of at least one role in roles
                if (!authorized)
                {
                    throw new ForbiddenAccessException();
                }
            }
        }

        private async Task ValidateUserPolicies(
            IList<AuthorizeAttribute> attributes)
        {
            if (!attributes.Any())
                return;

            foreach (var policy in attributes.Select(a => a.Policy))
            {
                var authorized = await _identityService.
                    AuthorizeAsync(_currentUserService.UserId!, policy);

                if (!authorized)
                {
                    throw new ForbiddenAccessException();
                }
            }
        }
    }
}