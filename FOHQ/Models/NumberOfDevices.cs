using System;
using SQLite;

namespace FOHQ.Models
{
    /// <summary>
    /// Количество приборов
    /// </summary>
    [Table("NumberOfDevices")]

    public class NumberOfDevices : BaseModel
    {
        private FOHQ.Data.ExtinguishingMethodType _method;

        /// <summary>
        /// Способы тушения
        /// </summary>
        public FOHQ.Data.ExtinguishingMethodType Method
        {
            get => _method;
            set
            {
                if (_method != value)
                {
                    _method = value;
                    OnPropertyChanged(nameof(Method));
                }
            }
        }

        private double _volume;

        /// <summary>
        /// Объем помещения
        /// </summary>
        public double Volume
        {
            get => _volume;
            set
            {
                if (_volume != value)
                {
                    _volume = value;
                    OnPropertyChanged(nameof(Volume));
                }
            }
        }

        private string _waterBarrels;

        /// <summary>
        /// Водяные стволы
        /// </summary>
        public string WaterBarrels
        {
            get => _waterBarrels;
            set
            {
                if (_waterBarrels != value)
                {
                    _waterBarrels = value;
                    Consumption = 0;
                    foreach (var Item in App.DataAccess.NumberOfDevicesRepository.WaterBarrelList)
                    {
                        if (value == Item.Device)
                        {
                            Consumption = Item.Consumption;
                            break;
                        }
                    }
                    OnPropertyChanged(nameof(WaterBarrels));
                }
            }
        }

        private double _consumption;

        /// <summary>
        /// Расход
        /// </summary>
        public double Consumption
        {
            get => _consumption;
            set
            {
                if (_consumption != value)
                {
                    _consumption = value;
                    OnPropertyChanged(nameof(Consumption));
                }
            }
        }

        private string _foamBarrels;

        /// <summary>
        /// Пенные стволы
        /// </summary>
        public string FoamBarrels
        {
            get => _foamBarrels;
            set
            {
                if (_foamBarrels != value)
                {
                    _foamBarrels = value;
                    Consumption2 = 0;
                    ConsumptionFoam = 0;
                    ExtinguishingAreaFoamBarrel = 0;
                    foreach (var Item in App.DataAccess.NumberOfDevicesRepository.FoamBarrelList)
                    {
                        if (value == Item.Device)
                        {
                            Consumption2 = Item.Consumption2;
                            ConsumptionFoam = Item.ConsumptionFoam;
                            if (Method == Data.ExtinguishingMethodType.GJ)
                            {
                                ExtinguishingAreaFoamBarrel = Item.GJ;
                            }
                            else if (Method == Data.ExtinguishingMethodType.LVJ)
                            {
                                ExtinguishingAreaFoamBarrel = Item.LVJ;
                            }
                            else
                            {
                                ExtinguishingAreaFoamBarrel = 0;
                            }
                            break;
                        }
                        OnPropertyChanged(nameof(FoamBarrels));
                    }
                }
            }
        }

        private double _consumption2;

        /// <summary>
        /// Расход 2
        /// </summary>
        public double Consumption2
        {
            get => _consumption2;
            set
            {
                if (_consumption2 != value)
                {
                    _consumption2 = value;
                    OnPropertyChanged(nameof(Consumption2));
                }
            }
        }

        private double _consumptionFoam;

        /// <summary>
        /// Расход по пене
        /// </summary>
        public double ConsumptionFoam
        {
            get => _consumptionFoam;
            set
            {
                if (_consumptionFoam != value)
                {
                    _consumptionFoam = value;
                    OnPropertyChanged(nameof(ConsumptionFoam));
                }
            }
        }

        private double _extinguishingAreaFoamBarrel;

        /// <summary>
        /// Площадь тушения пенного ствола
        /// </summary>
        public double ExtinguishingAreaFoamBarrel
        {
            get => _extinguishingAreaFoamBarrel;
            set
            {
                if (_extinguishingAreaFoamBarrel != value)
                {
                    _extinguishingAreaFoamBarrel = value;
                    OnPropertyChanged(nameof(ExtinguishingAreaFoamBarrel));
                }
            }
        }

        private string _barrelsForDefense;

