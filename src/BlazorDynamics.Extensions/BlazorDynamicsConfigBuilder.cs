using BlazorDynamics.Common.Enums;
using BlazorDynamics.Core.Contracts;
using BlazorDynamics.Core.Models;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorDynamics.Extensions
{
    public class BlazorDynamicsConfigBuilder
    {
        private readonly ComponentsList _components = new();

        private readonly List<IComponentProvider> _providers = new();


        public BlazorDynamicsConfigBuilder RegisterProvider(IComponentProvider componentProvider)
        {
            _providers.Add(componentProvider);
            return this;
        }

        public BlazorDynamicsConfigBuilder RegisterComponents(IComponentProvider componentProvider)
        {
            RegisterComponents(componentProvider.GetComponents());
            return this;
        }

        public BlazorDynamicsConfigBuilder RegisterComponents(ComponentsList components)
        {
            foreach (var item in components)
            {
                RegisterComponent(item.Key, item.Value);
            }
            return this;
        }

        public BlazorDynamicsConfigBuilder RegisterComponent(ComponentSelectionKey key, Type formComponentType)
        {
            _components[key] = formComponentType;
            return this;
        }

        public BlazorDynamicsConfigBuilder RegisterComponent(ComponentType componentType, string typeDefinitionName, Type formComponentType)
        {
            RegisterComponent(new ComponentSelectionKey(componentType, typeDefinitionName), formComponentType);
            return this;
        }

        public BlazorDynamicsConfigBuilder RegisterComponent(ComponentType componentType, Type formComponentType)
        {
            RegisterComponent(new ComponentSelectionKey(componentType), formComponentType);
            return this;
        }
        public void Build(IServiceCollection services)
        {
            // Register the ComponentsList as a singleton
            services.AddSingleton(_components);

            foreach (var provider in _providers)
            {
                services.AddScoped(provider.GetType());
            }
        }
    }
}
