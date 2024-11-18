namespace YTeAspMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatetablenumber : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Numbers", "Image");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Numbers", "Image", c => c.String(nullable: false, maxLength: 255));
        }
    }
}
