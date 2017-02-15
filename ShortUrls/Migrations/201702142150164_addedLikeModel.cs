namespace ShortUrls.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedLikeModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Likes",
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
            DropForeignKey("dbo.Likes", "TargetId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Likes", "RequestorId", "dbo.AspNetUsers");
            DropIndex("dbo.Likes", new[] { "TargetId" });
            DropIndex("dbo.Likes", new[] { "RequestorId" });
            DropTable("dbo.Likes");
        }
    }
}
