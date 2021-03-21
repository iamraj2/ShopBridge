using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using ShopBridge.Migrations;
using ShopBridge.Models;

namespace ShopBridge.Data
{
    public class InventoryContext : DbContext
    {
        public InventoryContext() : base("Inventory")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<InventoryContext, Configuration>());
        }
        public DbSet<Item> Inventory { get; set; }
    }
}