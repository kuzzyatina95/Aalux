namespace AaluxWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migrate3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Directions", "AddressOrigin", c => c.String());
            AddColumn("dbo.Directions", "LatOrigin", c => c.String());
            AddColumn("dbo.Directions", "LngOrigin", c => c.String());
            AddColumn("dbo.Directions", "AddressDestination", c => c.String());
            AddColumn("dbo.Directions", "LatDestination", c => c.String());
            AddColumn("dbo.Directions", "LngDestination", c => c.String());
            DropColumn("dbo.Directions", "Adress");
            DropColumn("dbo.Directions", "Lat");
            DropColumn("dbo.Directions", "Lot");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Directions", "Lot", c => c.String());
            AddColumn("dbo.Directions", "Lat", c => c.String());
            AddColumn("dbo.Directions", "Adress", c => c.String());
            DropColumn("dbo.Directions", "LngDestination");
            DropColumn("dbo.Directions", "LatDestination");
            DropColumn("dbo.Directions", "AddressDestination");
            DropColumn("dbo.Directions", "LngOrigin");
            DropColumn("dbo.Directions", "LatOrigin");
            DropColumn("dbo.Directions", "AddressOrigin");
        }
    }
}
