using FluentValidation;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using ReactDotnetTemplate.Application.Behaviours;
using ReactDotnetTemplate.Application.Data;
using ReactDotnetTemplate.Application.Services.Identity;
using ReactDotnetTemplate.Infrastructure.Data;
using ReactDotnetTemplate.Infrastructure.Services.Identity;
using ReactDotnetTemplate.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHealthChecks()
    .AddCheck("self", () => HealthCheckResult.Healthy(), ["live"]);


builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<IIdentityService, IdentityService>();
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(IIdentityService).Assembly);
    cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
    cfg.AddOpenBehavior(typeof(ValidatorBehavior<,>));
    cfg.AddOpenBehavior(typeof(TransactionBehavior<,>));
});

builder.Services.AddValidatorsFromAssembly(typeof(IIdentityService).Assembly);


builder.Services.AddScoped<ITodoRepository, TodoRepository>();

var app = builder.Build();

app.MapHealthChecks("/health");
app.MapHealthChecks("/liveness", new HealthCheckOptions
{
    Predicate = r => r.Tags.Contains("live")
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(setup =>
    {
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
