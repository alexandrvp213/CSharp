using System;
using SQLite;

namespace FOHQ.Models
{
    /// <summary>
    /// Методы подачи ОВ к месту пожара
    /// </summary>
    [Table("FilingMethods")]
    public class FilingMethods : BaseModel
    {
        const string plusStr = " + ";
        const string minusStr = " - ";

        private bool _IsWaterSupplyMethod;

        /// <summary>
        /// Метод Подвоз воды
        /// </summary>
        public bool IsWaterSupplyMethod
        {
            get => _IsWaterSupplyMethod;
            set
            {
                if (_IsWaterSupplyMethod != value)
                {
                    _IsWaterSupplyMethod = value;
                    OnPropertyChanged(nameof(IsWaterSupplyMethod));
                }
            }
        }

        private bool _IsWaterSupplyToPumpMethod;

        /// <summary>
        /// Метод подачи воды
        /// </summary>
        public bool IsWaterSupplyToPumpMethod
        {
            get => _IsWaterSupplyToPumpMethod;
            set
            {
                if (_IsWaterSupplyToPumpMethod != value)
                {
                    _IsWaterSupplyToPumpMethod = value;
                    OnPropertyChanged(nameof(IsWaterSupplyToPumpMethod));
                }
            }
        }

        private double _distance;

        /// <summary>
        /// Расстояние
        /// </summary>
        public double Distance
        {
            get => _distance;
            set
            {
                if (_distance != value)
                {
                    _distance = value;
                    OnPropertyChanged(nameof(Distance));
                }
            }
        }

        private double _travelSpeed;

        /// <summary>
        /// Скорость движения АЦ
        /// </summary>
        public double TravelSpeed
        {
            get => _travelSpeed;
            set
            {
                if (_travelSpeed != value)
                {
                    _travelSpeed = value;
                    OnPropertyChanged(nameof(TravelSpeed));
                }
            }
        }

        private double _cisterneVolume;

        /// <summary>
        /// Объем цистерны
        /// </summary>
        public double CisterneVolume
        {
            get => _cisterneVolume;
            set
            {
                if (_cisterneVolume != value)
                {
                    _cisterneVolume = value;
                    OnPropertyChanged(nameof(CisterneVolume));
                }
            }
        }

        private double _waterMediumPumping;

        /// <summary>
        /// Средняя подача воды насосом
        /// </summary>
        public double WaterMediumPumping
        {
            get => _waterMediumPumping;
            set
            {
                if (_waterMediumPumping != value)
                {
                    _waterMediumPumping = value;
                    OnPropertyChanged(nameof(WaterMediumPumping));
                }
            }
        }

        private double _lineWaterConsumption;

        /// <summary>
        /// Расход воды по наиболее загруженной линии
        /// </summary>
        public double LineWaterConsumption
        {
            get => _lineWaterConsumption;
            set
            {
                if (_lineWaterConsumption != value)
                {
                    _lineWaterConsumption = value;
                    OnPropertyChanged(nameof(LineWaterConsumption));
                }
            }
        }


        private double _ascentDescentHeight;

        /// <summary>
        /// Высота подъема/спуска местности
        /// </summary>
        public double AscentDescentHeight
        {
            get => _ascentDescentHeight;
            set
            {
                var oldValue = _ascentDescentHeight;
                if (!_ascentDescentHeight.Equals(value))
                {
                    if (value <= 10)
                    {
                        _ascentDescentHeight = value;
                    }
                    else
                    {
                        _ascentDescentHeight = oldValue;
                    }
                    OnPropertyChanged(nameof(AscentDescentHeight));
                }
            }
        }

        private double _heightLiftingLoweringTrunk;

        /// <summary>
        /// Высота подъема/спуска ствола
        /// </summary>
        public double HeightLiftingLoweringTrunk
        {
            get => _heightLiftingLoweringTrunk;
            set
            {
                var oldValue = _heightLiftingLoweringTrunk;
                if (!_heightLiftingLoweringTrunk.Equals(value))
                {
                    if (value <= 75)
                    {
                        _heightLiftingLoweringTrunk = value;
                    }
                    else
                    {
                        _heightLiftingLoweringTrunk = oldValue;
                    }
                    OnPropertyChanged(nameof(HeightLiftingLoweringTrunk));
                }
            }
        }

        private double _lineWaterConsumption2;

