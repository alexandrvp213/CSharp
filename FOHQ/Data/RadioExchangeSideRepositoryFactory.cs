using System;
using System.Linq;
using System.Threading.Tasks;

using SQLite;

using FOHQ.Models;

namespace FOHQ.Data
{
    /// <summary>
    /// Фабрика для репозитория "Участник радиообмена".
    /// </summary>
    public class RadioExchangeSideRepositoryFactory
    {
        private static readonly string[] _defaultItems =
        {
            "РТП",
            "РТП-1",
            "РТП-2",
            "РТП-3",
            "Диспетчер гарнизона",
            "НШ",
            "ПНШ",
            "НТ",
            "Связной"
        };

        public DbDirectoryObjectRepository<RadioExchangeSide> Create(SQLiteAsyncConnection connection)
        {
            if (connection == null)
                throw new ArgumentNullException(nameof(connection));

            var repository = new DbDirectoryObjectRepository<RadioExchangeSide>(connection);

            InitAsync(repository);

            return repository;
        }

        private static async Task InitAsync(DbDirectoryObjectRepository<RadioExchangeSide> repository)
        {
            if (repository == null)
                throw new ArgumentNullException(nameof(repository));

            var items = await repository.GetItemsAsync();
            if (!items.Any())
            {
                foreach (var defaultItem in _defaultItems)
                {
                    var item = new RadioExchangeSide()
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