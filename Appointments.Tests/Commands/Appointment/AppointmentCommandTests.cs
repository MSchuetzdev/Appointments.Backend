using Appointments.Application.Commands;

namespace Appointments.Tests.Commands.Appointment;

public class AppointmentCommandTests
{
    [Fact]
    public async Task CreateAppointmentCommand_ShouldReturnCreatedAppointment()
    {
        var command = new CreateAppointmentCommand()
        {
            Name = "Test-Termin",
            PersonId = Guid.Parse("d0af0d5f-78b8-42ec-a540-ccab06591f22"),
            StartTime = DateTime.Now.AddHours(3),
            EndTime = DateTime.Now.AddDays(3).AddHours(2),
        };


        var result = await new ApplicationFactory()
            .Send(command);


        Assert.Matches("Test-Termin", result.Name);
    }
}