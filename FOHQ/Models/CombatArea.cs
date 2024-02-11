using System;
using System.ComponentModel;

using SQLite;

using SQLiteNetExtensions.Attributes;

namespace FOHQ.Models
{
    /// <summary>
    /// Боевой участок (СПР).
    /// </summary>
    [Table("CombatAreas")]
    public class CombatArea : INotifyPropertyChanged
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

        private string _combatAreaName;

        /// <summary>
        /// Боевой участок
        /// </summary>
        public string CombatAreaName
        {
            get => _combatAreaName;
            set
            {
                _combatAreaName = value;
                OnPropertyChanged(nameof(CombatAreaName));
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

        private string _chief;

        /// <summary>
        /// Начальник
        /// </summary>
        public string Chief
        {
            get => _chief;
            set
            {
                _chief = value;
                OnPropertyChanged(nameof(Chief));
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

        private int _personnel;

        /// <summary>
        /// Количество л/с
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

        private int _departments;

        /// <summary>
        /// Количество отделений
        /// </summary>
        public int Departments
        {
            get => _departments;
            set
            {
                _departments = value;
                OnPropertyChanged(nameof(Departments));
            }
        }

        private int _gasUnits;

        /// <summary>
        /// Количество звеньев ГДЗС
        /// </summary>
        public int GasUnits
        {
            get => _gasUnits;
            set
            {
                _gasUnits = value;
                OnPropertyChanged(nameof(GasUnits));
            }
        }

        private int _barrelsRs50;

        /// <summary>
        /// Стволы РС-50
        /// </summary>
        public int BarrelsRs50
        {
            get => _barrelsRs50;
            set
            {
                _barrelsRs50 = value;
                OnPropertyChanged(nameof(BarrelsRs50));
            }
        }

        private int _barrelsRs70;

        /// <summary>
        /// Стволы РС-70
        /// </summary>
        public int BarrelsRs70
        {
            get => _barrelsRs70;
            set
            {
                _barrelsRs70 = value;
                OnPropertyChanged(nameof(BarrelsRs70));
            }
        }

        private int _barrelsVarFlow;

        /// <summary>
        /// Стволы с регулируемым расходом
        /// </summary>
        public int BarrelsVarFlow
        {
            get => _barrelsVarFlow;
            set
            {
                _barrelsVarFlow = value;
                OnPropertyChanged(nameof(BarrelsVarFlow));
            }
        }

        private int _barrelsL;

        /// <summary>
        /// Стволы Л
        /// </summary>
        public int BarrelsL
        {
            get => _barrelsL;
            set
            {
                _barrelsL = value;
                OnPropertyChanged(nameof(BarrelsL));
            }
        }

        private int _barrelsGps;

        /// <summary>
        /// Стволы ГПС
        /// </summary>
        public int BarrelsGps
        {
            get => _barrelsGps;
            set
            {
                _barrelsGps = value;
                OnPropertyChanged(nameof(BarrelsGps));
            }
        }

        private int _barrelsSvp;

        /// <summary>
        /// Стволы СВП
        /// </summary>
        public int BarrelsSvp
        {
            get => _barrelsSvp;
            set
            {
                _barrelsSvp = value;
                OnPropertyChanged(nameof(BarrelsSvp));
            }
        }

        private int _barrelsOther;

        /// <summary>
        /// Стволы другие
        /// </summary>
        public int BarrelsOther
        {
            get => _barrelsOther;
            set
            {
                _barrelsOther = value;
                OnPropertyChanged(nameof(BarrelsOther));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public CombatArea() : base()
        {
        }

        public CombatArea(Document document)
        {
            Document = document ?? throw new ArgumentNullException(nameof(document));
            DocumentId = document.Id;
        }

        public void Copy(CombatArea source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            Id = source.Id;
            DocumentId = source.DocumentId;
            Document = source.Document;
            CombatAreaName = source.CombatAreaName;
            WorkSector = source.WorkSector;
            Chief = source.Chief;
            MainTask = source.MainTask;
            Personnel = source.Personnel;
            Departments = source.Departments;
            GasUnits = source.GasUnits;
            BarrelsRs50 = source.BarrelsRs50;
            BarrelsRs70 = source.BarrelsRs70;
            BarrelsVarFlow = source.BarrelsVarFlow;
            BarrelsL = source.BarrelsL;
            BarrelsGps = source.BarrelsGps;
            BarrelsSvp = source.BarrelsSvp;
            BarrelsOther = source.BarrelsOther;
        }
    }
}