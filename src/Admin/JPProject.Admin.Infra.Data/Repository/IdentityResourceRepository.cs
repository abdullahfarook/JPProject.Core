using IdentityServer4.EntityFramework.Interfaces;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.Models;
using JPProject.Admin.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JPProject.Admin.Infra.Data.Repository
{
    public class IdentityResourceRepository : IIdentityResourceRepository
    {
        public DbSet<IdentityServer4.EntityFramework.Entities.IdentityResource> DbSet { get; set; }
        public IdentityResourceRepository(IConfigurationDbContext context)
        {
            this.DbSet = context.IdentityResources;
        }


        public Task<List<string>> SearchScopes(string search) => DbSet.Where(id => id.Name.Contains(search)).Select(x => x.Name).ToListAsync();
        public async Task<IEnumerable<IdentityResource>> All()
        {
            return await DbSet.Select(s => s.ToModel()).ToListAsync();
        }

        public async Task<IdentityResource> GetByName(string name)
        {
            var idr = await DbSet.AsNoTracking().FirstOrDefaultAsync(w => w.Name == name);
            return idr.ToModel();
        }

        public async Task UpdateWithChildrens(string oldName, IdentityResource irs)
        {
            var entity = irs.ToEntity();
            var savedIR = await RemoveIdentityResourceClaimsAsync(oldName);
            entity.Id = savedIR.Id;
            DbSet.Update(entity);
        }

        public async Task<IdentityResource> GetDetails(string name)
        {
            var ir = await DbSet.Include(s => s.UserClaims).AsNoTracking().FirstOrDefaultAsync(w => w.Name == name);
            return ir.ToModel();
        }

        private async Task<IdentityServer4.EntityFramework.Entities.IdentityResource> RemoveIdentityResourceClaimsAsync(string name)
        {
            var identityClaims = await DbSet.Include(s => s.UserClaims).Where(x => x.Name == name).AsNoTracking().FirstAsync();
            identityClaims.UserClaims.Clear();
            return identityClaims;
        }

        public void Add(IdentityResource obj)
        {
            DbSet.Add(obj.ToEntity());
        }

        public void Update(IdentityResource obj)
        {
            var idr = DbSet.FirstOrDefault(w => w.Name == obj.Name);
            var newOne = obj.ToEntity();
            newOne.Id = idr.Id;
            DbSet.Update(newOne);
        }

        public void Remove(IdentityResource obj)
        {
            var idr = DbSet.FirstOrDefault(w => w.Name == obj.Name);
            DbSet.Remove(idr);
        }

        public void Dispose()
        {
        }
    }
}