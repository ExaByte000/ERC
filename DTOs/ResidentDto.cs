namespace AccountService.DTOs
{
    public class ResidentDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? MiddleName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? PassportSeries { get; set; }
        public string? PassportNumber { get; set; }
        public DateTime RegistrationDate { get; set; }
        public int AccountId { get; set; }
        public string FullName { get; set; } = string.Empty;
    }

    public class CreateResidentDto
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? MiddleName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? PassportSeries { get; set; }
        public string? PassportNumber { get; set; }
        public int AccountId { get; set; }
    }

    public class UpdateResidentDto
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? MiddleName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? PassportSeries { get; set; }
        public string? PassportNumber { get; set; }
    }
}

