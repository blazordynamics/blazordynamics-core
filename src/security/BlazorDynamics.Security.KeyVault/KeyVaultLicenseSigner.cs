using System.Text;
using System.Text.Json;
using Azure.Identity;
using Azure.Security.KeyVault.Keys;
using Azure.Security.KeyVault.Keys.Cryptography;
using BlazorDynamics.Licensing.Core.Contracts;
using BlazorDynamics.Licensing.Core.Models;

namespace BlazorDynamics.Security.KeyVault;

public class KeyVaultLicenseSigner : ILicenseSigner
{
    private readonly string _keyVaultUri;
    private readonly string _keyName; // The name of the key in Azure Key Vault used to sign/verify

    public KeyVaultLicenseSigner(string keyVaultUri, string keyName)
    {
        _keyVaultUri = keyVaultUri;
        _keyName = keyName;
    }

    public byte[] SignLicense(string licenseText)
    {
        // Use Azure SDK to sign the serialized license using Key Vault
        var keyClient = new KeyClient(new Uri(_keyVaultUri), new DefaultAzureCredential(new DefaultAzureCredentialOptions()));
        var key = keyClient.GetKey(_keyName);

        var cryptoClient = new CryptographyClient(key.Value.Id, new DefaultAzureCredential(new DefaultAzureCredentialOptions()));
        var result = cryptoClient.SignDataAsync(SignatureAlgorithm.RS256, Encoding.UTF8.GetBytes(licenseText)).Result;
        return result.Signature;
    }

    public string SignLicense(LicenseDetails licenseData)
    {
        var serializedLicense = JsonSerializer.Serialize(licenseData);
        
        // Use Azure SDK to sign the serialized license using Key Vault
        var keyClient = new KeyClient(new Uri(_keyVaultUri), new DefaultAzureCredential(new DefaultAzureCredentialOptions()));
        var key = keyClient.GetKey(_keyName);

        var cryptoClient = new CryptographyClient(key.Value.Id, new DefaultAzureCredential(new DefaultAzureCredentialOptions()));
        var result = cryptoClient.SignDataAsync(SignatureAlgorithm.RS256, Encoding.UTF8.GetBytes(serializedLicense)).Result;
        
        return Convert.ToBase64String(result.Signature);
    }

    public bool ValidateSignature(string signedLicense, LicenseDetails licenseData)
    {
        try
        {
            var serializedLicense = JsonSerializer.Serialize(licenseData);

            var keyClient = new KeyClient(new Uri(_keyVaultUri), new DefaultAzureCredential());
            var key = keyClient.GetKey(_keyName);

            var cryptoClient = new CryptographyClient(key.Value.Id, new DefaultAzureCredential());
            var isVerified = cryptoClient.VerifyData(SignatureAlgorithm.RS256, Encoding.UTF8.GetBytes(serializedLicense), Convert.FromBase64String(signedLicense));

            if (isVerified.IsValid)
            {
                return true;
            }

            return false;
        }
        catch
        {
            return false;
        }
    }
}