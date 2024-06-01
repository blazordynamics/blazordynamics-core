using BlazorDynamics.Licensing.Core.Contracts;

namespace BlazorDynamics.Licensing.Core.Implementations
{
    public class PublicKeyLicenseVerifier : ILicenseVerifier
    {
        private readonly IKeyProvider _keyProvider;

        public PublicKeyLicenseVerifier(IKeyProvider keyProvider)
        {
            _keyProvider = keyProvider;
        }

        public bool VerifyLicense(string licenseText, byte[] signature)
        {
            var data = System.Text.Encoding.UTF8.GetBytes(licenseText);
            return _keyProvider.GetPublicKey().VerifyData(data, signature, System.Security.Cryptography.HashAlgorithmName.SHA256);
        }
    }
}
