using BlazorDynamics.Licensing.Core.Enums;
using BlazorDynamics.Licensing.Core.Models;

namespace BlazorDynamics.Licensing.Core.Contracts;

public interface ILicenseGenerator
{
   public LicenseData CreateLicenseData(LicenseType licenseType, LicenseDetails licenseDetails);
}
