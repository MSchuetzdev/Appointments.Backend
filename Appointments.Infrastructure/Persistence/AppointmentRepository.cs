using Appointments.Application.Commands;
using Appointments.Application.Common.Interfaces.Repositories;
using Appointments.Domain.Entities.Appointment;
using Appointments.Domain.Entities.Customer;
using Appointments.Infrastructure.Common.Interfaces;
using Appointments.Infrastructure.Common.Models.Record;
using Dapper;

namespace Appointments.Infrastructure.Persistence;

public class AppointmentRepository(
    ISqlConnectionProvider sqlConnectionProvider
)
    : IAppointmentRepository, IReadRepository<Appointment>
{
    public async Task<Appointment> CreateAsync(CreateAppointmentCommand entity)
    {
        const string createQuery = """
                                   WITH new_appointment AS (
                                       INSERT INTO appointments (name, start_time, end_time)
                                       VALUES (:Name, :StartTime, :EndTime)
                                       RETURNING id
                                   ),
                                   new_customer AS (
                                       INSERT INTO customers (person_id, appointment_id)
                                       SELECT :PersonId, id
                                       FROM new_appointment
                                   )
                                   SELECT id
                                   FROM new_appointment;
                                   """;

        using var connection = await sqlConnectionProvider.GetConnection("default");

        var appointmentId = await connection.QueryFirstOrDefaultAsync<Guid>(createQuery, new
        {
            entity.Name,
            entity.StartTime,
            entity.EndTime,
            entity.PersonId
        });

        return await ReadByIdAsync(appointmentId);
    }

    public async Task<IEnumerable<Appointment>> GetAsync(string sqlFilter, object param)
    {
        const string baseQuery = """
                                 SELECT a.id,
                                        a.name,
                                        a.start_time,
                                        a.end_time
                                 FROM appointments a
                                 WHERE id IN (SELECT id FROM TempAppointments);

                                 SELECT c.id,
                                        c.person_id,
                                        c.appointment_id,
                                        p.first_name , 
                                        p.last_name,
                                        p.email
                                 FROM customers c
                                 INNER JOIN persons p ON c.person_id = p.id
                                 WHERE c.appointment_id IN (SELECT id FROM TempAppointments);
                                 """;


        using var connection = await sqlConnectionProvider.GetConnection("default");

        await using var reader = await connection.QueryMultipleAsync(sqlFilter + baseQuery, param);

        var appointments = (await reader.ReadAsync<Appointment>()).ToList();

        var customers = (await reader.ReadAsync<CustomerRecord>()).GroupBy(recorder => recorder.AppointmentId)
            .ToDictionary(elem => elem.Key, elem => elem.ToList());

        foreach (var appointment in appointments)
        {
            if (customers.ContainsKey(appointment.Id))
            {
                foreach (var customer in customers[appointment.Id])
                {
                    appointment.Customer = new Customer()
                    {
                        Id = customer.Id,
                        Firstname = customer.FirstName,
                        Lastname = customer.LastName,
                        PersonId = customer.PersonId,
                    };
                }
            }
        }

        return appointments;
    }

    public async Task<Appointment> ReadByIdAsync(Guid id)
    {
        const string sqlFilter = """
                                 CREATE TEMP TABLE TempAppointments (id uuid PRIMARY KEY);

                                 INSERT INTO TempAppointments (id)
                                 SELECT id FROM appointments
                                 WHERE id = :Id; 
                                 """;

        object param = new { Id = id };

        var res = (await GetAsync(sqlFilter, param)).FirstOrDefault();

        return res ?? new Appointment();
    }
}