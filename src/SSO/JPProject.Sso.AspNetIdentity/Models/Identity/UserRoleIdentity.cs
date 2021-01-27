using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace JPProject.Sso.AspNetIdentity.Models.Identity
{
    [Table("UserRoles")]
    public class UserRoleIdentity : IdentityUserRole<string>
    {
        public UserRoleIdentity() { }

        public UserRoleIdentity(Guid tenantId, RoleIdentity role, UserIdentity user)
        {
            TenantId = tenantId.ToString();
            RoleId = role.Id;
            UserId = user.Id;
        }
        public UserRoleIdentity(Tenant tenant, RoleIdentity role, UserIdentity user)
        {
            TenantId = tenant.Id;
            RoleId = role.Id;
            UserId = user.Id;
        }
        public string TenantId { get; set; }
        public States State { get; set; }
        public virtual Tenant Tenant { get; set; }
        public virtual UserIdentity User { get; set; }
        public virtual RoleIdentity Role { get; set; }
    }
}
