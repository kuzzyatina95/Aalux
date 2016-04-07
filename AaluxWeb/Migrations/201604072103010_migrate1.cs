namespace AaluxWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migrate1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Drivers", "CarId", "dbo.Cars");
            DropIndex("dbo.Drivers", new[] { "CarId" });
            AlterColumn("dbo.Drivers", "CarId", c => c.Int());
            CreateIndex("dbo.Drivers", "CarId");
            AddForeignKey("dbo.Drivers", "CarId", "dbo.Cars", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Drivers", "CarId", "dbo.Cars");
            DropIndex("dbo.Drivers", new[] { "CarId" });
            AlterColumn("dbo.Drivers", "CarId", c => c.Int(nullable: false));
            CreateIndex("dbo.Drivers", "CarId");
            AddForeignKey("dbo.Drivers", "CarId", "dbo.Cars", "Id", cascadeDelete: true);
        }
    }
}
