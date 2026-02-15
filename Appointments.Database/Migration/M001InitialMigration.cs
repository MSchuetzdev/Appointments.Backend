namespace Appointments.Database.Migration;

public class M001InitialMigration : FluentMigrator.Migration
{
    public override void Up()
    {
        Create.Table("persons")
            .WithColumn("id").AsGuid().NotNullable().PrimaryKey().WithDefaultValue(Guid.NewGuid())
            .WithColumn("firstname").AsString().NotNullable()
            .WithColumn("lastname").AsString().NotNullable();

        Create.Table("organizations")
            .WithColumn("id").AsGuid().NotNullable().PrimaryKey().WithDefaultValue(Guid.NewGuid())
            .WithColumn("name").AsString().NotNullable();

        Create.Table("employees")
            .WithColumn("id").AsGuid().NotNullable().PrimaryKey().WithDefaultValue(Guid.NewGuid())
            .WithColumn("person_id").AsGuid().NotNullable().ForeignKey("persons", "id");

        Create.Table("organization_employees")
            .WithColumn("id").AsGuid().NotNullable().PrimaryKey().WithDefaultValue(Guid.NewGuid())
            .WithColumn("organization_id").AsGuid().NotNullable().ForeignKey("organizations", "id")
            .WithColumn("employee_id").AsGuid().NotNullable().ForeignKey("employees", "id");

        Create.Table("appointments")
            .WithColumn("id").AsGuid().NotNullable().PrimaryKey().WithDefaultValue(Guid.NewGuid())
            .WithColumn("name").AsGuid().NotNullable()
            .WithColumn("start_time").AsDateTime().NotNullable()
            .WithColumn("end_time").AsDateTime().NotNullable();

        Create.Table("customers")
            .WithColumn("id").AsGuid().NotNullable().PrimaryKey().WithDefaultValue(Guid.NewGuid())
            .WithColumn("person_id").AsGuid().NotNullable().ForeignKey("persons", "id")
            .WithColumn("appointment_id").AsGuid().NotNullable().ForeignKey("appointments", "id");

        Create.Table("appointment_employees")
            .WithColumn("id").AsGuid().NotNullable().PrimaryKey().WithDefaultValue(Guid.NewGuid())
            .WithColumn("appointment_id").AsGuid().NotNullable().ForeignKey("appointments", "id")
            .WithColumn("employee_id").AsGuid().NotNullable().ForeignKey("employees", "id");

        Create.Table("kinds")
            .WithColumn("id").AsGuid().NotNullable().PrimaryKey().WithDefaultValue(Guid.NewGuid())
            .WithColumn("name").AsString().NotNullable();

        Create.Table("appointment_kinds")
            .WithColumn("id").AsGuid().NotNullable().PrimaryKey().WithDefaultValue(Guid.NewGuid())
            .WithColumn("appointment_id").AsGuid().NotNullable().ForeignKey("appointments", "id")
            .WithColumn("kind_id").AsGuid().NotNullable().ForeignKey("kinds", "id");
    }

    public override void Down()
    {
        throw new NotImplementedException();
    }
}