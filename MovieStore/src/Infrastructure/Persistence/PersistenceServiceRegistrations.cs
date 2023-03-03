using Application.Abstractions.Services;
using Application.Abstractions.UnitOfWorks.Base;
using Application.Policies.Requirements;
using Domain.Entities.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Persistence.Contexts;
using Persistence.Services;
using Persistence.UnitOfWorks;
using System.Security.Claims;
using System.Text;

namespace Persistence
{
    public static class PersistenceServiceRegistrations
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            #region Database
            string movieDbConnectionString = configuration.GetConnectionString("MovieDb") ??
                throw new ArgumentNullException("Movie Database Connection String is not found");
            services.AddDbContext<MovieDbContext>(options => options.UseSqlServer(movieDbConnectionString));
            //services.AddDbContext<TestDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("TestDb")));
            #endregion

            #region Identity
            services.AddIdentity<User, Role>(options =>
            {
                options.User.RequireUniqueEmail = true;

                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireDigit = false;
                options.Password.RequiredUniqueChars = 0;
            }).AddEntityFrameworkStores<MovieDbContext>().AddDefaultTokenProviders();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.RequireHttpsMetadata = true;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new()
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Authentication:JwtBearer:SecurityKey"]!)),

                        ValidateIssuer = true,
                        ValidIssuer = configuration["Authentication:JwtBearer:Issuer"],

                        ValidateAudience = true,
                        ValidAudience = configuration["Authentication:JwtBearer:Audience"],

                        ValidateLifetime = true,
                        LifetimeValidator = (notBefore, expires, securityToken, validationParameters) => expires is not null && expires > DateTime.UtcNow,
                        ClockSkew = TimeSpan.Zero,

                        NameClaimType = ClaimTypes.Name,
                        RoleClaimType = ClaimTypes.Role
                    };
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Customer", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireRole("customer");
                    policy.Requirements.Add(new CustomerIsActiveRequirement());
                });
                options.AddPolicy("Admin", policy => policy.RequireRole("admin"));
            });
            #endregion

            #region Unit Of Works
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            #endregion

            #region Services
            services.AddScoped<ITokenHandler, Infrastructure.Services.TokenHandler>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IMovieService, MovieService>();
            services.AddScoped<IStarService, StarService>();
            services.AddScoped<IDirectorService, DirectorService>();
            #endregion            

            return services;
        }
    }
}