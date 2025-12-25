using System.ComponentModel.DataAnnotations;

namespace Nivo.API.Models.DTO
{
    public class BookAppointmentDto
    {
        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public string? PhoneNumber { get; set; }

        public int Age { get; set; }
    }
}