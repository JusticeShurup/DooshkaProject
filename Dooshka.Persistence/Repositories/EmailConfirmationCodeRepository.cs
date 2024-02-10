using Dooshka.Application.Persistance.Contracts;
using Dooshka.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dooshka.Persistence.Repositories
{
    public class EmailConfirmationCodeRepository : IRepository<EmailConfirmationCode>
    {
        private readonly ApplicationDbContext _context;

        public EmailConfirmationCodeRepository(ApplicationDbContext applicationContext)
        {
            _context = applicationContext;
        }

        public async Task CreateAsync(EmailConfirmationCode item)
        {
            await _context.EmailConfrimationCodes.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(EmailConfirmationCode item)
        {
            _context.EmailConfrimationCodes.Remove(item);
            await _context.SaveChangesAsync();
        }

        public EmailConfirmationCode? Find(Func<EmailConfirmationCode, bool> predicate)
        {
            return _context.EmailConfrimationCodes.FirstOrDefault(predicate);
        }

        public async Task<IEnumerable<EmailConfirmationCode>> FindAllAsync(Func<EmailConfirmationCode, bool> predicate)
        {
            return await Task.FromResult(_context.EmailConfrimationCodes.Where(predicate));
        }


        public async Task<IEnumerable<EmailConfirmationCode>> GetAllAsync()
        {
            return await _context.EmailConfrimationCodes.ToListAsync();
        }

        public async Task UpdateAsync(EmailConfirmationCode item)
        {
            _context.EmailConfrimationCodes.Update(item);
            await _context.SaveChangesAsync();
        }
    }
}
