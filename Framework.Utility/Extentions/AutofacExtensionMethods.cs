using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Utility.Extentions
{
    public static class AutofacExtensionMethods
    {
        /// <summary>
        /// for register automated dbContext
        /// </summary>
        /// <typeparam name="TContext"></typeparam>
        /// <param name="builder"></param>
        /// <param name="connectionStringName"></param>
        public static void RegisterContext<TContext>(this ContainerBuilder builder, string connectionStringName)
            where TContext : DbContext
        {
            builder.Register(componentContext =>
            {

                var serviceProvider = componentContext.Resolve<IServiceProvider>();
                var configuration = componentContext.Resolve<IConfiguration>();
                var dbContextOptions = new DbContextOptions<TContext>(new Dictionary<Type, IDbContextOptionsExtension>());
                var optionsBuilder = new DbContextOptionsBuilder<TContext>(dbContextOptions)
                    .UseApplicationServiceProvider(serviceProvider)
                    .UseSqlServer(configuration.GetConnectionString(connectionStringName),
                        serverOptions =>
                            serverOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(30), null)
                                .ExecutionStrategy(dependencies => new SqlServerExecutionStrategy(dependencies)))
                    .EnableSensitiveDataLogging();

                return optionsBuilder.Options;
            }).As<DbContextOptions<TContext>>()
                .InstancePerLifetimeScope();

            builder.Register(context => context.Resolve<DbContextOptions<TContext>>())
                .As<DbContextOptions>()
                .InstancePerLifetimeScope();

            builder.RegisterType<TContext>()
                .AsSelf()
                .InstancePerLifetimeScope();
        }

    }

    public static class ConnectionStringNames
    {
        #if DEBUG
            public const string Core = "local";
        #else
            public const string Core = "server";
        #endif   
    }
}
