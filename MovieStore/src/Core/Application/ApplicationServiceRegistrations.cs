using Application.Features.Auth.Rules;
using Application.Features.Directors.Rules;
using Application.Features.Movies.Rules;
using Application.Features.Orders.Rules;
using Application.Features.Stars.Rules;
using Application.Features.Users.Rules;
using Application.Policies.Handlers;
using Application.Validation;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application
{
    public static class ApplicationServiceRegistrations
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();

            services.AddAutoMapper(assembly);
            services.AddMediatR(options =>
            {
                options.RegisterServicesFromAssembly(assembly);
                options.AddBehavior(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
            });
            services.AddValidatorsFromAssembly(assembly);

            services.AddScoped<IAuthorizationHandler, CustomerIsActiveHandler>();

            #region Business Rules
            services.AddScoped<AuthBusinessRules>();
            services.AddScoped<OrderBusinessRules>();
            services.AddScoped<UserBusinessRules>();
            services.AddScoped<StarBusinessRules>();
            services.AddScoped<MovieBusinessRules>();
            services.AddScoped<DirectorBusinessRules>();
            #endregion

            return services;
        }
    }
}
