using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using SQLite;

using FOHQ.Models;

namespace FOHQ.Data
{
    /// <summary>
    /// Репозиторий "Силы и средства".
    /// </summary>
    public class ForcesAndMeansRepository
    {
        private readonly SQLiteAsyncConnection _connection;

        public string StatusMessage { get; set; }

        public ForcesAndMeansRepository(SQLiteAsyncConnection connection)
        {
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));

            _connection.CreateTableAsync<ForcesAndMeans>().Wait();
        }

        public async Task<ForcesAndMeans> GetItemAsync(int id)
        {
            try
            {
                var item = await _connection.GetAsync<ForcesAndMeans>(id);

                return item;
            }
            catch (Exception ex)
            {
                StatusMessage = $"Failed to retrieve data. {ex.Message}";
            }

            return new ForcesAndMeans();
        }

        public async Task<List<ForcesAndMeans>> GetItemsAsync()
        {
            try
            {
                var item = await _connection.Table<ForcesAndMeans>().ToListAsync();

                return item;
            }
            catch (Exception ex)
            {
                StatusMessage = $"Failed to retrieve data. {ex.Message}";
            }

            return new List<ForcesAndMeans>();
        }

        public async Task<int> DeleteItemAsync(ForcesAndMeans item)
        {
            try
            {
                return await _connection.DeleteAsync(item);
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format($"Failed to delete item. Error: {ex.Message}");
            }

            return 0;
        }

        public async Task<int> SaveItemAsync(ForcesAndMeans item)
        {
            try
            {
                if (item.Id != 0)
                {
                    await _connection.UpdateAsync(item);
                    return item.Id;
                }
                else
                {
                    await _connection.InsertAsync(item);
                    return item.Id;
                }
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format($"Failed to save item. Error: {ex.Message}");
            }

            return 0;
        }
    }
}