namespace FinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class asdf : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Flights", "HasDeparted", c => c.Boolean(nullable: false));
        }

        public override void Down()
        {
            DropColumn("dbo.Flights", "HasDeparted");
        }
    }
}
