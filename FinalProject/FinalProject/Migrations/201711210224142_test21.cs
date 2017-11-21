namespace FinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test21 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Flights", "BaseFare", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Flights", "BaseFare", c => c.Int(nullable: false));
        }
    }
}
