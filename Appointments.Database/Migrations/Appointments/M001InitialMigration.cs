using System.Data;
using FluentMigrator;

namespace Appointments.Database.Migration;

[Migration(1771178306)]
public class M001InitialMigration : FluentMigrator.Migration
{
    public override void Up()
    {
        Execute.Sql("CREATE EXTENSION IF NOT EXISTS \"uuid-ossp\";");

        Create.Table("appointments")
            .WithColumn("id").AsGuid().NotNullable().PrimaryKey().WithDefault(SystemMethods.NewGuid)
            .WithColumn("name").AsString().NotNullable()
            .WithColumn("start_time").AsDateTime().NotNullable()
            .WithColumn("end_time").AsDateTime().NotNullable()
            .WithColumn("deletion_time").AsDateTime().Nullable();

        Create.Table("customers")
            .WithColumn("id").AsGuid().NotNullable().PrimaryKey().WithDefault(SystemMethods.NewGuid)
            .WithColumn("person_id").AsGuid().NotNullable()
            .WithColumn("appointment_id").AsGuid().NotNullable().ForeignKey("appointments", "id");

        Create.Table("appointment_customers")
            .WithColumn("id").AsGuid().NotNullable().PrimaryKey().WithDefault(SystemMethods.NewGuid)
            .WithColumn("person_id").AsGuid().NotNullable()
            .WithColumn("appointment_id").AsGuid().NotNullable().ForeignKey("appointments", "id")
            .OnDeleteOrUpdate(Rule.Cascade);

        Create.Table("kinds")
            .WithColumn("id").AsGuid().NotNullable().PrimaryKey().WithDefault(SystemMethods.NewGuid)
            .WithColumn("name").AsString().NotNullable();

        Create.Table("appointment_kinds")
            .WithColumn("id").AsGuid().NotNullable().PrimaryKey().WithDefault(SystemMethods.NewGuid)
            .WithColumn("appointment_id").AsGuid().NotNullable().ForeignKey("appointments", "id")
            .OnDeleteOrUpdate(Rule.Cascade)
            .WithColumn("kind_id").AsGuid().NotNullable().ForeignKey("kinds", "id")
            .OnDeleteOrUpdate(Rule.Cascade);

        Create.Table("appointment_bookings")
            .WithColumn("id").AsGuid().NotNullable().PrimaryKey().WithDefault(SystemMethods.NewGuid)
            .WithColumn("customer_person_id").AsGuid().NotNullable()
            .WithColumn("appointment_id").AsGuid().NotNullable().ForeignKey("appointments", "id")
            .WithColumn("creation_time").AsDateTime().NotNullable()
            .WithColumn("cancellation_time").AsDateTime(); 
    }

    public override void Down()
    {
        throw new NotImplementedException();
    }
}