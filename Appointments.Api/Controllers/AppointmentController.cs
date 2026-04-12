using Appointments.Application.Commands.Appointments;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Appointments.Api.Controllers;

[Route("[controller]")]
[ApiController]
public class AppointmentController(IMediator mediator) : Controller
{
    /// <summary>
    /// Creates a new Appointment
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> CreateAppointment(CreateAppointmentCommand command)
    {
        var appointment = await mediator.Send(command);
        return Ok(appointment);
    }

    /// <summary>
    /// Updates an appointment
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPut]
    public async Task<IActionResult> UpdateAppointment(UpdateAppointmentCommand command)
    {
        var appointment = await mediator.Send(command);
        return Ok(appointment);
    }

    /// <summary>
    /// Cancel an appointment 
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost("cancel")]
    public async Task<IActionResult> CancelAppointment(CancelAppointmentCommand command)
    {
        var appointment = await mediator.Send(command);
        return Ok(appointment);
    }

    /// <summary>
    /// Book an appointment
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost("book")]
    public async Task<IActionResult> BookAppointment(BookAppointmentCommand command)
    {
        var appointment = await mediator.Send(command);

        if (appointment == null)
        {
            return NotFound();
        }

        return Ok(appointment);
    }
}