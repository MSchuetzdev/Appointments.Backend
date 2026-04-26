using System.Net.Http.Json;
using Appointments.Application.Commands.Appointments;
using Appointments.Application.Common.Interfaces.Persistence;
using Appointments.Domain.Entities.Appointment;
using Appointments.Domain.Entities.Customer;
using Appointments.Domain.Entities.Person;
using Appointments.Domain.Options;
using Appointments.Infrastructure.Common.Interfaces;
using Appointments.Infrastructure.Common.Models.Record;
using Dapper;
using Microsoft.Extensions.Options;

namespace Appointments.Infrastructure.Persistence;

public class BookedAppointmentRepository(
    IAppointmentRepository appointmentRepository,
    ISqlConnectionProvider sqlConnectionProvider,
    IOptions<ApiUrls> options
) : IBookedAppointmentRepository
{
    private readonly HttpClient _client = new HttpClient();

    /// <summary>
    /// Base method for getting booked appointments
    /// </summary>
    /// <returns></returns>
    private async Task<IEnumerable<BookedAppointment>> ReadAsync(string sqlFilter, object param)
    {
        const string query = """
                             SELECT id, person_id, appointment_id 
                             FROM appointment_bookings 
                             WHERE id IN (SELECT id FROM TempAppointmentBookings)
                             """;

        using var connection = await sqlConnectionProvider.GetConnection();

        var appointmentBookingsResult = await connection.QueryAsync<AppointmentBookingRecord>(sqlFilter + query, param);

        var appointmentIds = appointmentBookingsResult.DistinctBy(x => x.AppointmentId).Select(b => b.AppointmentId)
            .ToList();

        var appointments = await appointmentRepository.ReadByIdsAsync(appointmentIds);

        var groupedAppointmentBookings = appointmentBookingsResult.GroupBy(x => x.AppointmentId)
            .ToDictionary(x => x.Key, elem => elem.ToList());

        var bookedAppointments = new List<BookedAppointment>();

        foreach (var appointment in appointments)
        {
            var bookedAppointment = new BookedAppointment()
            {
                Id = appointment.Id,
                Name = appointment.Name,
                StartTime = appointment.StartTime,
                EndTime = appointment.EndTime,
            };


            // Set customers for appointments
            if (groupedAppointmentBookings.ContainsKey(appointment.Id))
            {
                var requestParameter = string.Join("&",
                    groupedAppointmentBookings[appointment.Id].Select(x => $"personIds{x.CustomerPersonId}"));

                var persons =
                    await _client.GetFromJsonAsync<IEnumerable<Person>>(
                        $"{options.Value.IdentityBackendUrl}/persons?{requestParameter}");

                foreach (var person in persons)
                {
                    bookedAppointment.Customer.AddRange(new Customer()
                    {
                        Firstname = person.Firstname,
                        Lastname = person.Lastname,
                        PersonId = person.Id
                    });
                }
            }

            bookedAppointments.Add(bookedAppointment);
        }

        return bookedAppointments;
    }

    /// <inheritdoc/>>
    public async Task<BookedAppointment> BookAppointmentAsync(BookAppointmentCommand command)
    {
        const string query = """
                             INSERT INTO appointment_bookings(customer_person_id, appointment_id)
                             VALUES (:CustomerPersonId, :AppointmentId)
                             RETURNING appointment_id;
                             """;

        await using var connection = await sqlConnectionProvider.GetConnection();

        object param = new
        {
            CustomerPersonId = command.PersonId,
            command.AppointmentId
        };

        var appointmentId = await connection.ExecuteScalarAsync<Guid>(query, param);

        return await GetBookedAppointmentByAppointmentIdAsync(appointmentId);
    }

    /// <summary>
    /// Get booked appointment by appointment id 
    /// </summary>
    /// <param name="appointmentId"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<BookedAppointment> GetBookedAppointmentByAppointmentIdAsync(Guid appointmentId)
    {
        const string sqlFilter = """
                                 CREATE TEMP TABLE TempBookedAppointmentBookings(id uuid PRIMARY KEY);

                                 INSERT INTO TempBookedAppointmentBookings(id)
                                 SELECT id FROM appointment_bookings
                                 WHERE appointment_id = :AppointmentId;
                                 """;

        object param = new
        {
            AppointmentId = appointmentId
        };

        return (await ReadAsync(sqlFilter, param)).First();
    }

    public async Task CancelBookedAppointmentAsync(Guid appointmentId)
    {
        const string query = """
                             UPDATE appointment_bookings 
                             SET cancellation_time = :CancellationTime
                             WHERE appointment_id = :AppointmentId;
                             """;

        await using var connection = await sqlConnectionProvider.GetConnection();

        object param = new { CancellationTime = DateTime.Now, AppointmentId = appointmentId };

        await connection.ExecuteAsync(query, param);
    }

    public Task<BookedAppointment> ReadByIdAsync(Guid appointmentId)
    {
        throw new NotImplementedException();
    }
}