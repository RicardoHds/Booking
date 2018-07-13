namespace Booking.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Changemessagetype : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Books", "Message", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Books", "Message", c => c.String());
        }
    }
}
