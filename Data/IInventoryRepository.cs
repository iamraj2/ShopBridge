using ShopBridge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopBridge.Data
{
    public interface IInventoryRepository
    {
        Task<bool> SaveChangesAsync();
        Task<Item[]> GetAllItemsAsync();
        Task<Item> GetItemAsync(int itemId);
        void AddItem(Item camp);
        void DeleteItem(Item camp);

    }
}