        /// <summary>
        /// Расход воды по наиболее загруженной линии
        /// </summary>
        public double LineWaterConsumption2
        {
            get => _lineWaterConsumption2;
            set
            {
                if (_lineWaterConsumption2 != value)
                {
                    _lineWaterConsumption2 = value;
                    OnPropertyChanged(nameof(LineWaterConsumption2));
                }
            }
        }

        private double _distanceIPPV;

        /// <summary>
        /// Расстояние до ИППВ
        /// </summary>
        public double DistanceIPPV
        {
            get => _distanceIPPV;
            set
            {
                if (_distanceIPPV != value)
                {
                    _distanceIPPV = value;
                    OnPropertyChanged(nameof(DistanceIPPV));
                }
            }
        }

        private int _pressureOnPump;

        /// <summary>
        /// Напор на насосе
        /// </summary>
        public int PressureOnPump
        {
            get => _pressureOnPump;
            set
            {
                if (_pressureOnPump != value)
                {
                    _pressureOnPump = value;
                    OnPropertyChanged(nameof(PressureOnPump));
                }
            }
        }

        private double _travelTime;

        /// <summary>
        /// Время следования
        /// </summary>
        public double TravelTime
        {
            get => _travelTime;
            set
            {
                if (_travelTime != value)
                {
                    _travelTime = value;
                    OnPropertyChanged(nameof(TravelTime));
                }
            }
        }

        private string _travelTimeFormula;

        /// <summary>
        /// Время следования - формула расчета
        /// </summary>
        public string TravelTimeFormula
        {
            get => _travelTimeFormula;
            set
            {
                if (_travelTimeFormula != value)
                {
                    _travelTimeFormula = value;
                    OnPropertyChanged(nameof(TravelTimeFormula));
                }
            }
        }

        private double _refuelingTime;

        /// <summary>
        /// Время заправки АЦ
        /// </summary>
        public double RefuelingTime
        {
            get => _refuelingTime;
            set
            {
                if (_refuelingTime != value)
                {
                    _refuelingTime = value;
                    OnPropertyChanged(nameof(RefuelingTime));
                }
            }
        }

        private string _refuelingTimeFormula;

        /// <summary>
        /// Время заправки АЦ - формула расчета
        /// </summary>
        public string RefuelingTimeFormula
        {
            get => _refuelingTimeFormula;
            set
            {
                if (_refuelingTimeFormula != value)
                {
                    _refuelingTimeFormula = value;
                    OnPropertyChanged(nameof(RefuelingTimeFormula));
                }
            }
        }

        private double _waterConsumptionTime;

        /// <summary>
        /// Время расхода воды АЦ
        /// </summary>
        public double WaterConsumptionTime
        {
            get => _waterConsumptionTime;
            set
            {
                if (_waterConsumptionTime != value)
                {
                    _waterConsumptionTime = value;
                    OnPropertyChanged(nameof(WaterConsumptionTime));
                }
            }
        }

        private string _waterConsumptionTimeFormula;

        /// <summary>
        /// Время расхода воды АЦ - формула расчета
        /// </summary>
        public string WaterConsumptionTimeFormula
        {
            get => _waterConsumptionTimeFormula;
            set
            {
                if (_waterConsumptionTimeFormula != value)
                {
                    _waterConsumptionTimeFormula = value;
                    OnPropertyChanged(nameof(WaterConsumptionTimeFormula));
                }
            }
        }

        private double _ACCount;

        /// <summary>
        /// Количество АЦ для подвоза
        /// </summary>
        public double ACCount
        {
            get => _ACCount;
            set
            {
                if (_ACCount != value)
                {
                    _ACCount = value;
                    OnPropertyChanged(nameof(ACCount));
                }
            }
        }

        private string _ACCountFormula;

        /// <summary>
        /// Количество АЦ для подвоза - формула расчета
        /// </summary>
        public string ACCountFormula
        {
            get => _ACCountFormula;
            set
            {
                if (_ACCountFormula != value)
                {
                    _ACCountFormula = value;
                    OnPropertyChanged(nameof(ACCountFormula));
                }
            }
        }

        private Data.AscentDescentType _ascentDescentLocality;

        /// <summary>
        /// Подъем/спуск для местности
        /// </summary>
        public Data.AscentDescentType AscentDescentLocality
        {
            get => _ascentDescentLocality;
            set
            {
                if (_ascentDescentLocality != value)
                {
                    _ascentDescentLocality = value;
                    OnPropertyChanged(nameof(AscentDescentLocality));
                }
            }
        }

        private Data.AscentDescentType _ascentDescentTrunk;

