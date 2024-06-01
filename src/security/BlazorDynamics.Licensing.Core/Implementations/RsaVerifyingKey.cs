using BlazorDynamics.Licensing.Core.Contracts;
using System.Security.Cryptography;

namespace BlazorDynamics.Licensing.Core.Implementations
{
    public class RsaVerifyingKey : IVerifyingKey
    {
        private readonly RSA _rsa;

        public RsaVerifyingKey(RSA rsa)
        {
            _rsa = rsa;
        }

        public bool VerifyData(byte[] data, byte[] signature, HashAlgorithmName hashAlgorithmName)
        {
            return _rsa.VerifyData(data, signature, hashAlgorithmName, RSASignaturePadding.Pkcs1);
        }
    }
}
