using System.ComponentModel.DataAnnotations;

namespace Nivo.API.Models.Domain
{
    public class Patient
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string UserId { get; set; } = string.Empty;

        [Required]
        public string Name { get; set; } = string.Empty;

        public string? PhoneNumber { get; set; }

        public int Age { get; set; }

        public DateTime EntryTime { get; set; }

        public DateTime Date { get; set; }

        public bool SelfRegistered { get; set; }

        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}