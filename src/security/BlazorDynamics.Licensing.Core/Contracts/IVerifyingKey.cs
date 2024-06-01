using System.Security.Cryptography;

namespace BlazorDynamics.Licensing.Core.Contracts;

public interface IVerifyingKey
{
    bool VerifyData(byte[] data, byte[] signature, HashAlgorithmName hashAlgorithmName);
}