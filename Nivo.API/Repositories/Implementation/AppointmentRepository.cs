using Nivo.API.Data;
using Nivo.API.Models.Domain;
using Nivo.API.Repositories.Interface;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Nivo.API.Repositories.Implementation
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly AppDbContext _context;

        public AppointmentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Appointment?> FindScheduledByUserAndStartTimeAsync(string userId, DateTime startTime)
        {
            //I only want to read this data.I will not modify it.
            return await _context.Appointments
                .AsNoTracking()
                .FirstOrDefaultAsync(a =>
                    a.UserId == userId &&
                    a.Status == "scheduled" &&
                    a.StartTime == startTime);
        }

        public async Task AddAsync(Appointment appointment)
        {
            await _context.Appointments.AddAsync(appointment);
        }
    }
}