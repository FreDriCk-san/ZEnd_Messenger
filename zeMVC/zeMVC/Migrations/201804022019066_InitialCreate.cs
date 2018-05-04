namespace zeMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Chats",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Creator = c.Int(nullable: false),
                        Name = c.String(nullable: false),
                        Type = c.String(nullable: false),
                        Avatar = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Creator)
                .Index(t => t.Creator);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Login = c.String(maxLength: 15),
                        Password = c.String(nullable: false),
                        Email = c.String(maxLength: 40),
                        Avatar = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DeletedMessages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        MessageId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Messages", t => t.MessageId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => new { t.UserId, t.MessageId }, unique: true, name: "User_Message");
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(nullable: false),
                        SentDate = c.DateTime(nullable: false),
                        IsRead = c.Boolean(nullable: false),
                        ChatId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Chats", t => t.ChatId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.ChatId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UsersInChats",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ChatId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Chats", t => t.ChatId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => new { t.ChatId, t.UserId }, unique: true, name: "User_In_Chat");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UsersInChats", "UserId", "dbo.Users");
            DropForeignKey("dbo.UsersInChats", "ChatId", "dbo.Chats");
            DropForeignKey("dbo.DeletedMessages", "UserId", "dbo.Users");
            DropForeignKey("dbo.DeletedMessages", "MessageId", "dbo.Messages");
            DropForeignKey("dbo.Messages", "UserId", "dbo.Users");
            DropForeignKey("dbo.Messages", "ChatId", "dbo.Chats");
            DropForeignKey("dbo.Chats", "Creator", "dbo.Users");
            DropIndex("dbo.UsersInChats", "User_In_Chat");
            DropIndex("dbo.Messages", new[] { "UserId" });
            DropIndex("dbo.Messages", new[] { "ChatId" });
            DropIndex("dbo.DeletedMessages", "User_Message");
            DropIndex("dbo.Chats", new[] { "Creator" });
            DropTable("dbo.UsersInChats");
            DropTable("dbo.Messages");
            DropTable("dbo.DeletedMessages");
            DropTable("dbo.Users");
            DropTable("dbo.Chats");
        }
    }
}
