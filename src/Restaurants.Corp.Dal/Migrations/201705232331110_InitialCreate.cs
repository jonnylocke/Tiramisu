namespace Restaurants.Corp.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MenuItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OrderItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Item_Id = c.Int(),
                        OrderId_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MenuItems", t => t.Item_Id)
                .ForeignKey("dbo.Orders", t => t.OrderId_Id)
                .Index(t => t.Item_Id)
                .Index(t => t.OrderId_Id);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Created = c.DateTime(nullable: false),
                        RestaurantId_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Restaurants", t => t.RestaurantId_Id)
                .Index(t => t.RestaurantId_Id);
            
            CreateTable(
                "dbo.Restaurants",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "RestaurantId_Id", "dbo.Restaurants");
            DropForeignKey("dbo.OrderItems", "OrderId_Id", "dbo.Orders");
            DropForeignKey("dbo.OrderItems", "Item_Id", "dbo.MenuItems");
            DropIndex("dbo.Orders", new[] { "RestaurantId_Id" });
            DropIndex("dbo.OrderItems", new[] { "OrderId_Id" });
            DropIndex("dbo.OrderItems", new[] { "Item_Id" });
            DropTable("dbo.Restaurants");
            DropTable("dbo.Orders");
            DropTable("dbo.OrderItems");
            DropTable("dbo.MenuItems");
        }
    }
}
