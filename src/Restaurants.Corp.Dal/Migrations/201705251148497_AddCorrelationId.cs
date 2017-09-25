namespace Restaurants.Corp.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCorrelationId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "CorrelationId", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "CorrelationId");
        }
    }
}
