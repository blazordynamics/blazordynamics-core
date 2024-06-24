using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using MudBlazor;
using WasmSampleApp;
using SharedDemos.Shared;
using BlazorDynamics.MudBlazorComponents.Extensions;
using BlazorDynamics.RadzenComponents.Extensions;
using BlazorDynamics.Core;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddMudServices();
builder.Services.AddMudMarkdownServices();
builder.Services.AddSingleton<UserSettingsService>();
builder.Services.AddBlazorDynamics(cfg => cfg.UseDefaultMudBlazorComponents());
builder.Services.AddSingleton<RadzenComponentProvider>();

var app = builder.Build();


await app.RunAsync();
