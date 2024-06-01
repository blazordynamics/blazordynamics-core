using BlazorDynamics.Licensing.Models;

namespace BlazorDynamics.Licensing.Contracts;

public interface ISignedLicenseValidator
{
    LicenseValidationResult ValidateLicense(string base64License);
}