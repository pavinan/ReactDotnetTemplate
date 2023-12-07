var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSpaStaticFiles(c => 
{
    c.RootPath = "wwwroot";
});

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddReverseProxy().LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));
}

var app = builder.Build();

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
