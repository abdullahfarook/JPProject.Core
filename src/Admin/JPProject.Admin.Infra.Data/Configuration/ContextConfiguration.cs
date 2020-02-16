using IdentityServer4.EntityFramework.Interfaces;
using IdentityServer4.EntityFramework.Options;
using JPProject.Admin.Domain.Interfaces;
using JPProject.Admin.Infra.Data.Context;
using JPProject.Admin.Infra.Data.Repository;
using JPProject.Domain.Core.Events;
using JPProject.Domain.Core.Interfaces;
using JPProject.EntityFrameworkCore.EventSourcing;
using JPProject.EntityFrameworkCore.Interfaces;
using JPProject.EntityFrameworkCore.Repository;
using Microsoft.EntityFrameworkCore;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ContextConfiguration
    {
        public static IJpProjectAdminBuilder AddAdminContext(this IJpProjectAdminBuilder services, Action<DbContextOptionsBuilder> optionsAction, JpDatabaseOptions options = null)
        {
            if (options == null)
                options = new JpDatabaseOptions();

            RegisterStorageServices(services.Services);
            ConfigureContexts(services, optionsAction);

            //ContextHelpers.CheckDatabases(services.BuildServiceProvider(), options).Wait();

            return services;
        }

        private static void ConfigureContexts(IJpProjectAdminBuilder services, Action<DbContextOptionsBuilder> optionsAction)
        {
            var operationalStoreOptions = new OperationalStoreOptions();
            services.Services.AddSingleton(operationalStoreOptions);

            var storeOptions = new ConfigurationStoreOptions();
            services.Services.AddSingleton(storeOptions);

            services.Services.AddDbContext<JpProjectAdminUiContext>(optionsAction);
        }

        private static void RegisterStorageServices(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IEventStoreRepository, EventStoreRepository>();
            services.AddScoped<IJpEntityFrameworkStore>(x => x.GetService<JpProjectAdminUiContext>());
            services.AddScoped<IConfigurationDbContext>(x => x.GetService<JpProjectAdminUiContext>());
            services.AddScoped<IPersistedGrantDbContext>(x => x.GetService<JpProjectAdminUiContext>());
            services.AddScoped<IEventStore, SqlEventStore>();

            services.AddScoped<IPersistedGrantRepository, PersistedGrantRepository>();
            services.AddScoped<IApiResourceRepository, ApiResourceRepository>();
            services.AddScoped<IIdentityResourceRepository, IdentityResourceRepository>();
            services.AddScoped<IClientRepository, ClientRepository>();


            services.AddScoped<IEventStoreRepository, EventStoreRepository>();
            services.AddScoped<IEventStore, SqlEventStore>();
        }

        public static IJpProjectAdminBuilder AddEventStore<TEventStore>(this IJpProjectAdminBuilder services)
            where TEventStore : class, IEventStoreContext
        {
            services.Services.AddScoped<IEventStoreContext, TEventStore>();
            return services;
        }
    }
}
