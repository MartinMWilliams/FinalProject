namespace FinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sync2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Cities", "AirportCode", c => c.String(maxLength: 3));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Cities", "AirportCode", c => c.String());
        }
    }
}
