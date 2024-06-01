namespace BlazorDynamics.Contracts;

public interface IJsonSchemaProvider
{
    public string ReadSchema(string fileName);
}