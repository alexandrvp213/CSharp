using System;
using System.ComponentModel;

using SQLite;

namespace FOHQ.Models
{
    /// <summary>
    /// Силы и средства.
    /// </summary>
    [Table("ForcesAndMeans")]
    public class ForcesAndMeans : INotifyPropertyChanged
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

        private string _equipment;

        /// <summary>
        /// Вид техники.
        /// </summary>
        [MaxLength(100)]
        public string Equipment
        {
            get => _equipment;
            set
            {
                _equipment = value;
                OnPropertyChanged(nameof(Equipment));
            }
        }

        private int _quantity;

        /// <summary>
        /// Количество.
        /// </summary>
        public int Quantity
        {
            get => _quantity;
            set
            {
                _quantity = value;
                OnPropertyChanged(nameof(Quantity));
            }
        }

        private int _personnel;

        /// <summary>
        /// Численность личного состава.
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

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Copy(ForcesAndMeans source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            Id = source.Id;
            Equipment = source.Equipment;
            Quantity = source.Quantity;
            Personnel = source.Personnel;
        }
    }
}