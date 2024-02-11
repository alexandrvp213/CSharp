using System;
using System.ComponentModel;

using SQLite;

using SQLiteNetExtensions.Attributes;

namespace FOHQ.Models
{
    /// <summary>
    /// Распоряжение и информация по ТП.
    /// </summary>
    [Table("Orders")]
    public class Order : INotifyPropertyChanged
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

        private DateTime _receiptDate;

        /// <summary>
        /// Дата сообщения
        /// </summary>
        public DateTime ReceiptDate
        {
            get => _receiptDate;
            set
            {
                _receiptDate = value;
                OnPropertyChanged(nameof(ReceiptDate));
            }
        }

        private TimeSpan _receiptTime;

        /// <summary>
        /// Время сообщения
        /// </summary>
        public TimeSpan ReceiptTime
        {
            get => _receiptTime;
            set
            {
                _receiptTime = value;
                OnPropertyChanged(nameof(ReceiptTime));
            }
        }

        private string _information;

        /// <summary>
        /// Что передано
        /// </summary>
        [MaxLength(1024)]
        public string Information
        {
            get => _information;
            set
            {
                _information = value;
                OnPropertyChanged(nameof(Information));
            }
        }

        private string _recipient;

        /// <summary>
        /// Кому передано
        /// </summary>
        [MaxLength(50)]
        public string Recipient
        {
            get => _recipient;
            set
            {
                _recipient = value;
                OnPropertyChanged(nameof(Recipient));
            }
        }

        private string _sender;

        /// <summary>
        /// Кто передал
        /// </summary>
        [MaxLength(50)]
        public string Sender
        {
            get => _sender;
            set
            {
                _sender = value;
                OnPropertyChanged(nameof(Sender));
            }
        }

        private string _received;

        /// <summary>
        /// Кто принял
        /// </summary>
        [MaxLength(50)]
        public string Received
        {
            get => _received;
            set
            {
                _received = value;
                OnPropertyChanged(nameof(Received));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Order() : base()
        {
        }

        public Order(Document document)
        {
            Document = document ?? throw new ArgumentNullException(nameof(document));
            DocumentId = document.Id;
        }

        public void Copy(Order source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            Id = source.Id;
            DocumentId = source.DocumentId;
            Document = source.Document;
            ReceiptDate = source.ReceiptDate;
            ReceiptTime = source.ReceiptTime;
            Information = source.Information;
            Recipient = source.Recipient;
            Sender = source.Sender;
            Received = source.Received;
        }
    }
}