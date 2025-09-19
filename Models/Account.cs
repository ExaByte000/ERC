using System.ComponentModel.DataAnnotations;

namespace AccountService.Models
{
    public class Account
    {
        public int Id { get; set; }

        [Required]
        [StringLength(10, MinimumLength = 10)]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Номер ЛС должен состоять из 10 цифр")]
        public string AccountNumber { get; set; } = string.Empty;

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        [Required]
        [StringLength(500)]
        public string Address { get; set; } = string.Empty;

        [Required]
        [Range(0.1, double.MaxValue, ErrorMessage = "Площадь должна быть больше 0")]
        public double Area { get; set; }

        public virtual ICollection<Resident> Residents { get; set; } = new List<Resident>();

        public bool IsActive => EndDate == null || EndDate > DateTime.Now;
    }
}
