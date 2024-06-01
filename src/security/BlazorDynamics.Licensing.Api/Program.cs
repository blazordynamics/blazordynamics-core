
using BlazorDynamics.Licensing.Contracts;
using BlazorDynamics.Licensing.Core.Contracts;
using BlazorDynamics.Licensing.Core.Implementations;
using BlazorDynamics.Licensing.Implementations;
using BlazorDynamics.Security.KeyVault;
using FastEndpoints;
using FastEndpoints.Swagger;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddFastEndpoints()
                .SwaggerDocument();

var services = builder.Services;

services.AddTransient<ILicenseManager, LicenseManager>();
services.AddTransient<ILicenseGenerator, LicenseGenerator>();
services.AddTransient<ILicenseSigner, OfflineLicenseSigner>();
services.AddTransient<ILicenseVerifier, PublicKeyLicenseVerifier>();

var keyVaultUri = builder.Configuration["KeyVault:Uri"];
var keyName = builder.Configuration["KeyVault:KeyName"];

services.AddSingleton<IKeyRetrievalStrategy>(sp => 
new KeyVaultBasedKeyRetrievalStrategy(keyVaultUri));

services.AddSingleton<IKeyProvider, RsaKeyProvider>();

services.AddTransient<ISignedLicenseManager, SignedLicenseManager>();
services.AddTransient<ISignedLicenseValidator, SignedLicenseValidator>();
services.AddSingleton<ISerialNumberGenerator, SerialNumberGenerator>();


var app = builder.Build();


app.UseHttpsRedirection();

app.UseAuthorization();
app.UseFastEndpoints()
   .UseSwaggerGen();
app.MapControllers();

app.Run();
