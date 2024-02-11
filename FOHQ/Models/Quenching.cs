using System;
using SQLite;

namespace FOHQ.Models
{
    /// <summary>
    /// Тушение
    /// </summary>
    [Table("Quenchings")]
    public class Quenching : BaseModel
    {
        private FOHQ.Data.FormOfQuenchingTypes _shape;

        /// <summary>
        /// Форма пожара
        /// </summary>
        public FOHQ.Data.FormOfQuenchingTypes Shape
        {
            get => _shape;
            set
            {
                if (_shape != value)
                {
                    _shape = value;
                    var formOfQuenching = App.DataAccess.QuenchingRepository.GetFormOfQuenching(Shape);
                    if (formOfQuenching != null)
                    {
                        IndicatorShape = formOfQuenching.Value;
                    }
                    OnPropertyChanged(nameof(Shape));
                }
            }
        }

        private double _indicatorShape;

        /// <summary>
        /// Форма пожара, показатель
        /// </summary>
        public double IndicatorShape
        {
            get => _indicatorShape;
            set
            {
                if (_indicatorShape != value)
                {
                    _indicatorShape = value;
                    OnPropertyChanged(nameof(IndicatorShape));
                }
            }
        }

        private double _path;
        /// <summary>
        /// Путь, пройденный огнем
        /// </summary>
        public double Path
        {
            get => _path;
            set
            {
                if (_path != value)
                {
                    _path = value;
                    OnPropertyChanged(nameof(Path));
                }
            }
        }

        private double _widthPath;

        /// <summary>
        /// Ширина
        /// </summary>
        public double WidthPath
        {
            get => _widthPath;
            set
            {
                if (_widthPath != value)
                {
                    _widthPath = value;
                    OnPropertyChanged(nameof(WidthPath));
                }
            }
        }

        private FOHQ.Data.QuenchingBarrelType _quenchingTrunk;

        /// <summary>
        /// Тушение стволами
        /// </summary>
        public FOHQ.Data.QuenchingBarrelType QuenchingTrunk
        {
            get => _quenchingTrunk;
            set
            {
                if (_quenchingTrunk != value)
                {
                    _quenchingTrunk = value;
                    var barrel = App.DataAccess.QuenchingRepository.GetQuenchingBarrel(QuenchingTrunk);
                    if (barrel != null)
                    {
                        IndicatorQuenchingTrunk = barrel.Value;
                    }
                    OnPropertyChanged(nameof(QuenchingTrunk));
                }
            }
        }

        private double _indicatorQuenchingTrunk;

        /// <summary>
        /// Тушение стволами, показатель
        /// </summary>
        public double IndicatorQuenchingTrunk
        {
            get => _indicatorQuenchingTrunk;
            set
            {
                if (_indicatorQuenchingTrunk != value)
                {
                    _indicatorQuenchingTrunk = value;
                    OnPropertyChanged(nameof(IndicatorQuenchingTrunk));
                }
            }
        }

        private double _fireArea;

        /// <summary>
        /// Площадь пожара
        /// </summary>
        public double FireArea
        {
            get => _fireArea;
            set
            {
                if (_fireArea != value)
                {
                    _fireArea = value;
                    OnPropertyChanged(nameof(FireArea));
                }
            }
        }

        private string _fireAreaFormula;

        /// <summary>
        /// Площадь пожара - формула
        /// </summary>
        public string FireAreaFormula
        {
            get => _fireAreaFormula;
            set
            {
                if (_fireAreaFormula != value)
                {
                    _fireAreaFormula = value;
                    OnPropertyChanged(nameof(FireAreaFormula));
                }
            }
        }

        private double _extinguishingAreaPerimeter;

        /// <summary>
        /// Площаль тушения по периметру
        /// </summary>
        public double ExtinguishingAreaPerimeter
        {
            get => _extinguishingAreaPerimeter;
            set
            {
                if (_extinguishingAreaPerimeter != value)
                {
                    _extinguishingAreaPerimeter = value;
                    OnPropertyChanged(nameof(ExtinguishingAreaPerimeter));
                }
            }
        }

        private string _extinguishingAreaPerimeterFormula;

        /// <summary>
        /// Площаль тушения по периметру - формула
        /// </summary>
        public string ExtinguishingAreaPerimeterFormula
        {
            get => _extinguishingAreaPerimeterFormula;
            set
            {
                if (_extinguishingAreaPerimeterFormula != value)
                {
                    _extinguishingAreaPerimeterFormula = value;
                    OnPropertyChanged(nameof(ExtinguishingAreaPerimeterFormula));
                }
            }
        }

