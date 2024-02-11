using System;

using FOHQ.Models;

namespace FOHQ.Data
{
    /// <summary>
    /// Доступ к данным.
    /// </summary>
    public class DataAccess
    {
        public DocumentRepository DocumentRepository { get; }
        public DbDirectoryObjectRepository<CombatAreaChief> CombatAreaChiefRepository { get; }
        public DbDirectoryObjectRepository<MainTask> MainTaskRepository { get; }
        public DbDirectoryObjectRepository<RadioExchangeSide> RadioExchangeSideRepository { get; }
        public DbDirectoryObjectRepository<TransmittedInformation> TransmittedInformationRepository { get; }
        public ForcesAndMeansRepository ForcesAndMeansRepository { get; }
        public FilingMethodsRepository FilingMethodsRepository { get; }
        public NumberOfDevicesRepository NumberOfDevicesRepository { get; }
        public QuenchingRepository QuenchingRepository { get; }
        public QuenchingSolutionRepository QuenchingSolutionRepository { get; }
        public RequiredConsumptionRepository RequiredConsumptionRepository { get; }

        public DataAccess(
            DocumentRepository documentRepository,
            DbDirectoryObjectRepository<CombatAreaChief> combatAreasChiefRepository,
            DbDirectoryObjectRepository<MainTask> mainTaskRepository,
            DbDirectoryObjectRepository<RadioExchangeSide> radioExchangeSideRepository,
            DbDirectoryObjectRepository<TransmittedInformation> transmittedInformationRepository,
            ForcesAndMeansRepository forcesAndMeansRepository,
            FilingMethodsRepository filingMethodsRepository,
            NumberOfDevicesRepository numberOfDevicesRepository,
            QuenchingRepository quenchingRepository,
            QuenchingSolutionRepository quenchingSolutionRepository,
            RequiredConsumptionRepository requiredConsumptionRepository
            )
        {
            DocumentRepository =
                documentRepository ?? throw new ArgumentNullException(nameof(documentRepository));
            CombatAreaChiefRepository =
                combatAreasChiefRepository ?? throw new ArgumentNullException(nameof(combatAreasChiefRepository));
            MainTaskRepository =
                mainTaskRepository ?? throw new ArgumentNullException(nameof(mainTaskRepository));
            RadioExchangeSideRepository =
                radioExchangeSideRepository ?? throw new ArgumentNullException(nameof(radioExchangeSideRepository));
            TransmittedInformationRepository =
                transmittedInformationRepository ?? throw new ArgumentNullException(nameof(transmittedInformationRepository));
            ForcesAndMeansRepository =
                forcesAndMeansRepository ?? throw new ArgumentNullException(nameof(forcesAndMeansRepository));
            FilingMethodsRepository =
                filingMethodsRepository ?? throw new ArgumentNullException(nameof(filingMethodsRepository));
            NumberOfDevicesRepository =
                numberOfDevicesRepository ?? throw new ArgumentNullException(nameof(numberOfDevicesRepository));
            QuenchingRepository =
                quenchingRepository ?? throw new ArgumentNullException(nameof(quenchingRepository));
            QuenchingSolutionRepository =
                quenchingSolutionRepository ?? throw new ArgumentNullException(nameof(quenchingSolutionRepository));
            RequiredConsumptionRepository =
                requiredConsumptionRepository ?? throw new ArgumentNullException(nameof(requiredConsumptionRepository));
        }
    }
}