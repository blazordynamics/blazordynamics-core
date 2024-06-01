using BlazorDynamics.Licensing.Core.Contracts;
using System.Reflection;

namespace BlazorDynamics.Licensing.Core.Implementations
{
    public class EmbeddedResourceKeyRetrievalStrategy: IKeyRetrievalStrategy
    {
        private readonly Assembly _assembly;

        public EmbeddedResourceKeyRetrievalStrategy(Assembly assembly)
        {
            _assembly = assembly;
        }

        public byte[] GetKey(string keyName)
        {
            string resourceName = $"{_assembly.GetName().Name}.{keyName}";

            using (Stream stream = _assembly.GetManifestResourceStream(resourceName))
            {
                if (stream == null)
                {
                    throw new InvalidOperationException($"Resource '{resourceName}' not found.");
                }

                using (StreamReader reader = new StreamReader(stream))
                {
                    var secret = reader.ReadToEnd();
                    return Convert.FromBase64String(secret);
                }
            }
        }
    }
}
