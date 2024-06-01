using BlazorDynamics.Licensing.Core.Models;

namespace BlazorDynamics.Licensing.Contracts;

public interface ILicenseCreator
{
    public License CreateLicense(LicenseData licenseData);
}
