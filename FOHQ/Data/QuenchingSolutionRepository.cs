using System.Collections.Generic;

namespace FOHQ.Data
{
    /// <summary>
    /// Репозиторий для Тушение ЛВЖ и ГЖ (по раствору)
    /// </summary>
    public class QuenchingSolutionRepository : BaseRepository
    {
        /// <summary>
        /// Коэффициент для кратности ПО - низкая
        /// </summary>
        public const double KvLow = 24;
        /// <summary>
        /// Коэффициент для кратности ПО - средня
        /// </summary>
        public const double KvMedium = 15.7;
        /// <summary>
        /// требуемая интенсивность подачи раствора ПО на тушение для ЛВЖ
        /// </summary>
        public const double ItrLVJ = 0.08;
        /// <summary>
        /// требуемая интенсивность подачи раствора ПО на тушение для ГЖ
        /// </summary>
        public const double ItrGJ = 0.05;

        /// <summary>
        /// Список диаметров рукавов
        /// </summary>
        private readonly List<DiameterHoseItem> _diameterHoseLIst = new List<DiameterHoseItem>();
        /// <summary>
        /// Пенные стволы низкой кратности
        /// </summary>
        private readonly List<MultiplicityFoamBarrelItem> _foamBarrelLowList = new List<MultiplicityFoamBarrelItem>();
        /// <summary>
        /// Пенные стволы средней кратности
        /// </summary>
        private readonly List<MultiplicityFoamBarrelItem> _foamBarrelMediumList = new List<MultiplicityFoamBarrelItem>();

        /// <summary>
        /// Список диаметров рукавов
        /// </summary>
        public List<DiameterHoseItem> DiameterHoseLIst { get { return _diameterHoseLIst; } }
        /// <summary>
        /// Пенные стволы низкой кратности
        /// </summary>
        public List<MultiplicityFoamBarrelItem> FoamBarrelLowList { get { return _foamBarrelLowList; } }
        /// <summary>
        /// Пенные стволы средней кратности
        /// </summary>
        public List<MultiplicityFoamBarrelItem> FoamBarrelMediumList { get { return _foamBarrelMediumList; } }

        public QuenchingSolutionRepository() : base()
        {

            _diameterHoseLIst.Add(new DiameterHoseItem() { Diameter = 38, Volume = 22 });
            _diameterHoseLIst.Add(new DiameterHoseItem() { Diameter = 51, Volume = 40 });
            _diameterHoseLIst.Add(new DiameterHoseItem() { Diameter = 66, Volume = 70 });
            _diameterHoseLIst.Add(new DiameterHoseItem() { Diameter = 77, Volume = 90 });
            _diameterHoseLIst.Add(new DiameterHoseItem() { Diameter = 150, Volume = 350 });

            _foamBarrelLowList.Add(new MultiplicityFoamBarrelItem() { Device = "СВП", Сonsumption = 6, Multiplicity = 8 });
            _foamBarrelLowList.Add(new MultiplicityFoamBarrelItem() { Device = "СВП-2", Сonsumption = 4, Multiplicity = 8 });
            _foamBarrelLowList.Add(new MultiplicityFoamBarrelItem() { Device = "СВП-4", Сonsumption = 8, Multiplicity = 8 });
            _foamBarrelLowList.Add(new MultiplicityFoamBarrelItem() { Device = "СВП-8", Сonsumption = 16, Multiplicity = 8 });
            _foamBarrelLowList.Add(new MultiplicityFoamBarrelItem() { Device = "ПЛСК-20П", Сonsumption = 20, Multiplicity = 10 });
            _foamBarrelLowList.Add(new MultiplicityFoamBarrelItem() { Device = "ПЛСК-60С", Сonsumption = 60, Multiplicity = 10 });

            _foamBarrelMediumList.Add(new MultiplicityFoamBarrelItem() { Device = "ГПС-200", Сonsumption = 2, Multiplicity = 100 });
            _foamBarrelMediumList.Add(new MultiplicityFoamBarrelItem() { Device = "ГПС - 600", Сonsumption = 6, Multiplicity = 100 });
            _foamBarrelMediumList.Add(new MultiplicityFoamBarrelItem() { Device = "ГПС-2000", Сonsumption = 20, Multiplicity = 100 });
            _foamBarrelMediumList.Add(new MultiplicityFoamBarrelItem() { Device = "УКТП Пурга-2", Сonsumption = 2, Multiplicity = 70 });
            _foamBarrelMediumList.Add(new MultiplicityFoamBarrelItem() { Device = "УКТП Пурга-5", Сonsumption = 5, Multiplicity = 70 });
            _foamBarrelMediumList.Add(new MultiplicityFoamBarrelItem() { Device = "УКТП Пурга-7", Сonsumption = 7, Multiplicity = 70 });
            _foamBarrelMediumList.Add(new MultiplicityFoamBarrelItem() { Device = "УКТП Пурга-10", Сonsumption = 10, Multiplicity = 70 });
            _foamBarrelMediumList.Add(new MultiplicityFoamBarrelItem() { Device = "УКТП Пурга-20", Сonsumption = 20, Multiplicity = 40 });
            _foamBarrelMediumList.Add(new MultiplicityFoamBarrelItem() { Device = "УКТП Пурга-30", Сonsumption = 30, Multiplicity = 40 });
        }

        public string GetMultiplicityStr(MultiplicityType type)
        {
            if (type == MultiplicityType.Low)
            {
                return "Низкая";
            }
            else if (type == MultiplicityType.Medium)
            {
                return "Средняя";
            }
            else
            {
                return "";
            }
        }

        public string GetLiquorTypeStr(LiquorType type)
        {
            if (type == LiquorType.LVJ)
            {
                return "ЛВЖ";
            }
            else if (type == LiquorType.GJ)
            {
                return "ГЖ";
            }
            else
            {
                return "";
            }
        }


    }

    /// <summary>
    /// Тип кратности ПО
    /// </summary>
    public enum MultiplicityType
    {
        None = 0,
        /// <summary>
        /// Низкая
        /// </summary>
        Low = 1,
        /// <summary>
        /// Средняя
        /// </summary>
        Medium = 2
    }

    /// <summary>
    /// Тип раствора
    /// </summary>
    public enum LiquorType
    {
        None = 0,
        /// <summary>
        /// ЛВЖ
        /// </summary>
        LVJ = 1,
        /// <summary>
        /// ГЖ
        /// </summary>
        GJ = 2
    }

    /// <summary>
    /// Диаметр рукава
    /// </summary>
    public class DiameterHoseItem
    {
        /// <summary>
        /// Диаметр рукава
        /// </summary>
        public int Diameter { get; set; }
        /// <summary>
        /// Объем
        /// </summary>
        public int Volume { get; set; }
    }

    /// <summary>
    /// Элемент - пенный ствол
    /// </summary>
    public class MultiplicityFoamBarrelItem
    {
        /// <summary>
        /// Наименование
        /// </summary>
        public string Device { get; set; }
        /// <summary>
        /// Расход
        /// </summary>
        public int Сonsumption { get; set; }
        /// <summary>
        /// Кратность пены
        /// </summary>
        public int Multiplicity { get; set; }
    }
}
