using Appointments.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Appointments.Api.Controllers;

[Route("[controller]")]
[ApiController]
public class AppointmentController(IMediator mediator):Controller
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
    
    
    
}