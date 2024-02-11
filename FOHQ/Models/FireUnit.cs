using System;
using System.ComponentModel;

using SQLite;

using SQLiteNetExtensions.Attributes;

namespace FOHQ.Models
{
    /// <summary>
    /// Подразделение пожарной охраны, взаимодействующая служба.
    /// </summary>
    [Table("FireUnits")]
    public class FireUnit : INotifyPropertyChanged
    {
        private int _id;

        [PrimaryKey, AutoIncrement]
        public int Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        [ForeignKey(typeof(Document))]
        public int DocumentId { get; set; }

        [ManyToOne(CascadeOperations = CascadeOperation.CascadeRead)]
        public Document Document { get; set; }

        private string _departments;

        /// <summary>
        /// Подразделения ПО, взаимодействующие службы
        /// </summary>
        public string Departments
        {
            get => _departments;
            set
            {
                _departments = value;
                OnPropertyChanged(nameof(Departments));
            }
        }

        private DateTime _arrivalDate;

        /// <summary>
        /// Дата прибытия
        /// </summary>
        public DateTime ArrivalDate
        {
            get => _arrivalDate;
            set
            {
                _arrivalDate = value;
                OnPropertyChanged(nameof(ArrivalDate));
            }
        }

        private TimeSpan _arrivalTime;

        /// <summary>
        /// Время прибытия
        /// </summary>
        public TimeSpan ArrivalTime
        {
            get => _arrivalTime;
            set
            {
                _arrivalTime = value;
                OnPropertyChanged(nameof(ArrivalTime));
            }
        }

        private int _personnel;

        /// <summary>
        /// Численность расчёта
        /// </summary>
        public int Personnel
        {
            get => _personnel;
            set
            {
                _personnel = value;
                OnPropertyChanged(nameof(Personnel));
            }
        }

        private string _mainTask;

        /// <summary>
        /// Основная задача
        /// </summary>
        public string MainTask
        {
            get => _mainTask;
            set
            {
                _mainTask = value;
                OnPropertyChanged(nameof(MainTask));
            }
        }

        private DateTime _mainTaskDate;

        /// <summary>
        /// Дата получения
        /// </summary>
        public DateTime MainTaskDate
        {
            get => _mainTaskDate;
            set
            {
                _mainTaskDate = value;
                OnPropertyChanged(nameof(MainTaskDate));
            }
        }

        private TimeSpan _mainTaskTime;

        /// <summary>
        /// Время получения
        /// </summary>
        public TimeSpan MainTaskTime
        {
            get => _mainTaskTime;
            set
            {
                _mainTaskTime = value;
                OnPropertyChanged(nameof(MainTaskTime));
            }
        }

        private string _combatArea;

        /// <summary>
        /// Боевой участок
        /// </summary>
        public string CombatArea
        {
            get => _combatArea;
            set
            {
                _combatArea = value;
                OnPropertyChanged(nameof(CombatArea));
            }
        }

        private string _workSector;

        /// <summary>
        /// СПР
        /// </summary>
        public string WorkSector
        {
            get => _workSector;
            set
            {
                _workSector = value;
                OnPropertyChanged(nameof(WorkSector));
            }
        }

        private DateTime _firstBarrelDate;

        /// <summary>
        /// Дата введения первого ствола
        /// </summary>
        public DateTime FirstBarrelDate
        {
            get => _firstBarrelDate;
            set
            {
                _firstBarrelDate = value;
                OnPropertyChanged(nameof(FirstBarrelDate));
            }
        }

        private TimeSpan _firstBarrelTime;

        /// <summary>
        /// Время введения первого ствола
        /// </summary>
        public TimeSpan FirstBarrelTime
        {
            get => _firstBarrelTime;
            set
            {
                _firstBarrelTime = value;
                OnPropertyChanged(nameof(FirstBarrelTime));
            }
        }

        private DateTime _departureDate;

        /// <summary>
        /// Дата убытия с места пожара
        /// </summary>
        public DateTime DepartureDate
        {
            get => _departureDate;
            set
            {
                _departureDate = value;
                OnPropertyChanged(nameof(DepartureDate));
            }
        }

        private TimeSpan _departureTime;

        /// <summary>
        /// Время убытия с места пожара
        /// </summary>
        public TimeSpan DepartureTime
        {
            get => _departureTime;
            set
            {
                _departureTime = value;
                OnPropertyChanged(nameof(DepartureTime));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public FireUnit() : base()
        {
        }

        public FireUnit(Document document)
        {
            Document = document ?? throw new ArgumentNullException(nameof(document));
            DocumentId = document.Id;
        }

        public void Copy(FireUnit source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            Id = source.Id;
            DocumentId = source.DocumentId;
            Document = source.Document;
            Departments = source.Departments;
            ArrivalDate = source.ArrivalDate;
            ArrivalTime = source.ArrivalTime;
            Personnel = source.Personnel;
            MainTask = source.MainTask;
            MainTaskDate = source.MainTaskDate;
            MainTaskTime = source.MainTaskTime;
            CombatArea = source.CombatArea;
            WorkSector = source.WorkSector;
            FirstBarrelDate = source.FirstBarrelDate;
            FirstBarrelTime = source.FirstBarrelTime;
            DepartureDate = source.DepartureDate;
            DepartureTime = source.DepartureTime;
        }
    }
}