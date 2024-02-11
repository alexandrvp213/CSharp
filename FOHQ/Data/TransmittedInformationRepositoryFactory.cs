using System;
using System.Linq;
using System.Threading.Tasks;

using SQLite;

using FOHQ.Models;

namespace FOHQ.Data
{
    /// <summary>
    /// Фабрика для репозитория "Передаваемая информация".
    /// </summary>
    public class TransmittedInformationRepositoryFactory
    {
        private static readonly string[] _defaultItems =
        {
            "Организовать подвоз воды",
            "Подать ствол на тушение в составе звена ГДЗС"
        };

        public DbDirectoryObjectRepository<TransmittedInformation> Create(SQLiteAsyncConnection connection)
        {
            if (connection == null)
                throw new ArgumentNullException(nameof(connection));

            var repository = new DbDirectoryObjectRepository<TransmittedInformation>(connection);

            InitAsync(repository);

            return repository;
        }

        private static async Task InitAsync(DbDirectoryObjectRepository<TransmittedInformation> repository)
        {
            if (repository == null)
                throw new ArgumentNullException(nameof(repository));

            var items = await repository.GetItemsAsync();
            if (!items.Any())
            {
                foreach (var defaultItem in _defaultItems)
                {
                    var item = new TransmittedInformation()
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