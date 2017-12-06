namespace FinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sync5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reservations", "ReservationNumber", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Reservations", "ReservationNumber");
        }
    }
}
