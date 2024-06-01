using BlazorDynamics.Licensing.Core.Models;

namespace BlazorDynamics.Licensing.Contracts;

public interface ISignedLicenseManager
{
    public string CreateEncodedLicense(LicenseData licenseData, string signature);

    public License GetLicense(string encodedLicense);
}
