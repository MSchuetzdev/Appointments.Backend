using FluentMigrator;

namespace Appointments.Database.Migration;

[Migration(1776006391)]
public class M002_AddAppointmentBookingsTable: FluentMigrator.Migration
{
    public override void Up()
    {
        Create.Table("appointment_bookings")
            .WithColumn("id").AsGuid().NotNullable().PrimaryKey().WithDefault(SystemMethods.NewGuid)
            .WithColumn("customer_person_id").AsGuid().NotNullable()
            .WithColumn("appointment_id").AsGuid().NotNullable().ForeignKey("appointments", "id")
            .WithColumn("creation_time").AsDateTime().NotNullable(); 
    }

    public override void Down()
    {
        throw new NotImplementedException();
    }
}