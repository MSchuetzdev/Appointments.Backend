using Appointments.Application.Commands.Appointments;
using Microsoft.AspNetCore.Mvc;
using TimeWarp.Mediator;

namespace Appointments.Api.Controllers;

public class AppointmentBookingController(IMediator mediator) : Controller
{
    /// <summary>
    /// Cancel an booked appointment
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    public async Task<IActionResult> CancelBookedAppointment(UpdateAppointmentCommand command)
    {
        await mediator.Send(command);
        return NoContent();
    }
}