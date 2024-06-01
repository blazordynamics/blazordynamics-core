using BlazorDynamics.Licensing.Contracts;
using FastEndpoints;

namespace BlazorDynamics.Licensing.Api.Features.Licensing.Verifiy
{
    public class Endpoint : Endpoint<Request, Response>
    {
        private readonly ISignedLicenseValidator _signedLicenseValidator;

        public Endpoint(ISignedLicenseValidator signedLicenseValidator)
        {
            _signedLicenseValidator = signedLicenseValidator;
        }

        public override void Configure()
        {
            Post("/api/license/verify");
            AllowAnonymous();
        }

        public override async Task HandleAsync(Request req, CancellationToken ct)
        {
            var result = _signedLicenseValidator.ValidateLicense(req.Base64License);

            await SendAsync(new Response()
            {
                IsValid = result.IsValid
            });
        }
    }
}
