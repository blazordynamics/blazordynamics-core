using BlazorDynamics.Licensing.Core.Contracts;
using System.Security.Cryptography;

namespace BlazorDynamics.Licensing.Core.Implementations
{
    public class RsaSigningKey : ISigningKey
    {
        private readonly RSA _rsa;

        public RsaSigningKey(RSA rsa)
        {
            _rsa = rsa;
        }

        public byte[] SignData(byte[] data, HashAlgorithmName hashAlgorithmName)
        {
            return _rsa.SignData(data, hashAlgorithmName, RSASignaturePadding.Pkcs1);
        }
    }
}