        private double _extinguishingAreaFront;

        /// <summary>
        /// Площаль тушения по фронту
        /// </summary>
        public double ExtinguishingAreaFront
        {
            get => _extinguishingAreaFront;
            set
            {
                if (_extinguishingAreaFront != value)
                {
                    _extinguishingAreaFront = value;
                    OnPropertyChanged(nameof(ExtinguishingAreaFront));
                }
            }
        }

        private string _extinguishingAreaFrontForumla;

        /// <summary>
        /// Площаль тушения по фронту - формула
        /// </summary>
        public string ExtinguishingAreaFrontFormula
        {
            get => _extinguishingAreaFrontForumla;
            set
            {
                if (_extinguishingAreaFrontForumla != value)
                {
                    _extinguishingAreaFrontForumla = value;
                    OnPropertyChanged(nameof(ExtinguishingAreaFrontFormula));
                }
            }
        }

        private double _sizeAreaPerimeter;

        /// <summary>
        /// Величина в метрах периметра
        /// </summary>
        public double SizeAreaPerimeter
        {
            get => _sizeAreaPerimeter;
            set
            {
                if (_sizeAreaPerimeter != value)
                {
                    _sizeAreaPerimeter = value;
                    OnPropertyChanged(nameof(SizeAreaPerimeter));
                }
            }
        }

        private string _sizeAreaPerimeterFormula;

        /// <summary>
        /// Величина в метрах периметра - формула
        /// </summary>
        public string SizeAreaPerimeterFormula
        {
            get => _sizeAreaPerimeterFormula;
            set
            {
                if (_sizeAreaPerimeterFormula != value)
                {
                    _sizeAreaPerimeterFormula = value;
                    OnPropertyChanged(nameof(SizeAreaPerimeterFormula));
                }
            }
        }


        private double _sizeAreaFront;

        /// <summary>
        /// Величина в метрах фронта
        /// </summary>
        public double SizeAreaFront
        {
            get => _sizeAreaFront;
            set
            {
                if (_sizeAreaFront != value)
                {
                    _sizeAreaFront = value;
                    OnPropertyChanged(nameof(SizeAreaFront));
                }
            }
        }

        private string _sizeAreaFrontFormula;

        /// <summary>
        /// Величина в метрах фронта - формула
        /// </summary>
        public string SizeAreaFrontFormula
        {
            get => _sizeAreaFrontFormula;
            set
            {
                if (_sizeAreaFrontFormula != value)
                {
                    _sizeAreaFrontFormula = value;
                    OnPropertyChanged(nameof(SizeAreaFrontFormula));
                }
            }
        }

        /// <summary>
        /// Расчет площади пожара
        /// </summary>
        private double GetFireArea()
        {
            if (Shape == Data.FormOfQuenchingTypes.Quadrant)
            {
                FireAreaFormula = "0.25 * 3.14 * " + Path + " * " + Path;
                return 0.25 * 3.14 * Path * Path;
            }
            else if (Shape == Data.FormOfQuenchingTypes.Semicircle)
            {
                FireAreaFormula = "0.5 * 3.14 * " + Path + " * " + Path;
                return 0.5 * 3.14 * Path * Path;
            }
            else if (Shape == Data.FormOfQuenchingTypes.Cicle)
            {
                FireAreaFormula = "1 * 3.14 * " + Path + " * " + Path;
                return 1 * 3.14 * Path * Path;
            }
            else if (Shape == Data.FormOfQuenchingTypes.RectangleOneWay || Shape == Data.FormOfQuenchingTypes.RectangleTwoWayLeft ||
                    Shape == Data.FormOfQuenchingTypes.RectangleTwoWayRigt || Shape == Data.FormOfQuenchingTypes.RectangleTwoWayLeft ||
                    Shape == Data.FormOfQuenchingTypes.RectangleThirdWayLeft || Shape == Data.FormOfQuenchingTypes.RectangleThirdWayRight ||
                    Shape == Data.FormOfQuenchingTypes.RectangleFourWay)
            {
                FireAreaFormula = WidthPath + " * " + Path;
                return WidthPath * Path;
            }
            else
            {
                FireAreaFormula = "";
                return 0;
            }
        }

