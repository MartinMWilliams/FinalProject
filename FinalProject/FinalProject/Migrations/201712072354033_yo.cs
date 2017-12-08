namespace FinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class yo : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.FlightCrews", "Flight_FlightID", "dbo.Flights");
            DropIndex("dbo.FlightCrews", new[] { "Flight_FlightID" });
            DropTable("dbo.FlightCrews");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.FlightCrews",
                c => new
                    {
                        FlightCrewID = c.Int(nullable: false, identity: true),
                        Pilot = c.String(),
                        CoPilot = c.String(),
                        CabinCrew = c.String(),
                        Flight_FlightID = c.Int(),
                    })
                .PrimaryKey(t => t.FlightCrewID);
            
            CreateIndex("dbo.FlightCrews", "Flight_FlightID");
            AddForeignKey("dbo.FlightCrews", "Flight_FlightID", "dbo.Flights", "FlightID");
        }
    }
}
