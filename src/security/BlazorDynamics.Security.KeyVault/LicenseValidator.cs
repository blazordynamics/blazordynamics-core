using System.Text;
using System.Text.Json;
using Azure.Identity;
using Azure.Security.KeyVault.Keys;
using Azure.Security.KeyVault.Keys.Cryptography;
using BlazorDynamics.Licensing.Core.Contracts;
using BlazorDynamics.Licensing.Core.Models;

namespace BlazorDynamics.Security.KeyVault;

public class LicenseValidator : ILicenseValidator
{
    private readonly string _keyVaultUri;
    private readonly string _keyName;

    public LicenseValidator(string keyVaultUri, string keyName)
    {
        _keyVaultUri = keyVaultUri;
        _keyName = keyName;
    }

    public bool IsValidLicense(License license)
    {
        var keyClient = new KeyClient(new Uri(_keyVaultUri), new DefaultAzureCredential());
        var key = keyClient.GetKey(_keyName);
        var cryptoClient = new CryptographyClient(key.Value.Id, new DefaultAzureCredential());

        byte[] licenseBytes = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(license.Data));

        // Assuming your ILicense interface exposes the Signature as a byte[]
        byte[] signatureBytes = Encoding.UTF8.GetBytes(license.Signature);

        var verifyResult = cryptoClient.Verify(SignatureAlgorithm.RS256, licenseBytes, signatureBytes);

        return verifyResult.IsValid;
    }
}
