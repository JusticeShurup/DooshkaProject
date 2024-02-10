using Dooshka.Application.Features.AuthFeatures.Handlers.Commands;
using Dooshka.Application.Services;
using Dooshka.Domain;
using JpProject.AspNetCore.PasswordHasher.Bcrypt;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Dooshka.Application
{
    public static class ApplicationServicesRegistration
    {
        public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services)
        {

            services.AddSingleton<IPasswordHasher<User>, BCrypt<User>>();
            services.AddScoped<TokenService>();

            services.AddMediatR(cfg => 
            {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());

                cfg.AddRequestPostProcessor<PostRegisterRequestHandler>();
                cfg.AddRequestPostProcessor<PostLoginRequestHandler>();
            });

            return services;
        }
    }
}
