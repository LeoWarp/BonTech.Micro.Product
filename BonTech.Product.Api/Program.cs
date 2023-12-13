using System.Reflection;
using BonTech.Product.Api;
using BonTech.Product.Application.DependencyInjection;
using BonTech.Product.Application.Validations;
using BonTech.Product.Consumer.DependencyInjection;
using BonTech.Product.Domain.Interfaces.Validations;
using BonTech.Product.Domain.Settings;
using BonTech.Product.Persistence.DependencyInjection;
using BonTech.Product.Producer.DependencyInjection;
using FluentValidation;
using OpenTelemetry.Metrics;
using Prometheus;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var connection = builder.Configuration.GetConnectionString("PostgreSQL");
builder.Services.AddHealthChecks().AddNpgSql(connection);
builder.Services.AddHealthChecksUI(setupSettings: setup =>
{
    setup.SetEvaluationTimeInSeconds(5);
    setup.SetApiMaxActiveRequests(1);
})
.AddInMemoryStorage();

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection(JwtSettings.DefaultSection));
builder.Services.Configure<RabbitMqParams>(builder.Configuration.GetSection(nameof(RabbitMqParams)));
builder.Services.AddCors();

builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));
builder.Services.AddControllers();

builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddApplication(builder.Configuration);
builder.Services.AddProducer(builder.Configuration);
builder.Services.AddConsumer(builder.Configuration);

builder.Services.AddSwagger();
builder.Services.AddAuthenticationAndAuthorization(builder);

builder.Services.AddOpenTelemetry().WithMetrics(x =>
{
    x.AddPrometheusExporter();

    x.AddMeter("Microsoft.AspNetCore.Hosting", "Microsoft.AspNetCore.Server.Kestrel");
    x.AddView("request-duration", new ExplicitBucketHistogramConfiguration()
    {
        Boundaries = new [] { 0, 0.005, 0.01, 0.025, 0.05, 0.075, 0.1, 0.25, 0.5, 0.75, 1 }
    });
});

builder.Services.AddScoped<IProductValidator, ProductValidator>();
builder.Services.AddValidatorsFromAssemblies(new [] { Assembly.GetExecutingAssembly() });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Awesome CMS Core API V1");
        c.SwaggerEndpoint("/swagger/v2/swagger.json", "Awesome CMS Core API V2");
        c.RoutePrefix = string.Empty;
    });
}

app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
app.UseSerilogRequestLogging();

app.UseHttpsRedirection();
app.UseHttpMetrics();

app.UseAuthentication();
app.UseAuthorization();

app.MapMetrics();
app.MapControllers();

app.MapHealthChecks("/healthcheck");
app.UseHealthChecksUI();

app.Run();