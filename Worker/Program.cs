using Hangfire;
using HangfirePOC.Business.SimpleCommand;
using HangfirePOC.Business.SimpleService;
using HangfirePOC.Infrastructure;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

public static class Program
{
    static void Main(string[] args)
    {
        CreateHostBuilder(args)
            .Build()
            .Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration(app => { app.AddJsonFile("appsettings.json"); })
            .ConfigureServices(services =>
            {
                var serviceConfiguration = services.BuildServiceProvider().GetService<IConfiguration>();
                services.AddTransient<ISimpleService, SimpleService>();
                services.AddMediatR(typeof(SimpleCommand).Assembly);
                services.AddTransient<IMessageDispatcher, HangFireDispatcher>();
                services.AddHangfire(configuration =>
                {
                    configuration.UseSqlServerStorage(serviceConfiguration.GetConnectionString("Default"));
                    configuration.UseMediatR();
                });
                services.AddHangfireServer();
            });
}