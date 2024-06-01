namespace BlazorDynamics.Licensing.Core.Contracts
{
    public interface IKeyRetrievalStrategy
    {
        byte[] GetKey(string keyName);
    }
}