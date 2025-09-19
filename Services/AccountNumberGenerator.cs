
using AccountService.Repositories;

namespace AccountService.Services
{
    public class AccountNumberGenerator : IAccountNumberGenerator
    {
        private readonly IAccountRepository _accountRepository;

        public AccountNumberGenerator(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<string> GenerateAccountNumberAsync()
        {
            string accountNumber;
            bool exists;

            do
            {
                accountNumber = GenerateRandomNumber();
                exists = await _accountRepository.AccountNumberExistsAsync(accountNumber);
            } while (exists);

            return accountNumber;
        }

        private string GenerateRandomNumber()
        {
            var random = new Random();
            return random.Next(1000000000, int.MaxValue).ToString().PadLeft(10, '0');
        }
    }
}
