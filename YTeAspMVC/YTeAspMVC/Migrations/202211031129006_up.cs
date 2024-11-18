namespace YTeAspMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class up : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Services", "Created", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Services", "Created", c => c.Int(nullable: false));
        }
    }
}
