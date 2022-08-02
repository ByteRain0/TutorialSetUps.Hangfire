using Hangfire;
using Hangfire.SqlServer;

namespace HangfirePOC.Infrastructure
{
    public static class HangFireMessageDispatcherInstaller
    {
        public static IServiceCollection AddMessageDispatcher(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddSingleton<IMessageDispatcher, HangFireDispatcher>();

            var connectionString = configuration.GetConnectionString("Default");

            services.AddHangfire(hangFireConfiguration =>
            {
                hangFireConfiguration.UseSqlServerStorage(connectionString);
                hangFireConfiguration.UseMediatR();
            });

            JobStorage.Current = new SqlServerStorage(connectionString);

            services.AddHangfireServer();

            return services;
        }

        public static IApplicationBuilder UseMessageDispatcher(this IApplicationBuilder app)
        {
            MediatrQueueExtension.Configure(app.ApplicationServices.GetService<IMessageDispatcher>());

            app.UseHangfireDashboard("/hangfire", new DashboardOptions()
            {
                AppPath = "https://localhost:7086/swagger"
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHangfireDashboard();
            });

            return app;
        }
    }
}
