using System;
using System.Linq;
using System.Collections.Generic;
using JPProject.Domain.Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using Bk.Common.Exceptions;
using Bk.Common.LinqUtils;
using Bk.Common.Roles;
using Bk.Common.StringUtils;

namespace JPProject.Sso.AspNetIdentity.Models.Identity
{
    public class UserIdentity : IdentityUser, IDomainUser
    {
        private static NotFoundException BusinessNotFound(Guid businessId) => new NotFoundException($"Businesses Not Found against Id: {businessId}");
        private static NotFoundException WorkerNotFound(Guid workerId) => new NotFoundException($"Businesses Not Found against Id: {workerId}");

        public bool UserProfileCompleted { get; set; }
        public bool TenantProfileCompleted { get; set; }
        public string DisplayName { get; set; }
        public Gender Gender { get; protected set; }
        public States State { get; protected set; }
        public string Pic { get; protected set; }
        public string FirstName { get; protected set; }
        public string LastName { get; protected set; }
        public bool? MultitenantEnabled { get; protected set; }
        #region Constructors

        public UserIdentity() { }
        public UserIdentity(Guid id, string firstName, string lastName, string email, Gender gender, States state = States.Active)
        {
            Id = id.ToString();
            DisplayName = $"{firstName.Trim()} {lastName.Trim()}";
            Email = email;
            Gender = gender;
            State = state;
        }
        public UserIdentity(string displayName, string email, Gender gender = Gender.NA, States state = States.Active)
        {
            UpdateName(displayName);
            Gender = gender;
            State = state;
            UserName = email;
            Email = email;

        }
        #endregion
        public virtual List<UserRoleIdentity> UserRoles { get; protected set; } = new List<UserRoleIdentity>();
        public UserIdentity Update(string firstName, string lastName, string email, Gender gender, States state)
        {
            FirstName = firstName.Trim();
            LastName = lastName.Trim();
            DisplayName = $"{firstName.Trim()} {lastName.Trim()}";
            Gender = gender;
            State = state;
            Email = email;
            return this;

        }

        public void UpdateName(string displayName)
        {
            if (!displayName.IsNullOrEmpty())
            {
                var names = displayName.Split(" ").ToList();
                FirstName = names.FirstOrDefault();
                LastName = names.LastOrDefault();
                DisplayName = displayName;
            }
        }
        public void Archive()
            => State = States.Inactive;
        public void Restore()
            => State = States.Active;
        public void UpdatePhoto(string pic)
            => Pic = pic;
        public void ReplaceUserRole(Guid businessId, RoleIdentity role)
        {
            if (role.Name.Contains(ApplicationRoles.Owner))
                throw new NotSupportedException("Cannot change owner role");
            var businessRoles = GetRolesByBusiness(businessId);
            businessRoles = new List<UserRoleIdentity> { new UserRoleIdentity(businessId, role, this) };
        }
        public void ReplaceUserRoles(Guid businessId, List<RoleIdentity> roles)
        {
            if (!roles.Any()) throw new Exception("No role found");
            if (roles.Any(x => x.Name.Contains(ApplicationRoles.Owner)))
                throw new NotSupportedException("Cannot change owner role");

            var businessRoles = GetRolesByBusiness(businessId);
            businessRoles = roles.Select(role => new UserRoleIdentity(businessId, role, this)).ToList();
        }

        public void ValidateRoleAssignment(List<RoleIdentity> roles)
        {

        }
        public List<UserRoleIdentity> GetRolesByBusiness(Guid businessId)
            => UserRoles.Where(x => x.TenantId == businessId.ToString()).ToList();

        public void ConfirmEmail()
        {
            EmailConfirmed = true;
            UserProfileCompleted = true;
        }
        public void CompleteTenantProfile()
        {
            TenantProfileCompleted = true;
        }
    }

    public class RoleIdentity : IdentityRole
    {
        public RoleIdentity() : base() { }
        public RoleIdentity(string name) : base(name) { }
        public virtual List<UserRoleIdentity> UserRoles { get; protected set; } = new List<UserRoleIdentity>();
    }
    public enum Gender
    {
        // ReSharper disable once InconsistentNaming
        NA = 0,
        Male = 1,
        Female = 2
    }
}