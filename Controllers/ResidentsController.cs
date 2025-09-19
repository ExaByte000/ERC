using Microsoft.AspNetCore.Mvc;
using AccountService.Services;
using AccountService.DTOs;

namespace AccountService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ResidentsController : ControllerBase
    {
        private readonly IResidentService _residentService;

        public ResidentsController(IResidentService residentService)
        {
            _residentService = residentService;
        }

        [HttpGet("account/{accountId}")]
        public async Task<ActionResult<IEnumerable<ResidentDto>>> GetResidentsByAccount(int accountId)
        {
            var residents = await _residentService.GetResidentsByAccountIdAsync(accountId);
            return Ok(residents);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResidentDto>> GetResident(int id)
        {
            var resident = await _residentService.GetResidentByIdAsync(id);

            if (resident == null)
                return NotFound($"Проживающий с ID {id} не найден");

            return Ok(resident);
        }

        [HttpPost]
        public async Task<ActionResult<ResidentDto>> CreateResident(CreateResidentDto createResidentDto)
        {
            try
            {
                var resident = await _residentService.CreateResidentAsync(createResidentDto);
                return CreatedAtAction(nameof(GetResident), new { id = resident.Id }, resident);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ResidentDto>> UpdateResident(int id, UpdateResidentDto updateResidentDto)
        {
            var resident = await _residentService.UpdateResidentAsync(id, updateResidentDto);

            if (resident == null)
                return NotFound($"Проживающий с ID {id} не найден");

            return Ok(resident);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteResident(int id)
        {
            var deleted = await _residentService.DeleteResidentAsync(id);

            if (!deleted)
                return NotFound($"Проживающий с ID {id} не найден");

            return NoContent();
        }
    }
}
