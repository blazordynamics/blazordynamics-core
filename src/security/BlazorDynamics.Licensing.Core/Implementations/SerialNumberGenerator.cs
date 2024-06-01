using BlazorDynamics.Licensing.Core.Contracts;

namespace BlazorDynamics.Licensing.Core.Implementations
{
    public class SerialNumberGenerator : ISerialNumberGenerator
    {
        public string GenerateSerialNumber()
        {
            Guid guid = Guid.NewGuid();
            string guidStr = guid.ToString("N");  // Removes dashes
            return $"{guidStr.Substring(0, 7)}-{guidStr.Substring(7, 4)}-{guidStr.Substring(11, 4)}-{guidStr.Substring(15, 4)}-{guidStr.Substring(19)}";
        }
    }
}