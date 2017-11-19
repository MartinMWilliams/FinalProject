namespace FinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitSetup : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cities", "State", c => c.Int(nullable: false));
            DropColumn("dbo.AspNetUsers", "State");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "State", c => c.Int(nullable: false));
            DropColumn("dbo.Cities", "State");
        }
    }
}
