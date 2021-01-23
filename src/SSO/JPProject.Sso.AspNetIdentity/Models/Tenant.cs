#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using Bk.Common.Exceptions;
using Bk.Common.LinqUtils;
using Bk.Common.Roles;
using JPProject.Sso.AspNetIdentity.Models.Identity;
using Microsoft.EntityFrameworkCore.Internal;
using MultiTenancyServer;

namespace JPProject.Sso.AspNetIdentity.Models
{
    public class Tenant : TenancyTenant
    {
        protected Tenant(){}
        public Tenant(string name,string displayName, string country, string currency, TenantTypes tenanTypeId, Industries industryId, string? logo = null)
        {
            base.CanonicalName = name.Trim();
            DisplayName = displayName.Trim();
            Country = country;
            Currency = currency;
            State = States.Active;
            CreatedOn = DateTime.UtcNow;
            UpdatedOn = DateTime.UtcNow;
            TenantTypeId = tenanTypeId;
            IndustryId = industryId;
            Logo = logo;
        }
        public Tenant(Guid id, string name)
        {
            base.CanonicalName = name;
            Id = id.ToString();
        }
        public Tenant(UserIdentity owner, string name, string country, string currency,
            TenantTypes businessTypeId, Industries industryId)
        {
            Country = country;
            Currency = currency;
            TenantTypeId = businessTypeId;
            IndustryId = industryId;
            State = States.Active;
            CreatedById = owner.Id;
            UpdatedById = owner.Id;
            //UserRoles.Add(new UserRoleIdentity(this,new RoleIdentity("), ));
        }
        public string DisplayName { get; set; } = null!;
        public string? Logo { get; set; }
        public string? Email { get; protected set; }
        public string? Phone { get; protected set; }
        public string? Fax { get; protected set; }
        public string? Mobile { get; protected set; }
        public string? TollFree { get; protected set; }
        public string? Website { get; protected set; }
        public string Country { get; private set; }
        public string Currency { get; private set; }
        public TenantTypes TenantTypeId { get; private set; }
        public Industries IndustryId { get; private set; }
        public States State { get; protected set; }
        public DateTime CreatedOn { get; set; }
        public string? CreatedById { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string? UpdatedById { get; set; }
        public virtual List<UserRoleIdentity> UserRoles { get; protected set; } = new List<UserRoleIdentity>();

        public void UpdateLogo(string logo)
        {
            Logo = logo;
        }

        public void Update(string name, string phone, string? fax = null, string? mobile = null, string? tollFree = null, string? website = null)
        {
            DisplayName = name.Trim();
            Phone = phone;
            Fax = fax;
            Mobile = mobile;
            TollFree = tollFree;
            Website = website;
        }
        public void Archive()
            => State = States.Inactive;

        public void Restore()
            => State = States.Active;
        
    }
    public enum TenantTypes
    {
        CEMENT_RETAIL = 1,
        SHOES_RETAIL = 2,
        Export = 3,
        GROCERY_RETAIL = 4,
        GENERIC = 5
    }
    public enum Industries
    {
        SoleProprietorShip = 1,
        Partnership = 2,
        Corporation = 3,
    }

}
