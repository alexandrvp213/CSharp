using System.Collections.Generic;

namespace FOHQ.Data
{
    /// <summary>
    /// Репозиторий для количества приборов
    /// </summary>
    public class NumberOfDevicesRepository : BaseRepository
    {
        /// <summary>
        /// Коэффициент  разрушения пены
        /// </summary>
        public const double K3 = 3;
        /// <summary>
        /// Расчётное время тушения
        /// </summary>
        public const double Tr = 15;

        /// <summary>
        /// Водные стволы
        /// </summary>
        private readonly List<WaterBarrelItem> _waterBarrelList = new List<WaterBarrelItem>();
        /// <summary>
        /// Пенные стволы
        /// </summary>
        private readonly List<FoamBarrelItem> _foamBarrelList = new List<FoamBarrelItem>();
        /// <summary>
        /// Стволы на защиту
        /// </summary>
        private readonly List<BarrelForDefenseItem> _barrelForDefenseList = new List<BarrelForDefenseItem>();

        /// <summary>
        /// Способы тушения
        /// </summary>
        private readonly List<ExtinguishingMethodItem> _extinguishingMethodList = new List<ExtinguishingMethodItem>();

        /// <summary>
        /// Водные стволы
        /// </summary>
        public List<WaterBarrelItem> WaterBarrelList { get { return _waterBarrelList; } }
        /// <summary>
        /// Пенные стволы
        /// </summary>
        public List<FoamBarrelItem> FoamBarrelList { get { return _foamBarrelList; } }
        /// <summary>
        /// Стволы на защиту
        /// </summary>
        public List<BarrelForDefenseItem> BarrelForDefenseList { get { return _barrelForDefenseList; } }
        /// <summary>
        /// Способы тушения
        /// </summary>
        public List<ExtinguishingMethodItem> ExtinguishingMethodList { get { return _extinguishingMethodList; } }

        public NumberOfDevicesRepository() : base()
        {
            _extinguishingMethodList.Add(new ExtinguishingMethodItem() { Type = ExtinguishingMethodType.Water, Name = "по воде" });
            _extinguishingMethodList.Add(new ExtinguishingMethodItem() { Type = ExtinguishingMethodType.SurfaceQuenching, Name = "поверхностное тушение" });
            _extinguishingMethodList.Add(new ExtinguishingMethodItem() { Type = ExtinguishingMethodType.LVJ, Name = "ЛВЖ" });
            _extinguishingMethodList.Add(new ExtinguishingMethodItem() { Type = ExtinguishingMethodType.GJ, Name = "ГЖ" });
            _extinguishingMethodList.Add(new ExtinguishingMethodItem() { Type = ExtinguishingMethodType.BulkQuenching, Name = "объемное тушение" });

            _waterBarrelList.Add(new WaterBarrelItem() { Device = "УРСК-50-2", Consumption = 2 });
            _waterBarrelList.Add(new WaterBarrelItem() { Device = "УРСК-50-4", Consumption = 4 });
            _waterBarrelList.Add(new WaterBarrelItem() { Device = "УРСК-50-6", Consumption = 6 });
            _waterBarrelList.Add(new WaterBarrelItem() { Device = "УРСК-50-8", Consumption = 8 });
            _waterBarrelList.Add(new WaterBarrelItem() { Device = "УРСК-70-9", Consumption = 9 });
            _waterBarrelList.Add(new WaterBarrelItem() { Device = "УРСК-70-11", Consumption = 11 });
            _waterBarrelList.Add(new WaterBarrelItem() { Device = "УРСК-70-13", Consumption = 13 });
            _waterBarrelList.Add(new WaterBarrelItem() { Device = "УРСК-70-15", Consumption = 15 });
            _waterBarrelList.Add(new WaterBarrelItem() { Device = "РС-50", Consumption = 3.7 });
            _waterBarrelList.Add(new WaterBarrelItem() { Device = "РС-70", Consumption = 7.4 });
            _waterBarrelList.Add(new WaterBarrelItem() { Device = "СВД", Consumption = 2 });
            _waterBarrelList.Add(new WaterBarrelItem() { Device = "ПЛС", Consumption = 20 });
            _waterBarrelList.Add(new WaterBarrelItem() { Device = "ЛС-П15", Consumption = 15 });
            _waterBarrelList.Add(new WaterBarrelItem() { Device = "ЛС-П20", Consumption = 20 });
            _waterBarrelList.Add(new WaterBarrelItem() { Device = "ЛС-П25", Consumption = 25 });

            _foamBarrelList.Add(new FoamBarrelItem() { Device = "СВП", Consumption2 = 6, ConsumptionFoam = 3, LVJ = 75, GJ = 120 });
            _foamBarrelList.Add(new FoamBarrelItem() { Device = "СВП-2", Consumption2 = 4, ConsumptionFoam = 2, LVJ = 50, GJ = 80 });
            _foamBarrelList.Add(new FoamBarrelItem() { Device = "СВП-4", Consumption2 = 8, ConsumptionFoam = 4, LVJ = 100, GJ = 160 });
            _foamBarrelList.Add(new FoamBarrelItem() { Device = "СВП-8", Consumption2 = 16, ConsumptionFoam = 8, LVJ = 200, GJ = 320 });
            _foamBarrelList.Add(new FoamBarrelItem() { Device = "ГПС-200", Consumption2 = 2, ConsumptionFoam = 12, LVJ = 25, GJ = 40 });
            _foamBarrelList.Add(new FoamBarrelItem() { Device = "ГПС-600", Consumption2 = 6, ConsumptionFoam = 36, LVJ = 75, GJ = 120 });
            _foamBarrelList.Add(new FoamBarrelItem() { Device = "ГПС-2000", Consumption2 = 20, ConsumptionFoam = 120, LVJ = 250, GJ = 400 });
            _foamBarrelList.Add(new FoamBarrelItem() { Device = "УКТП Пурга-2", Consumption2 = 2, ConsumptionFoam = 10.5, LVJ = 25, GJ = 40 });
            _foamBarrelList.Add(new FoamBarrelItem() { Device = "УКТП Пурга-5", Consumption2 = 5, ConsumptionFoam = 25.5, LVJ = 62.5, GJ = 100 });
            _foamBarrelList.Add(new FoamBarrelItem() { Device = "УКТП Пурга-7", Consumption2 = 7, ConsumptionFoam = 28, LVJ = 87.5, GJ = 140 });
            _foamBarrelList.Add(new FoamBarrelItem() { Device = "УКТП Пурга-10", Consumption2 = 10, ConsumptionFoam = 56, LVJ = 125, GJ = 200 });
            _foamBarrelList.Add(new FoamBarrelItem() { Device = "УКТП Пурга-20", Consumption2 = 20, ConsumptionFoam = 64, LVJ = 250, GJ = 400 });
            _foamBarrelList.Add(new FoamBarrelItem() { Device = "УКТП Пурга-30", Consumption2 = 20, ConsumptionFoam = 72, LVJ = 375, GJ = 600 });
            _foamBarrelList.Add(new FoamBarrelItem() { Device = "ПЛСК-20П", Consumption2 = 20, ConsumptionFoam = 12, LVJ = 250, GJ = 400 });
            _foamBarrelList.Add(new FoamBarrelItem() { Device = "ПЛСК-60С", Consumption2 = 60, ConsumptionFoam = 30, LVJ = 750, GJ = 1200 });

            _barrelForDefenseList.Add(new BarrelForDefenseItem() { Device = "УРСК-50-2", Consumption3 = 2 });
            _barrelForDefenseList.Add(new BarrelForDefenseItem() { Device = "УРСК-50-4", Consumption3 = 4 });
            _barrelForDefenseList.Add(new BarrelForDefenseItem() { Device = "УРСК-50-6", Consumption3 = 6 });
            _barrelForDefenseList.Add(new BarrelForDefenseItem() { Device = "УРСК-50-8", Consumption3 = 8 });
            _barrelForDefenseList.Add(new BarrelForDefenseItem() { Device = "УРСК-70-9", Consumption3 = 9 });
            _barrelForDefenseList.Add(new BarrelForDefenseItem() { Device = "УРСК-70-11", Consumption3 = 11 });
            _barrelForDefenseList.Add(new BarrelForDefenseItem() { Device = "УРСК-70-13", Consumption3 = 13 });
            _barrelForDefenseList.Add(new BarrelForDefenseItem() { Device = "УРСК-70-15", Consumption3 = 15 });
            _barrelForDefenseList.Add(new BarrelForDefenseItem() { Device = "РС-50", Consumption3 = 3.7 });
            _barrelForDefenseList.Add(new BarrelForDefenseItem() { Device = "РС-70", Consumption3 = 7.4 });
            _barrelForDefenseList.Add(new BarrelForDefenseItem() { Device = "СВД", Consumption3 = 2 });
            _barrelForDefenseList.Add(new BarrelForDefenseItem() { Device = "ПЛС", Consumption3 = 20 });
            _barrelForDefenseList.Add(new BarrelForDefenseItem() { Device = "ЛС-П15", Consumption3 = 15 });
            _barrelForDefenseList.Add(new BarrelForDefenseItem() { Device = "ЛС-П20", Consumption3 = 20 });
            _barrelForDefenseList.Add(new BarrelForDefenseItem() { Device = "ЛС-П25", Consumption3 = 25 });
        }

