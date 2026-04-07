
using System.ComponentModel.DataAnnotations;
namespace DAL.Models
{
    public class Appointment
    {
       public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Enter a valid email address")]
        public string Email { get; set; }

       [Required(ErrorMessage = "Appointment date is required")]
[DataType(DataType.DateTime)]
[CustomValidation(typeof(Appointment), nameof(ValidateDate))]
public DateTime Date { get; set; }

public static ValidationResult ValidateDate(DateTime date, ValidationContext context)
{
    if (date < DateTime.Now)
    {
        return new ValidationResult("Appointment date cannot be in the past");
    }
    return ValidationResult.Success;
}

        public AppointmentStatus Status { get; set; } = AppointmentStatus.Pending;
    
    }
}