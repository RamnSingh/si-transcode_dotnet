namespace Transcode.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removedStorageOccupiedFromDb : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "StorageOccupied");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "StorageOccupied", c => c.Long(nullable: false));
        }
    }
}