        /// <summary>
        /// Стволы на защиту
        /// </summary>
        public string BarrelsForDefense
        {
            get => _barrelsForDefense;
            set
            {
                if (_barrelsForDefense != value)
                {
                    _barrelsForDefense = value;
                    Consumption3 = 0;
                    foreach (var Item in App.DataAccess.NumberOfDevicesRepository.BarrelForDefenseList)
                    {
                        if (value == Item.Device)
                        {
                            Consumption3 = Item.Consumption3;
                            break;
                        }
                    }
                    OnPropertyChanged(nameof(BarrelsForDefense));
                }
            }
        }

        private double _consumption3;

        /// <summary>
        /// Расход 3
        /// </summary>
        public double Consumption3
        {
            get => _consumption3;
            set
            {
                if (_consumption3 != value)
                {
                    _consumption3 = value;
                    OnPropertyChanged(nameof(Consumption3));
                }
            }
        }

        private double GetCountOfFoam()
        {
            if (Document.RequiredConsumption.ExtinguishingType == Data.ExtinguishingType.ExtinguishingFront)
            {
                if (ExtinguishingAreaFoamBarrel != 0)
                {
                    CountExtinguishingFormula = Document.Quenching.ExtinguishingAreaFront + " / " + ExtinguishingAreaFoamBarrel;
                    return Document.Quenching.ExtinguishingAreaFront / ExtinguishingAreaFoamBarrel;
                }
                else
                {
                    CountExtinguishingFormula = "";
                    return 0;
                }

            }
            else if (Document.RequiredConsumption.ExtinguishingType == Data.ExtinguishingType.ExtinguishingPerimeter)
            {
                if (ExtinguishingAreaFoamBarrel != 0)
                {
                    CountExtinguishingFormula = Document.Quenching.ExtinguishingAreaPerimeter  + " / " + ExtinguishingAreaFoamBarrel;
                    return Document.Quenching.ExtinguishingAreaPerimeter / ExtinguishingAreaFoamBarrel;
                }
                else
                {
                    CountExtinguishingFormula = "";
                    return 0;
                }
            }
            else
            {
                CountExtinguishingFormula = "";
                return 0;
            }
        }

        private double _countExtinguishing;
        /// <summary>
        /// Требуемое количество приборов на тушение
        /// </summary>
        public double CountExtinguishing
        {
            get => _countExtinguishing;
            set
            {
                if (_countExtinguishing != value)
                {
                    _countExtinguishing = value;
                    OnPropertyChanged(nameof(CountExtinguishing));
                }
            }

        }

        private string _countExtinguishingFormula;
        /// <summary>
        /// Требуемое количество приборов на тушение - формула
        /// </summary>
        public string CountExtinguishingFormula
        {
            get => _countExtinguishingFormula;
            set
            {
                if (_countExtinguishingFormula != value)
                {
                    _countExtinguishingFormula = value;
                    OnPropertyChanged(nameof(CountExtinguishingFormula));
                }
            }

        }

        private double _countDefense;

        /// <summary>
        /// Требуемое количество приборов на защиту
        /// </summary>
        public double CountDefense
        {
            get => _countDefense;
            set
            {
                if (_countDefense != value)
                {
                    _countDefense = value;
                    OnPropertyChanged(nameof(CountDefense));
                }
            }
        }

        private string _countDefenseFormula;

        /// <summary>
        /// Требуемое количество приборов на защиту - формула расчета
        /// </summary>
        public string CountDefenseFormula
        {
            get => _countDefenseFormula;
            set
            {
                if (_countDefenseFormula != value)
                {
                    _countDefenseFormula = value;
                    OnPropertyChanged(nameof(CountDefenseFormula));
                }
            }
        }

        private bool _alternativeSolution1;

        /// <summary>
        /// Альтернативное решение 1
        /// </summary>
        public bool AlternativeSolution1
        {
            get => _alternativeSolution1;
            set
            {
                if (_alternativeSolution1 != value)
                {
                    _alternativeSolution1 = value;
                    OnPropertyChanged(nameof(AlternativeSolution1));
                }
            }
        }

        private bool _alternativeSolution2;

        /// <summary>
        /// Альтернативное решение 2
        /// </summary>
        public bool AlternativeSolution2
        {
            get => _alternativeSolution2;
            set
            {
                if (_alternativeSolution2 != value)
                {
                    _alternativeSolution2 = value;
                    OnPropertyChanged(nameof(AlternativeSolution2));
                }
            }
        }

