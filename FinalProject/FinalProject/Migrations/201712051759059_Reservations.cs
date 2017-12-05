namespace FinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Reservations : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ReservationFlightDetails",
                c => new
                    {
                        ReservationFlightDetailID = c.Int(nullable: false, identity: true),
                        SeatAssignment = c.Int(nullable: false),
                        Fare = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Flight_FlightID = c.Int(),
                        Reservation_ReservationID = c.Int(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ReservationFlightDetailID)
                .ForeignKey("dbo.Flights", t => t.Flight_FlightID)
                .ForeignKey("dbo.Reservations", t => t.Reservation_ReservationID)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.Flight_FlightID)
                .Index(t => t.Reservation_ReservationID)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Reservations",
                c => new
                    {
                        ReservationID = c.Int(nullable: false, identity: true),
                        TotalFare = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ReservationID);
            
            AddColumn("dbo.AspNetUsers", "Reservation_ReservationID", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "Reservation_ReservationID");
            AddForeignKey("dbo.AspNetUsers", "Reservation_ReservationID", "dbo.Reservations", "ReservationID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ReservationFlightDetails", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "Reservation_ReservationID", "dbo.Reservations");
            DropForeignKey("dbo.ReservationFlightDetails", "Reservation_ReservationID", "dbo.Reservations");
            DropForeignKey("dbo.ReservationFlightDetails", "Flight_FlightID", "dbo.Flights");
            DropIndex("dbo.AspNetUsers", new[] { "Reservation_ReservationID" });
            DropIndex("dbo.ReservationFlightDetails", new[] { "User_Id" });
            DropIndex("dbo.ReservationFlightDetails", new[] { "Reservation_ReservationID" });
            DropIndex("dbo.ReservationFlightDetails", new[] { "Flight_FlightID" });
            DropColumn("dbo.AspNetUsers", "Reservation_ReservationID");
            DropTable("dbo.Reservations");
            DropTable("dbo.ReservationFlightDetails");
        }
    }
}
