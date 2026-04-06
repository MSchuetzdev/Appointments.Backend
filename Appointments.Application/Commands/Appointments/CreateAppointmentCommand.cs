using Appointments.Application.Common.Interfaces.Persistence;
using Appointments.Domain.Entities.Appointment;
using Appointments.Domain.Entities.Appointment.Interfaces;
using MediatR;

namespace Appointments.Application.Commands.Appointments;

public class CreateAppointmentCommand : IRequest<Appointment>
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
    /// Id of the person 
    /// </summary>
    public Guid PersonId { get; set; }
    
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