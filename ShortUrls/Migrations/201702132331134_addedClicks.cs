namespace ShortUrls.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedClicks : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bookmarks", "Clicks", c => c.Int(nullable: false));
            DropColumn("dbo.Clicks", "ClickCount");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Clicks", "ClickCount", c => c.Int(nullable: false));
            DropColumn("dbo.Bookmarks", "Clicks");
        }
    }
}
