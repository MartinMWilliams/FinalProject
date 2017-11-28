namespace FinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class yay : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Durations", "FlightTime", c => c.Time(nullable: false, precision: 7));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Durations", "FlightTime", c => c.DateTime(nullable: false));
        }
    }
}
