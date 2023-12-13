using System.Reflection;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace BonTech.Product.Api;

public static class Startup
{
    /// <summary>
    /// Подключение аутентификации и авторизации
    /// </summary>
    /// <param name="services"></param>
    /// <param name="builder"></param>
    public static void AddAuthenticationAndAuthorization(this IServiceCollection services, WebApplicationBuilder builder)
    {
        services.AddAuthorization();
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(o =>
        {
            var options = builder.Configuration.GetSection(JwtSettings.DefaultSection).Get<JwtSettings>();
            var jwtKey = options.JwtKey;
            o.Authority = options.Authority;
            o.RequireHttpsMetadata = false;
            o.TokenValidationParameters = new TokenValidationParameters
            {
                RequireAudience = true,
                RequireExpirationTime = true,
                RoleClaimType = ClaimTypes.Role,
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateLifetime = true,
                ValidAudience = options.Audience,
                ValidIssuer = options.Issuer,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
            };
        });
    }
    
    /// <summary>
    /// Подключение Swagger
    /// </summary>
    /// <param name="services"></param>
    public static void AddSwagger(this IServiceCollection services)
    {
        services.AddApiVersioning(setup =>
        {
            setup.DefaultApiVersion = new ApiVersion(1, 0);
            setup.AssumeDefaultVersionWhenUnspecified = true;
            setup.ReportApiVersions = true;
        });
        
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddSwaggerGen(option =>
        {
            option.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "BonTech.Product API",
                Description = "Microservice for product logics",
                TermsOfService = new Uri("https://test.com/terms"),
                Contact = new OpenApiContact
                {
                    Name = "Test Contact",
                    Url = new Uri("https://test.com/contact")
                },
                License = new OpenApiLicense
                {
                    Name = "Test License",
                    Url = new Uri("https://test.com/license")
                }
            });
            
            option.SwaggerDoc("v2", new OpenApiInfo
            {
                Version = "v2",
                Title = "BonTech.Product API",
                Description = "Microservice for product logics",
                TermsOfService = new Uri("https://test.com/terms"),
                Contact = new OpenApiContact
                {
                    Name = "Test Contact",
                    Url = new Uri("https://test.com/contact")
                },
                License = new OpenApiLicense
                {
                    Name = "Test License",
                    Url = new Uri("https://test.com/license")
                }
            });
            
            option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter a valid token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "Bearer"
            });
            option.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type=ReferenceType.SecurityScheme,
                            Id="Bearer"
                        }
                    },
                    new string[]{}
                }
            });
            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            option.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
        });
    }
}