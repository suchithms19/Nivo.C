using Nivo.API.Data;
using Nivo.API.Models.Domain;
using Nivo.API.Repositories.Interface;
using System.Threading.Tasks;


namespace Nivo.API.Repositories.Implementation
{
    public class PatientRepository : IPatientRepository
    {
        private readonly AppDbContext _context;

        public PatientRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Patient patient)
        {
            await _context.Patients.AddAsync(patient);
        }
    }
}