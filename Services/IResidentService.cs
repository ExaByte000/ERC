using AccountService.DTOs;

namespace AccountService.Services
{
    public interface IResidentService
    {
        Task<IEnumerable<ResidentDto>> GetResidentsByAccountIdAsync(int accountId);
        Task<ResidentDto?> GetResidentByIdAsync(int id);
        Task<ResidentDto> CreateResidentAsync(CreateResidentDto createResidentDto);
        Task<ResidentDto?> UpdateResidentAsync(int id, UpdateResidentDto updateResidentDto);
        Task<bool> DeleteResidentAsync(int id);
    }
}
