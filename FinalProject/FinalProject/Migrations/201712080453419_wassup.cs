namespace FinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class wassup : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Flights", "HasDeparted");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Flights", "HasDeparted", c => c.Boolean(nullable: false));
        }
    }
}
