using Appointments.Application.Common.Interfaces.Persistence;
using Appointments.Domain.Entities.Appointment;
using MediatR;

namespace Appointments.Application.Commands.Appointments;

/// <summary>
/// Command for cancel an appointment
/// </summary>
public class CancelAppointmentCommand : IRequest<Appointment>
{
    /// <summary>
    /// Id of the appointment that should be cancelled
    /// </summary>
    public Guid AppointmentId { get; set; }
}

public class CancelAppointmentCommandHandler(
    IAppointmentRepository appointmentRepository
) : IRequestHandler<CancelAppointmentCommand, Appointment>
{
    public async Task<Appointment> Handle(CancelAppointmentCommand request, CancellationToken cancellationToken)
    {
        var appointment = await appointmentRepository.CancelAppointmentByIdAsync(request.AppointmentId);

        if (!appointment.IsAppointmentCancelled())
        {
            throw new Exception("Cancel appointment failed");
        }

        return appointment;
    }
}