        /// <summary>
        /// Расчет площади тушения по периметру
        /// </summary>
        private double GetExtinguishingAreaPerimeter()
        {
            if (Shape == FOHQ.Data.FormOfQuenchingTypes.Quadrant)
            {
                if (Path > 3 * IndicatorQuenchingTrunk)
                {
                    ExtinguishingAreaPerimeterFormula = "3.57 * " + IndicatorQuenchingTrunk + " * (" + Path + " - " + IndicatorQuenchingTrunk + ")";
                    return 3.57 * IndicatorQuenchingTrunk * (Path - IndicatorQuenchingTrunk);
                }
                else
                {
                    ExtinguishingAreaPerimeterFormula = Convert.ToString(FireArea);
                    return FireArea;
                }
            }
            else if (Shape == FOHQ.Data.FormOfQuenchingTypes.Semicircle)
            {
                if (Path > 2 * IndicatorQuenchingTrunk)
                {
                    ExtinguishingAreaPerimeterFormula = "3.57 * " + IndicatorQuenchingTrunk + " * (1.4 * " + Path + " - " + IndicatorQuenchingTrunk + ")";
                    return 3.57 * IndicatorQuenchingTrunk * (1.4 * Path - IndicatorQuenchingTrunk);
                }
                else
                {
                    ExtinguishingAreaPerimeterFormula = Convert.ToString(FireArea);
                    return FireArea;
                }
            }
            else if (Shape == FOHQ.Data.FormOfQuenchingTypes.Cicle)
            {
                if (Path > IndicatorQuenchingTrunk)
                {
                    ExtinguishingAreaPerimeterFormula = "3.14 * " + IndicatorQuenchingTrunk + " * (2 * " + Path + " - " + IndicatorQuenchingTrunk + ")";
                    return 3.14 * IndicatorQuenchingTrunk * (2 * Path - IndicatorQuenchingTrunk);
                }
                else
                {
                    ExtinguishingAreaPerimeterFormula = Convert.ToString(FireArea);
                    return FireArea;
                }
            }
            else if (Shape == FOHQ.Data.FormOfQuenchingTypes.RectangleTwoWayRigt)
            {
                if (WidthPath > 2 * IndicatorQuenchingTrunk)
                {
                    ExtinguishingAreaPerimeterFormula = IndicatorQuenchingTrunk + " * (" + WidthPath + " + " + Path + " - " + IndicatorQuenchingTrunk + ")";
                    return IndicatorQuenchingTrunk * (WidthPath + Path - IndicatorQuenchingTrunk);
                }
                else
                {
                    ExtinguishingAreaPerimeterFormula = Convert.ToString(FireArea);
                    return FireArea;
                }
            }
            else if (Shape == FOHQ.Data.FormOfQuenchingTypes.RectangleThirdWayLeft)
            {
                if (WidthPath > 2 * IndicatorQuenchingTrunk)
                {
                    ExtinguishingAreaPerimeterFormula = "2 * " + IndicatorQuenchingTrunk + " * " + WidthPath + " + " + IndicatorQuenchingTrunk + " * (" + Path + " - 2 * " + IndicatorQuenchingTrunk + ")";
                    return 2 * IndicatorQuenchingTrunk * WidthPath + IndicatorQuenchingTrunk * (Path - 2 * IndicatorQuenchingTrunk);
                }
                else
                {
                    ExtinguishingAreaPerimeterFormula = Convert.ToString(FireArea);
                    return FireArea;
                }
            }
            else if (Shape == FOHQ.Data.FormOfQuenchingTypes.RectangleThirdWayRight)
            {
                if (WidthPath > 2 * IndicatorQuenchingTrunk)
                {
                    ExtinguishingAreaPerimeterFormula = "2 * " + IndicatorQuenchingTrunk + " * " + Path + " + " + IndicatorQuenchingTrunk + " * (" + WidthPath + " - 2 * " + IndicatorQuenchingTrunk + ")";
                    return 2 * IndicatorQuenchingTrunk * Path + IndicatorQuenchingTrunk * (WidthPath - 2 * IndicatorQuenchingTrunk);
                }
                else
                {
                    ExtinguishingAreaPerimeterFormula = Convert.ToString(FireArea);
                    return FireArea;
                }
            }
            else if (Shape == FOHQ.Data.FormOfQuenchingTypes.RectangleFourWay)
            {
                if (WidthPath > 2 * IndicatorQuenchingTrunk)
                {
                    ExtinguishingAreaPerimeterFormula = "2 * " + IndicatorQuenchingTrunk + " * " + WidthPath + " + 2 * " + IndicatorQuenchingTrunk + " * (" + Path + " - 2 * " + IndicatorQuenchingTrunk + ")";
                    return 2 * IndicatorQuenchingTrunk * WidthPath + 2 * IndicatorQuenchingTrunk * (Path - 2 * IndicatorQuenchingTrunk);
                }
                else
                {
                    ExtinguishingAreaPerimeterFormula = Convert.ToString(FireArea);
                    return FireArea;
                }
            }
            else
            {
                ExtinguishingAreaPerimeterFormula = "";
                return 0;
            }
        }

