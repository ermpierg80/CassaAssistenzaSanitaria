using CassaAssistenzaSanitaria.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace CassaAssistenzaSanitaria.Repositories
{
    public class HealtCareItemRepository : IHealtCareItemRepository
    {
        private SQLiteAsyncConnection connection;

        public event EventHandler<HealtCareItem> OnItemAdded;
        public event EventHandler<HealtCareItem> OnItemUpdated;
        public event EventHandler<HealtCareItem> OnItemDeleted;

        public async Task<List<HealtCareItem>> GetItems()
        {
            await CreateConnection();
            return await connection.Table<HealtCareItem>().OrderByDescending(i => i.DataRichiesta).ToListAsync();
        }

        public async Task AddItem(HealtCareItem item)
        {
            await CreateConnection();
            await connection.InsertAsync(item);
            OnItemAdded?.Invoke(this, item);
        }

        public async Task UpdateItem(HealtCareItem item)
        {
            await CreateConnection();
            await connection.UpdateAsync(item);
            OnItemUpdated?.Invoke(this, item);
        }

        public async Task AddOrUpdate(HealtCareItem item)
        {
            if (item.Id == 0)
            {
                await AddItem(item);
            }
            else
            {
                await UpdateItem(item);
            }
        }

        public async Task RemoveItem(HealtCareItem item)
        {
            await CreateConnection();
            await connection.DeleteAsync(item);
            OnItemDeleted?.Invoke(this, item);
        }

        private async Task CreateConnection()
        {
            if (connection != null)
            {
                return;
            }

            var documentPath = Environment.GetFolderPath(
                               Environment.SpecialFolder.MyDocuments);
            var databasePath = Path.Combine(documentPath, "HealtCareItems.db");

            connection = new SQLiteAsyncConnection(databasePath);
            await connection.CreateTableAsync<HealtCareItem>();
        }
    }
}
