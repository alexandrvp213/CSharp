using System;
using System.Collections.Generic;
using SQLite;

namespace FOHQ.Models
{
    /// <summary>
    /// Требуемый расход на защиту и тушение
    /// </summary>
    [Table("RequiredConsumption")]
    public class RequiredConsumption : BaseModel
    {
        private FOHQ.Data.SectionByObjectsType _sectionByObject;

        /// <summary>
        /// Раздел по виду объекта
        /// </summary>
        public FOHQ.Data.SectionByObjectsType SectionByObject
        {
            get => _sectionByObject;
            set
            {
                if (_sectionByObject != value)
                {
                    _sectionByObject = value;
                    OnPropertyChanged(nameof(SectionByObject));
                }
            }
        }

        private string _sectionByDestination;

        /// <summary>
        /// Раздел по назначению
        /// </summary>
        public string SectionByDestination
        {
            get => _sectionByDestination;
            set
            {
                if (_sectionByDestination != value)
                {
                    _sectionByDestination = value;
                    Indicator = 0;
                    List<FOHQ.Data.SectionByDestinations> listItems;
                    if (SectionByObject == FOHQ.Data.SectionByObjectsType.Buildings)
                    {
                        listItems = App.DataAccess.RequiredConsumptionRepository.SectionDestinationsBuildings;
                    }
                    else if (SectionByObject == FOHQ.Data.SectionByObjectsType.Liquids)
                    {
                        listItems = App.DataAccess.RequiredConsumptionRepository.SectionDestinationsLiquids;
                    }
                    else if (SectionByObject == FOHQ.Data.SectionByObjectsType.Materials)
                    {
                        listItems = App.DataAccess.RequiredConsumptionRepository.SectionDestinationsMaterials;
                    }
                    else if (SectionByObject == FOHQ.Data.SectionByObjectsType.Transport)
                    {
                        listItems = App.DataAccess.RequiredConsumptionRepository.SectionDestinationsTransport;
                    }
                    else
                    {
                        listItems = null;
                    }
                    if (listItems != null)
                    {
                        foreach (var Item in listItems)
                        {
                            if (value == Item.Name)
                            {
                                Indicator = Item.Indicator;
                                break;
                            }
                        }
                    }
                    OnPropertyChanged(nameof(SectionByDestination));
                }
            }
        }

        private double _indicator;

        /// <summary>
        /// Показатель
        /// </summary>
        public double Indicator
        {
            get => _indicator;
            set
            {
                if (_indicator != value)
                {
                    _indicator = value;
                    OnPropertyChanged(nameof(Indicator));
                }
            }
        }

        private FOHQ.Data.ExtinguishingType _extinguishingType;

        /// <summary>
        /// Тушение (тип)
        /// </summary>
        public FOHQ.Data.ExtinguishingType ExtinguishingType
        {
            get => _extinguishingType;
            set
            {
                if (_extinguishingType != value)
                {
                    _extinguishingType = value;
                    OnPropertyChanged(nameof(ExtinguishingType));
                }
            }
        }

        private double _consumptionExtinguishing;

        /// <summary>
        /// Требуемый расход на тушение
        /// </summary>
        public double ConsumptionExtinguishing
        {
            get => _consumptionExtinguishing;
            set
            {
                if (_consumptionExtinguishing != value)
                {
                    _consumptionExtinguishing = value;
                    OnPropertyChanged(nameof(ConsumptionExtinguishing));
                }
            }
        }

        private string _consumptionExtinguishingFormula;

        /// <summary>
        /// Требуемый расход на тушение - формула
        /// </summary>
        public string ConsumptionExtinguishingFormula
        {
            get => _consumptionExtinguishingFormula;
            set
            {
                if (_consumptionExtinguishingFormula != value)
                {
                    _consumptionExtinguishingFormula = value;
                    OnPropertyChanged(nameof(ConsumptionExtinguishingFormula));
                }
            }
        }

        private double _consumptionDefense;

        /// <summary>
        /// Требуемый расход на защиту
        /// </summary>
        public double ConsumptionDefense
        {
            get => _consumptionDefense;
            set
            {
                if (_consumptionDefense != value)
                {
                    _consumptionDefense = value;
                    OnPropertyChanged(nameof(ConsumptionDefense));
                }
            }
        }

        private string _consumptionDefenseFormula;

        /// <summary>
        /// Требуемый расход на защиту - формула
        /// </summary>
        public string ConsumptionDefenseFormula
        {
            get => _consumptionDefenseFormula;
            set
            {
                if (_consumptionDefenseFormula != value)
                {
                    _consumptionDefenseFormula = value;
                    OnPropertyChanged(nameof(ConsumptionDefenseFormula));
                }
            }
        }

        /// <summary>
        /// Расчет расхода на тушение
        /// </summary>
        private double GetConsumptionExtinguishing()
        {
            double extinguishingArea;
            if (ExtinguishingType == Data.ExtinguishingType.ExtinguishingFront)
            {
                extinguishingArea = Document.Quenching.ExtinguishingAreaFront;
            }
            else if (ExtinguishingType == Data.ExtinguishingType.ExtinguishingPerimeter)
            {
                extinguishingArea = Document.Quenching.ExtinguishingAreaPerimeter;               
            }
            else
            {
                extinguishingArea = 0;          
            }
            ConsumptionExtinguishingFormula = Indicator + " * " + extinguishingArea;
            return Indicator * extinguishingArea;
        }

        /// <summary>
        /// Расчет расхода на защиту
        /// </summary>
        private double GetConsumptionDefense()
        {
            ConsumptionDefenseFormula = "0.25 * " + Indicator + " * " + Document.Quenching.FireArea;
            return 0.25 * Indicator * Document.Quenching.FireArea;
        }

        public override void UpdateParams()
        {
            //Расчетные данные
            ConsumptionExtinguishing = Math.Round(GetConsumptionExtinguishing(), 2);
            ConsumptionDefense = Math.Round(GetConsumptionDefense(), 2);
        }

        protected override void Init()
        {
            SectionByObject = Data.SectionByObjectsType.None;
            ExtinguishingType = Data.ExtinguishingType.None;
        }
        public RequiredConsumption() : base()
        {

        }

        public RequiredConsumption(Document document) : base(document)
        {

        }
    }
}
