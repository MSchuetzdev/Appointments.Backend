using Appointments.Application.Commands.Appointments;
using Appointments.Application.Common.Interfaces.Persistence;
using Appointments.Domain.Entities.Appointment;
using Appointments.Domain.Entities.Appointment.Interfaces;
using Appointments.Domain.Options;
using Appointments.Infrastructure.Common.Interfaces;
using Appointments.Infrastructure.Common.Models.Record;
using Dapper;
using Microsoft.Extensions.Options;

namespace Appointments.Infrastructure.Persistence;

public class AppointmentRepository(
    ISqlConnectionProvider sqlConnectionProvider,
    IOptions<ApiUrls> options,
    IBookedAppointmentRepository bookedAppointmentRepository
)
    : IAppointmentRepository
{
    private readonly HttpClient _client = new HttpClient();

    /// <inheritdoc/>
    public async Task<IAppointment> CreateAsync(CreateAppointmentCommand entity)
    {
        const string createQuery = """
                                       INSERT INTO appointments (name, start_time, end_time)
                                       VALUES (:Name, :StartTime, :EndTime)
                                       RETURNING id; 
                                   """;

        using var connection = await sqlConnectionProvider.GetConnection();

        var appointmentId = await connection.QueryFirstOrDefaultAsync<Guid>(createQuery, new
        {
            entity.Name,
            entity.StartTime,
            entity.EndTime,
        });

        return await ReadByIdAsync(appointmentId);
    }

    /// <summary>
    /// Base method to read appointments
    /// </summary>
    /// <param name="sqlFilter"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    public async Task<IEnumerable<IAppointment>> ReadAsync(string sqlFilter, object param)
    {
        const string baseQuery = """
                                 SELECT a.id,
                                        a.name,
                                        a.start_time,
                                        a.end_time,
                                        a.deletion_time
                                 FROM appointments a
                                 WHERE a.id IN (SELECT id FROM TempAppointments);
                                 """;

        var appointmentResult = new List<IAppointment>();

        using var connection = await sqlConnectionProvider.GetConnection("default");

        await using var reader = await connection.QueryMultipleAsync(sqlFilter + baseQuery, param);

        var appointmentRecords = (await reader.ReadAsync<AppointmentRecord>()).ToList();

        var customerPersonIds = appointmentRecords.Select(x => x.PersonId);

        var requestParameter = string.Join("&", customerPersonIds.Select(id => $"personIds={id}"));


        /*var personsById = persons.ToDictionary(x => x.Id);*/

        foreach (var appointmentsRecord in appointmentRecords)
        {
            var appointment = new Appointment()
            {
                Id = appointmentsRecord.Id,
                Name = appointmentsRecord.Name,
                StartTime = appointmentsRecord.StartTime,
                EndTime = appointmentsRecord.EndTime,
            };

            appointmentResult.Add(appointment);
        }

        return appointmentResult;
    }

    /// <inheritdoc/>
    public async Task<IAppointment> ReadByIdAsync(Guid id)
    {
        const string sqlFilter = """
                                 CREATE TEMP TABLE TempAppointments (id uuid PRIMARY KEY);

                                 INSERT INTO TempAppointments (id)
                                 SELECT id FROM appointments
                                 WHERE id = :Id; 
                                 """;

        object param = new { Id = id };

        var res = (await ReadAsync(sqlFilter, param)).FirstOrDefault();

        return res ?? new Appointment();
    }

    /// <inheritdoc/>
    public async Task<IAppointment> UpdateAsync(UpdateAppointmentCommand command)
    {
        const string updateQuery = """
                                   UPDATE appointments
                                   SET name =:Name,
                                       start_time = :StartTime,
                                       end_time = :EndTime
                                   WHERE id =:appointmentId;

                                   UPDATE customers
                                   SET person_id = :CustomerPersonId
                                   WHERE appointment_id = :AppointmentId
                                   RETURNING appointment_id;
                                   """;

        using var connection = await sqlConnectionProvider.GetConnection();

        var appointmentId = await connection.QueryFirstOrDefaultAsync<Guid>(updateQuery, new
        {
            command.Name,
            command.StartTime,
            command.EndTime,
            command.CustomerPersonId,
            AppointmentId = command.Id
        });

        return await ReadByIdAsync(appointmentId);
    }

    /// <inheritdoc/>
    public async Task<IAppointment> CancelAppointmentByIdAsync(Guid id)
    {
        const string cancelAppointmentQuery = """
                                              UPDATE appointments 
                                              SET deletion_time = :DeletionTime
                                              WHERE id = :AppointmentId
                                              RETURNING id; 
                                              """;

        using var connection = await sqlConnectionProvider.GetConnection();

        object param = new { DeletionTime = DateTime.Now, AppointmentId = id };

        var appointmentId = await connection.ExecuteScalarAsync<Guid>(cancelAppointmentQuery, param);

        return await ReadByIdAsync(appointmentId);
    }


    public Task<IEnumerable<Appointment>> ReadByIdsAsync(List<Guid> ids)
    {
        throw new NotImplementedException();
    }
}