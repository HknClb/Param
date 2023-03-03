using Application.Policies.Requirements;
using Domain.Entities.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Application.Policies.Handlers
{
    public class CustomerIsActiveHandler : AuthorizationHandler<CustomerIsActiveRequirement>
    {
        private readonly UserManager<User> _userManager;

        public CustomerIsActiveHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, CustomerIsActiveRequirement requirement)
        {
            User? user = await _userManager.GetUserAsync(context.User);
            if (user is null)
                return;

            if (user.IsActive == requirement.IsActive)
                context.Succeed(requirement);
        }
    }
}
