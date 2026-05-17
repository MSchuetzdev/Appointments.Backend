using System.Data;
using FluentMigrator;

namespace Appointments.Database.Migrations.Appointments;

[Migration(1779034848)]
public class M001InitialMigration : Migration
{
    public override void Up()
    {
        Execute.Sql("CREATE EXTENSION IF NOT EXISTS \"uuid-ossp\";");

        Create.Table("kinds")
            .WithColumn("id").AsGuid().NotNullable().PrimaryKey().WithDefault(SystemMethods.NewGuid)
            .WithColumn("name").AsString().NotNullable()
            .WithColumn("organization_id").AsGuid().NotNullable()
            .WithColumn("duration_minutes").AsInt32().NotNullable()
            .WithColumn("creation_person_id").AsGuid().NotNullable();
        
        Create.Table("appointments")
            .WithColumn("id").AsGuid().NotNullable().PrimaryKey().WithDefault(SystemMethods.NewGuid)
            .WithColumn("name").AsString().NotNullable()
            .WithColumn("start_time").AsDateTime().NotNullable()
            .WithColumn("end_time").AsDateTime().NotNullable()
            .WithColumn("deletion_time").AsDateTime().Nullable()
            .WithColumn("creator_person_id").AsGuid().NotNullable()
            .WithColumn("appointment_kind_id").AsGuid().NotNullable().ForeignKey("kinds", "id")
            .OnDeleteOrUpdate(Rule.Cascade)
            .WithColumn("host_organization_id").AsGuid().NotNullable();


        Create.Table("appointment_bookings")
            .WithColumn("id").AsGuid().NotNullable().PrimaryKey().WithDefault(SystemMethods.NewGuid)
            .WithColumn("customer_person_id").AsGuid().NotNullable()
            .WithColumn("appointment_id").AsGuid().NotNullable().ForeignKey("appointments", "id")
            .WithColumn("creation_time").AsDateTime().NotNullable()
            .WithColumn("is_cancelled").AsBoolean().NotNullable().WithDefaultValue(false)
            .WithColumn("cancellation_time").AsDateTime().Nullable();
    }

    public override void Down()
    {
        throw new NotImplementedException();
    }
}