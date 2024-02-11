using System;
using System.Linq;
using System.Threading.Tasks;

using SQLite;

using FOHQ.Models;

namespace FOHQ.Data
{
    /// <summary>
    /// Фабрика для репозитория "Основная задача".
    /// </summary>
    public class MainTaskRepositoryFactory
    {
        private static readonly string[] _defaultItems =
        {
            "Спасение людей",
            "Спасение имущества",
            "Спасение участников БД",
            "Спасение животных",
            "Тушение пожара",
            "Предотвращение взрыва",
            "Предотвращение обрушения строительных конструкций",
            "Защита соседнего здания",
            "Защита смежного здания"
        };

        public DbDirectoryObjectRepository<MainTask> Create(SQLiteAsyncConnection connection)
        {
            if (connection == null)
                throw new ArgumentNullException(nameof(connection));

            var repository = new DbDirectoryObjectRepository<MainTask>(connection);

            InitAsync(repository);

            return repository;
        }

        private static async Task InitAsync(DbDirectoryObjectRepository<MainTask> repository)
        {
            if (repository == null)
                throw new ArgumentNullException(nameof(repository));

            var items = await repository.GetItemsAsync();
            if (!items.Any())
            {
                foreach (var defaultItem in _defaultItems)
                {
                    var item = new MainTask()
                    {
                        Name = defaultItem
                    };
                    items.Add(item);
                    await repository.SaveItemAsync(item);
                }
            }
        }
    }
}