using System;
using System.ComponentModel;
using Xamarin.Forms;
using System.Windows.Input;

using SQLite;

using SQLiteNetExtensions.Attributes;

namespace FOHQ.Models
{
    /// <summary>
    /// Базовая модель для расчетов СиС
    /// </summary>
    public class BaseModel : INotifyPropertyChanged
    {
        private int _id;

        /// <summary>
        /// Идентификатор
        /// </summary>
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

        /// <summary>
        /// ИД документа
        /// </summary>
        [ForeignKey(typeof(Document))]
        public int DocumentId
        {
            get;
            set;
        }

        /// <summary>
        /// Документ
        /// </summary>
        [OneToOne(CascadeOperations = CascadeOperation.CascadeRead)]
        public Document Document { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public virtual ICommand UpdateParamsCommand { get; }

        /// <summary>
        /// Обновление расчетов
        /// </summary>
        public virtual void UpdateParams()
        {
            //Расчетные данные
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Инициализация
        /// </summary>
        protected virtual void Init()
        {

        }
        public BaseModel() : base()
        {
            UpdateParamsCommand = new Command(UpdateParams);
            Init();
        }

        public BaseModel(Document document)
        {
            Document = document ?? throw new ArgumentNullException(nameof(document));
            DocumentId = document.Id;
            UpdateParamsCommand = new Command(UpdateParams);
            Init();
        }
    }
}
