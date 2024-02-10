using Dooshka.Application.Persistance.Contracts;
using Dooshka.Domain;
using Microsoft.EntityFrameworkCore;

namespace Dooshka.Persistence.Repositories
{
    public class ToDoNotificationRepository : IRepository<ToDoNotification>
    {
        private readonly ApplicationDbContext _context;

        public ToDoNotificationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(ToDoNotification item)
        {
            await _context.ToDoNotifications.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(ToDoNotification item)
        {
            _context.ToDoNotifications.Remove(item);
            await _context.SaveChangesAsync();
        }

        public ToDoNotification? Find(Func<ToDoNotification, bool> predicate)
        {
            return _context.ToDoNotifications.SingleOrDefault(predicate);
        }

        public async Task<IEnumerable<ToDoNotification>> FindAllAsync(Func<ToDoNotification, bool> predicate)
        {
            return await Task.FromResult(_context.ToDoNotifications.Where(predicate).ToList());
        }

        public async Task<IEnumerable<ToDoNotification>> GetAllAsync()
        {
            return await _context.ToDoNotifications.ToListAsync();
        }

        public async Task UpdateAsync(ToDoNotification item)
        {
            _context.ToDoNotifications.Update(item);
            await _context.SaveChangesAsync();
        }
    }
}
