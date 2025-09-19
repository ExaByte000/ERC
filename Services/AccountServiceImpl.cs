using AccountService.DTOs;
using AccountService.Models;
using AccountService.Repositories;

namespace AccountService.Services
{
    public class AccountServiceImpl : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IAccountNumberGenerator _accountNumberGenerator;

        public AccountServiceImpl(IAccountRepository accountRepository, IAccountNumberGenerator accountNumberGenerator)
        {
            _accountRepository = accountRepository;
            _accountNumberGenerator = accountNumberGenerator;
        }

        public async Task<IEnumerable<AccountDto>> GetAllAccountsAsync()
        {
            var accounts = await _accountRepository.GetAllAccountsAsync();
            return accounts.Select(MapToDto);
        }

        public async Task<AccountDto?> GetAccountByIdAsync(int id)
        {
            var account = await _accountRepository.GetAccountByIdAsync(id);
            return account != null ? MapToDto(account) : null;
        }

        public async Task<AccountDto> CreateAccountAsync(CreateAccountDto createAccountDto)
        {
            if (createAccountDto.EndDate.HasValue && createAccountDto.StartDate >= createAccountDto.EndDate)
            {
                throw new ArgumentException("Дата начала должна быть раньше даты окончания");
            }

            var accountNumber = await _accountNumberGenerator.GenerateAccountNumberAsync();

            var account = new Account
            {
                AccountNumber = accountNumber,
                StartDate = createAccountDto.StartDate,
                EndDate = createAccountDto.EndDate,
                Address = createAccountDto.Address,
                Area = createAccountDto.Area
            };

            var createdAccount = await _accountRepository.CreateAccountAsync(account);
            return MapToDto(createdAccount);
        }

        public async Task<AccountDto?> UpdateAccountAsync(int id, UpdateAccountDto updateAccountDto)
        {
            var account = await _accountRepository.GetAccountByIdAsync(id);
            if (account == null) return null;

            if (updateAccountDto.EndDate.HasValue && updateAccountDto.StartDate >= updateAccountDto.EndDate)
            {
                throw new ArgumentException("Дата начала должна быть раньше даты окончания");
            }

            account.StartDate = updateAccountDto.StartDate;
            account.EndDate = updateAccountDto.EndDate;
            account.Address = updateAccountDto.Address;
            account.Area = updateAccountDto.Area;

            var updatedAccount = await _accountRepository.UpdateAccountAsync(account);
            return MapToDto(updatedAccount);
        }

        public async Task<bool> DeleteAccountAsync(int id)
        {
            return await _accountRepository.DeleteAccountAsync(id);
        }

        public async Task<(IEnumerable<AccountDto> accounts, int totalCount)> FilterAccountsAsync(AccountFilterDto filter)
        {
            var accounts = await _accountRepository.FilterAccountsAsync(filter);
            var totalCount = await _accountRepository.GetFilteredAccountsCountAsync(filter);

            return (accounts.Select(MapToDto), totalCount);
        }

        public async Task<string> GenerateAccountNumberAsync()
        {
            return await _accountNumberGenerator.GenerateAccountNumberAsync();
        }

        private AccountDto MapToDto(Account account)
        {
            return new AccountDto
            {
                Id = account.Id,
                AccountNumber = account.AccountNumber,
                StartDate = account.StartDate,
                EndDate = account.EndDate,
                Address = account.Address,
                Area = account.Area,
                IsActive = account.IsActive,
                Residents = account.Residents.Select(r => new ResidentDto
                {
                    Id = r.Id,
                    FirstName = r.FirstName,
                    LastName = r.LastName,
                    MiddleName = r.MiddleName,
                    DateOfBirth = r.DateOfBirth,
                    PassportSeries = r.PassportSeries,
                    PassportNumber = r.PassportNumber,
                    RegistrationDate = r.RegistrationDate,
                    AccountId = r.AccountId,
                    FullName = r.FullName
                }).ToList()
            };
        }
    }
}