        public ExtinguishingMethodItem GetExtinguishingMethod(ExtinguishingMethodType type)
        {
            foreach (FOHQ.Data.ExtinguishingMethodItem item in ExtinguishingMethodList)
            {
                if (type == item.Type)
                {
                    return item;
                }
            }
            return null;
        }
    }

    /// <summary>
    /// Способы тушения
    /// </summary>
    public enum ExtinguishingMethodType
    {
        /// <summary>
        /// по воде
        /// </summary>
        Water,
        /// <summary>
        /// поверхностное тушение
        /// </summary>
        SurfaceQuenching, //
        /// <summary>
        /// объемное тушение
        /// </summary>
        BulkQuenching,
        /// <summary>
        /// ЛВЖ
        /// </summary>
        LVJ,
        /// <summary>
        /// ГЖ
        /// </summary>
        GJ,
        None
    }

    /// <summary>
    /// Элемент - Способ тушения
    /// </summary>
    public class ExtinguishingMethodItem
    {
        public ExtinguishingMethodType Type { get; set; } // Способ тушения
        public string Name { get; set; } // Наименование
    }

    /// <summary>
    /// Элемент - Водный ствол
    /// </summary>
    public class WaterBarrelItem
    {
        public string Device { get; set; } // Водный ствол
        public double Consumption { get; set; } // Расход
    }

    /// <summary>
    /// Элемент - Пенный ствол
    /// </summary>
    public class FoamBarrelItem
    {
        public string Device { get; set; } // Пенный ствол
        public double Consumption2 { get; set; } // Расход 2
        public double ConsumptionFoam { get; set; } // Расход по пене
        public double LVJ { get; set; } // ЛВЖ
        public double GJ { get; set; } // ГЖ
    }

    /// <summary>
    /// Элемент - Ствол на защиту
    /// </summary>
    public class BarrelForDefenseItem
    {
        public string Device { get; set; } // Ствол на защиту
        public double Consumption3 { get; set; } // Расход 3
    }
}
