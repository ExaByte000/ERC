using AccountService.Models;

namespace AccountService.Repositories
{
    public interface IResidentRepository
    {
        Task<IEnumerable<Resident>> GetResidentsByAccountIdAsync(int accountId);
        Task<Resident?> GetResidentByIdAsync(int id);
        Task<Resident> CreateResidentAsync(Resident resident);
        Task<Resident> UpdateResidentAsync(Resident resident);
        Task<bool> DeleteResidentAsync(int id);
    }
}
