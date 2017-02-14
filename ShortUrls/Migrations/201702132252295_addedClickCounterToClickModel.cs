namespace ShortUrls.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedClickCounterToClickModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Clicks", "ClickCount", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Clicks", "ClickCount");
        }
    }
}
