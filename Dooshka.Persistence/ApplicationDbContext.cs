using Dooshka.Domain;
using Microsoft.EntityFrameworkCore;

namespace Dooshka.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<ToDoItem> ToDoItems { get; set; }

        public DbSet<RevokedToken> RevokedTokens { get; set; }

        public DbSet<ToDoNotification> ToDoNotifications { get; set; }

        public DbSet<EmailConfirmationCode> EmailConfrimationCodes { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {
        
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }

    }
}