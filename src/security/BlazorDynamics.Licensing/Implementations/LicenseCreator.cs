using BlazorDynamics.Licensing.Contracts;
using BlazorDynamics.Licensing.Core.Contracts;
using BlazorDynamics.Licensing.Core.Models;
using Newtonsoft.Json;

namespace BlazorDynamics.Licensing.Implementations;

public class LicenseCreator : ILicenseCreator
{
    private readonly ILicenseSigner _licenseSigner;

    public LicenseCreator(ILicenseSigner licenseSigner)
    {
        _licenseSigner = licenseSigner;
    }

    public License CreateLicense(LicenseData licenseData)
    {
        var licenseDetails = new LicenseDetails
        {
            Email = licenseData.EmailTo,
            LicensedTo = licenseData.LicensedTo,
            LicenseType = licenseData.LicenseType,
            OrderId = licenseData.OrderId,
            UserId = licenseData.UserId
        };

        var signature = _licenseSigner.SignLicense(JsonConvert.SerializeObject(licenseDetails));
        ;
        return new License
        {
            Data = licenseData,
            Signature = Convert.ToBase64String(signature)
        };
    }
}