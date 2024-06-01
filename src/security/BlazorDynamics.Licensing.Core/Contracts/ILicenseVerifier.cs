namespace BlazorDynamics.Licensing.Core.Contracts;

public interface ILicenseVerifier
{
    bool VerifyLicense(string licenseText, byte[] signature);
}
