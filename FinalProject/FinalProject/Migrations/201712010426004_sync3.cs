namespace FinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sync3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "State", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "State");
        }
    }
}
