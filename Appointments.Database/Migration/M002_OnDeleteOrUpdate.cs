using System.Data;
using FluentMigrator;

namespace Appointments.Database.Migration;

[Migration(1771682848)]
public class M002_OnDeleteOrUpdate : FluentMigrator.Migration
{
    public override void Up()
    {
        Execute.Sql("CREATE EXTENSION IF NOT EXISTS \"uuid-ossp\";");
        
        Delete.Table("customers");
        Delete.Table("appointment_employees");
        Delete.Table("appointment_kinds");
        Delete.Table("organization_employees");

        Create.Table("organization_employees")
            .WithColumn("id").AsGuid().NotNullable().PrimaryKey().WithDefault(SystemMethods.NewGuid)
            .WithColumn("organization_id").AsGuid().NotNullable().ForeignKey("organizations", "id")
            .WithColumn("employee_id").AsGuid().NotNullable().ForeignKey("employees", "id")
            .OnDeleteOrUpdate(Rule.Cascade);

        Create.Table("customers")
            .WithColumn("id").AsGuid().NotNullable().PrimaryKey().WithDefault(SystemMethods.NewGuid)
            .WithColumn("person_id").AsGuid().NotNullable().ForeignKey("persons", "id")
            .WithColumn("appointment_id").AsGuid().NotNullable().ForeignKey("appointments", "id")
            .OnDeleteOrUpdate(Rule.Cascade);

        Create.Table("appointment_employees")
            .WithColumn("id").AsGuid().NotNullable().PrimaryKey().WithDefault(SystemMethods.NewGuid)
            .WithColumn("appointment_id").AsGuid().NotNullable().ForeignKey("appointments", "id")
            .WithColumn("employee_id").AsGuid().NotNullable().ForeignKey("employees", "id")
            .OnDeleteOrUpdate(Rule.Cascade);

        Create.Table("appointment_kinds")
            .WithColumn("id").AsGuid().NotNullable().PrimaryKey().WithDefault(SystemMethods.NewGuid)
            .WithColumn("appointment_id").AsGuid().NotNullable().ForeignKey("appointments", "id")
            .WithColumn("kind_id").AsGuid().NotNullable().ForeignKey("kinds", "id")
            .OnDeleteOrUpdate(Rule.Cascade);
    }

    public override void Down()
    {
        throw new NotImplementedException();
    }
}