using System.Security.Cryptography;

namespace BlazorDynamics.Licensing.Core.Contracts;

public interface ISigningKey
{
    byte[] SignData(byte[] data, HashAlgorithmName hashAlgorithmName);
}
