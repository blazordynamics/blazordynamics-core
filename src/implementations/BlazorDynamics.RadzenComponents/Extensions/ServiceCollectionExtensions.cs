using BlazorDynamics.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorDynamics.RadzenComponents.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static BlazorDynamicsConfigBuilder UseDefaultRadzenComponents(this BlazorDynamicsConfigBuilder builder)
        {
            var provider = new RadzenComponentProvider();
            builder.RegisterProvider(provider);
            builder.RegisterComponents(provider);
            return builder;
        }
    }
}
