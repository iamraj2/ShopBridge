using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using ShopBridge.Models;

namespace ShopBridge.Data
{
    public class InventoryRepository : IInventoryRepository
    {
        private InventoryContext _context = new InventoryContext();
        public async Task<bool> SaveChangesAsync()
        {
            try
            {
                // Only return success if at least one row was changed

                return (await _context.SaveChangesAsync()) > 0;
            }
            catch(Exception)
            {
                throw;
            }
        }

        public async Task<Item[]> GetAllItemsAsync()
        {
            IQueryable<Item> query = _context.Inventory.OrderByDescending(c => c.ItemId);

            return await query.ToArrayAsync();
        }

        public async Task<Item> GetItemAsync(int itemId)
        {
            IQueryable<Item> query = _context.Inventory.Where(c => c.ItemId == itemId);

            return await query.FirstOrDefaultAsync();
        }

        public void AddItem(Item camp)
        {
            _context.Inventory.Add(camp);
        }

        public void DeleteItem(Item camp)
        {
            _context.Inventory.Remove(camp);
        }

    }
}