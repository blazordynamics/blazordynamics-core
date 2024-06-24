using BlazorDynamics.Core;

namespace BlazorDynamics.MudBlazorComponents.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static BlazorDynamicsConfigBuilder UseDefaultMudBlazorComponents(this BlazorDynamicsConfigBuilder builder)
        {
            var provider = new MudBlazorComponentProvider();
            builder.RegisterProvider(provider);
            builder.RegisterComponents(provider);
            return builder;
        }
    }
}
