using Appointments.Application.Commands.Appointments;

namespace Appointments.Tests.Commands.Appointment;

public class AppointmentCommandTests
{
    [Fact]
    public async Task CreateAppointmentCommand_ShouldReturnCreatedAppointment()
    {
        var command = new CreateAppointmentCommand()
        {
            Name = "Zahnpflege-Termin",
            PersonId = Guid.Parse("b837a339-eea1-4686-9279-b81f36083f5d"),
            StartTime = DateTime.Now.AddHours(3),
            EndTime = DateTime.Now.AddDays(3).AddHours(2),
        };

        var result = await new ApplicationFactory()
            .Send(command);

        Assert.Matches("Zahnpflege-Termin", result.Name);
    }

    [Fact]
    public async Task UpdateAppointmentCommand_ShouldReturnUpdatedAppointment()
    {
        var command = new UpdateAppointmentCommand()
        {
            Id = Guid.Parse("98c9f750-9de3-45e5-afec-6d2c5f2ec0e0"),
            Name = "Test",
            StartTime = DateTime.Now.AddDays(3),
            EndTime = DateTime.Now.AddDays(3).AddHours(2),
            CustomerPersonId = Guid.Parse("b1e24ba1-37c4-4712-9652-81900a68ce4c")
        };

        var result = await new ApplicationFactory()
            .Send(command);

        Assert.NotNull(result);
    }

    [Fact]
    public async Task CancelAppointmentCommand_ShouldIsAppointmentCancelled()
    {
        var command = new CancelAppointmentCommand()
        {
            AppointmentId = Guid.Parse("e2afc211-f5e0-4143-80ec-faf5ab1c251d")
        };

        var result = await new ApplicationFactory()
            .Send(command);

        Assert.True(result.IsAppointmentCancelled());
    }
}