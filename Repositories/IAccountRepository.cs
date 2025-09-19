using AccountService.Models;
using AccountService.DTOs;

namespace AccountService.Repositories
{
    public interface IAccountRepository
    {
        Task<IEnumerable<Account>> GetAllAccountsAsync();
        Task<Account?> GetAccountByIdAsync(int id);
        Task<Account?> GetAccountByNumberAsync(string accountNumber);
        Task<Account> CreateAccountAsync(Account account);
        Task<Account> UpdateAccountAsync(Account account);
        Task<bool> DeleteAccountAsync(int id);
        Task<IEnumerable<Account>> FilterAccountsAsync(AccountFilterDto filter);
        Task<int> GetFilteredAccountsCountAsync(AccountFilterDto filter);
        Task<bool> AccountNumberExistsAsync(string accountNumber, int? excludeId = null);
    }
}
