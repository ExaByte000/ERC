namespace AccountService.DTOs
{
    public class AccountDto
    {
        public int Id { get; set; }
        public string AccountNumber { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Address { get; set; } = string.Empty;
        public double Area { get; set; }
        public List<ResidentDto> Residents { get; set; } = new List<ResidentDto>();
        public bool IsActive { get; set; }
    }

    public class CreateAccountDto
    {
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Address { get; set; } = string.Empty;
        public double Area { get; set; }
    }

    public class UpdateAccountDto
    {
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Address { get; set; } = string.Empty;
        public double Area { get; set; }
    }

    public class AccountFilterDto
    {
        public string? AccountNumber { get; set; }
        public bool? OnlyWithResidents { get; set; }
        public DateTime? ActiveOnDate { get; set; }
        public string? ResidentName { get; set; }
        public string? Address { get; set; }
        public string? SortBy { get; set; }
        public bool SortDescending { get; set; } = false;
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
