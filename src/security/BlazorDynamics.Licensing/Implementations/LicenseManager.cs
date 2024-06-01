using BlazorDynamics.Licensing.Contracts;
using BlazorDynamics.Licensing.Core.Contracts;
using BlazorDynamics.Licensing.Core.Enums;
using BlazorDynamics.Licensing.Core.Models;
using Newtonsoft.Json;

namespace BlazorDynamics.Licensing.Implementations;

public class LicenseManager : ILicenseManager
{
    private readonly ILicenseGenerator _licenseGenerator;
    private readonly ILicenseSigner _licenseSigner;
    private readonly ISignedLicenseManager _signedLicenseManager;

    public LicenseManager(ILicenseGenerator licenseGenerator, ILicenseSigner licenseSigner,
        ISignedLicenseManager signedLicenseManager)
    {
        _licenseGenerator = licenseGenerator;
        _licenseSigner = licenseSigner;
        _signedLicenseManager = signedLicenseManager;
    }

    public string GenerateSignedLicense(LicenseDetails licenseDetails)
    {
        var licenseType = (LicenseType)Enum.Parse(typeof(LicenseType), licenseDetails.LicenseType);
        var license = _licenseGenerator.CreateLicenseData(licenseType, licenseDetails);

        var signedLicense = _licenseSigner.SignLicense(JsonConvert.SerializeObject(licenseDetails));
        var base64License = Convert.ToBase64String(signedLicense);
        return _signedLicenseManager.CreateEncodedLicense(license, base64License);
    }
}
