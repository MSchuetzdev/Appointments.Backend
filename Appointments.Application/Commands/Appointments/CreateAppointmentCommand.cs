using Appointments.Application.Common.Interfaces.Persistence;
using Appointments.Domain.Entities.Appointment.Interfaces;
using TimeWarp.Mediator;

namespace Appointments.Application.Commands.Appointments;

public class CreateAppointmentCommand : IRequest<IAppointment>
{
    /// <summary>
    /// Name of the appointment that will be created 
    /// </summary>
    public string Name { get; set; } = "";

    /// <summary>
    /// Starttime of the appointment that will be created
    /// </summary>
    public DateTime StartTime { get; set; }

    /// <summary>
    /// Endtime of the appointment that will be created
    /// </summary>
    public DateTime EndTime { get; set; }
    
    /// <summary>
    /// Time when the appointment is deleted
    /// </summary>
    public DateTime? DeletionTime { get; set; }
    
    /// <summary>
    /// Id of the person who created the appointment
    /// </summary>
    public Guid CreatorPersonId { get; set; }

    /// <summary>
    /// Id from the organization that the appointment will be created for
    /// </summary>
    public Guid HostOrganizationId { get; set; }
    
    /// <summary>
    /// Id of the appointment kind that will be created
    /// </summary>
    public Guid AppointmentKindId { get; set; }

    /// <summary>
    /// Check start is before end
    /// </summary>
    /// <returns></returns>
    public bool IsStartBeforeEndtime()
    {
        return StartTime < EndTime;
    }
}

public class CreateAppointmentCommandHandler(
    IAppointmentRepository appointmentRepository
)
    : IRequestHandler<CreateAppointmentCommand, IAppointment>
{
    public async Task<IAppointment> Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
    {
        if (!request.IsStartBeforeEndtime())
        {
            throw new Exception("Start time must be before end time");
        }

        return await appointmentRepository.CreateAsync(request);
    }
}