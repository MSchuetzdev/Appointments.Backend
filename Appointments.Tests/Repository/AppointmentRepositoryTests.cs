using Appointments.Application.Common.Interfaces.Persistence;

namespace Appointments.Tests.Repository;

public class AppointmentRepositoryTests
{
    [Fact]
    public async Task GetByIdAsync_ShouldGetAppointment()
    {
        Guid id = Guid.Parse("98c9f750-9de3-45e5-afec-6d2c5f2ec0e0");

        var appointment = await new ApplicationFactory()
            .GetScopedService<IAppointmentRepository>()
            .ReadByIdAsync(id);


        Assert.NotNull(appointment);
    }
}