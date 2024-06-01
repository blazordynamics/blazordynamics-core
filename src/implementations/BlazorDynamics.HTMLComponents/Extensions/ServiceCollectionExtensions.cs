using BlazorDynamics.Extensions;

namespace BlazorDynamics.HTMLComponents.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static BlazorDynamicsConfigBuilder UseDefaultHTMLComponents(this BlazorDynamicsConfigBuilder builder)
        {
            var provider = new HtmlComponentsProvider();
            builder.RegisterProvider(provider);
            builder.RegisterComponents(provider);
            return builder;
        }
    }
}
