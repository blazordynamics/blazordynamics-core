using BlazorDynamics.Licensing.Core.Contracts;
using BlazorDynamics.Licensing.Core.Enums;
using BlazorDynamics.Licensing.Core.Models;

namespace BlazorDynamics.Licensing.Implementations;

public class LicenseGenerator : ILicenseGenerator
{
    private readonly ISerialNumberGenerator _serialNumberGenerator;
    public LicenseGenerator(ISerialNumberGenerator serialNumberGenerator)
    {
        _serialNumberGenerator = serialNumberGenerator;
    }

    public LicenseData CreateLicenseData(LicenseType licenseType, LicenseDetails licenseDetails)
    {
        switch (licenseType)
        {
            default:
            case LicenseType.Trial:
                return new LicenseData()
                {
                    EmailTo = licenseDetails.Email,
                    LicensedTo = licenseDetails.LicensedTo,
                    SubscriptionExpiry = DateTime.UtcNow.AddDays(30).ToString("yyyyMMdd"),
                    UserId = licenseDetails.UserId,
                    LicenseNote = "Use this license for trial purposes. Valid only 30 days",
                    LicenseInstruction = "https://purchase.bff.com/policies/use-license",
                    SerialNumber = _serialNumberGenerator.GenerateSerialNumber(),
                    LicenseType = Enum.GetName(typeof(LicenseType), LicenseType.Trial),
                    LicenseVersion = "1.0.0",
                    OrderId = licenseDetails.OrderId,
                    Products = new List<string>
                        {
                            "BlazorDynamicsUIv1"
                        }
                };
            case LicenseType.Standard:
                return new LicenseData()
                {
                    EmailTo = licenseDetails.Email,
                    LicensedTo = licenseDetails.LicensedTo,
                    UserId = licenseDetails.UserId,
                    OrderId = licenseDetails.OrderId,
                    SubscriptionExpiry = DateTime.UtcNow.AddYears(1).ToString("yyyyMMdd"),
                    LicenseNote = "Use this license for....",
                    LicenseInstruction = "https://purchase.bff.com/policies/use-license",
                    SerialNumber = _serialNumberGenerator.GenerateSerialNumber(),
                    LicenseType = Enum.GetName(typeof(LicenseType), LicenseType.Standard),
                    LicenseVersion = "1.0.0",
                    Products = new List<string>
                        {
                            "BlazorDynamicsUIv1"
                        }
                };
        }
    }
}