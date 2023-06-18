using CQRSBus.CommandBus.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace CQRSBus.CommandBus;

public static class CommandBusServiceExtension
{
    public static IServiceCollection ConfigureCommandBus(this IServiceCollection serviceCollection, CommandBusBuilder commandBusBuilder)
    {
        serviceCollection.AddSingleton(commandBusBuilder.Build());

        return serviceCollection;
    }
}
