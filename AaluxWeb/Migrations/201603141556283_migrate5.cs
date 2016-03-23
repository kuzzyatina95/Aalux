namespace AaluxWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migrate5 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Licenses", "UserID", "dbo.Drivers");
            DropIndex("dbo.Licenses", new[] { "UserID" });
            AlterColumn("dbo.Licenses", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Licenses", "UserID", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Licenses", "UserID");
            AddForeignKey("dbo.Licenses", "UserID", "dbo.Drivers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Licenses", "UserID", "dbo.Drivers");
            DropIndex("dbo.Licenses", new[] { "UserID" });
            AlterColumn("dbo.Licenses", "UserID", c => c.String(maxLength: 128));
            AlterColumn("dbo.Licenses", "Name", c => c.String());
            CreateIndex("dbo.Licenses", "UserID");
            AddForeignKey("dbo.Licenses", "UserID", "dbo.Drivers", "Id");
        }
    }
}
