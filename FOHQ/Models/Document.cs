using System;
using System.Collections.Generic;
using System.ComponentModel;

using SQLite;

using SQLiteNetExtensions.Attributes;

namespace FOHQ.Models
{
    /// <summary>
    /// Документ приложения.
    /// </summary>
    [Table("Documents")]
    public class Document : INotifyPropertyChanged
    {
        private int _id;

        [PrimaryKey, AutoIncrement]
        public int Id
        {
            get => _id;
            set
            {
                _id = value;
                // Quenching.DocumentId = value;
                // RequiredConsumption.DocumentId = value;
                // NumberOfDevices.DocumentId = value;
                // QuenchingSolution.DocumentId = value;
                // FilingMethods.DocumentId = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        private string _number;

        /// <summary>
        /// Номер документа.
        /// </summary>
        [MaxLength(10)]
        public string Number
        {
            get => _number;
            set
            {
                _number = value;
                OnPropertyChanged(nameof(Number));
            }
        }

        private DateTime _documentDate;

        /// <summary>
        /// Дата документа.
        /// </summary>
        public DateTime DocumentDate
        {
            get => _documentDate;
            set
            {
                _documentDate = value;
                OnPropertyChanged(nameof(DocumentDate));
            }
        }

        private TimeSpan _documentTime;

        /// <summary>
        /// Время документа.
        /// </summary>
        public TimeSpan DocumentTime
        {
            get => _documentTime;
            set
            {
                _documentTime = value;
                OnPropertyChanged(nameof(DocumentTime));
            }
        }

        private string _description;

        /// <summary>
        /// Описание документа.
        /// </summary>
        [MaxLength(250)]
        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        /// <summary>
        /// Распоряжения и информация по ТП..
        /// </summary>
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Order> Orders { get; set; } = new List<Order>();

        [OneToOne(CascadeOperations = CascadeOperation.All)]
        public Quenching Quenching { get; set; }

        [OneToOne(CascadeOperations = CascadeOperation.All)]
        public RequiredConsumption RequiredConsumption { get; set; }

        [OneToOne(CascadeOperations = CascadeOperation.All)]
        public NumberOfDevices NumberOfDevices { get; set; }

        [OneToOne(CascadeOperations = CascadeOperation.All)]
        public QuenchingSolution QuenchingSolution { get; set; }

        [OneToOne(CascadeOperations = CascadeOperation.All)]
        public FilingMethods FilingMethods { get; set; }


        /// <summary>
        /// Подразделения пожарной охраны, взаимодействующие службы.
        /// </summary>
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<FireUnit> FireUnits { get; set; } = new List<FireUnit>();

        /// <summary>
        /// Боевые участки (СПР).
        /// </summary>
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<CombatArea> CombatAreas { get; set; } = new List<CombatArea>();

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Document() : base()
        {
            Quenching = new Quenching((Document)this);
            RequiredConsumption = new RequiredConsumption((Document)this);
            NumberOfDevices = new NumberOfDevices((Document)this);
            QuenchingSolution = new QuenchingSolution((Document)this);
            FilingMethods = new FilingMethods((Document)this);
        }

    }
}