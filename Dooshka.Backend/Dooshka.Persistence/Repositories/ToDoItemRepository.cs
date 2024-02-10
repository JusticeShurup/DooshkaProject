using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Dooshka.Domain;
using Dooshka.Application.Persistence.Contracts;

namespace Dooshka.Persistence.Repositories
{
    public class ToDoItemRepository : IRepository<ToDoItem>
    {
        private readonly ApplicationDbContext _context;

        public ToDoItemRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(ToDoItem item)
        {
            await _context.ToDoItems.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public ToDoItem? Find(Func<ToDoItem, bool> predicate)
        {
            return _context.ToDoItems.SingleOrDefault(predicate);
        }

        public IEnumerable<ToDoItem> GetAll()
        {
            return _context.ToDoItems;
        }

        public async Task<IEnumerable<ToDoItem>> FindAllAsync(Func<ToDoItem, bool> predicate)
        {
            return _context.ToDoItems.Where(predicate).ToList();
        }

        public async Task UpdateAsync(ToDoItem item)
        {
            _context.ToDoItems.Update(item);
            await _context.SaveChangesAsync();

        }

        public async Task DeleteAsync(ToDoItem item)
        {
            _context.ToDoItems.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ToDoItem>> GetAllAsync()
        {
            return await _context.ToDoItems.ToListAsync();
        }
    }
}
