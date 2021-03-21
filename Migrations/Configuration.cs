namespace ShopBridge.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using ShopBridge.Models;
    internal sealed class Configuration : DbMigrationsConfiguration<ShopBridge.Data.InventoryContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "ShopBridge.Data.InventoryContext";
        }

        protected override void Seed(ShopBridge.Data.InventoryContext ctx)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            if (!ctx.Inventory.Any())
            {
                ctx.Inventory.AddOrUpdate(x => x.ItemId,
                  new Item()
                  {
                      ItemId = 1,
                      Name = "Book",
                      Description = "this is book description.",
                      ExpiryDate = new DateTime(2025, 10, 18),
                      Price = 100
                  });
                ctx.Inventory.AddOrUpdate(x => x.ItemId,
                  new Item()
                  {
                      ItemId = 2,
                      Name = "Pencil",
                      Description = "this is Pencil description.",
                      ExpiryDate = new DateTime(2023, 05, 18),
                      Price = 60
                  });
            }
        }
    }
}
