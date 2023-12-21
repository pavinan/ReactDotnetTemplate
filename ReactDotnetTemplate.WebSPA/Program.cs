using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHealthChecks()
    .AddCheck("self", () => HealthCheckResult.Healthy(), ["live"]);

builder.Services.AddSpaStaticFiles(c => 
{
    c.RootPath = "wwwroot";
});

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddReverseProxy().LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));
}

var app = builder.Build();

app.MapHealthChecks("/health");

app.MapHealthChecks("/liveness", new HealthCheckOptions
{
    Predicate = r => r.Tags.Contains("live")
});

app.UseHttpsRedirection();

app.UseFileServer();
app.UseSpaStaticFiles();

if (app.Environment.IsDevelopment())
{
    app.MapReverseProxy();
}
else
{
    app.UseSpa(spa => 
    {
        spa.Options.SourcePath = "wwwroot";
    });
}

app.Run();
