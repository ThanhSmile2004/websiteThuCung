namespace YTeAspMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatecolumnTime : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Bookings", "Time", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Bookings", "Time", c => c.String(maxLength: 255));
        }
    }
}
