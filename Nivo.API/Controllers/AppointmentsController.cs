using Microsoft.AspNetCore.Mvc;
using Nivo.API.Models.DTO;
using Nivo.API.Services;
using System;
using System.Threading.Tasks;

namespace Nivo.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentsController : ControllerBase
    {
        private readonly AppointmentService _appointmentService;

        public AppointmentsController(AppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [HttpPost("{userId}/book")]
        public async Task<IActionResult> BookAppointment(string userId, [FromBody] BookAppointmentDto dto)
        {
            try
            {
                var (appointment, patient) = await _appointmentService.BookAppointmentAsync(userId, dto);

                return Ok(new
                {
                    message = "Appointment booked successfully"
                });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { error = "Something went wrong" });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAppointments()
        {
            try
            {
                var appointments = await _appointmentService.GetAllAppointmentsAsync();

                return Ok(appointments);
            }
            catch (Exception)
            {
                return StatusCode(500, new { error = "Something went wrong" });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAppointmentById(Guid id)
        {
            try
            {
                var appointment = await _appointmentService.GetAppointmentByIdAsync(id);

                if (appointment == null)
                {
                    return NotFound(new { error = "Appointment not found" });
                }

                return Ok(appointment);
            }
            catch (Exception)
            {
                return StatusCode(500, new { error = "Something went wrong" });
            }
        }
    }
}
