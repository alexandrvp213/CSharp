using System.Collections.Generic;

namespace FOHQ.Data
{
    /// <summary>
    /// Репозиторий для Требуемый расход на защиту и тушение
    /// </summary>
    public class RequiredConsumptionRepository : BaseRepository
    {

        private readonly List<SectionByObjects> sectionByObjectsList = new List<SectionByObjects>();
        private readonly List<SectionByDestinations> sectionDestinationsBuildings = new List<SectionByDestinations>();
        private readonly List<SectionByDestinations> sectionDestinationsTransport = new List<SectionByDestinations>();
        private readonly List<SectionByDestinations> sectionDestinationsMaterials = new List<SectionByDestinations>();
        private readonly List<SectionByDestinations> sectionDestinationsLiquids = new List<SectionByDestinations>();

        /// <summary>
        /// Список разделов по виду объекта
        /// </summary>
        public List<SectionByObjects> SectionByObjectsList { get { return sectionByObjectsList; } }
        /// <summary>
        /// Список разделов по назначению - Здания и сооружения
        /// </summary>
        public List<SectionByDestinations> SectionDestinationsBuildings { get { return sectionDestinationsBuildings; } }
        /// <summary>
        /// Список разделов по назначению - Транспортные средства
        /// </summary>
        public List<SectionByDestinations> SectionDestinationsTransport { get { return sectionDestinationsTransport; } }
        /// <summary>
        /// Список разделов по назначению - Твердые материалы
        /// </summary>
        public List<SectionByDestinations> SectionDestinationsMaterials { get { return sectionDestinationsMaterials; } }
        /// <summary>
        /// Список разделов по назначению - Легковоспламеняющиеся и горючие жидкости
        /// </summary>
        public List<SectionByDestinations> SectionDestinationsLiquids { get { return sectionDestinationsLiquids; } }
        public RequiredConsumptionRepository() : base()
        {

            sectionByObjectsList.Add(new SectionByObjects() { Section = FOHQ.Data.SectionByObjectsType.Buildings, Name = "Здания и сооружения" });
            sectionByObjectsList.Add(new SectionByObjects() { Section = FOHQ.Data.SectionByObjectsType.Transport, Name = "Транспортные средства" });
            sectionByObjectsList.Add(new SectionByObjects() { Section = FOHQ.Data.SectionByObjectsType.Materials, Name = "Твердые материалы" });
            sectionByObjectsList.Add(new SectionByObjects() { Section = FOHQ.Data.SectionByObjectsType.Liquids, Name = "Легковоспламеняющиеся и горючие жидкости" });

            sectionDestinationsBuildings.Add(new SectionByDestinations() { Name = "Административные здания 1-3 степени огнестойкости", Indicator = 0.06, GroupName = "Административные здания" });
            sectionDestinationsBuildings.Add(new SectionByDestinations() { Name = "Административные здания 4 степень огнестойкости", Indicator = 0.10, GroupName = "Административные здания" });
            sectionDestinationsBuildings.Add(new SectionByDestinations() { Name = "Административные здания 5 степень огнестойкости", Indicator = 0.15, GroupName = "Административные здания" });
            sectionDestinationsBuildings.Add(new SectionByDestinations() { Name = "Административные здания Подвальные помещения", Indicator = 0.10, GroupName = "Административные здания" });
            sectionDestinationsBuildings.Add(new SectionByDestinations() { Name = "Административные здания Чердачные помещения", Indicator = 0.10, GroupName = "Административные здания" });


            sectionDestinationsBuildings.Add(new SectionByDestinations() { Name = "Ангары, гаражи, мастерские, трамвайные и троллейбусные депо", Indicator = 0.20, GroupName = "Ангары, гаражи, мастерские, трамвайные и троллейбусные депо" });
            sectionDestinationsBuildings.Add(new SectionByDestinations() { Name = "Больницы", Indicator = 0.10, GroupName = "Больницы" });

            sectionDestinationsBuildings.Add(new SectionByDestinations() { Name = "Жилые дома и подсобные постройки 1-3 степени огнестойкости", Indicator = 0.06, GroupName = "Жилые дома и подсобные постройки" });
            sectionDestinationsBuildings.Add(new SectionByDestinations() { Name = "Жилые дома и подсобные постройки 4 степень огнестойкости", Indicator = 0.10, GroupName = "Жилые дома и подсобные постройки" });
            sectionDestinationsBuildings.Add(new SectionByDestinations() { Name = "Жилые дома и подсобные постройки 5 степень огнестойкости", Indicator = 0.15, GroupName = "Жилые дома и подсобные постройки" });
            sectionDestinationsBuildings.Add(new SectionByDestinations() { Name = "Жилые дома и подсобные постройки Подвальные помещения", Indicator = 0.15, GroupName = "Жилые дома и подсобные постройки" });
            sectionDestinationsBuildings.Add(new SectionByDestinations() { Name = "Жилые дома и подсобные постройки Чердачные помещения", Indicator = 0.15, GroupName = "Жилые дома и подсобные постройки" });

            sectionDestinationsBuildings.Add(new SectionByDestinations() { Name = "Животноводческие здания 1-3 степени огнестойкости", Indicator = 0.10, GroupName = "Животноводческие здания" });
            sectionDestinationsBuildings.Add(new SectionByDestinations() { Name = "Животноводческие здания 4 степень огнестойкости", Indicator = 0.15, GroupName = "Животноводческие здания" });
            sectionDestinationsBuildings.Add(new SectionByDestinations() { Name = "Животноводческие здания 5 степень огнестойкости", Indicator = 0.20, GroupName = "Животноводческие здания" });

            sectionDestinationsBuildings.Add(new SectionByDestinations() { Name = "Культурно - зрелищные учреждения(театры, кинотеатры, клубы, дворцы культуры) Сцена", Indicator = 0.20, GroupName = "Культурно - зрелищные учреждения(театры, кинотеатры, клубы, дворцы культуры)" });
            sectionDestinationsBuildings.Add(new SectionByDestinations() { Name = "Культурно - зрелищные учреждения(театры, кинотеатры, клубы, дворцы культуры) Зрительный зал", Indicator = 0.15, GroupName = "Культурно - зрелищные учреждения(театры, кинотеатры, клубы, дворцы культуры)" });
            sectionDestinationsBuildings.Add(new SectionByDestinations() { Name = "Культурно - зрелищные учреждения(театры, кинотеатры, клубы, дворцы культуры) Подсобные помещения", Indicator = 0.15, GroupName = "Культурно - зрелищные учреждения(театры, кинотеатры, клубы, дворцы культуры)" });

            sectionDestinationsBuildings.Add(new SectionByDestinations() { Name = "Мельницы и элеваторы", Indicator = 0.14, GroupName = "Мельницы и элеваторы" });

            sectionDestinationsBuildings.Add(new SectionByDestinations() { Name = "Участки и цехи с категорией производства в зданиях 1-2 степени огнестойкости", Indicator = 0.15, ExtraGroupName = "Участки и цехи с категорией производства в зданиях", GroupName = "Производственные здания" });
            sectionDestinationsBuildings.Add(new SectionByDestinations() { Name = "Участки и цехи с категорией производства в зданиях 3 степень огнестойкости", Indicator = 0.20, ExtraGroupName = "Участки и цехи с категорией производства в зданиях", GroupName = "Производственные здания" });
            sectionDestinationsBuildings.Add(new SectionByDestinations() { Name = "Участки и цехи с категорией производства в зданиях 4-5 степень огнестойкости", Indicator = 0.25, ExtraGroupName = "Участки и цехи с категорией производства в зданиях", GroupName = "Производственные здания" });
            sectionDestinationsBuildings.Add(new SectionByDestinations() { Name = "Производственные здания Окрасочные цехи", Indicator = 0.20, GroupName = "Производственные здания" });
            sectionDestinationsBuildings.Add(new SectionByDestinations() { Name = "Производственные здания Подвальные помещения", Indicator = 0.30, GroupName = "Производственные здания" });
            sectionDestinationsBuildings.Add(new SectionByDestinations() { Name = "Производственные здания Чердачные помещения", Indicator = 0.15, GroupName = "Производственные здания" });
            sectionDestinationsBuildings.Add(new SectionByDestinations() { Name = "Сгораемые покрытия больших площадей в производственных зданиях При тушении снизу внутри здания", Indicator = 0.15, ExtraGroupName = "Сгораемые покрытия больших площадей в производственных зданиях:", GroupName = "Производственные здания" });
            sectionDestinationsBuildings.Add(new SectionByDestinations() { Name = "Сгораемые покрытия больших площадей в производственных зданиях При тушении снаружи со стороны покрытия", Indicator = 0.08, ExtraGroupName = "Сгораемые покрытия больших площадей в производственных зданиях:", GroupName = "Производственные здания" });
            sectionDestinationsBuildings.Add(new SectionByDestinations() { Name = "Сгораемые покрытия больших площадей в производственных зданиях При резвившемся пожаре", Indicator = 0.15, ExtraGroupName = "Сгораемые покрытия больших площадей в производственных зданиях:", GroupName = "Производственные здания" });

            sectionDestinationsBuildings.Add(new SectionByDestinations() { Name = "Строящиеся здания", Indicator = 0.10, GroupName = "Строящиеся здания" });

            sectionDestinationsBuildings.Add(new SectionByDestinations() { Name = "Торговые предприятия и склады товарно-материальных ценностей", Indicator = 0.20, GroupName = "Торговые предприятия и склады товарно-материальных ценностей" });

            sectionDestinationsBuildings.Add(new SectionByDestinations() { Name = "Холодильники", Indicator = 0.10, GroupName = "Холодильники" });

            sectionDestinationsBuildings.Add(new SectionByDestinations() { Name = "Электростанции и подстанции Кабельные туннели и полуэтажи(подача распыленной воды)", Indicator = 0.20, GroupName = "Электростанции и подстанции" });
            sectionDestinationsBuildings.Add(new SectionByDestinations() { Name = "Электростанции и подстанции Машинные залы и котельные отделения", Indicator = 0.20, GroupName = "Электростанции и подстанции" });
            sectionDestinationsBuildings.Add(new SectionByDestinations() { Name = "Электростанции и подстанции Галереи топливоподачи", Indicator = 0.10, GroupName = "Электростанции и подстанции" });
            sectionDestinationsBuildings.Add(new SectionByDestinations() { Name = "Электростанции и подстанции Трансформаторы, реакторы, масляные выключатели(подача распыленной воды)", Indicator = 0.10, GroupName = "Электростанции и подстанции" });

            sectionDestinationsTransport.Add(new SectionByDestinations() { Name = "Автомобили, трамваи, троллейбусы на открытых стоянках", Indicator = 0.10, GroupName = "Автомобили, трамваи, троллейбусы на открытых стоянках" });
            sectionDestinationsTransport.Add(new SectionByDestinations() { Name = "Самолеты и вертолеты Внутренняя отделка (при подаче распыленной воды)", Indicator = 0.08, GroupName = "Самолеты и вертолеты" });
            sectionDestinationsTransport.Add(new SectionByDestinations() { Name = "Самолеты и вертолеты Конструкции с наличием магниевых сплавов", Indicator = 0.25, GroupName = "Самолеты и вертолеты" });
            sectionDestinationsTransport.Add(new SectionByDestinations() { Name = "Самолеты и вертолеты Корпус ", Indicator = 0.15, GroupName = "Самолеты и вертолеты" });

            sectionDestinationsTransport.Add(new SectionByDestinations() { Name = "Суда (сухогрузные и пассажирские) Надстройки (пожары внутренние и наружные) при подаче цельных и распыленных струй ", Indicator = 0.20, GroupName = "Суда (сухогрузные и пассажирские)" });
            sectionDestinationsTransport.Add(new SectionByDestinations() { Name = "Суда (сухогрузные и пассажирские) Трюмы", Indicator = 0.20, GroupName = "Суда (сухогрузные и пассажирские)" });

            sectionDestinationsMaterials.Add(new SectionByDestinations() { Name = "Бумага разрыхленная", Indicator = 0.30, GroupName = "Бумага разрыхленная" });
            sectionDestinationsMaterials.Add(new SectionByDestinations() { Name = "Древесина Балансовая, при влажности, %: 40-50", Indicator = 0.20, GroupName = "Древесина", ExtraGroupName = "Балансовая, при влажности, %:" });
            sectionDestinationsMaterials.Add(new SectionByDestinations() { Name = "Древесина Балансовая, при влажности, %: Менее 40", Indicator = 0.50, GroupName = "Древесина", ExtraGroupName = "Балансовая, при влажности, %:" });
            sectionDestinationsMaterials.Add(new SectionByDestinations() { Name = "Древесина Пиломатериалы в штабелях в пределах 1 группы при влажности, %: 8-14 ", Indicator = 0.45, GroupName = "Древесина", ExtraGroupName = "Пиломатериалы в штабелях в пределах 1 группы при влажности, %:" });
            sectionDestinationsMaterials.Add(new SectionByDestinations() { Name = "Древесина Пиломатериалы в штабелях в пределах 1 группы при влажности, %: 20-30", Indicator = 0.30, GroupName = "Древесина", ExtraGroupName = "Пиломатериалы в штабелях в пределах 1 группы при влажности, %:" });
            sectionDestinationsMaterials.Add(new SectionByDestinations() { Name = "Древесина Пиломатериалы в штабелях в пределах 1 группы при влажности, %: Свыше 30 ", Indicator = 0.20, GroupName = "Древесина", ExtraGroupName = "Пиломатериалы в штабелях в пределах 1 группы при влажности, %:" });
            sectionDestinationsMaterials.Add(new SectionByDestinations() { Name = "Древесина Круглый лес в штабелях в пределах одной группы", Indicator = 0.35, GroupName = "Древесина", ExtraGroupName = "" });
            sectionDestinationsMaterials.Add(new SectionByDestinations() { Name = "Древесина Щепа в кучах с влажностью 30", Indicator = 0.10, GroupName = "Древесина", ExtraGroupName = "" });

            sectionDestinationsMaterials.Add(new SectionByDestinations() { Name = "Каучук(натуральный или искусственный), резина и резинотехнические изделия", Indicator = 0.30, GroupName = "Каучук(натуральный или искусственный), резина и резинотехнические изделия", ExtraGroupName = "" });
            sectionDestinationsMaterials.Add(new SectionByDestinations() { Name = "Льнокостра в отвалах(подача распыленной воды)", Indicator = 0.20, GroupName = "Льнокостра в отвалах(подача распыленной воды)", ExtraGroupName = "" });
            sectionDestinationsMaterials.Add(new SectionByDestinations() { Name = "Льнотреста(скирды, тюки)", Indicator = 0.25, GroupName = "Льнотреста(скирды, тюки)", ExtraGroupName = "" });

            sectionDestinationsMaterials.Add(new SectionByDestinations() { Name = "Пластмассы Термопласты", Indicator = 0.14, GroupName = "Пластмассы", ExtraGroupName = "" });
            sectionDestinationsMaterials.Add(new SectionByDestinations() { Name = "Пластмассы Реактопласты", Indicator = 0.10, GroupName = "Пластмассы", ExtraGroupName = "" });
            sectionDestinationsMaterials.Add(new SectionByDestinations() { Name = "Пластмассы Полимерные материалы и изделия из них", Indicator = 0.20, GroupName = "Пластмассы", ExtraGroupName = "" });
            sectionDestinationsMaterials.Add(new SectionByDestinations() { Name = "Пластмассы Текстолит, карболит, отходы пластмасс, триацетатная пленка", Indicator = 0.30, GroupName = "Пластмассы", ExtraGroupName = "" });

            sectionDestinationsMaterials.Add(new SectionByDestinations() { Name = "Торф на фрезерных полях влажностью 15-30 % (при удельном расходе воды 110-140 л/м2 и времени тушения 20 мин)", Indicator = 0.10, GroupName = "Торф на фрезерных полях влажностью 15-30 % (при удельном расходе воды 110-140 л/м2 и времени тушения 20 мин)", ExtraGroupName = "" });
            sectionDestinationsMaterials.Add(new SectionByDestinations() { Name = "Торф фрезерный в штабелях(при удельном расходе воды 235 л/м2 и времени тушения 20 мин)", Indicator = 0.20, GroupName = "Торф фрезерный в штабелях(при удельном расходе воды 235 л/м2 и времени тушения 20 мин)", ExtraGroupName = "" });
            sectionDestinationsMaterials.Add(new SectionByDestinations() { Name = "Хлопок и другие волокнистые материалы Открытые склады", Indicator = 0.20, GroupName = "Хлопок и другие волокнистые материалы", ExtraGroupName = "" });
            sectionDestinationsMaterials.Add(new SectionByDestinations() { Name = "Хлопок и другие волокнистые материалы Закрытые склады", Indicator = 0.30, GroupName = "Хлопок и другие волокнистые материалы", ExtraGroupName = "" });
            sectionDestinationsMaterials.Add(new SectionByDestinations() { Name = "Целлулоид и изделия из него", Indicator = 0.40, GroupName = "Целлулоид и изделия из него", ExtraGroupName = "" });
            sectionDestinationsMaterials.Add(new SectionByDestinations() { Name = "Ядохимикаты и удобрения", Indicator = 0.20, GroupName = "Ядохимикаты и удобрения", ExtraGroupName = "" });

            sectionDestinationsLiquids.Add(new SectionByDestinations() { Name = "Ацетон", Indicator = 0.40, GroupName = "Ацетон", ExtraGroupName = "" });
            sectionDestinationsLiquids.Add(new SectionByDestinations() { Name = "Нефтепродукты в емкостях С температурой вспышки ниже 28 °C ", Indicator = 0.40, GroupName = "Нефтепродукты в емкостях", ExtraGroupName = "" });
            sectionDestinationsLiquids.Add(new SectionByDestinations() { Name = "Нефтепродукты в емкостях С температурой вспышки 28 - 60 °C ", Indicator = 0.30, GroupName = "Нефтепродукты в емкостях", ExtraGroupName = "" });
            sectionDestinationsLiquids.Add(new SectionByDestinations() { Name = "Нефтепродукты в емкостях С температурой вспышки более 60 °C", Indicator = 0.20, GroupName = "Нефтепродукты в емкостях", ExtraGroupName = "" });
            sectionDestinationsLiquids.Add(new SectionByDestinations() { Name = "Горючая жидкость, разлившаяся на поверхности площадки, в траншеях и технологических лотках", Indicator = 0.20, GroupName = "Горючая жидкость, разлившаяся на поверхности площадки, в траншеях и технологических лотках", ExtraGroupName = "" });
            sectionDestinationsLiquids.Add(new SectionByDestinations() { Name = "Термоизоляция, пропитанная нефтепродуктами", Indicator = 0.20, GroupName = "Термоизоляция, пропитанная нефтепродуктами", ExtraGroupName = "" });
            sectionDestinationsLiquids.Add(new SectionByDestinations() { Name = "Спирты (этиловый, метиловый, пропиловый, бутиловый и др.) на складах и спиртзаводах", Indicator = 0.40, GroupName = "Спирты (этиловый, метиловый, пропиловый, бутиловый и др.) на складах и спиртзаводах", ExtraGroupName = "" });
            sectionDestinationsLiquids.Add(new SectionByDestinations() { Name = "Нефть и конденсат вокруг скважины фонтана", Indicator = 0.20, GroupName = "Нефть и конденсат вокруг скважины фонтана", ExtraGroupName = "" });
            sectionDestinationsLiquids.Add(new SectionByDestinations() { Name = "ЛВЖ", Indicator = 0.08, GroupName = "ЛВЖ", ExtraGroupName = "" });
            sectionDestinationsLiquids.Add(new SectionByDestinations() { Name = "ГЖ", Indicator = 0.05, GroupName = "ГЖ", ExtraGroupName = "" });

        }
        public static void Main()
        {

        }

