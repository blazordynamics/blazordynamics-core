namespace BlazorDynamics.Licensing.Api.Features.Licensing.Create
{
    public class Request
    {
        public string CompanyName { get; set; }

        public string Email { get; set; }

        public string OrderId { get; set; }
        public string UserId { get; set; }

        public string LicenseType { get; set; }
    }
}
