namespace AaluxWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migrate9 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Orders", "TimeBegin", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Orders", "TimeEnd", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orders", "TimeEnd", c => c.Time(precision: 7));
            AlterColumn("dbo.Orders", "TimeBegin", c => c.Time(nullable: false, precision: 7));
        }
    }
}
