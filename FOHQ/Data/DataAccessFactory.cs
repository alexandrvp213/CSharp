using System;

using SQLite;

namespace FOHQ.Data
{
    /// <summary>
    /// Фабрика для создания "Доступ к данным.".
    /// </summary>
    public class DataAccessFactory
    {
        public DataAccess Create(string databasePath)
        {
            if (string.IsNullOrWhiteSpace(databasePath))
                throw new ArgumentNullException(nameof(databasePath));

            var connection = new SQLiteAsyncConnection(databasePath);

            var documentRepository = new DocumentRepository(connection);
            var combatAreasChiefRepository = new CombatAreaChiefRepositoryFactory().Create(connection);
            var mainTaskRepository = new MainTaskRepositoryFactory().Create(connection);
            var radioExchangeSideRepository = new RadioExchangeSideRepositoryFactory().Create(connection);
            var transmittedInformationRepository = new TransmittedInformationRepositoryFactory().Create(connection);
            var forcesAndMeansRepository = new ForcesAndMeansRepositoryFactory().Create(connection);
            var filingMethodsRepository = new FilingMethodsRepository();
            var numberOfDevicesRepository = new NumberOfDevicesRepository();
            var quenchingRepository = new QuenchingRepository();
            var quenchingSolutionRepository = new QuenchingSolutionRepository();
            var requiredConsumptionRepository = new RequiredConsumptionRepository();

            return new DataAccess(
                documentRepository,
                combatAreasChiefRepository,
                mainTaskRepository,
                radioExchangeSideRepository,
                transmittedInformationRepository,
                forcesAndMeansRepository,
                filingMethodsRepository,
                numberOfDevicesRepository,
                quenchingRepository,
                quenchingSolutionRepository,
                requiredConsumptionRepository);
        }
    }
}