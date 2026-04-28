using Microsoft.AspNetCore.Mvc;
using vehicle_management_backend.Models;

namespace vehicle_management_backend.Controllers;

[ApiController]
[Route("api/appointments")]
public class AppointmentController : ControllerBase
{
    private static readonly List<Appointment> Appointments = new();

    [HttpGet]
    public ActionResult<IEnumerable<Appointment>> GetAll()
    {
        return Ok(Appointments);
    }

    [HttpGet("{id:int}")]
    public ActionResult<Appointment> GetById(int id)
    {
        var appointment = Appointments.FirstOrDefault(a => a.Id == id);
        if (appointment is null)
        {
            return NotFound();
        }

        return Ok(appointment);
    }

    [HttpPost]
    public ActionResult<Appointment> Create([FromBody] Appointment appointment)
    {
        appointment.Id = Appointments.Count == 0 ? 1 : Appointments.Max(a => a.Id) + 1;
        appointment.Status = string.IsNullOrWhiteSpace(appointment.Status) ? "Pending" : appointment.Status;

        Appointments.Add(appointment);

        return CreatedAtAction(nameof(GetById), new { id = appointment.Id }, appointment);
    }

    [HttpDelete("{id:int}")]
    public IActionResult Cancel(int id)
    {
        var appointment = Appointments.FirstOrDefault(a => a.Id == id);
        if (appointment is null)
        {
            return NotFound();
        }

        appointment.Status = "Cancelled";
        return NoContent();
    }
}
