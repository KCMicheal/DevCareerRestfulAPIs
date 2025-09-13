using System;
using System.Reflection;
using Microsoft.OpenApi.Models;

namespace DevCareer.API.Infrastructure.StartupExtensions
{
    public static class SwaggerConfigurations
    {
        public static IServiceCollection RegisterAndConfigureSwaggerAuthorizationOptions(this IServiceCollection services)
        {
            services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo { Title = "DevvCareer", Version = "v1" });
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                option.IncludeXmlComments(GetXmlCommentsPath(),includeControllerXmlComments:true);
                option.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
            });
            return services;
        }
        private static string GetXmlCommentsPath()
        {
            string xmlFileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            string filePath = Path.Combine(AppContext.BaseDirectory, xmlFileName);
            return filePath;
        }

    }
}
