namespace BlazorDynamics.Licensing.Core.Contracts;

public interface ILicenseSigner
{
    //string SignLicense(LicenseDetails licenseData);
    //bool ValidateSignature(string signedLicense, LicenseDetails licenseDetails);

    public byte[] SignLicense(string licenseText);
}
