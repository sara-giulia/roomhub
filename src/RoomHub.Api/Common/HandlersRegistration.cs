namespace RoomHub.Api.Common;

public static class HandlersRegistration
{
    public static IServiceCollection AddHandlers(this IServiceCollection services)
    {
        var assembly = typeof(HandlersRegistration).Assembly;

        var handlers = assembly.GetTypes()
            .Where(t => t is { IsAbstract: false, IsInterface: false })
            .SelectMany(t => t.GetInterfaces()
                .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ICommandHandler<,>))
                .Select(i => (Service: i, Implementation: t)));

        foreach (var (service, implementation) in handlers)
        {
            services.AddScoped(service, implementation);
        }

        return services;
    }
}
