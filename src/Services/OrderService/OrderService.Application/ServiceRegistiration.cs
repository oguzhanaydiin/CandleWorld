using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace OrderService.Application;

public static class ServiceRegistiration
{
    public static IServiceCollection AddApplicationRegistiration(this IServiceCollection services, Type startup)
    {
        var assm = Assembly.GetExecutingAssembly();

        services.AddAutoMapper(assm);
        services.AddMediatR(assm);

        return services;
    }
}
