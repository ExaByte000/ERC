using System.ComponentModel.DataAnnotations;

namespace AccountService.Models
{
    public class Resident
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string LastName { get; set; } = string.Empty;

        [StringLength(100)]
        public string? MiddleName { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [StringLength(20)]
        public string? PassportSeries { get; set; }

        [StringLength(20)]
        public string? PassportNumber { get; set; }

        public DateTime RegistrationDate { get; set; } = DateTime.Now;

        public int AccountId { get; set; }
        public virtual Account Account { get; set; } = null!;

        public string FullName => $"{LastName} {FirstName} {MiddleName}".Trim();
    }
}
