using AccountService.Repositories;

namespace AccountService.Services
{
    public interface IAccountNumberGenerator
    {
        Task<string> GenerateAccountNumberAsync();
    }
}
