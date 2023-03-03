using Microsoft.AspNetCore.Authorization;

namespace Application.Policies.Requirements
{
    public class CustomerIsActiveRequirement : IAuthorizationRequirement
    {
        public bool IsActive { get; set; }
        public CustomerIsActiveRequirement(bool isActive = true)
            => IsActive = isActive;
    }
}
