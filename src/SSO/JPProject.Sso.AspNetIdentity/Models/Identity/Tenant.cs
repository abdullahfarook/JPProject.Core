#nullable enable
using MultiTenancyServer;

namespace JPProject.Sso.AspNetIdentity.Models.Identity
{
    public class Tenant : TenancyTenant
    {
        protected Tenant(){}
        public Tenant(string name,string displayName, string? logo = null)
        {
            base.CanonicalName = name;
            DisplayName = displayName;
            Logo = logo;
        }
        public string DisplayName { get; set; } = null!;
        public string? Logo { get; set; }
    }
}
