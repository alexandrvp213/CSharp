using System;
using SQLite;

namespace FOHQ.Models
{
    /// <summary>
    /// Тушение ЛВЖ и ГЖ (по раствору) 
    /// </summary>
    [Table("QuenchingSolution")]
    public class QuenchingSolution : BaseModel
    {
        private Data.MultiplicityType _multiplicity;

        /// <summary>
        /// Кратность ПО
        /// </summary>
        public FOHQ.Data.MultiplicityType Multiplicity
        {
            get => _multiplicity;
            set
            {
                if (_multiplicity != value)
                {
                    _multiplicity = value;
                    OnPropertyChanged(nameof(Multiplicity));
                }
            }
        }

        private Data.LiquorType _liquorType;

        /// <summary>
        /// ЛВЖ/ГЖ
        /// </summary>
        public FOHQ.Data.LiquorType LiquorType
        {
            get => _liquorType;
            set
            {
                if (_liquorType != value)
                {
                    _liquorType = value;
                    OnPropertyChanged(nameof(LiquorType));
                }
            }
        }

        private double _cisternVolume;

        /// <summary>
        /// Объем цистерны
        /// </summary>
        public double CisternVolume
        {
            get => _cisternVolume;
            set
            {
                if (_cisternVolume != value)
                {
                    _cisternVolume = value;
                    FactKoeff = GetFactKoeff();
                    ConditionMore = GetConditionMore();
                    OnPropertyChanged(nameof(CisternVolume));
                }
            }
        }

        private double _volumePO;

        /// <summary>
        /// Объем ПО
        /// </summary>
        public double VolumePO
        {
            get => _volumePO;
            set
            {
                if (_volumePO != value)
                {
                    _volumePO = value;
                    FactKoeff = GetFactKoeff();
                    ConditionMore = GetConditionMore();
                    OnPropertyChanged(nameof(VolumePO));
                }
            }
        }

