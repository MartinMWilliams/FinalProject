namespace FinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        CityID = c.Int(nullable: false, identity: true),
                        CityNumber = c.Int(nullable: false),
                        CityName = c.String(),
                        AirportCode = c.String(),
                    })
                .PrimaryKey(t => t.CityID);
            
            CreateTable(
                "dbo.Durations",
                c => new
                    {
                        DurationID = c.Int(nullable: false, identity: true),
                        Mileage = c.Int(nullable: false),
                        FlightTime = c.DateTime(nullable: false),
                        City1_CityID = c.Int(),
                        City2_CityID = c.Int(),
                        City_CityID = c.Int(),
                    })
                .PrimaryKey(t => t.DurationID)
                .ForeignKey("dbo.Cities", t => t.City1_CityID)
                .ForeignKey("dbo.Cities", t => t.City2_CityID)
                .ForeignKey("dbo.Cities", t => t.City_CityID)
                .Index(t => t.City1_CityID)
                .Index(t => t.City2_CityID)
                .Index(t => t.City_CityID);
            
            CreateTable(
                "dbo.Flights",
                c => new
                    {
                        FlightID = c.Int(nullable: false, identity: true),
                        FlightNumber = c.Int(nullable: false),
                        DepartureCity = c.String(),
                        ArrivalCity = c.String(),
                        Date = c.DateTime(nullable: false),
                        BaseFare = c.Int(nullable: false),
                        DepartureTime = c.DateTime(nullable: false),
                        ArrivalTime = c.DateTime(nullable: false),
                        DurationInfo_DurationID = c.Int(),
                    })
                .PrimaryKey(t => t.FlightID)
                .ForeignKey("dbo.Durations", t => t.DurationInfo_DurationID)
                .Index(t => t.DurationInfo_DurationID);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        AdvantageNumber = c.Int(nullable: false),
                        FirstName = c.String(nullable: false),
                        MiddleInitial = c.String(),
                        LastName = c.String(nullable: false),
                        DateofBirth = c.DateTime(nullable: false),
                        Address = c.String(),
                        City = c.String(),
                        State = c.Int(nullable: false),
                        Zip = c.String(),
                        Miles = c.Int(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Flights", "DurationInfo_DurationID", "dbo.Durations");
            DropForeignKey("dbo.Durations", "City_CityID", "dbo.Cities");
            DropForeignKey("dbo.Durations", "City2_CityID", "dbo.Cities");
            DropForeignKey("dbo.Durations", "City1_CityID", "dbo.Cities");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Flights", new[] { "DurationInfo_DurationID" });
            DropIndex("dbo.Durations", new[] { "City_CityID" });
            DropIndex("dbo.Durations", new[] { "City2_CityID" });
            DropIndex("dbo.Durations", new[] { "City1_CityID" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Flights");
            DropTable("dbo.Durations");
            DropTable("dbo.Cities");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
        }
    }
}
