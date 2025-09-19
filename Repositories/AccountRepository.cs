using Microsoft.EntityFrameworkCore;
using AccountService.Data;
using AccountService.Models;
using AccountService.DTOs;

namespace AccountService.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ApplicationDbContext _context;

        public AccountRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Account>> GetAllAccountsAsync()
        {
            return await _context.Accounts
                .Include(a => a.Residents)
                .ToListAsync();
        }

        public async Task<Account?> GetAccountByIdAsync(int id)
        {
            return await _context.Accounts
                .Include(a => a.Residents)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Account?> GetAccountByNumberAsync(string accountNumber)
        {
            return await _context.Accounts
                .Include(a => a.Residents)
                .FirstOrDefaultAsync(a => a.AccountNumber == accountNumber);
        }

        public async Task<Account> CreateAccountAsync(Account account)
        {
            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();
            return account;
        }

        public async Task<Account> UpdateAccountAsync(Account account)
        {
            _context.Accounts.Update(account);
            await _context.SaveChangesAsync();
            return account;
        }

        public async Task<bool> DeleteAccountAsync(int id)
        {
            var account = await _context.Accounts.FindAsync(id);
            if (account == null) return false;

            _context.Accounts.Remove(account);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Account>> FilterAccountsAsync(AccountFilterDto filter)
        {
            var query = _context.Accounts.Include(a => a.Residents).AsQueryable();

            query = ApplyFilters(query, filter);
            query = ApplySorting(query, filter);

            return await query
                .Skip((filter.Page - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToListAsync();
        }

        public async Task<int> GetFilteredAccountsCountAsync(AccountFilterDto filter)
        {
            var query = _context.Accounts.Include(a => a.Residents).AsQueryable();
            query = ApplyFilters(query, filter);
            return await query.CountAsync();
        }

        private IQueryable<Account> ApplyFilters(IQueryable<Account> query, AccountFilterDto filter)
        {
            if (!string.IsNullOrEmpty(filter.AccountNumber))
            {
                query = query.Where(a => a.AccountNumber.Contains(filter.AccountNumber));
            }

            if (filter.OnlyWithResidents == true)
            {
                query = query.Where(a => a.Residents.Any());
            }

            if (filter.ActiveOnDate.HasValue)
            {
                var date = filter.ActiveOnDate.Value;
                query = query.Where(a => a.StartDate <= date && (a.EndDate == null || a.EndDate > date));
            }

            if (!string.IsNullOrEmpty(filter.ResidentName))
            {
                query = query.Where(a => a.Residents.Any(r =>
                    (r.FirstName + " " + r.LastName + " " + (r.MiddleName ?? "")).Contains(filter.ResidentName)));
            }

            if (!string.IsNullOrEmpty(filter.Address))
            {
                query = query.Where(a => a.Address.Contains(filter.Address));
            }

            return query;
        }

        private IQueryable<Account> ApplySorting(IQueryable<Account> query, AccountFilterDto filter)
        {
            if (string.IsNullOrEmpty(filter.SortBy))
                return query.OrderBy(a => a.Id);

            return filter.SortBy.ToLower() switch
            {
                "accountnumber" => filter.SortDescending ? query.OrderByDescending(a => a.AccountNumber) : query.OrderBy(a => a.AccountNumber),
                "startdate" => filter.SortDescending ? query.OrderByDescending(a => a.StartDate) : query.OrderBy(a => a.StartDate),
                "enddate" => filter.SortDescending ? query.OrderByDescending(a => a.EndDate) : query.OrderBy(a => a.EndDate),
                "address" => filter.SortDescending ? query.OrderByDescending(a => a.Address) : query.OrderBy(a => a.Address),
                "area" => filter.SortDescending ? query.OrderByDescending(a => a.Area) : query.OrderBy(a => a.Area),
                _ => query.OrderBy(a => a.Id)
            };
        }

        public async Task<bool> AccountNumberExistsAsync(string accountNumber, int? excludeId = null)
        {
            var query = _context.Accounts.Where(a => a.AccountNumber == accountNumber);

            if (excludeId.HasValue)
            {
                query = query.Where(a => a.Id != excludeId.Value);
            }

            return await query.AnyAsync();
        }
    }
}
