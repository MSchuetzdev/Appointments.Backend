using Appointments.Application.Common.Interfaces.Repositories;
using Appointments.Domain.Entities.Appointment;
using Appointments.Domain.Entities.Customer;
using MediatR;

namespace Appointments.Application.Commands;

public class CreateAppointmentCommand : IRequest
{
    /// <summary>
    /// Starttime of the appointment that will be created
    /// </summary>
    public DateTime StartTime { get; set; }

    /// <summary>
    /// Endtime of the appointment that will be created
    /// </summary>
    public DateTime EndTime { get; set; }

    /// <summary>
    /// Customers of the appointment
    /// </summary>
    public Customer Customer { get; set; } = new Customer();

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
    : IRequestHandler<CreateAppointmentCommand>
{
    public async Task Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
    {
        var appointmentEntity = new Appointment()
        {
            Id = Guid.NewGuid(),
            StartTime = request.StartTime,
            EndTime = request.EndTime,
            Customer = request.Customer
        };

        if (!request.IsStartBeforeEndtime())
        {
            throw new Exception("Start time must be before end time");
        }


        var appointment = await appointmentRepository.CreateAsync(appointmentEntity);
    }
}