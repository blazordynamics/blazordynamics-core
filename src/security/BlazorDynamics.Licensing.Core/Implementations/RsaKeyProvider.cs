using BlazorDynamics.Licensing.Core.Contracts;
using System.Security.Cryptography;
using System.Text;

namespace BlazorDynamics.Licensing.Core.Implementations
{
    public class RsaKeyProvider : IKeyProvider
    {
        private readonly IKeyRetrievalStrategy _keyRetrievalStrategy;

        public RsaKeyProvider(IKeyRetrievalStrategy keyRetrievalStrategy)
        {
            _keyRetrievalStrategy = keyRetrievalStrategy;
        }

        public ISigningKey GetPrivateKey()
        {
            var rsa = RSA.Create();
            var privateKey = _keyRetrievalStrategy.GetKey("private");
           var pemContent =  Encoding.UTF8.GetString(privateKey);
            rsa.ImportFromPem(pemContent);

            var privateKeyParameters = rsa.ExportParameters(true);

            rsa.ImportParameters(privateKeyParameters);
            return new RsaSigningKey(rsa);
        }

        public IVerifyingKey GetPublicKey()
        {
            var rsa = RSA.Create();
            var publicKeyData = _keyRetrievalStrategy.GetKey("public");
            var pemContent = Encoding.UTF8.GetString(publicKeyData);
            rsa.ImportFromPem(pemContent);

            return new RsaVerifyingKey(rsa);
        }

        private RSAParameters DeserializeRsaParameters(byte[] data)
        {
            using var memoryStream = new MemoryStream(data);
            using var reader = new BinaryReader(memoryStream);
            return new RSAParameters
            {
                Modulus = reader.ReadBytes(reader.ReadInt32()),
                Exponent = reader.ReadBytes(reader.ReadInt32()),
                D = reader.ReadBytes(reader.ReadInt32()),
                DP = reader.ReadBytes(reader.ReadInt32()),
                DQ = reader.ReadBytes(reader.ReadInt32()),
                InverseQ = reader.ReadBytes(reader.ReadInt32()),
                P = reader.ReadBytes(reader.ReadInt32()),
                Q = reader.ReadBytes(reader.ReadInt32())
            };
        }
    }
}
