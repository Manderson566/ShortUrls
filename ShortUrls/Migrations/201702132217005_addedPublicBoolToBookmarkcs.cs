namespace ShortUrls.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedPublicBoolToBookmarkcs : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bookmarks", "Public", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Bookmarks", "Public");
        }
    }
}
