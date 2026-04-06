using System.Data;
using FluentMigrator;

namespace Appointments.Database.Migrations.BusinessHours;

public class M002BusinessHoursInitialMigration : FluentMigrator.Migration
{
    public override void Up()
    {
        Execute.Sql("CREATE EXTENSION IF NOT EXISTS \"uuid-ossp\";");

        Create.Table("weekdays")
            .WithColumn("id").AsInt32().PrimaryKey().Identity()
            .WithColumn("day").AsInt32().NotNullable();

        Create.Table("businesshours")
            .WithColumn("id").AsGuid().PrimaryKey().WithDefault(SystemMethods.NewGuid)
            .WithColumn("start_time").AsTime().NotNullable()
            .WithColumn("end_time").AsTime().NotNullable()
            .WithColumn("organization_id").AsGuid().NotNullable()
            .WithColumn("weekday_id").AsInt32().ForeignKey("weekdays", "id")
            .OnDeleteOrUpdate(Rule.Cascade);
    }


    public override void Down()
    {
        throw new NotImplementedException();
    }
}