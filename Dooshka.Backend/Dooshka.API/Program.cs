using Dooshka.API.ExceptionHandlers;
using Dooshka.API.Middlewares;
using Dooshka.Application;
using Dooshka.Infrastructure;
using Dooshka.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace Dooshka.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var config = builder.Configuration;


            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder =>
                    {
                        builder.SetIsOriginAllowed(_ => true)
                               .AllowAnyMethod()
                               .AllowAnyHeader()
                               .AllowCredentials();
                    });
            });

            builder.Services.AddControllers();

            builder.Services.AddHttpContextAccessor();

            builder.Services.AddExceptionHandler<UnauthorizedExceptionHandler>();
            builder.Services.AddExceptionHandler<BadRequestExceptionHandler>();
            builder.Services.AddExceptionHandler<NotFoundExceptionHandler>();
            builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

            builder.Services.AddScoped<TokenValidationMiddleware>();

            builder.Services.ConfigureApplicationServices();
            builder.Services.ConfigurePersistenceServices(config);
            builder.Services.ConfigureInfrastructureServices(config);

            builder.Services.AddProblemDetails();

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new()
                    {
                        ClockSkew = TimeSpan.Zero,
                        ValidateIssuer = true,
                        ValidIssuer = config["JwtToken:Issuer"]!,
                        ValidateAudience = true,
                        ValidAudience = config["JwtToken:Audience"]!,
                        ValidateLifetime = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JwtToken:SecretKey"]!)),
                        ValidateIssuerSigningKey = true
                    };
                });

            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen(swagger =>
            {
                swagger.SwaggerDoc("v1", new OpenApiInfo { Title = "DooshkaApi", Version = "v1" });
                swagger.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme."
                });
                swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "bearerAuth"
                                }
                            },
                            new string[] {}
                    }
                });

                var filePath = Path.Combine(System.AppContext.BaseDirectory, "MyApi.xml");
                swagger.IncludeXmlComments(filePath);
            });

            var app = builder.Build();

            app.UseExceptionHandler();

            app.UseSwagger();
            app.UseSwaggerUI();

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("AllowAll");

            app.UseAuthorization();

            app.UseMiddleware<TokenValidationMiddleware>();


            app.MapControllers();

            ApplyMigration(app);

            app.Run();
        }

        public static void ApplyMigration(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                if (dbContext.Database.GetPendingMigrations().Count() > 0)
                {
                    dbContext.Database.Migrate();
                }
            }
        }
    }
}