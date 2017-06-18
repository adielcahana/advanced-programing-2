namespace WebServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Ranks",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        JoinDate = c.String(nullable: false),
                        GamesWon = c.Int(nullable: false),
                        GamesLost = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Ranks");
        }
    }
}
