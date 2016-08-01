namespace Transcode.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedPaymentTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "StorageOccupied", c => c.Long(nullable: false));
            AddColumn("dbo.AspNetUsers", "CreatedOn", c => c.DateTime(nullable: false));
            AddColumn("dbo.AspNetUsers", "LastLogin", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "LastLogin");
            DropColumn("dbo.AspNetUsers", "CreatedOn");
            DropColumn("dbo.AspNetUsers", "StorageOccupied");
        }
    }
}
