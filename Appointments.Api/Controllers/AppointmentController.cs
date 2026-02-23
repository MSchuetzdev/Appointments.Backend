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
    [HttpPost("/create")]
    public async Task<IActionResult> CreateAppointment(CreateAppointmentCommand command)
    {
        var appointment = await mediator.Send(command);
        return Ok(appointment);
    }    [HttpPost("/update")]
    public async Task<IActionResult> UpdateAppointment(UpdateAppointmentCommand command)
    {
        var appointment = await mediator.Send(command);
        return Ok(appointment);
    }

    [HttpPost("/cancel")]
    public async Task<IActionResult> CancelAppointment(CancelAppointmentCommand command)
    {
        var appointment = await mediator.Send(command);
        return Ok(appointment);
    }
}