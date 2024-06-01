using BlazorDynamics.Licensing.Core.Models;

namespace BlazorDynamics.Licensing.Core.Contracts;

public interface ILicenseValidator
{
    bool IsValidLicense(License license);
}