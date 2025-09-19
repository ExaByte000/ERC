using AccountService.DTOs;
using AccountService.Models;
using AccountService.Repositories;

namespace AccountService.Services
{
    public class ResidentService : IResidentService
    {
        private readonly IResidentRepository _residentRepository;
        private readonly IAccountRepository _accountRepository;

        public ResidentService(IResidentRepository residentRepository, IAccountRepository accountRepository)
        {
            _residentRepository = residentRepository;
            _accountRepository = accountRepository;
        }

        public async Task<IEnumerable<ResidentDto>> GetResidentsByAccountIdAsync(int accountId)
        {
            var residents = await _residentRepository.GetResidentsByAccountIdAsync(accountId);
            return residents.Select(MapToDto);
        }

        public async Task<ResidentDto?> GetResidentByIdAsync(int id)
        {
            var resident = await _residentRepository.GetResidentByIdAsync(id);
            return resident != null ? MapToDto(resident) : null;
        }

        public async Task<ResidentDto> CreateResidentAsync(CreateResidentDto createResidentDto)
        {
            // Проверяем существование лицевого счета
            var account = await _accountRepository.GetAccountByIdAsync(createResidentDto.AccountId);
            if (account == null)
            {
                throw new ArgumentException("Лицевой счет не найден");
            }

            var resident = new Resident
            {
                FirstName = createResidentDto.FirstName,
                LastName = createResidentDto.LastName,
                MiddleName = createResidentDto.MiddleName,
                DateOfBirth = createResidentDto.DateOfBirth,
                PassportSeries = createResidentDto.PassportSeries,
                PassportNumber = createResidentDto.PassportNumber,
                AccountId = createResidentDto.AccountId,
                RegistrationDate = DateTime.Now
            };

            var createdResident = await _residentRepository.CreateResidentAsync(resident);
            return MapToDto(createdResident);
        }

        public async Task<ResidentDto?> UpdateResidentAsync(int id, UpdateResidentDto updateResidentDto)
        {
            var resident = await _residentRepository.GetResidentByIdAsync(id);
            if (resident == null) return null;

            resident.FirstName = updateResidentDto.FirstName;
            resident.LastName = updateResidentDto.LastName;
            resident.MiddleName = updateResidentDto.MiddleName;
            resident.DateOfBirth = updateResidentDto.DateOfBirth;
            resident.PassportSeries = updateResidentDto.PassportSeries;
            resident.PassportNumber = updateResidentDto.PassportNumber;

            var updatedResident = await _residentRepository.UpdateResidentAsync(resident);
            return MapToDto(updatedResident);
        }

        public async Task<bool> DeleteResidentAsync(int id)
        {
            return await _residentRepository.DeleteResidentAsync(id);
        }

        private ResidentDto MapToDto(Resident resident)
        {
            return new ResidentDto
            {
                Id = resident.Id,
                FirstName = resident.FirstName,
                LastName = resident.LastName,
                MiddleName = resident.MiddleName,
                DateOfBirth = resident.DateOfBirth,
                PassportSeries = resident.PassportSeries,
                PassportNumber = resident.PassportNumber,
                RegistrationDate = resident.RegistrationDate,
                AccountId = resident.AccountId,
                FullName = resident.FullName
            };
        }
    }
}
