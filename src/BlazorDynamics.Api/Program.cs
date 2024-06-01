using FastEndpoints;
using FastEndpoints.Swagger;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddFastEndpoints()
                .SwaggerDocument();

var services = builder.Services;

var app = builder.Build();


app.UseHttpsRedirection();

app.UseAuthorization();
app.UseFastEndpoints()
   .UseSwaggerGen();
app.MapControllers();

app.Run();
