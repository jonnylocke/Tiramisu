namespace Restaurants.Corp.Dal.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Collections.Generic;
    using Restaurants.Corp.Domain;
    internal sealed class Configuration : DbMigrationsConfiguration<RestaurantCorpContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "Restaurants.Corp.Dal.RestaurantCorpContext";
        }

        protected override void Seed(RestaurantCorpContext context)
        {
            context.MenuItems.AddOrUpdate(
                new MenuItem {
                    Id = 1,
                    Name = "Bruschetta"
                },
                new MenuItem {
                    Id = 2,
                    Name = "Margherita"
                },
                new MenuItem
                {
                    Id = 3,
                    Name = "Tiramisu"
                }
            );

            context.Restaurants.AddOrUpdate(
                new Restaurant {
                    Id = 1,
                    Name = "RestaurantX"
                },
                new Restaurant
                {
                    Id = 2,
                    Name = "RestaurantY"
                }
            );

            context.Orders.AddOrUpdate(
                new Order {
                    CorrelationId = Guid.Parse("d6366e9d-c6af-4ca2-b0ea-e5e5d5737bb3"),
                    Created = DateTime.Now,
                    RestaurantId = 1,
                    Items = new List<OrderItem> {
                        new OrderItem {
                            MenuItem = 1,
                        },
                        new OrderItem {
                            MenuItem = 2,
                        },
                        new OrderItem {
                            MenuItem = 3,
                        },
                        new OrderItem {
                            MenuItem = 3,
                        }
                    }

                }
            );

        }
    }
}
