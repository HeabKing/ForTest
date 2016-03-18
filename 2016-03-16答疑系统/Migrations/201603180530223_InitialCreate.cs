namespace _2016_03_16答疑系统.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Z_Roles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.Z_UserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                        IdentityUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.Z_Roles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.Z_Users", t => t.IdentityUser_Id)
                .Index(t => t.RoleId)
                .Index(t => t.IdentityUser_Id);
            
            CreateTable(
                "dbo.Z_Users",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.Z_UserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        IdentityUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Z_Users", t => t.IdentityUser_Id)
                .Index(t => t.IdentityUser_Id);
            
            CreateTable(
                "dbo.Z_UserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                        IdentityUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.Z_Users", t => t.IdentityUser_Id)
                .Index(t => t.IdentityUser_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Z_UserRoles", "IdentityUser_Id", "dbo.Z_Users");
            DropForeignKey("dbo.Z_UserLogins", "IdentityUser_Id", "dbo.Z_Users");
            DropForeignKey("dbo.Z_UserClaims", "IdentityUser_Id", "dbo.Z_Users");
            DropForeignKey("dbo.Z_UserRoles", "RoleId", "dbo.Z_Roles");
            DropIndex("dbo.Z_UserLogins", new[] { "IdentityUser_Id" });
            DropIndex("dbo.Z_UserClaims", new[] { "IdentityUser_Id" });
            DropIndex("dbo.Z_Users", "UserNameIndex");
            DropIndex("dbo.Z_UserRoles", new[] { "IdentityUser_Id" });
            DropIndex("dbo.Z_UserRoles", new[] { "RoleId" });
            DropIndex("dbo.Z_Roles", "RoleNameIndex");
            DropTable("dbo.Z_UserLogins");
            DropTable("dbo.Z_UserClaims");
            DropTable("dbo.Z_Users");
            DropTable("dbo.Z_UserRoles");
            DropTable("dbo.Z_Roles");
        }
    }
}
