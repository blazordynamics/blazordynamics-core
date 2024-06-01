using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using BlazorDynamics.Licensing.Core.Contracts;

namespace BlazorDynamics.Security.KeyVault;

public class KeyVaultBasedKeyRetrievalStrategy : IKeyRetrievalStrategy
{
    private readonly string _vaultUrl;

    public KeyVaultBasedKeyRetrievalStrategy(string vaultUrl)
    {
        _vaultUrl = vaultUrl;
    }

    public byte[] GetKey(string keyName)
    {
        var client = new SecretClient(new Uri(_vaultUrl), new DefaultAzureCredential());
        var secret = client.GetSecret(keyName).Value.Value;
        return Convert.FromBase64String(secret);
    }
}
