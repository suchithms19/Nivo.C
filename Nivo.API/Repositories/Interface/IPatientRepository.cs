using Nivo.API.Models.Domain;

namespace Nivo.API.Repositories.Interface
{
    public interface IPatientRepository
    {
        Task AddAsync(Patient patient);
    }
}