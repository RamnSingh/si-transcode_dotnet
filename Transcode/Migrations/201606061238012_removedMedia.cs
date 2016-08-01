namespace Transcode.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removedMedia : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Conversions", "ConvertedMediaId", "dbo.Media");
            DropForeignKey("dbo.Conversions", "ProvidedMediaId", "dbo.Media");
            DropIndex("dbo.Conversions", new[] { "ProvidedMediaId" });
            DropIndex("dbo.Conversions", new[] { "ConvertedMediaId" });
            AddColumn("dbo.Conversions", "ProvidedMedia", c => c.String());
            DropColumn("dbo.Conversions", "ProvidedMediaId");
            DropColumn("dbo.Conversions", "ConvertedMediaId");
            DropTable("dbo.Media");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Media",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.String(nullable: false),
                        Format = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Conversions", "ConvertedMediaId", c => c.Int(nullable: false));
            AddColumn("dbo.Conversions", "ProvidedMediaId", c => c.Int(nullable: false));
            DropColumn("dbo.Conversions", "ProvidedMedia");
            CreateIndex("dbo.Conversions", "ConvertedMediaId");
            CreateIndex("dbo.Conversions", "ProvidedMediaId");
            AddForeignKey("dbo.Conversions", "ProvidedMediaId", "dbo.Media", "Id");
            AddForeignKey("dbo.Conversions", "ConvertedMediaId", "dbo.Media", "Id");
        }
    }
}
