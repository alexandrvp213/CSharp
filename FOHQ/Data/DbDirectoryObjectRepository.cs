using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using SQLite;

using FOHQ.Models;

namespace FOHQ.Data
{
    /// <summary>
    /// Репозиторий объектов "Объект справочника базы данных".
    /// </summary>
    public class DbDirectoryObjectRepository<T> where T : DbDirectoryObject, new()
    {
        private readonly SQLiteAsyncConnection _connection;

        public string StatusMessage { get; set; }

        public DbDirectoryObjectRepository(SQLiteAsyncConnection connection)
        {
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));

#if DEBUG
            // для пересоздания таблицы
            //_connection.DropTableAsync<T>().Wait();
#endif

            _connection.CreateTableAsync<T>().Wait();
        }

        public async Task<T> GetItemAsync(int id)
        {
            try
            {
                var item = await _connection.GetAsync<T>(id);

                return item;
            }
            catch (Exception ex)
            {
                StatusMessage = $"Failed to retrieve data. {ex.Message}";
            }

            return new T();
        }

        public async Task<List<T>> GetItemsAsync()
        {
            try
            {
                var item = await _connection.Table<T>().ToListAsync();

                return item;
            }
            catch (Exception ex)
            {
                StatusMessage = $"Failed to retrieve data. {ex.Message}";
            }

            return new List<T>();
        }

        public async Task<int> DeleteItemAsync(T item)
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

        public async Task<int> SaveItemAsync(T item)
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