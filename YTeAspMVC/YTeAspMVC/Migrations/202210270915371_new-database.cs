namespace YTeAspMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newdatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bookings",
                c => new
                    {
                        IdBooking = c.Int(nullable: false, identity: true),
                        Day = c.String(maxLength: 255),
                        Time = c.String(maxLength: 255),
                        Reason = c.String(maxLength: 255),
                        Status = c.Int(nullable: false),
                        IdUser = c.Int(nullable: false),
                        IdDoctor = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdBooking)
                .ForeignKey("dbo.Doctors", t => t.IdDoctor)
                .ForeignKey("dbo.Users", t => t.IdUser)
                .Index(t => t.IdUser)
                .Index(t => t.IdDoctor);
            
            CreateTable(
                "dbo.Doctors",
                c => new
                    {
                        IdDoctor = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false, maxLength: 255),
                        FullName = c.String(nullable: false, maxLength: 255),
                        Password = c.String(nullable: false, maxLength: 255),
                        Specialist = c.String(maxLength: 255),
                        Image = c.String(maxLength: 255),
                        Describe = c.String(),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdDoctor);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        IdUser = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false, maxLength: 255),
                        FullName = c.String(nullable: false, maxLength: 255),
                        Password = c.String(nullable: false, maxLength: 255),
                        PhoneNumber = c.String(maxLength: 255),
                        Address = c.String(maxLength: 255),
                        Gender = c.String(),
                        Status = c.Int(nullable: false),
                        IdRole = c.Int(nullable: false),
                        Created = c.String(),
                    })
                .PrimaryKey(t => t.IdUser)
                .ForeignKey("dbo.Roles", t => t.IdRole)
                .Index(t => t.IdRole);
            
            CreateTable(
                "dbo.Numbers",
                c => new
                    {
                        IdNumber = c.Int(nullable: false, identity: true),
                        Image = c.String(nullable: false, maxLength: 255),
                        NumberInt = c.Int(nullable: false),
                        Day = c.String(),
                        IdUser = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdNumber)
                .ForeignKey("dbo.Users", t => t.IdUser)
                .Index(t => t.IdUser);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        IdRole = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.IdRole);
            
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        IdPost = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 255),
                        Image = c.String(nullable: false, maxLength: 255),
                        Description = c.String(),
                        Created = c.String(),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdPost);
            
            CreateTable(
                "dbo.Services",
                c => new
                    {
                        IdService = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        Image = c.String(maxLength: 255),
                        Content = c.String(),
                        Price = c.Int(nullable: false),
                        Created = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdService);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "IdRole", "dbo.Roles");
            DropForeignKey("dbo.Numbers", "IdUser", "dbo.Users");
            DropForeignKey("dbo.Bookings", "IdUser", "dbo.Users");
            DropForeignKey("dbo.Bookings", "IdDoctor", "dbo.Doctors");
            DropIndex("dbo.Numbers", new[] { "IdUser" });
            DropIndex("dbo.Users", new[] { "IdRole" });
            DropIndex("dbo.Bookings", new[] { "IdDoctor" });
            DropIndex("dbo.Bookings", new[] { "IdUser" });
            DropTable("dbo.Services");
            DropTable("dbo.Posts");
            DropTable("dbo.Roles");
            DropTable("dbo.Numbers");
            DropTable("dbo.Users");
            DropTable("dbo.Doctors");
            DropTable("dbo.Bookings");
        }
    }
}
