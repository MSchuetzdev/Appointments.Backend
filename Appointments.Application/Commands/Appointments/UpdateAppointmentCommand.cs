using Appointments.Application.Common.Interfaces.Persistence;
using Appointments.Domain.Entities.Appointment;
using Appointments.Domain.Entities.Appointment.Interfaces;
using TimeWarp.Mediator;

namespace Appointments.Application.Commands.Appointments;

/// <summary>
/// Command for updating an appointment
/// </summary>
public class UpdateAppointmentCommand : IRequest<Appointment>, IUpdateAppointment
{
    /// <summary>
    /// Id of the appointment that will be updated
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Updated name of the appointment
    /// </summary>
    public string Name { get; set; } = "";

    /// <summary>
    /// Updated start time of the appointment
    /// </summary>
    public DateTime StartTime { get; set; }

    /// <summary>
    /// Updated end time of the appointment
    /// </summary>
    public DateTime EndTime { get; set; }

    /// <summary>
    /// Updated customer of the appointment
    /// </summary>
    public Guid CustomerPersonId { get; set; }
}

public class UpdateAppointmentCommandHandler(
    IAppointmentRepository appointmentRepository
)
    : IRequestHandler<UpdateAppointmentCommand, IAppointment>
{
    public async Task<IAppointment> Handle(UpdateAppointmentCommand request, CancellationToken cancellationToken)
    {
        return await appointmentRepository.UpdateAsync(request);
    }
}