using CassaAssistenzaSanitaria.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CassaAssistenzaSanitaria.Repositories
{
    public interface IHealtCareItemRepository
    {
        event EventHandler<HealtCareItem> OnItemAdded;
        event EventHandler<HealtCareItem> OnItemUpdated;
        event EventHandler<HealtCareItem> OnItemDeleted;

        Task<List<HealtCareItem>> GetItems();
        Task AddItem(HealtCareItem item);
        Task UpdateItem(HealtCareItem item);
        Task AddOrUpdate(HealtCareItem item);
        Task RemoveItem(HealtCareItem item);
    }
}
