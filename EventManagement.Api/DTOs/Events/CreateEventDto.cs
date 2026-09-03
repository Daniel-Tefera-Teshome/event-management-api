namespace EventManagement.DTOs.Events;

using System.ComponentModel.DataAnnotations;

public class CreateEventDto
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    [MaxLength(500)]
    public string? Description { get; set; }

    [Required]
    public DateTime StartDate { get; set; }

    [Required]
    public DateTime EndDate { get; set; }

    [Required]
    [MaxLength(200)]
    public string Location { get; set; }
}