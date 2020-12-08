using Microsoft.AspNetCore.Identity;

namespace JPProject.Sso.AspNetIdentity.Models.Identity
{
    public sealed class UserRoleIdentity : IdentityUserRole<string>
    {
        public UserRoleIdentity() { }

        public UserRoleIdentity(Tenant tenant, RoleIdentity role, UserIdentity user)
        {
            TenantId = tenant.Id;
            RoleId = role.Id;
            UserId = user.Id;
        }
        public string TenantId { get; set; }
    }
}
