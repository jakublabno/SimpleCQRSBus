using CQRSBus.QueryBus.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace CQRSBus.QueryBus;

public static class QueryBusServiceExtension
{
    public static IServiceCollection ConfigureQueryBus(this IServiceCollection serviceCollection, QueryBusBuilder queryBusBuilder)
    {
        serviceCollection.AddSingleton(queryBusBuilder.Build());

        return serviceCollection;
    }
}
