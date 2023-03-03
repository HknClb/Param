using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace MovieStoreWebApi.Constants
{
    public static class CustomSwaggerGenOptions
    {
        public readonly static Action<SwaggerGenOptions> SecurityOptions = (options) =>
        {
            options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new()
            {
                In = ParameterLocation.Header,
                Description = "Please enter a valid token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = JwtBearerDefaults.AuthenticationScheme
            });

            options.AddSecurityRequirement(new()
            {
                {
                    new()
                    {
                        Reference = new()
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = JwtBearerDefaults.AuthenticationScheme
                        }
                    },
                    Array.Empty<string>()
                }
            });
        };
    }
}
