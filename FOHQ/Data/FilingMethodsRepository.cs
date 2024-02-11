using System.Collections.ObjectModel;

namespace FOHQ.Data
{
    /// <summary>
    /// Репозиторий для методов подачи ОВ к месту пожара
    /// </summary>
    public class FilingMethodsRepository : BaseRepository
    {
        /// <summary>
        /// Сопротивление одного пожарного рукава
        /// </summary>
        public const double FireHoseResistance = 0.015;
        /// <summary>
        /// Напор у ствола 
        /// </summary>
        public const double PressureAtStem = 40;
        /// <summary>
        /// Напор на входе в насос следующей ступени перекачки
        /// </summary>
        public const double PressureAtPumpInlet = 10;

        /// <summary>
        /// Напор на насосе
        /// </summary>
        private readonly ObservableCollection<int> _pressureOnPumpList = new ObservableCollection<int>();
        /// <summary>
        /// Напор на насосе
        /// </summary>
        public ObservableCollection<int> PressureOnPumpList { get { return _pressureOnPumpList; } }

        /// <summary>
        /// Напор на насосе
        /// </summary>
        private readonly ObservableCollection<AscentDescentTypeItem> _ascentDescentTypeList = new ObservableCollection<AscentDescentTypeItem>();
        /// <summary>
        /// Напор на насосе
        /// </summary>
        public ObservableCollection<AscentDescentTypeItem> AscentDescentTypeList { get { return _ascentDescentTypeList; } }

        public FilingMethodsRepository() : base()
        {
            _pressureOnPumpList.Add(100);
            _pressureOnPumpList.Add(90);
            _pressureOnPumpList.Add(80);

            _ascentDescentTypeList.Add(new AscentDescentTypeItem() { Type = FOHQ.Data.AscentDescentType.Ascent, Name = "Подъем" });
            _ascentDescentTypeList.Add(new AscentDescentTypeItem() { Type = FOHQ.Data.AscentDescentType.Descent, Name = "Спуск" });

        }
    }


    /// <summary>
    /// Методы подачи воды
    /// </summary>
    public enum SupplyMethodType
    {
        None = 0,
        /// <summary>
        /// Подвоз воды
        /// </summary>
        WaterSupplyMethod = 1,
        /// <summary>
        /// Подача воды в перекачку
        /// </summary>
        WaterSupplyToPumpMethod = 2
    }

    /// <summary>
    /// Подъем\спуск
    /// </summary>
    public enum AscentDescentType
    {
        /// <summary>
        /// Подъем
        /// </summary>
        Ascent = 0,
        /// <summary>
        /// Спуск
        /// </summary>
        Descent = 1
    }

    /// <summary>
    /// Подъем\спуск
    /// </summary>
    public class AscentDescentTypeItem
    {
        /// <summary>
        /// Тип 
        /// </summary>
        public AscentDescentType Type { get; set; }
        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; set; }
    }
}
