namespace Nivo.API.Models.DTO
{
    public class AppointmentDto
    {
        public Guid Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public Guid PatientId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Status { get; set; } = string.Empty;
        public PatientDto? Patient { get; set; }
    }

    public class PatientDto
    {
        public Guid Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public int Age { get; set; }
        public DateTime EntryTime { get; set; }
        public DateTime Date { get; set; }
        public bool SelfRegistered { get; set; }
    }
}

