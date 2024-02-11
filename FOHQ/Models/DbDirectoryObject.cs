using System.ComponentModel;

using SQLite;

namespace FOHQ.Models
{
    /// <summary>
    /// Объект справочника базы данных.
    /// </summary>
    public class DbDirectoryObject : INotifyPropertyChanged
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

        private string _name;

        /// <summary>
        /// Наименование.
        /// </summary>
        [MaxLength(100)]
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}