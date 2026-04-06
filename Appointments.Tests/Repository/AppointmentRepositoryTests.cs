using Appointments.Application.Common.Interfaces.Persistence;

namespace Appointments.Tests.Repository;

public class AppointmentRepositoryTests
{
    [Fact]
    public async Task GetByIdAsync_ShouldGetAppointment()
    {
        Guid id = Guid.Parse("53347aa2-0daf-40d3-ac5c-54e0daa96d25");

        var appointment = await new ApplicationFactory()
            .GetScopedService<IAppointmentRepository>()
            .ReadByIdAsync(id);

        Assert.NotNull(appointment);
    }
}