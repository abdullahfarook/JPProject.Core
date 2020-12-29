#nullable enable
using System;
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
        public Tenant(Guid id, string name)
        {
            base.CanonicalName = name;
            Id = id.ToString();
        }
        public string DisplayName { get; set; } = null!;
        public string? Logo { get; set; }
    }
}
