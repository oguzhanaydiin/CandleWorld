using Consul;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Http.Features;

namespace BasketService.Api.Extensions;

public static class ConsulRegistiration
{
    public static IServiceCollection ConfigureConsul(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IConsulClient, ConsulClient>(p => new ConsulClient(consulConfig =>
         {
             var address = configuration["ConsulConfig:Address"];
             consulConfig.Address = new Uri(address);
         }));

        return services;
    }

    public static IApplicationBuilder RegisterWithConsul(this IApplicationBuilder app, IHostApplicationLifetime lifetime)
    {
        var consulClient = app.ApplicationServices.GetRequiredService<IConsulClient>();
        var loggingFactory = app.ApplicationServices.GetRequiredService<ILoggerFactory>();
        var logger = loggingFactory.CreateLogger<IApplicationBuilder>();

        //get server ip address
        var features = app.Properties["server.Features"] as FeatureCollection;
        var addresses = features.Get<IServerAddressesFeature>();
        var address = addresses.Addresses.First();

        //register service with consul
        var uri = new Uri(address);
        var registiration = new AgentServiceRegistration()
        {
            ID = $"BasketService",
            Name = "BasketService",
            Address = $"{uri.Host}",
            Port = uri.Port,
            Tags = new[] { "Basket Service", "Basket" }
        };

        logger.LogInformation("Registiring with Consul");
        consulClient.Agent.ServiceDeregister(registiration.ID).Wait();
        consulClient.Agent.ServiceRegister(registiration).Wait();

        lifetime.ApplicationStopping.Register(() =>
        {
            logger.LogInformation("Deregistering from Consul");
            consulClient.Agent.ServiceDeregister(registiration.ID).Wait();
        });

        return app;
    }
}
