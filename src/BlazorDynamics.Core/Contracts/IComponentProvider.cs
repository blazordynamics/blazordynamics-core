using BlazorDynamics.Core.Models;

namespace BlazorDynamics.Core.Contracts
{
    public interface IComponentProvider
    {
        public ComponentsList GetComponents();
    }
}
