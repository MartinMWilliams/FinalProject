namespace FinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class helpme : DbMigration
    {
        public override void Up()
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
                .PrimaryKey(t => t.FlightCrewID)
                .ForeignKey("dbo.Flights", t => t.Flight_FlightID)
                .Index(t => t.Flight_FlightID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FlightCrews", "Flight_FlightID", "dbo.Flights");
            DropIndex("dbo.FlightCrews", new[] { "Flight_FlightID" });
            DropTable("dbo.FlightCrews");
        }
    }
}
