using Appointments.Application.Interfaces.Repositories;
using Appointments.Domain.Entities.Appointment;

namespace Appointments.Application.Common.Interfaces.Repositories;

public interface IAppointmentRepository : IBaseRepository<Appointment>
{
}