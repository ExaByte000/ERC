using AccountService.DTOs;

namespace AccountService.Services
{
    public interface IAccountService
    {
        Task<IEnumerable<AccountDto>> GetAllAccountsAsync();
        Task<AccountDto?> GetAccountByIdAsync(int id);
        Task<AccountDto> CreateAccountAsync(CreateAccountDto createAccountDto);
        Task<AccountDto?> UpdateAccountAsync(int id, UpdateAccountDto updateAccountDto);
        Task<bool> DeleteAccountAsync(int id);
        Task<(IEnumerable<AccountDto> accounts, int totalCount)> FilterAccountsAsync(AccountFilterDto filter);
        Task<string> GenerateAccountNumberAsync();
    }
}