        public SectionByObjects GetSectionByObject(SectionByObjectsType type)
        {
            foreach (FOHQ.Data.SectionByObjects item in SectionByObjectsList)
            {
                if (type == item.Section)
                {
                    return item;
                }
            }
            return null;
        }
    }

    /// <summary>
    /// Раздел по виду объекта
    /// </summary>
    public enum SectionByObjectsType
    {
        /// <summary>
        /// Здания и сооружения
        /// </summary>
        Buildings,
        /// <summary>
        /// Транспортные средства
        /// </summary>
        Transport,
        /// <summary>
        /// Твердые материалы
        /// </summary>
        Materials,
        /// <summary>
        /// Легковоспламеняющиеся и горючие жидкости
        /// </summary>
        Liquids,
        None
    }

    /// <summary>
    /// Тип тушения
    /// </summary>
    public enum ExtinguishingType
    {
        ExtinguishingPerimeter, //по периметру
        ExtinguishingFront,     //по фронту
        None
    }

    /// <summary>
    /// Раздел по виду объекта
    /// </summary>
    public class SectionByObjects
    {
        public SectionByObjectsType Section { get; set; } // один тип
        public string Name { get; set; } // другой тип

    }

    /// <summary>
    /// Раздел по назначению
    /// </summary>
    public class SectionByDestinations
    {
        public string Name { get; set; } // другой тип
        public double Indicator { get; set; } // другой тип
        public string GroupName { get; set; }
        public string ExtraGroupName { get; set; }
    }
}
