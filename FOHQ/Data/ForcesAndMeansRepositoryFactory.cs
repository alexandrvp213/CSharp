using System;

using SQLite;

namespace FOHQ.Data
{
    /// <summary>
    /// Фабрика для репозитория "Силы и средства".
    /// </summary>
    public class ForcesAndMeansRepositoryFactory
    {
        public ForcesAndMeansRepository Create(SQLiteAsyncConnection connection)
        {
            if (connection == null)
                throw new ArgumentNullException(nameof(connection));

            var repository = new ForcesAndMeansRepository(connection);

            return repository;
        }
    }
}