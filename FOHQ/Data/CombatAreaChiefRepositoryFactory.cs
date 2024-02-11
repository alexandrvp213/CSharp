using System;
using System.Linq;
using System.Threading.Tasks;

using SQLite;

using FOHQ.Models;

namespace FOHQ.Data
{
    /// <summary>
    /// Фабрика для репозитория "Начальник БУ (СПР)".
    /// </summary>
    public class CombatAreaChiefRepositoryFactory
    {
        private static readonly string[] _defaultItems =
        {
            "Начальник службы пожаротушения",
            "Начальник ДС СПТ",
            "Старший помощник ДС СПТ",
            "Начальник 53 ПСЧ",
            "Начальник 34 ПСЧ"
        };

        public DbDirectoryObjectRepository<CombatAreaChief> Create(SQLiteAsyncConnection connection)
        {
            if (connection == null)
                throw new ArgumentNullException(nameof(connection));

            var repository = new DbDirectoryObjectRepository<CombatAreaChief>(connection);

            InitAsync(repository);

            return repository;
        }

        private static async Task InitAsync(DbDirectoryObjectRepository<CombatAreaChief> repository)
        {
            if (repository == null)
                throw new ArgumentNullException(nameof(repository));

            var items = await repository.GetItemsAsync();
            if (!items.Any())
            {
                foreach (var defaultItem in _defaultItems)
                {
                    var item = new CombatAreaChief()
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