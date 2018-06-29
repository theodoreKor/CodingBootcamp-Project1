namespace TheodoreKoronaios_P1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class theodore_final : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        MessageId = c.Int(nullable: false, identity: true),
                        DateCreated = c.DateTime(nullable: false),
                        Content = c.String(maxLength: 250),
                        Subject = c.String(),
                        IsMessageActive = c.Boolean(nullable: false),
                        Recipient_UserId = c.Int(),
                        Sender_UserId = c.Int(),
                    })
                .PrimaryKey(t => t.MessageId)
                .ForeignKey("dbo.Users", t => t.Recipient_UserId)
                .ForeignKey("dbo.Users", t => t.Sender_UserId)
                .Index(t => t.Recipient_UserId)
                .Index(t => t.Sender_UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        Password = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                        UserType = c.Int(nullable: false),
                        IsUserActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Messages", "Sender_UserId", "dbo.Users");
            DropForeignKey("dbo.Messages", "Recipient_UserId", "dbo.Users");
            DropIndex("dbo.Messages", new[] { "Sender_UserId" });
            DropIndex("dbo.Messages", new[] { "Recipient_UserId" });
            DropTable("dbo.Users");
            DropTable("dbo.Messages");
        }
    }
}
