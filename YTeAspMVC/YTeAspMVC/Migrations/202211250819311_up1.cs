namespace YTeAspMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class up1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Advises",
                c => new
                    {
                        IdAdvise = c.Int(nullable: false, identity: true),
                        FullName = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Question = c.String(),
                        Created = c.String(),
                    })
                .PrimaryKey(t => t.IdAdvise);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Advises");
        }
    }
}
