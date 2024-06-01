using MudBlazor;
using MudBlazor.Services;
using SharedDemos.Shared;
using BlazorDynamics.Extensions;
using BlazorDynamics.MudBlazorComponents.Extensions;
using BlazorDynamics.RadzenComponents.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddMudServices();
builder.Services.AddMudMarkdownServices();
builder.Services.AddSingleton<UserSettingsService>();
builder.Services.AddBlazorDynamics(cfg => cfg.UseDefaultMudBlazorComponents());
builder.Services.AddSingleton<RadzenComponentProvider>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
