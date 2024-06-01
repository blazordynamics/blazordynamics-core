using BlazorDynamics.Licensing.Contracts;
using BlazorDynamics.Licensing.Core.Contracts;
using BlazorDynamics.Licensing.Core.Models;
using BlazorDynamics.Licensing.Models;
using Newtonsoft.Json;

namespace BlazorDynamics.Licensing.Implementations;

public class SignedLicenseValidator : ISignedLicenseValidator
{
    private readonly ISignedLicenseManager _signedLicenseManager;
    private readonly ILicenseVerifier _licenseVerifier;

    public SignedLicenseValidator(ILicenseVerifier licenseVerifier, ISignedLicenseManager signedLicenseManager)
    {
        _licenseVerifier = licenseVerifier;
        _signedLicenseManager = signedLicenseManager;
    }

    public LicenseValidationResult ValidateLicense(string base64License)
    {
        License license;
        try
        {
            license = _signedLicenseManager.GetLicense(base64License);
        }
        catch (Exception ex)
        {
            return new LicenseValidationResult(false, ex.Message);
        }
        var licenseDetails = new LicenseDetails
        {
            UserId = license.Data.UserId,
            Email = license.Data.EmailTo,
            LicensedTo = license.Data.LicensedTo,
            LicenseType = license.Data.LicenseType,
            OrderId = license.Data.OrderId
        };

        var hasValidSignature = _licenseVerifier.VerifyLicense(JsonConvert.SerializeObject(licenseDetails),
            Convert.FromBase64String(license.Signature));

        if (!hasValidSignature)
        {
            return new LicenseValidationResult(false, "License data is not valid.");
        }

        if (!license.Data.IsValid())
        {
            return new LicenseValidationResult(false, $"The license has expired. Expiration date: {license.Data.SubscriptionExpiry}");
        }

        return new LicenseValidationResult(true);

    }
}
