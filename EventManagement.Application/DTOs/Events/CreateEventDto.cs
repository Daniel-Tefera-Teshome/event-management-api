using System.ComponentModel.DataAnnotations;

namespace EventManagement.Application.DTOs.Events;

public class CreateEventDto : IValidatableObject
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(500)]
    public string? Description { get; set; }

    [Required]
    public DateTime? StartDate { get; set; }

    [Required]
    public DateTime? EndDate { get; set; }

    [Required]
    [MaxLength(200)]
    public string Location { get; set; } = string.Empty;

    public IEnumerable<ValidationResult> Validate(
        ValidationContext validationContext)
    {
        if (StartDate.HasValue &&
            EndDate.HasValue &&
            EndDate <= StartDate)
        {
            yield return new ValidationResult(
                "End date must be after start date.",
                new[] { nameof(EndDate) }
            );
        }
    }
}
