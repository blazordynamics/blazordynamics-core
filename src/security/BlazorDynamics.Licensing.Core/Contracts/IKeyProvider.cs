namespace BlazorDynamics.Licensing.Core.Contracts;

public interface IKeyProvider
{
    ISigningKey GetPrivateKey();
    IVerifyingKey GetPublicKey();
}