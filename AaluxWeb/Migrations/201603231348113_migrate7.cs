namespace AaluxWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migrate7 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Orders", "DateEnd", c => c.DateTime());
            AlterColumn("dbo.Orders", "TimeEnd", c => c.Time(precision: 7));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orders", "TimeEnd", c => c.Time(nullable: false, precision: 7));
            AlterColumn("dbo.Orders", "DateEnd", c => c.DateTime(nullable: false));
        }
    }
}
