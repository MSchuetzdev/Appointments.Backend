using Appointments.Application.Common.Interfaces.Persistence;
using Appointments.Domain.Entities.Appointment;
using TimeWarp.Mediator;

namespace Appointments.Application.Commands.Appointments;

/// <summary>
/// Comand for book an appointment
/// </summary>
public class BookAppointmentCommand : IRequest<AppointmentBooking>
{
    /// <summary>
    /// Id of the appointment wich will be booked
    /// </summary>
    public Guid AppointmentId { get; set; }

    /// <summary>
    /// Id of the person who booked the appointment
    /// </summary>
    public Guid PersonId { get; set; }
}

public class BookAppointmentCommandHandler(IBookedAppointmentRepository bookedAppointmentRepository)
    : IRequestHandler<BookAppointmentCommand, AppointmentBooking>
{
    public async Task<AppointmentBooking> Handle(BookAppointmentCommand request, CancellationToken cancellationToken)
    {
        var appointment = await bookedAppointmentRepository.BookAppointmentAsync(request);

        return appointment;
    }
}