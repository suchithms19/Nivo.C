using Nivo.API.Data;
using Nivo.API.Models.Domain;
using Nivo.API.Models.DTO;
using Nivo.API.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace Nivo.API.Services
{
    public class AppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly AppDbContext _context;

        public AppointmentService(
            IAppointmentRepository appointmentRepository,
            IPatientRepository patientRepository,
            AppDbContext context)
        {
            _appointmentRepository = appointmentRepository;
            _patientRepository = patientRepository;
            _context = context;
        }

        public async Task<(Appointment appointment, Patient patient)> BookAppointmentAsync(string userId, BookAppointmentDto dto)
        {
            var appointmentStartTime = dto.StartTime;

            var existingAppointment = await _appointmentRepository
                .FindScheduledByUserAndStartTimeAsync(userId, appointmentStartTime);

            if (existingAppointment != null)
            {
                throw new InvalidOperationException("This slot is no longer available. Please choose another time.");
            }

            var patient = new Patient
            {
                UserId = userId,
                Name = dto.Name,
                PhoneNumber = dto.PhoneNumber,
                Age = dto.Age,
                EntryTime = appointmentStartTime,
                Date = appointmentStartTime,
                SelfRegistered = true
            };

            // Begin transaction so both patient and appointment persist together
            await using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                await _patientRepository.AddAsync(patient);
                await _context.SaveChangesAsync();

                var appointment = new Appointment
                {
                    UserId = userId,
                    PatientId = patient.Id,
                    StartTime = appointmentStartTime,
                    EndTime = appointmentStartTime.AddMinutes(30),
                    Status = "scheduled"
                };

                await _appointmentRepository.AddAsync(appointment);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                return (appointment, patient);
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<IEnumerable<AppointmentDto>> GetAllAppointmentsAsync()
        {
            var appointments = await _appointmentRepository.GetAllAsync();
            return appointments.Select(MapToDto);
        }

        public async Task<AppointmentDto?> GetAppointmentByIdAsync(Guid id)
        {
            var appointment = await _appointmentRepository.GetByIdAsync(id);
            return appointment == null ? null : MapToDto(appointment);
        }

        private AppointmentDto MapToDto(Appointment appointment)
        {
            return new AppointmentDto
            {
                Id = appointment.Id,
                UserId = appointment.UserId,
                StartTime = appointment.StartTime,
                EndTime = appointment.EndTime,
                Status = appointment.Status,
                Patient = appointment.Patient == null ? null : new PatientDto
                {
                    Id = appointment.Patient.Id,
                    Name = appointment.Patient.Name,
                    PhoneNumber = appointment.Patient.PhoneNumber,
                    Age = appointment.Patient.Age,
                    SelfRegistered = appointment.Patient.SelfRegistered
                }
            };
        }
    }
}