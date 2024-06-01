using BlazorDynamics.Licensing.Core.Models;

namespace BlazorDynamics.Licensing.Contracts;

public interface ILicenseManager
{
    string GenerateSignedLicense(LicenseDetails licenseDetails);
}
