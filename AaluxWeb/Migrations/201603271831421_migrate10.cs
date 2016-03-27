namespace AaluxWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migrate10 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Orders", "TimePost", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orders", "TimePost", c => c.Time(nullable: false, precision: 7));
        }
    }
}
