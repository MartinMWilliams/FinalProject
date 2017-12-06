namespace FinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class reserve : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "Reservation_ReservationID", "dbo.Reservations");
            DropIndex("dbo.AspNetUsers", new[] { "Reservation_ReservationID" });
            CreateTable(
                "dbo.AppUserReservations",
                c => new
                    {
                        AppUser_Id = c.String(nullable: false, maxLength: 128),
                        Reservation_ReservationID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.AppUser_Id, t.Reservation_ReservationID })
                .ForeignKey("dbo.AspNetUsers", t => t.AppUser_Id, cascadeDelete: true)
                .ForeignKey("dbo.Reservations", t => t.Reservation_ReservationID, cascadeDelete: true)
                .Index(t => t.AppUser_Id)
                .Index(t => t.Reservation_ReservationID);
            
            DropColumn("dbo.AspNetUsers", "Reservation_ReservationID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Reservation_ReservationID", c => c.Int());
            DropForeignKey("dbo.AppUserReservations", "Reservation_ReservationID", "dbo.Reservations");
            DropForeignKey("dbo.AppUserReservations", "AppUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.AppUserReservations", new[] { "Reservation_ReservationID" });
            DropIndex("dbo.AppUserReservations", new[] { "AppUser_Id" });
            DropTable("dbo.AppUserReservations");
            CreateIndex("dbo.AspNetUsers", "Reservation_ReservationID");
            AddForeignKey("dbo.AspNetUsers", "Reservation_ReservationID", "dbo.Reservations", "ReservationID");
        }
    }
}
