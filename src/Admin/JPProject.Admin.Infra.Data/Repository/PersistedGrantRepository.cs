using IdentityServer4.EntityFramework.Interfaces;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.Models;
using JPProject.Admin.Domain.Interfaces;
using JPProject.Domain.Core.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JPProject.Admin.Infra.Data.Repository
{
    public class PersistedGrantRepository : IPersistedGrantRepository
    {
        public DbSet<IdentityServer4.EntityFramework.Entities.PersistedGrant> DbSet { get; set; }
        public PersistedGrantRepository(IPersistedGrantDbContext context)
        {
            this.DbSet = context.PersistedGrants;
        }

        public async Task<List<PersistedGrant>> GetGrants(PagingViewModel paging)
        {
            var grants = await DbSet.AsNoTracking().OrderByDescending(s => s.CreationTime).Skip(paging.Offset).Take(paging.Limit).ToListAsync();
            return grants.Select(s => s.ToModel()).ToList();
        }

        public Task<int> Count()
        {
            return DbSet.CountAsync();
        }

        public async Task<PersistedGrant> GetGrant(string key)
        {
            var grant = await DbSet.AsNoTracking().FirstAsync(f => f.Key == key);
            return grant.ToModel();
        }

        public void Remove(PersistedGrant grant)
        {
            var grantDb = DbSet.FirstOrDefault(x => x.Key == grant.Key);
            DbSet.Remove(grantDb);
        }
    }
}