        private string _extinguishingBarrel1;
        /// <summary>
        /// Альтернативное решение - ствол 1 на тушение
        /// </summary>
        public string ExtinguishingBarrel1
        {
            get => _extinguishingBarrel1;
            set
            {
                if (_extinguishingBarrel1 != value)
                {
                    _extinguishingBarrel1 = value;
                    OnPropertyChanged(nameof(ExtinguishingBarrel1));
                }
            }

        }

        private string _extinguishingBarrel2;
        /// <summary>
        /// Альтернативное решение - ствол 2 на тушение
        /// </summary>
        public string ExtinguishingBarrel2
        {
            get => _extinguishingBarrel2;
            set
            {
                if (_extinguishingBarrel2 != value)
                {
                    _extinguishingBarrel2 = value;
                    OnPropertyChanged(nameof(ExtinguishingBarrel2));
                }
            }

        }

        private int _countExtinguishingBarrel1;

        /// <summary>
        /// Альтернативное решение - количество приборов 1 на тушение
        /// </summary>
        public int CountExtinguishingBarrel1
        {
            get => _countExtinguishingBarrel1;
            set
            {
                if (_countExtinguishingBarrel1 != value)
                {
                    _countExtinguishingBarrel1 = value;
                    OnPropertyChanged(nameof(CountExtinguishingBarrel1));
                }
            }
        }

        private int _countExtinguishingBarrel2;

        /// <summary>
        /// Альтернативное решение - количество приборов 2 на тушение
        /// </summary>
        public int CountExtinguishingBarrel2
        {
            get => _countExtinguishingBarrel2;
            set
            {
                if (_countExtinguishingBarrel2 != value)
                {
                    _countExtinguishingBarrel2 = value;
                    OnPropertyChanged(nameof(CountExtinguishingBarrel2));
                }
            }
        }

        private string _defenseBarrel1;
        /// <summary>
        /// Альтернативное решение - ствол 1 на защиту
        /// </summary>
        public string DefenseBarrel1
        {
            get => _defenseBarrel1;
            set
            {
                if (_defenseBarrel1 != value)
                {
                    _defenseBarrel1 = value;
                    OnPropertyChanged(nameof(DefenseBarrel1));
                }
            }

        }

        private string _defenseBarrel2;
        /// <summary>
        /// Альтернативное решение - ствол 2 на защиту
        /// </summary>
        public string DefenseBarrel2
        {
            get => _defenseBarrel2;
            set
            {
                if (_defenseBarrel2 != value)
                {
                    _defenseBarrel2 = value;
                    OnPropertyChanged(nameof(DefenseBarrel2));
                }
            }

        }

        private int _countDefenseBarrel1;

        /// <summary>
        /// Альтернативное решение - количество приборов 1 на защиту
        /// </summary>
        public int CountDefenseBarrel1
        {
            get => _countDefenseBarrel1;
            set
            {
                if (_countDefenseBarrel1 != value)
                {
                    _countDefenseBarrel1 = value;
                    OnPropertyChanged(nameof(CountDefenseBarrel1));
                }
            }
        }

        private int _countDefenseBarrel2;

        /// <summary>
        /// Альтернативное решение - количество приборов 2 на защиту
        /// </summary>
        public int CountDefenseBarrel2
        {
            get => _countDefenseBarrel2;
            set
            {
                if (_countDefenseBarrel2 != value)
                {
                    _countDefenseBarrel2 = value;
                    OnPropertyChanged(nameof(CountDefenseBarrel2));
                }
            }
        }

        private int _totalCountExtinguishingBarrel;

        /// <summary>
        /// Альтернативное решение - итоговое количество приборов на тушение
        /// </summary>
        public int TotalCountExtinguishingBarrel
        {
            get => _totalCountExtinguishingBarrel;
            set
            {
                if (_totalCountExtinguishingBarrel != value)
                {
                    _totalCountExtinguishingBarrel = value;
                    OnPropertyChanged(nameof(TotalCountExtinguishingBarrel));
                }
            }
        }

        private int _totalCountDefenseBarrel;

        /// <summary>
        /// Альтернативное решение - итоговое количество приборов на защиту
        /// </summary>
        public int TotalCountDefenseBarrel
        {
            get => _totalCountDefenseBarrel;
            set
            {
                if (_totalCountDefenseBarrel != value)
                {
                    _totalCountDefenseBarrel = value;
                    OnPropertyChanged(nameof(TotalCountDefenseBarrel));
                }
            }
        }

        private string _totalCountExtinguishingBarrelFormula;

