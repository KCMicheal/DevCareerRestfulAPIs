using Microsoft.OpenApi.Models;
using System.Reflection;

namespace DevCareer.API.Infrastructure.StartupExtentions
{
    public static class SwaggerConfiguration
    {
        public static IServiceCollection RegisterAndConfigureSwaggerAuthorizationOptions(this IServiceCollection services)
        {
            services.AddSwaggerGen(x =>
            {
                OpenApiSecurityRequirement security = new OpenApiSecurityRequirement
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
                        Array.Empty<string>()
                    }
                };

                x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                x.AddSecurityRequirement(security);
                x.IncludeXmlComments(GetXmlCommentsPath(), includeControllerXmlComments: true);
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
