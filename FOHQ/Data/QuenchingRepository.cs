using System.Collections.ObjectModel;

namespace FOHQ.Data
{
    /// <summary>
    /// Репозиторий для тушения
    /// </summary>
    public class QuenchingRepository : BaseRepository
    {
        /// <summary>
        /// Список форм пожара
        /// </summary>
        private readonly ObservableCollection<FormOfQuenching> _formOfQuenchingTypes = new ObservableCollection<FormOfQuenching>();
        /// <summary>
        /// Элемент - Пенный ствол
        /// </summary>
        private readonly ObservableCollection<QuenchingBarrel> _quenchingBarrelTypes = new ObservableCollection<QuenchingBarrel>();
        /// <summary>
        /// Список форм пожара
        /// </summary>
        public ObservableCollection<FormOfQuenching> FormOfQuenchingTypes { get { return _formOfQuenchingTypes; } }
        /// <summary>
        /// Элемент - Пенный ствол
        /// </summary>
        public ObservableCollection<QuenchingBarrel> QuenchingBarrelTypes { get { return _quenchingBarrelTypes; } }
        public QuenchingRepository() : base()
        {
            _quenchingBarrelTypes.Add(new QuenchingBarrel() { BarrelType = FOHQ.Data.QuenchingBarrelType.HandTrunk, Value = 5, Name = "Ручные стволы" });
            _quenchingBarrelTypes.Add(new QuenchingBarrel() { BarrelType = FOHQ.Data.QuenchingBarrelType.WaterCannon, Value = 10, Name = "Лафетные стволы" });
            _quenchingBarrelTypes.Add(new QuenchingBarrel() { BarrelType = FOHQ.Data.QuenchingBarrelType.None});

            _formOfQuenchingTypes.Add(new FormOfQuenching() { FormOfQuenchingType = FOHQ.Data.FormOfQuenchingTypes.Quadrant, Name = "Четверть круга" });
            _formOfQuenchingTypes.Add(new FormOfQuenching() { FormOfQuenchingType = FOHQ.Data.FormOfQuenchingTypes.Semicircle, Name = "Полукруг" });
            _formOfQuenchingTypes.Add(new FormOfQuenching() { FormOfQuenchingType = FOHQ.Data.FormOfQuenchingTypes.Cicle, Name = "Круг" });
            _formOfQuenchingTypes.Add(new FormOfQuenching() { FormOfQuenchingType = FOHQ.Data.FormOfQuenchingTypes.RectangleOneWay, Name = "Прямоугольник в 1 сторону", Value = 1 });
            _formOfQuenchingTypes.Add(new FormOfQuenching() { FormOfQuenchingType = FOHQ.Data.FormOfQuenchingTypes.RectangleTwoWayLeft, Name = "Прямоугольник в 2 стороны 1", Value = 2 });
            _formOfQuenchingTypes.Add(new FormOfQuenching() { FormOfQuenchingType = FOHQ.Data.FormOfQuenchingTypes.RectangleTwoWayRigt, Name = "Прямоугольник в 2 стороны 2", Value = 2 });
            _formOfQuenchingTypes.Add(new FormOfQuenching() { FormOfQuenchingType = FOHQ.Data.FormOfQuenchingTypes.RectangleThirdWayLeft, Name = "Прямоугольник в 3 стороны 1" });
            _formOfQuenchingTypes.Add(new FormOfQuenching() { FormOfQuenchingType = FOHQ.Data.FormOfQuenchingTypes.RectangleThirdWayRight, Name = "Прямоугольник в 3 стороны 2" });
            _formOfQuenchingTypes.Add(new FormOfQuenching() { FormOfQuenchingType = FOHQ.Data.FormOfQuenchingTypes.RectangleFourWay, Name = "Прямоугольник в 4 стороны" });
            _formOfQuenchingTypes.Add(new FormOfQuenching() { FormOfQuenchingType = FOHQ.Data.FormOfQuenchingTypes.None });

        }
        public static void Main()
        {

        }

        public QuenchingBarrel GetQuenchingBarrel(QuenchingBarrelType barrelType)
        {
            foreach (FOHQ.Data.QuenchingBarrel barrel in QuenchingBarrelTypes)
            {
                if (barrelType == barrel.BarrelType)
                {
                    return barrel;
                }
            }
            return null;
        }

        public FormOfQuenching GetFormOfQuenching(FormOfQuenchingTypes formOfQuenching)
        {
            foreach (FOHQ.Data.FormOfQuenching item in FormOfQuenchingTypes)
            {
                if (formOfQuenching == item.FormOfQuenchingType)
                {
                    return item;
                }
            }
            return null;
        }
    }

    /// <summary>
    /// Форма пожара
    /// </summary>
    public enum FormOfQuenchingTypes
    {
        None,
        Quadrant, //Четверть круга
        Semicircle, //Полукруг
        Cicle, //   "Круг"
        RectangleOneWay, //    "Прямоугольник в 1 сторону", n=1
        RectangleTwoWayLeft,//    "Прямоугольник в 2 стороны 1", n=2
        RectangleTwoWayRigt,//    "Прямоугольник в 2 стороны 2", n=2
        RectangleThirdWayLeft,//    "Прямоугольник в 3 стороны", n=3
        RectangleThirdWayRight,//    "Прямоугольник в 3 стороны", n=4
        RectangleFourWay  //    "Прямоугольник в 4 стороны"
    }

    /// <summary>
    /// Тип стволов
    /// </summary>
    public enum QuenchingBarrelType
    {
        None = 0,
        /// <summary>
        /// Ручные стволы
        /// </summary>
        HandTrunk = 1,  // h=5
        /// <summary>
        /// Лафетные стволы
        /// </summary>
        WaterCannon = 2 // h=10
    }

    /// <summary>
    /// Элемент - тип ствола
    /// </summary>
    public class QuenchingBarrel
    {
        /// <summary>
        /// Тип ствола
        /// </summary>
        public QuenchingBarrelType BarrelType { get; set; }
        /// <summary>
        /// Значение h
        /// </summary>
        public int Value { get; set; }
        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; set; }
    }

    /// <summary>
    /// Элемент - форма пожара
    /// </summary>
    public class FormOfQuenching
    {
        /// <summary>
        /// Форма пожара
        /// </summary>
        public FormOfQuenchingTypes FormOfQuenchingType { get; set; } // форма
        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Значение
        /// </summary>
        public int Value { get; set; }
    }
}
