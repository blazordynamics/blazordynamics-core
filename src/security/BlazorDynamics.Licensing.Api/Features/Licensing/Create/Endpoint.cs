using BlazorDynamics.Licensing.Contracts;
using BlazorDynamics.Licensing.Core.Models;
using FastEndpoints;

namespace BlazorDynamics.Licensing.Api.Features.Licensing.Create
{
    public class Endpoint : Endpoint<Request, Response>
    {
        private readonly ILicenseManager licenseManager;

        public Endpoint(ILicenseManager licenseManager)
        {
            this.licenseManager = licenseManager;
        }

        public override void Configure()
        {
            Post("/api/license/create");
            AllowAnonymous();
        }

        public override async Task HandleAsync(Request req, CancellationToken ct)
        {
            var result = licenseManager.GenerateSignedLicense(new LicenseDetails()
            {
                LicensedTo = req.CompanyName,
                Email = req.Email,
                OrderId = req.OrderId,
                UserId = req.UserId,
                LicenseType = req.LicenseType
            });

            await SendAsync(new Response(result));
        }
    }
}
