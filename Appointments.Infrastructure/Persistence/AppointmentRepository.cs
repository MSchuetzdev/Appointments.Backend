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
                                   INSERT INTO appointments a (name, start_time, end_time)
                                   VALUES (:Name, :StartTime, :EndTime);
                                   RETURNING id; 


                                   INSERT INTO customers(person_id, appointment_id)
                                   VALUES (:PersonId, a.id);
                                   """;

        using var connection = await sqlConnectionProvider.GetConnection("default");

        var appointmentId = await connection.ExecuteScalarAsync<Guid>(createQuery, new
        {
            entity.Name,
            entity.StartTime,
            entity.EndTime,
            entity.PersonId
        });

        return await GetByIdAsync(appointmentId);
    }

    public async Task<IEnumerable<Appointment>> GetAsync(string sqlFilter, object param)
    {
        const string baseQuery = """
                                 SELECT a.id,
                                        a.start_time,
                                        a.end_time,
                                 FROM appointments 
                                 WHERE id IN (SELECT id FROM TempAppointments)

                                 SELECT c.id,
                                        c.firstname,
                                        c.lastname,
                                        c.person_id
                                 FROM customers c 
                                 INNER JOIN appointments a ON
                                 c.appointment_id = a.appointment_id;
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
                        Firstname = customer.Firstname,
                        Lastname = customer.Lastname,
                        PersonId = customer.PersonId,
                    };
                }
            }
        }

        return appointments;
    }

    public async Task<Appointment> GetByIdAsync(Guid id)
    {
        const string sqlFilter = """
                                 CREATE TEMP TABLE TempAppointments (id GUID PRIMARY KEY);

                                 INSERT INTO TempAppointments (id)
                                 WHERE id = :Id; 
                                 """; 

        object param = new { Id = id };

        var res = (await GetAsync(sqlFilter, param)).FirstOrDefault();

        return res ?? new Appointment();
    }
}