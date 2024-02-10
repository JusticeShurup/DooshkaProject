using Dooshka.Application.Persistence.Contracts;
using Dooshka.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dooshka.Persistence.Repositories
{
    public class RevokedTokenRepository : IRepository<RevokedToken>
    {
        private readonly ApplicationDbContext _context;

        public RevokedTokenRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(RevokedToken item)
        {
            await _context.RevokedTokens.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(RevokedToken item)
        {
            _context.RevokedTokens.Remove(item);
            await _context.SaveChangesAsync();
        }

        public RevokedToken? Find(Func<RevokedToken, bool> predicate)
        {
            return _context.RevokedTokens.FirstOrDefault(predicate);
        }

        public async Task<IEnumerable<RevokedToken>> FindAllAsync(Func<RevokedToken, bool> predicate)
        {
            return await Task.FromResult(_context.RevokedTokens.Where(predicate));
        }

        public async Task<IEnumerable<RevokedToken>> GetAllAsync()
        {
            return await _context.RevokedTokens.ToListAsync();
        }

        public async Task UpdateAsync(RevokedToken item)
        {
            _context.RevokedTokens.Update(item);
            await _context.SaveChangesAsync();
        }
    }
}
