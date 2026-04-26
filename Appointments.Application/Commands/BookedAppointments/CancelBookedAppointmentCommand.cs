using Appointments.Application.Common.Interfaces.Persistence;
using TimeWarp.Mediator;

namespace Appointments.Application.Commands.BookedAppointments;

/// <summary>
/// Command for cancel an appointment
/// </summary>
public class CancelBookedAppointmentCommand : IRequest
{
    /// <summary>
    /// Id of the appointment that should be cancelled
    /// </summary>
    public Guid AppointmentId { get; set; }
}

public class CancelAppointmentCommandHandler(
    IBookedAppointmentRepository bookedAppointmentRepository
) : IRequestHandler<CancelBookedAppointmentCommand>
{
    public async Task Handle(CancelBookedAppointmentCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await bookedAppointmentRepository.CancelBookedAppointmentAsync(request.AppointmentId);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception("Cancel appointment failed");
        }
    }
}