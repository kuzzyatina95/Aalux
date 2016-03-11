namespace AaluxWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migrate4 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Clients", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Clients", "Surname", c => c.String(nullable: false));
            AlterColumn("dbo.Clients", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.Clients", "Phone", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Clients", "Phone", c => c.String());
            AlterColumn("dbo.Clients", "Email", c => c.String());
            AlterColumn("dbo.Clients", "Surname", c => c.String());
            AlterColumn("dbo.Clients", "Name", c => c.String());
        }
    }
}
