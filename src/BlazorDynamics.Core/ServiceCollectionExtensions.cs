using Microsoft.Extensions.DependencyInjection;

namespace BlazorDynamics.Core
{
    public static class ServiceCollectionExtensions
    {

        public static IServiceCollection AddBlazorDynamics(this IServiceCollection services,
            Action<BlazorDynamicsConfigBuilder> configAction = null)
        {
            var builder = new BlazorDynamicsConfigBuilder();
            configAction?.Invoke(builder);
            builder.Build(services);

            return services;
        }

    }
}