        /// <summary>
        /// Подъем/спуск для ствола
        /// </summary>
        public Data.AscentDescentType AscentDescentTrunk
        {
            get => _ascentDescentTrunk;
            set
            {
                if (_ascentDescentTrunk != value)
                {
                    _ascentDescentTrunk = value;
                    OnPropertyChanged(nameof(AscentDescentTrunk));
                }
            }
        }


        private double _distanceMainPA;

        /// <summary>
        /// Расстояние до головной ПА
        /// </summary>
        public double DistanceMainPA
        {
            get => _distanceMainPA;
            set
            {
                if (_distanceMainPA != value)
                {
                    _distanceMainPA = value;
                    OnPropertyChanged(nameof(DistanceMainPA));
                }
            }
        }

        private string _distanceMainPAFormula;

        /// <summary>
        /// Расстояние до головной ПА - формула расчета
        /// </summary>
        public string DistanceMainPAFormula
        {
            get => _distanceMainPAFormula;
            set
            {
                if (_distanceMainPAFormula != value)
                {
                    _distanceMainPAFormula = value;
                    OnPropertyChanged(nameof(DistanceMainPAFormula));
                }
            }
        }

        private double _distanceBetweenPA;

        /// <summary>
        /// Расстояние между ПА
        /// </summary>
        public double DistanceBetweenPA
        {
            get => _distanceBetweenPA;
            set
            {
                if (_distanceBetweenPA != value)
                {
                    _distanceBetweenPA = value;
                    OnPropertyChanged(nameof(DistanceBetweenPA));
                }
            }
        }

        private string _distanceBetweenPAFormula;

        /// <summary>
        /// Расстояние между ПА - формула
        /// </summary>
        public string DistanceBetweenPAFormula
        {
            get => _distanceBetweenPAFormula;
            set
            {
                if (_distanceBetweenPAFormula != value)
                {
                    _distanceBetweenPAFormula = value;
                    OnPropertyChanged(nameof(DistanceBetweenPAFormula));
                }
            }
        }


        private double _totalNumberHoses;

        /// <summary>
        /// Общее количество рукавов в магистральной линии
        /// </summary>
        public double TotalNumberHoses
        {
            get => _totalNumberHoses;
            set
            {
                if (_totalNumberHoses != value)
                {
                    _totalNumberHoses = value;
                    OnPropertyChanged(nameof(TotalNumberHoses));
                }
            }
        }

        private string _totalNumberHosesFormula;

        /// <summary>
        /// Общее количество рукавов в магистральной линии - формула
        /// </summary>
        public string TotalNumberHosesFormula
        {
            get => _totalNumberHosesFormula;
            set
            {
                if (_totalNumberHosesFormula != value)
                {
                    _totalNumberHosesFormula = value;
                    OnPropertyChanged(nameof(TotalNumberHosesFormula));
                }
            }
        }

        private double _numberPumpingStages;

        /// <summary>
        /// Количество ступеней перекачки
        /// </summary>
        public double NumberPumpingStages
        {
            get => _numberPumpingStages;
            set
            {
                if (_numberPumpingStages != value)
                {
                    _numberPumpingStages = value;
                    OnPropertyChanged(nameof(NumberPumpingStages));
                }
            }
        }

        private string _numberPumpingStagesFormula;

        /// <summary>
        /// Количество ступеней перекачки -формула расчета
        /// </summary>
        public string NumberPumpingStagesFormula
        {
            get => _numberPumpingStagesFormula;
            set
            {
                if (_numberPumpingStagesFormula != value)
                {
                    _numberPumpingStagesFormula = value;
                    OnPropertyChanged(nameof(NumberPumpingStagesFormula));
                }
            }
        }

        private double _numberPAForPumping;

        /// <summary>
        /// Количество ПА для перекачки 
        /// </summary>
        public double NumberPAForPumping
        {
            get => _numberPAForPumping;
            set
            {
                if (_numberPAForPumping != value)
                {
                    _numberPAForPumping = value;
                    OnPropertyChanged(nameof(NumberPAForPumping));
                }
            }
        }

        private string _numberPAForPumpingFormula;

        /// <summary>
        /// Количество ПА для перекачки - формула расчета
        /// </summary>
        public string NumberPAForPumpingFormula
        {
            get => _numberPAForPumpingFormula;
            set
            {
                if (_numberPAForPumpingFormula != value)
                {
                    _numberPAForPumpingFormula = value;
                    OnPropertyChanged(nameof(NumberPAForPumpingFormula));
                }
            }
        }

