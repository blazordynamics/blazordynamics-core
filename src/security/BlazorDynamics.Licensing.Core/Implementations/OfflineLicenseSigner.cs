using BlazorDynamics.Licensing.Core.Contracts;

namespace BlazorDynamics.Licensing.Core.Implementations
{
    public class OfflineLicenseSigner : ILicenseSigner
    {
        private readonly IKeyProvider _keyProvider;

        public OfflineLicenseSigner(IKeyProvider keyProvider)
        {
            _keyProvider = keyProvider;
        }

        public byte[] SignLicense(string licenseText)
        {
            var data = System.Text.Encoding.UTF8.GetBytes(licenseText);
            return _keyProvider.GetPrivateKey().SignData(data, System.Security.Cryptography.HashAlgorithmName.SHA256);
        }
    }
}