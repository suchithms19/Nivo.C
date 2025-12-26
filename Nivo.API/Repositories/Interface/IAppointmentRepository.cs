using Nivo.API.Models.Domain;

namespace Nivo.API.Repositories.Interface
{
    public interface IAppointmentRepository
    {
        Task<Appointment?> FindScheduledByUserAndStartTimeAsync(string userId, DateTime startTime);
        Task AddAsync(Appointment appointment);
        Task<IEnumerable<Appointment>> GetAllAsync();
        Task<Appointment?> GetByIdAsync(Guid id);
    }
}