        private double _actualDistanceFromPAToJunction;

        /// <summary>
        /// Фактическое расстояние от ПА до разветвления 
        /// </summary>
        public double ActualDistanceFromPAToJunction
        {
            get => _actualDistanceFromPAToJunction;
            set
            {
                if (_actualDistanceFromPAToJunction != value)
                {
                    _actualDistanceFromPAToJunction = value;
                    OnPropertyChanged(nameof(ActualDistanceFromPAToJunction));
                }
            }
        }

        private string _actualDistanceFromPAToJunctionFormula;

        /// <summary>
        /// Фактическое расстояние от ПА до разветвления - формула расчета
        /// </summary>
        public string ActualDistanceFromPAToJunctionFormula
        {
            get => _actualDistanceFromPAToJunctionFormula;
            set
            {
                if (_actualDistanceFromPAToJunctionFormula != value)
                {
                    _actualDistanceFromPAToJunctionFormula = value;
                    OnPropertyChanged(nameof(ActualDistanceFromPAToJunctionFormula));
                }
            }
        }

        /// <summary>
        /// Расчет времени следования
        /// </summary>
        private double GetTravelTime()
        {
            if (CisterneVolume != 0)
            {
                TravelTimeFormula = Distance + " * 60 / " + TravelSpeed;
                return Distance * 60 / TravelSpeed;
            }
            else
            {
                TravelTimeFormula = "";
                return 0;
            }
        }

        /// <summary>
        /// Расчет времени заправки АЦ
        /// </summary>
        private double GetRefuelingTime()
        {
            if (WaterMediumPumping != 0)
            {
                RefuelingTimeFormula = CisterneVolume + " / (" + WaterMediumPumping + " * 60)";
                return CisterneVolume / (WaterMediumPumping * 60);
            }
            else
            {
                RefuelingTimeFormula = "";
                return 0;
            }
        }

        /// <summary>
        /// Расчет времени расхода воды АЦ
        /// </summary>
        private double GetWaterConsumptionTime()
        {
            if (LineWaterConsumption != 0)
            {
                WaterConsumptionTimeFormula = CisterneVolume + " / (" + LineWaterConsumption + " * 60)";
                return CisterneVolume / (LineWaterConsumption * 60);
            }
            else
            {
                WaterConsumptionTimeFormula = "";
                return 0;
            }
        }

        /// <summary>
        /// Расчет количества АЦ для подвоза
        /// </summary>
        private double GetACCount()
        {
            if (WaterConsumptionTime != 0)
            {
                ACCountFormula = "((2 * " + TravelTime  + " + " + RefuelingTime + ") / "  + WaterConsumptionTime + ") + 1";
                return ((2 * TravelTime + RefuelingTime) / WaterConsumptionTime) + 1;
            }
            else
            {
                ACCountFormula = "";
                return 0;
            }
        }

        /// <summary>
        /// Расчет расстояния до головной ПА
        /// </summary>
        private double GetDistanceMainPA()
        {
            if (LineWaterConsumption2 != 0)
            {
                var kTrunk = 1;
                var kLocality = 1;
                var kTrunkStr = plusStr;
                var kLocalityStr = plusStr;
                if (AscentDescentLocality == Data.AscentDescentType.Descent)
                {
                    kLocality = -1;
                    kLocalityStr = minusStr;
                }
                if (AscentDescentTrunk == Data.AscentDescentType.Descent)
                {
                    kTrunk = -1;
                    kTrunkStr = minusStr;
                }
                DistanceMainPAFormula = "(" + PressureOnPump + " - ((" + Data.FilingMethodsRepository.PressureAtStem + " + 10) + " +
                    Data.FilingMethodsRepository.PressureAtStem + 
                  kLocalityStr + AscentDescentHeight + kTrunkStr + HeightLiftingLoweringTrunk + ")) / (" +
                   Data.FilingMethodsRepository.FireHoseResistance + " * " + LineWaterConsumption2 
                    + " * " + LineWaterConsumption2 + ")";
                return (PressureOnPump - ((Data.FilingMethodsRepository.PressureAtStem + 10) + Data.FilingMethodsRepository.PressureAtStem +
                   kLocality * AscentDescentHeight + kTrunk * HeightLiftingLoweringTrunk)) / (Data.FilingMethodsRepository.FireHoseResistance * LineWaterConsumption2
                   * LineWaterConsumption2);
            }
            else
            {
                DistanceMainPAFormula = "";
                return 0;
            }
        }

