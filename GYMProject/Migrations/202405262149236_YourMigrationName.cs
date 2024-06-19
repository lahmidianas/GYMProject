namespace GYMProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class YourMigrationName : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Admins",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Member",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        NOM = c.String(nullable: false, maxLength: 30),
                        PRENOM = c.String(nullable: false, maxLength: 30),
                        phone = c.String(nullable: false, maxLength: 10),
                        TYPE = c.String(nullable: false, maxLength: 30),
                        StartMembershipDate = c.DateTime(nullable: false),
                        EndMembershipDate = c.DateTime(),
                        Session_Id = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.SESSION", t => t.Session_Id)
                .Index(t => t.Session_Id);
            
            CreateTable(
                "dbo.SESSION",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        HDEBUT = c.Int(nullable: false),
                        HFIN = c.Int(nullable: false),
                        SessionType = c.String(nullable: false, maxLength: 30),
                        StaffId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Staff", t => t.StaffId, cascadeDelete: true)
                .Index(t => t.StaffId);
            
            CreateTable(
                "dbo.Staff",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        NOM = c.String(nullable: false, maxLength: 30),
                        PRENOM = c.String(nullable: false, maxLength: 30),
                        JOB = c.String(nullable: false, maxLength: 10),
                        phone = c.String(nullable: false, maxLength: 10),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SESSION", "StaffId", "dbo.Staff");
            DropForeignKey("dbo.Member", "Session_Id", "dbo.SESSION");
            DropIndex("dbo.SESSION", new[] { "StaffId" });
            DropIndex("dbo.Member", new[] { "Session_Id" });
            DropTable("dbo.Staff");
            DropTable("dbo.SESSION");
            DropTable("dbo.Member");
            DropTable("dbo.Admins");
        }
    }
}
