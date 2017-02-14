namespace ShortUrls.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedFriendModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Friends",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RequestorId = c.String(maxLength: 128),
                        TargetId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.RequestorId)
                .ForeignKey("dbo.AspNetUsers", t => t.TargetId)
                .Index(t => t.RequestorId)
                .Index(t => t.TargetId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Friends", "TargetId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Friends", "RequestorId", "dbo.AspNetUsers");
            DropIndex("dbo.Friends", new[] { "TargetId" });
            DropIndex("dbo.Friends", new[] { "RequestorId" });
            DropTable("dbo.Friends");
        }
    }
}
