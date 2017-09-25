namespace Restaurants.Corp.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class amendedmodel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.OrderItems", "Item_Id", "dbo.MenuItems");
            DropForeignKey("dbo.Orders", "RestaurantId_Id", "dbo.Restaurants");
            DropIndex("dbo.OrderItems", new[] { "Item_Id" });
            DropIndex("dbo.Orders", new[] { "RestaurantId_Id" });
            AddColumn("dbo.OrderItems", "MenuItem", c => c.Int(nullable: false));
            AddColumn("dbo.Orders", "RestaurantId", c => c.Int(nullable: false));
            DropColumn("dbo.OrderItems", "Item_Id");
            DropColumn("dbo.Orders", "RestaurantId_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "RestaurantId_Id", c => c.Int());
            AddColumn("dbo.OrderItems", "Item_Id", c => c.Int());
            DropColumn("dbo.Orders", "RestaurantId");
            DropColumn("dbo.OrderItems", "MenuItem");
            CreateIndex("dbo.Orders", "RestaurantId_Id");
            CreateIndex("dbo.OrderItems", "Item_Id");
            AddForeignKey("dbo.Orders", "RestaurantId_Id", "dbo.Restaurants", "Id");
            AddForeignKey("dbo.OrderItems", "Item_Id", "dbo.MenuItems", "Id");
        }
    }
}
