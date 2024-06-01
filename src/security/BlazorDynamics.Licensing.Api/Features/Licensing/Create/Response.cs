namespace BlazorDynamics.Licensing.Api.Features.Licensing.Create
{
    public class Response
    {
        public string EncodedLicense { get; }

        public Response(string encodedLicense)
        {
            EncodedLicense = encodedLicense;
        }
    }
}
