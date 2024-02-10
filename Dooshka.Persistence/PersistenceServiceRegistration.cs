using Dooshka.Application.Persistance.Contracts;
using Dooshka.Domain;
using Dooshka.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Dooshka.Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection ConfigurePersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
               options.UseNpgsql(
                   configuration.GetConnectionString("DatabaseConnection")));


            services.AddScoped<IRepository<EmailConfirmationCode>, EmailConfirmationCodeRepository>();
            services.AddScoped<IRepository<ToDoNotification>, ToDoNotificationRepository>();
            services.AddScoped<IRepository<User>, UserRepository>();
            services.AddScoped<IRepository<ToDoItem>, ToDoItemRepository>();
            services.AddScoped<IRepository<RevokedToken>, RevokedTokenRepository>();

            return services;
        }
    }
}