        /// <summary>
        /// Фактический коэффициент Кф - вычисление
        /// </summary>
        private double GetFactKoeff()
        {
            if (VolumePO != 0)
            {
                double result = _cisternVolume / _volumePO;
                return result;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// Вычисление знака неравенства
        /// </summary>
        private string GetConditionMore()
        {
            double Kv = 0;
            if (_multiplicity == Data.MultiplicityType.Low)
            {
                Kv = Data.QuenchingSolutionRepository.KvLow;
            }
            else if (_multiplicity == Data.MultiplicityType.Medium)
            {
                Kv = Data.QuenchingSolutionRepository.KvMedium;
            }
            if (_factKoeff > Kv)
            {
                return ">";
            }
            else if (_factKoeff < Kv)
            {
                return "<";
            }
            else
            {
                return "=";
            }
        }

        private double _factKoeff;

        /// <summary>
        /// Фактический коэффициент Кф
        /// </summary>
        public double FactKoeff
        {
            get => _factKoeff;
            set
            {
                if (_factKoeff != value)
                {
                    _factKoeff = value;
                    OnPropertyChanged(nameof(FactKoeff));
                }
            }
        }

        private string _conditionMore;

        /// <summary>
        /// Условие
        /// </summary>
        public string ConditionMore
        {
            get => _conditionMore;
            set
            {
                if (_conditionMore != value)
                {
                    _conditionMore = value;
                    OnPropertyChanged(nameof(ConditionMore));
                }
            }
        }

        private double _estimateTime;

        /// <summary>
        /// Расчетное время тушения
        /// </summary>
        public double EstimateTime
        {
            get => _estimateTime;
            set
            {
                if (_estimateTime != value)
                {
                    _estimateTime = value;
                    OnPropertyChanged(nameof(EstimateTime));
                }
            }
        }

        private int _hoseCount;

        /// <summary>
        /// Количество рукавов
        /// </summary>
        public int HoseCount
        {
            get => _hoseCount;
            set
            {
                if (_hoseCount != value)
                {
                    _hoseCount = value;
                    OnPropertyChanged(nameof(HoseCount));
                }
            }
        }

        private double _hoseDiameter;

        /// <summary>
        /// Диаметр рукава
        /// </summary>
        public double HoseDiameter
        {
            get => _hoseDiameter;
            set
            {
                if (_hoseDiameter != value)
                {
                    _hoseDiameter = value;
                    HoseVolume = 0;
                    foreach (FOHQ.Data.DiameterHoseItem diameterHose in App.DataAccess.QuenchingSolutionRepository.DiameterHoseLIst)
                    {
                        if (_hoseDiameter == diameterHose.Diameter)
                        {
                            HoseVolume = diameterHose.Volume;
                            break;
                        }
                    }
                    OnPropertyChanged(nameof(HoseDiameter));
                }
            }
        }

        private double _hoseVolume;

        /// <summary>
        /// Объем рукава
        /// </summary>
        public double HoseVolume
        {
            get => _hoseVolume;
            set
            {
                if (_hoseVolume != value)
                {
                    _hoseVolume = value;
                    OnPropertyChanged(nameof(HoseVolume));
                }
            }
        }

        private int _barrelsCount;

        /// <summary>
        /// Количество стволов
        /// </summary>
        public int BarrelsCount
        {
            get => _barrelsCount;
            set
            {
                if (_barrelsCount != value)
                {
                    _barrelsCount = value;
                    OnPropertyChanged(nameof(BarrelsCount));
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
                    Сonsumption = 0;
                    FoamMultiplicity = 0;
                    System.Collections.Generic.List<Data.MultiplicityFoamBarrelItem> list = null;
                    if (Multiplicity == Data.MultiplicityType.Low)
                    {
                        list = App.DataAccess.QuenchingSolutionRepository.FoamBarrelLowList;
                    }
                    else if (Multiplicity == Data.MultiplicityType.Medium)
                    {
                        list = App.DataAccess.QuenchingSolutionRepository.FoamBarrelMediumList;
                    }

                    if (list != null)
                    {
                        foreach (FOHQ.Data.MultiplicityFoamBarrelItem foamBarrel in list)
                        {
                            if (_foamBarrels == foamBarrel.Device)
                            {
                                Сonsumption = foamBarrel.Сonsumption;
                                FoamMultiplicity = foamBarrel.Multiplicity;
                                break;
                            }
                        }
                    }
                    OnPropertyChanged(nameof(FoamBarrels));
                }
            }
        }

        private int _consumption;

        /// <summary>
        /// Расход
        /// </summary>
        public int Сonsumption
        {
            get => _consumption;
            set
            {
                if (_consumption != value)
                {
                    _consumption = value;
                    OnPropertyChanged(nameof(Сonsumption));
                }
            }
        }

        private int _foamMultiplicity;

        /// <summary>
        /// Кратность пены
        /// </summary>
        public int FoamMultiplicity
        {
            get => _foamMultiplicity;
            set
            {
                if (_foamMultiplicity != value)
                {
                    _foamMultiplicity = value;
                    OnPropertyChanged(nameof(FoamMultiplicity));
                }
            }
        }

        private double _liquorVolume;

        /// <summary>
        /// Объем раствора
        /// </summary>
        public double LiquorVolume
        {
            get => _liquorVolume;
            set
            {
                if (_liquorVolume != value)
                {
                    _liquorVolume = value;
                    OnPropertyChanged(nameof(LiquorVolume));
                }
            }
        }

        private string _liquorVolumeFormula;

        /// <summary>
        /// Объем раствора
        /// </summary>
        public string LiquorVolumeFormula
        {
            get => _liquorVolumeFormula;
            set
            {
                if (_liquorVolumeFormula != value)
                {
                    _liquorVolumeFormula = value;
                    OnPropertyChanged(nameof(LiquorVolumeFormula));
                }
            }
        }

        private double _volumeVMP;

        /// <summary>
        /// Объем ВМП
        /// </summary>
        public double VolumeVMP
        {
            get => _volumeVMP;
            set
            {
                if (_volumeVMP != value)
                {
                    _volumeVMP = value;
                    OnPropertyChanged(nameof(VolumeVMP));
                }
            }
        }

        private string _volumeVMPFormula;

        /// <summary>
        /// Объем ВМП - формула расчета
        /// </summary>
        public string VolumeVMPFormula
        {
            get => _volumeVMPFormula;
            set
            {
                if (_volumeVMPFormula != value)
                {
                    _volumeVMPFormula = value;
                    OnPropertyChanged(nameof(VolumeVMPFormula));
                }
            }
        }

        private double _quenchingArea;

        /// <summary>
        /// Площадь тушения
        /// </summary>
        public double QuenchingArea
        {
            get => _quenchingArea;
            set
            {
                if (_quenchingArea != value)
                {
                    _quenchingArea = value;
                    OnPropertyChanged(nameof(QuenchingArea));
                }
            }
        }

        private string _quenchingAreaFormula;

        /// <summary>
        /// Площадь тушения - формула расчета
        /// </summary>
        public string QuenchingAreaFormula
        {
            get => _quenchingAreaFormula;
            set
            {
                if (_quenchingAreaFormula != value)
                {
                    _quenchingAreaFormula = value;
                    OnPropertyChanged(nameof(QuenchingAreaFormula));
                }
            }
        }

        private double _workTime;

        /// <summary>
        /// Время работы
        /// </summary>
        public double WorkTime
        {
            get => _workTime;
            set
            {
                if (_workTime != value)
                {
                    _workTime = value;
                    OnPropertyChanged(nameof(WorkTime));
                }
            }
        }

        private string _workTimeFormula;

        /// <summary>
        /// Время работы - формула расчета
        /// </summary>
        public string WorkTimeFormula
        {
            get => _workTimeFormula;
            set
            {
                if (_workTimeFormula != value)
                {
                    _workTimeFormula = value;
                    OnPropertyChanged(nameof(WorkTimeFormula));
                }
            }
        }

        /// <summary>
        /// Расчет объема раствора
        /// </summary>
        private double GetLiquorVolume()
        {
            double Kv = 0;
            if (Multiplicity == Data.MultiplicityType.Low)
            {
                Kv = Data.QuenchingSolutionRepository.KvLow;
            }
            else if (Multiplicity == Data.MultiplicityType.Medium)
            {
                Kv = Data.QuenchingSolutionRepository.KvMedium;
            }
            if (ConditionMore == ">")
            {
                LiquorVolumeFormula = VolumePO + " * " + Kv + " + " + VolumePO;
                return VolumePO * Kv + VolumePO;
            }
            else if (ConditionMore == "<")
            {
                LiquorVolumeFormula = CisternVolume + " / " + Kv + " + " + CisternVolume;
                return CisternVolume / Kv + CisternVolume;
            }
            else
            {
                LiquorVolumeFormula = "";
                return 0;
            }
        }

        /// <summary>
        /// Расчет объема ВМП
        /// </summary>
        private double GetVolumeVMP()
        {
            VolumeVMPFormula = LiquorVolume + " * " + FoamMultiplicity;
            return LiquorVolume * FoamMultiplicity;
        }

        /// <summary>
        /// Расчет площади тушения
        /// </summary>
        private double GetQuenchingArea()
        {
            double Itr = 0;
            if (LiquorType == Data.LiquorType.LVJ)
            {
                Itr = Data.QuenchingSolutionRepository.ItrLVJ;
            }
            else if (LiquorType == Data.LiquorType.GJ)
            {
                Itr = Data.QuenchingSolutionRepository.ItrGJ;
            }
            if (Itr != 0)
            {
                QuenchingAreaFormula = "(" + LiquorVolume + " - " + HoseCount + " * " + HoseVolume + ") / (" + Itr + " * " + EstimateTime + " * 60)";
                return (LiquorVolume - HoseCount * HoseVolume) / (Itr * EstimateTime * 60);
            }
            QuenchingAreaFormula = "";
            return 0;

        }

        /// <summary>
        /// Расчет времени работы
        /// </summary>
        private double GetWorkTime()
        {
            if (BarrelsCount != 0)
            {
                WorkTimeFormula = "(" + LiquorVolume + " - " + HoseCount + " * " + HoseVolume + ") / (" + BarrelsCount + " * " + Сonsumption + " * 60)";
                return (LiquorVolume - HoseCount * HoseVolume) / (BarrelsCount * Сonsumption * 60);
            }
            else
            {
                WorkTimeFormula = "";
                return 0;
            } 
        }
        public override void UpdateParams()
        {
            //Расчетные данные
            LiquorVolume = Math.Round(GetLiquorVolume(), 2);
            VolumeVMP = Math.Round(GetVolumeVMP(), 2);
            QuenchingArea = Math.Round(GetQuenchingArea(), 2);
            WorkTime = Math.Round(GetWorkTime(), 2);
        }

        protected override void Init()
        {
            ConditionMore = "=";
        }
        public QuenchingSolution() : base()
        {

        }
        public QuenchingSolution(Document document) : base(document)
        {

        }
    }
}