        /// <summary>
        /// Расчет площади тушения по фронту
        /// </summary>
        private double GetExtinguishingAreaFront()
        {
            if (Shape == FOHQ.Data.FormOfQuenchingTypes.Quadrant)
            {
                if (Path > IndicatorQuenchingTrunk)
                {
                    ExtinguishingAreaFrontFormula = "0.25 * 3.14 * " + IndicatorQuenchingTrunk + " * (2 * " + Path + " - " + IndicatorQuenchingTrunk + ")";
                    return 0.25 * 3.14 * IndicatorQuenchingTrunk * (2 * Path - IndicatorQuenchingTrunk);
                }
                else
                {
                    ExtinguishingAreaFrontFormula = Convert.ToString(FireArea);
                    return FireArea;
                }
            }
            else if (Shape == FOHQ.Data.FormOfQuenchingTypes.Semicircle)
            {
                if (Path > IndicatorQuenchingTrunk)
                {
                    ExtinguishingAreaFrontFormula = "0.5 * 3.14 * " + IndicatorQuenchingTrunk + " * (2 * " + Path + " - " + IndicatorQuenchingTrunk + ")";
                    return 0.5 * 3.14 * IndicatorQuenchingTrunk * (2 * Path - IndicatorQuenchingTrunk);
                }
                else
                {
                    ExtinguishingAreaFrontFormula = Convert.ToString(FireArea);
                    return FireArea;
                }
            }
            else if (Shape == FOHQ.Data.FormOfQuenchingTypes.Cicle)
            {
                if (Path > IndicatorQuenchingTrunk)
                {
                    ExtinguishingAreaFrontFormula = "3.14 * " + IndicatorQuenchingTrunk + " * (2 * " + Path + " - " + IndicatorQuenchingTrunk + ")";
                    return 3.14 * IndicatorQuenchingTrunk * (2 * Path - IndicatorQuenchingTrunk);
                }
                else
                {
                    ExtinguishingAreaFrontFormula = Convert.ToString(FireArea);
                    return FireArea;
                }
            }
            else if (Shape == FOHQ.Data.FormOfQuenchingTypes.RectangleOneWay)
            {
                ExtinguishingAreaFrontFormula = IndicatorShape + " * " + WidthPath + " * " + IndicatorQuenchingTrunk;
                return IndicatorShape * WidthPath * IndicatorQuenchingTrunk;
            }
            else if (Shape == FOHQ.Data.FormOfQuenchingTypes.RectangleTwoWayLeft)
            {
                if (Path > IndicatorShape * IndicatorQuenchingTrunk)
                {
                    ExtinguishingAreaFrontFormula = IndicatorShape + " * " + WidthPath + " * " + IndicatorQuenchingTrunk;
                    return IndicatorShape * WidthPath * IndicatorQuenchingTrunk;
                }
                else
                {
                    ExtinguishingAreaFrontFormula = Convert.ToString(FireArea);
                    return FireArea;
                }

            }
            else
            {
                ExtinguishingAreaFrontFormula = "";
                return 0;
            }
        }

