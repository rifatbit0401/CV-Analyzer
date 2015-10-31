namespace CVAnalyzer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedAuthenticationModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AuthTokens",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        TokenValue = c.String(),
                        LastAccessTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.AuthTokens");
        }
    }
}