        /// <summary>
        /// Расчет расстояния между ПА
        /// </summary>
        private double GetDistanceBetweenPA()
        {
            if (LineWaterConsumption2 != 0)
            {
                var kLocality = 1;
                var kLocalityStr = plusStr;
                if (AscentDescentLocality == Data.AscentDescentType.Descent)
                {
                    kLocality = -1;
                    kLocalityStr = minusStr;
                }
                DistanceBetweenPAFormula = "(" + PressureOnPump + " - (" + Data.FilingMethodsRepository.PressureAtPumpInlet + 
                  kLocalityStr + AscentDescentHeight + ")) / (" + Data.FilingMethodsRepository.FireHoseResistance + " * " + LineWaterConsumption2 +
                  " * " + LineWaterConsumption2 + ")";
                return (PressureOnPump - (Data.FilingMethodsRepository.PressureAtPumpInlet +
                  kLocality * AscentDescentHeight)) / (Data.FilingMethodsRepository.FireHoseResistance * LineWaterConsumption2
                   * LineWaterConsumption2);
            }
            else
            {
                DistanceBetweenPAFormula = "";
                return 0;
            }
        }

        /// <summary>
        /// Расчет общего количества рукавов в магистральной линии
        /// </summary>
        private double GetTotalNumberHoses()
        {
            TotalNumberHosesFormula = "(1.2 * " + DistanceIPPV + ") / 20";
            return (1.2 * DistanceIPPV) / 20;
        }

        /// <summary>
        /// Расчет количества ступеней перекачки
        /// </summary>
        private double GetNumberPumpingStages()
        {
            if (DistanceBetweenPA != 0)
            {
                NumberPumpingStagesFormula = "(" + TotalNumberHoses + " - " + DistanceMainPA + ") / " + DistanceBetweenPA;
                return (TotalNumberHoses - DistanceMainPA) / DistanceBetweenPA;
            }
            else
            {
                NumberPumpingStagesFormula = "";
                return 0;
            }
        }

        /// <summary>
        /// Расчет количества ПА для перекачки
        /// </summary>
        private double GetNumberPAForPumping()
        {
            NumberPAForPumpingFormula = NumberPumpingStages  + " + 1";
            return NumberPumpingStages + 1;
        }

        /// <summary>
        /// Расчет фактического расстояния от ПА до разветвления
        /// </summary>
        private double GetActualDistanceFromPAToJunction()
        {
            ActualDistanceFromPAToJunctionFormula = TotalNumberHoses + " - " + NumberPumpingStages + " * " + DistanceBetweenPA;
            return TotalNumberHoses - NumberPumpingStages * DistanceBetweenPA;
        }

        public override void UpdateParams()
        {
            //Расчетные данные
            if (IsWaterSupplyMethod)
            {
                //Подвоз воды
                TravelTime = Math.Round(GetTravelTime(), 2);
                RefuelingTime = Math.Round(GetRefuelingTime(), 2);
                WaterConsumptionTime = Math.Round(GetWaterConsumptionTime(), 2);
                ACCount = Math.Ceiling(GetACCount());
            }
            if (IsWaterSupplyToPumpMethod)
            {
                //Перекачка воды 
                DistanceMainPA = GetDistanceMainPA();
                DistanceBetweenPA = GetDistanceBetweenPA();
                TotalNumberHoses = GetTotalNumberHoses();
                NumberPumpingStages = GetNumberPumpingStages();
                NumberPAForPumping = GetNumberPAForPumping();
                ActualDistanceFromPAToJunction = GetActualDistanceFromPAToJunction();
                // округление до целого
                DistanceMainPA = Math.Ceiling(DistanceMainPA);
                DistanceBetweenPA = Math.Ceiling(DistanceBetweenPA);
                TotalNumberHoses = Math.Ceiling(TotalNumberHoses);
                NumberPumpingStages = Math.Ceiling(NumberPumpingStages);
                NumberPAForPumping = Math.Ceiling(NumberPAForPumping);
                ActualDistanceFromPAToJunction = Math.Ceiling(ActualDistanceFromPAToJunction);
            }
        }

        protected override void Init()
        {
            AscentDescentLocality = Data.AscentDescentType.Ascent;
            AscentDescentTrunk = Data.AscentDescentType.Ascent;
        }

        public FilingMethods() : base()
        {

        }

        public FilingMethods(Document document) : base(document)
        {

        }

    }
}