        /// <summary>
        /// Расчет величины в метрах - по периметру
        /// </summary>
        private double GetSizeAreaPerimeter()
        {
            if (Shape == FOHQ.Data.FormOfQuenchingTypes.Quadrant)
            {
                SizeAreaPerimeterFormula = "(3.14 * " + Path + ") / 2 + 2 * " + Path;
                return (3.14 * Path) / 2 + 2 * Path;
            }
            else if (Shape == FOHQ.Data.FormOfQuenchingTypes.Semicircle)
            {
                SizeAreaPerimeterFormula = "2 * 3.14 + 2 * " + Path;
                return 2 * 3.14 + 2 * Path;
            }
            else if (Shape == FOHQ.Data.FormOfQuenchingTypes.Cicle)
            {
                SizeAreaPerimeterFormula = "2 * 3.14 * " + Path;
                return 2 * 3.14 * Path;
            }
            else if (Shape == FOHQ.Data.FormOfQuenchingTypes.RectangleOneWay)
            {
                SizeAreaPerimeterFormula = "2 * (" + Path + " + " + WidthPath + ")";
                return 2 * (Path + WidthPath);
            }
            else if ((Shape == FOHQ.Data.FormOfQuenchingTypes.RectangleTwoWayLeft) ||
                    (Shape == FOHQ.Data.FormOfQuenchingTypes.RectangleTwoWayRigt) ||
                    (Shape == FOHQ.Data.FormOfQuenchingTypes.RectangleThirdWayLeft) ||
                    (Shape == FOHQ.Data.FormOfQuenchingTypes.RectangleThirdWayRight) ||
                    (Shape == FOHQ.Data.FormOfQuenchingTypes.RectangleFourWay))
            {
                SizeAreaPerimeterFormula = "2 * (" + Path + " + " + WidthPath + ")";
                return 2 * (Path + WidthPath);
            }
            else
            {
                SizeAreaPerimeterFormula = "";
                return 0;
            }
        }

        /// <summary>
        /// Расчет величины в метрах по фронту
        /// </summary>
        private double GetSizeAreaFront()
        {
            if (Shape == FOHQ.Data.FormOfQuenchingTypes.Quadrant)
            {
                SizeAreaFrontFormula = "(3.14 * " + Path + ") / 2";
                return (3.14 * Path) / 2;
            }
            else if (Shape == FOHQ.Data.FormOfQuenchingTypes.Semicircle)
            {
                SizeAreaFrontFormula = "3.14 * " + Path;
                return 3.14 * Path;
            }
            else if (Shape == FOHQ.Data.FormOfQuenchingTypes.Cicle)
            {
                SizeAreaFrontFormula = "2 * 3.14 * " + Path;
                return 2 * 3.14 * Path;
            }
            else if (Shape == FOHQ.Data.FormOfQuenchingTypes.RectangleOneWay)
            {
                SizeAreaFrontFormula = Convert.ToString(WidthPath);
                return WidthPath;
            }
            else if (Shape == FOHQ.Data.FormOfQuenchingTypes.RectangleTwoWayLeft)
            {
                SizeAreaFrontFormula = "2 * " + WidthPath;
                return 2 * WidthPath;
            }
            else if (Shape == FOHQ.Data.FormOfQuenchingTypes.RectangleTwoWayRigt)
            {
                SizeAreaFrontFormula = WidthPath + " + " + Path;
                return WidthPath + Path;
            }
            else if (Shape == FOHQ.Data.FormOfQuenchingTypes.RectangleThirdWayLeft)
            {
                SizeAreaFrontFormula = "2 * " + WidthPath + " + " + Path;
                return 2 * WidthPath + Path;
            }
            else if (Shape == FOHQ.Data.FormOfQuenchingTypes.RectangleThirdWayRight)
            {
                SizeAreaFrontFormula = WidthPath + " + 2 * " + Path;
                return WidthPath + 2 * Path;
            }
            else if (Shape == FOHQ.Data.FormOfQuenchingTypes.RectangleFourWay)
            {
                SizeAreaFrontFormula = "2 * (" + WidthPath + " + " + Path + ")";
                return 2 * (WidthPath + Path);
            }
            else
            {
                SizeAreaFrontFormula = "";
                return 0;
            }
        }

        public override void UpdateParams()
        {
            //Расчетные данные
            FireArea = Math.Round(GetFireArea(), 2);
            ExtinguishingAreaFront = Math.Round(GetExtinguishingAreaFront(), 2);
            ExtinguishingAreaPerimeter = Math.Round(GetExtinguishingAreaPerimeter(), 2);
            SizeAreaFront = Math.Round(GetSizeAreaFront(), 2);
            SizeAreaPerimeter = Math.Round(GetSizeAreaPerimeter(), 2);
        }

        protected override void Init()
        {
            Shape = Data.FormOfQuenchingTypes.None;
            QuenchingTrunk = Data.QuenchingBarrelType.None;
        }

        public Quenching() : base()
        {

        }

        public Quenching(Document document) : base(document)
        {

        }
    }
}