        /// <summary>
        /// Альтернативное решение - итоговое количество приборов на тушение - формула расчета
        /// </summary>
        public string TotalCountExtinguishingBarrelFormula
        {
            get => _totalCountExtinguishingBarrelFormula;
            set
            {
                if (_totalCountExtinguishingBarrelFormula != value)
                {
                    _totalCountExtinguishingBarrelFormula = value;
                    OnPropertyChanged(nameof(TotalCountExtinguishingBarrelFormula));
                }
            }
        }

        private string _totalCountDefenseBarrelFormula;

        /// <summary>
        /// Альтернативное решение - итоговое количество приборов на защиту - формула расчета
        /// </summary>
        public string TotalCountDefenseBarrelFormula
        {
            get => _totalCountDefenseBarrelFormula;
            set
            {
                if (_totalCountDefenseBarrelFormula != value)
                {
                    _totalCountDefenseBarrelFormula = value;
                    OnPropertyChanged(nameof(TotalCountDefenseBarrelFormula));
                }
            }
        }

        /// <summary>
        /// Расчет количества приборов на тушение
        /// </summary>
        private double GetCountExtinguishing()
        {
            switch (Method)
            {
                case Data.ExtinguishingMethodType.Water:
                    if (Consumption != 0)
                    {
                        CountExtinguishingFormula = Document.RequiredConsumption.ConsumptionExtinguishing + " / " + Consumption;
                        return Document.RequiredConsumption.ConsumptionExtinguishing / Consumption;
                    }
                    else
                    {
                        CountExtinguishingFormula = "";
                        return 0;
                    }
                case Data.ExtinguishingMethodType.BulkQuenching:
                    if (ConsumptionFoam != 0)
                    {
                        CountExtinguishingFormula = "(" + Volume + " * " + Data.NumberOfDevicesRepository.K3 + ") / (" + ConsumptionFoam + " * " + Data.NumberOfDevicesRepository.Tr + ")";
                        return (Volume * Data.NumberOfDevicesRepository.K3) / (ConsumptionFoam * Data.NumberOfDevicesRepository.Tr);
                    }
                    else
                    {
                        CountExtinguishingFormula = "";
                        return 0;
                    }
                case Data.ExtinguishingMethodType.LVJ:
                    return GetCountOfFoam();
                case Data.ExtinguishingMethodType.GJ:
                    return GetCountOfFoam();
                default:
                    {
                        CountExtinguishingFormula = "";
                        return 0;
                    }
            }
        }

        /// <summary>
        /// Расчет количества приборов на защиту
        /// </summary>
        private double GetCountDefense()
        {
            if (Consumption3 != 0)
            {
                CountDefenseFormula = Document.RequiredConsumption.ConsumptionDefense + " / " + Consumption3;
                return Document.RequiredConsumption.ConsumptionDefense / Consumption3;
            }
            else
            {
                CountDefenseFormula = "";
                return 0;
            }
        }

        /// <summary>
        /// Расчет альтернативного решения 1
        /// </summary>
        private int GetAlternativeSolution1()
        {
            if (AlternativeSolution1)
            {
                TotalCountExtinguishingBarrelFormula = CountExtinguishingBarrel1 + " + " + CountExtinguishingBarrel2;
                return CountExtinguishingBarrel1 + CountExtinguishingBarrel2;
            }
            else
            {
                TotalCountExtinguishingBarrelFormula = "";
                return 0;
            }           
           
        }

        /// <summary>
        /// Расет альтернативного решения 2
        /// </summary>
        private int GetAlternativeSolution2()
        {
            if (AlternativeSolution2)
            {
                TotalCountDefenseBarrelFormula = CountDefenseBarrel1 + " + " + CountDefenseBarrel2;
                return CountDefenseBarrel1 + CountDefenseBarrel2;
            }
            else
            {
                TotalCountDefenseBarrelFormula = "";
                return 0;
            }            
        }

        public override void UpdateParams()
        {
            //Расчетные данные
            CountExtinguishing = Math.Ceiling(GetCountExtinguishing());
            CountDefense = Math.Ceiling(GetCountDefense());
            TotalCountExtinguishingBarrel = GetAlternativeSolution1();
            TotalCountDefenseBarrel = GetAlternativeSolution2();
        }

        protected override void Init()
        {
            Method = Data.ExtinguishingMethodType.None;
        }

        public NumberOfDevices() : base()
        {

        }

        public NumberOfDevices(Document document) : base(document)
        {

        }

    }
}
