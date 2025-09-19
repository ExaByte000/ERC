using Microsoft.EntityFrameworkCore;
using AccountService.Data;
using AccountService.Models;

namespace AccountService.Repositories
{
    public class ResidentRepository : IResidentRepository
    {
        private readonly ApplicationDbContext _context;

        public ResidentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Resident>> GetResidentsByAccountIdAsync(int accountId)
        {
            return await _context.Residents
                .Where(r => r.AccountId == accountId)
                .ToListAsync();
        }

        public async Task<Resident?> GetResidentByIdAsync(int id)
        {
            return await _context.Residents
                .Include(r => r.Account)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Resident> CreateResidentAsync(Resident resident)
        {
            _context.Residents.Add(resident);
            await _context.SaveChangesAsync();
            return resident;
        }

        public async Task<Resident> UpdateResidentAsync(Resident resident)
        {
            _context.Residents.Update(resident);
            await _context.SaveChangesAsync();
            return resident;
        }

        public async Task<bool> DeleteResidentAsync(int id)
        {
            var resident = await _context.Residents.FindAsync(id);
            if (resident == null) return false;

            _context.Residents.Remove(resident);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
