using Microsoft.AspNetCore.Mvc;
using AccountService.Services;
using AccountService.DTOs;

namespace AccountService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountsController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccountDto>>> GetAccounts()
        {
            var accounts = await _accountService.GetAllAccountsAsync();
            return Ok(accounts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AccountDto>> GetAccount(int id)
        {
            var account = await _accountService.GetAccountByIdAsync(id);

            if (account == null)
                return NotFound($"Лицевой счет с ID {id} не найден");

            return Ok(account);
        }

        [HttpPost]
        public async Task<ActionResult<AccountDto>> CreateAccount(CreateAccountDto createAccountDto)
        {
            try
            {
                var account = await _accountService.CreateAccountAsync(createAccountDto);
                return CreatedAtAction(nameof(GetAccount), new { id = account.Id }, account);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<AccountDto>> UpdateAccount(int id, UpdateAccountDto updateAccountDto)
        {
            try
            {
                var account = await _accountService.UpdateAccountAsync(id, updateAccountDto);

                if (account == null)
                    return NotFound($"Лицевой счет с ID {id} не найден");

                return Ok(account);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAccount(int id)
        {
            var deleted = await _accountService.DeleteAccountAsync(id);

            if (!deleted)
                return NotFound($"Лицевой счет с ID {id} не найден");

            return NoContent();
        }

        [HttpPost("search")]
        public async Task<ActionResult> SearchAccounts(AccountFilterDto filter)
        {
            var (accounts, totalCount) = await _accountService.FilterAccountsAsync(filter);

            var response = new
            {
                Data = accounts,
                TotalCount = totalCount,
                Page = filter.Page,
                PageSize = filter.PageSize,
                TotalPages = (int)Math.Ceiling((double)totalCount / filter.PageSize)
            };

            return Ok(response);
        }

        [HttpGet("generate-number")]
        public async Task<ActionResult<string>> GenerateAccountNumber()
        {
            var accountNumber = await _accountService.GenerateAccountNumberAsync();
            return Ok(new { AccountNumber = accountNumber });
        }
    }
}
