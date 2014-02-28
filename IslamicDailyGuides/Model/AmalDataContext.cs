using System;
using System.ComponentModel;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace IslamicDailyGuides.Model
{
    [Table]
    public class AmalItem : INotifyPropertyChanged, INotifyPropertyChanging
    {

        // Define ID: private field, public property, and database column.
        private int _amalItemId;

        public static int SENIN     = 0x0000001;
        public static int SELASA    = 0x0000010;
        public static int RABU      = 0x0000100;
        public static int KAMIS     = 0x0001000;
        public static int JUMAT     = 0x0010000;
        public static int SABTU     = 0x0100000;
        public static int MINGGU    = 0x1000000;

        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        public int AmalItemId
        {
            get { return _amalItemId; }
            set
            {
                if (_amalItemId != value)
                {
                    NotifyPropertyChanging("AmalItemId");
                    _amalItemId = value;
                    NotifyPropertyChanged("AmalItemId");
                }
            }
        }

        // Define item name: private field, public property, and database column.
        private string _itemName;

        [Column]
        public string ItemName
        {
            get { return _itemName; }
            set
            {
                if (_itemName != value)
                {
                    NotifyPropertyChanging("ItemName");
                    _itemName = value;
                    NotifyPropertyChanged("ItemName");
                }
            }
        }

        // Define item name: private field, public property, and database column.
        private string _dalil;

        [Column]
        public string Dalil
        {
            get { return _dalil; }
            set
            {
                if (_dalil != value)
                {
                    NotifyPropertyChanging("Dalil");
                    _dalil = value;
                    NotifyPropertyChanged("Dalil");
                }
            }
        }

        // Define item name: private field, public property, and database column.
        private string _riwayat;

        [Column]
        public string Riwayat
        {
            get { return _riwayat; }
            set
            {
                if (_riwayat != value)
                {
                    NotifyPropertyChanging("Riwayat");
                    _riwayat = value;
                    NotifyPropertyChanged("Riwayat");
                }
            }
        }


        // Define image (dalil) path in Isolated Storage.
        private string _imageDalilPath;

        [Column]
        public string DalilImagePath
        {
            get { return _imageDalilPath; }
            set
            {
                if (_imageDalilPath != value)
                {
                    NotifyPropertyChanging("DalilImagePath");
                    _imageDalilPath = value;
                    NotifyPropertyChanged("DalilImagePath");
                }
            }
        }

        // Define if there is image for pray
        private bool _isTherePrayImage;

        [Column]
        public bool IsTherePrayImage
        {
            get { return _isTherePrayImage;  }
            set
            {
                if (_isTherePrayImage != value)
                {
                    NotifyPropertyChanging("IsTherePrayImage");
                    _isTherePrayImage = value;
                    NotifyPropertyChanged("IsTherePrayImage");
                }
            }
        }

        // Define pray image, if any.
        private string _prayImagePath;

        [Column]
        public string PrayImagePath
        {
            get { return _prayImagePath; }
            set
            {
                if (_prayImagePath != value)
                {
                    NotifyPropertyChanging("PrayImagePath");
                    _prayImagePath = value;
                    NotifyPropertyChanged("PrayImagePath");
                }
            }
        }

        // Define completion value: private field, public property, and database column.
        private bool _isComplete;

        [Column]
        public bool IsComplete
        {
            get { return _isComplete; }
            set
            {
                if (_isComplete != value)
                {
                    NotifyPropertyChanging("IsComplete");
                    _isComplete = value;
                    NotifyPropertyChanged("IsComplete");
                }
            }
        }


        // Internal column for the associated ToDoCategory ID value
        [Column]
        internal int _categoryId;

        // Entity reference, to identify the ToDoCategory "storage" table
        private EntityRef<AmalTime> _category;

        // Association, to describe the relationship between this key and that "storage" table
        [Association(Storage = "_category", ThisKey = "_categoryId", OtherKey = "Id", IsForeignKey = true)]
        public AmalTime Category
        {
            get { return _category.Entity; }
            set
            {
                NotifyPropertyChanging("Category");
                _category.Entity = value;

                if (value != null)
                {
                    _categoryId = value.Id;
                }

                NotifyPropertyChanging("Category");
            }
        }

        // Version column aids update performance.
        [Column(IsVersion = true)]
        private Binary _version;

        // Define item name: private field, public property, and database column.
        private int _daftarHari;

        [Column]
        public int DaftarHari
        {
            get { return _daftarHari; }
            set
            {
                if (_daftarHari != value)
                {
                    NotifyPropertyChanging("DaftarHari");
                    _daftarHari = value;
                    NotifyPropertyChanged("DaftarHari");
                }
            }
        }


        // Define item name: private field, public property, and database column.
        private DateTime _waktuAmal;

        [Column]
        public DateTime WaktuAmal
        {
            get { return _waktuAmal; }
            set
            {
                if (_waktuAmal != value)
                {
                    NotifyPropertyChanging("WaktuAmal");
                    _waktuAmal = value;
                    NotifyPropertyChanged("WaktuAmal");
                }
            }
        }

        // Define completion value: private field, public property, and database column.
        private bool _isEnabled;

        [Column]
        public bool IsEnabled
        {
            get { return _isEnabled; }
            set
            {
                if (_isEnabled != value)
                {
                    NotifyPropertyChanging("IsEnabled");
                    _isEnabled = value;
                    NotifyPropertyChanged("IsEnabled");
                }
            }
        }

        // Define completion value: private field, public property, and database column.
        private bool _isCustomized;

        [Column]
        public bool IsCustomized
        {
            get { return _isCustomized; }
            set
            {
                if (_isCustomized != value)
                {
                    NotifyPropertyChanging("IsCustomized");
                    _isCustomized = value;
                    NotifyPropertyChanged("IsCustomized");
                }
            }
        }
        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        // Used to notify that a property changed
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        #region INotifyPropertyChanging Members

        public event PropertyChangingEventHandler PropertyChanging;

        // Used to notify that a property is about to change
        private void NotifyPropertyChanging(string propertyName)
        {
            if (PropertyChanging != null)
            {
                PropertyChanging(this, new PropertyChangingEventArgs(propertyName));
            }
        }

        #endregion
    }

    [Table]
    public class AmalTime : INotifyPropertyChanged, INotifyPropertyChanging
    {

        // Define ID: private field, public property, and database column.
        private int _id;

        [Column(DbType = "INT NOT NULL IDENTITY", IsDbGenerated = true, IsPrimaryKey = true)]
        public int Id
        {
            get { return _id; }
            set
            {
                NotifyPropertyChanging("Id");
                _id = value;
                NotifyPropertyChanged("Id");
            }
        }

        // Define category name: private field, public property, and database column.
        private string _name;

        [Column]
        public string Name
        {
            get { return _name; }
            set
            {
                NotifyPropertyChanging("Name");
                _name = value;
                NotifyPropertyChanged("Name");
            }
        }


        // Version column aids update performance.
        [Column(IsVersion = true)]
        private Binary _version;
        
                // Define the entity set for the collection side of the relationship.
        private EntitySet<AmalItem> _amals;

        [Association(Storage = "_amals", OtherKey = "_categoryId", ThisKey = "Id")]
        public EntitySet<AmalItem> Amals
        {
            get { return this._amals; }
            set { this._amals.Assign(value); }
        }


        // Assign handlers for the add and remove operations, respectively.
        public AmalTime()
        {
            _amals = new EntitySet<AmalItem>(
                new Action<AmalItem>(this.attach_Amal), 
                new Action<AmalItem>(this.detach_Amal)
                );
        }

        // Called during an add operation
        private void attach_Amal(AmalItem amal)
        {
            NotifyPropertyChanging("AmalItem");
            amal.Category = this;
        }

        // Called during a remove operation
        private void detach_Amal(AmalItem amal)
        {
            NotifyPropertyChanging("AmalItem");
            amal.Category = null;
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        // Used to notify that a property changed
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        #region INotifyPropertyChanging Members

        public event PropertyChangingEventHandler PropertyChanging;

        // Used to notify that a property is about to change
        private void NotifyPropertyChanging(string propertyName)
        {
            if (PropertyChanging != null)
            {
                PropertyChanging(this, new PropertyChangingEventArgs(propertyName));
            }
        }

        #endregion
    }

    public class AmalDataContext : DataContext
    {
        // Pass the connection string to the base class.
        public AmalDataContext(string connectionString)
            : base(connectionString)
        { }

        // Specify a table for the to-do items.
        public Table<AmalItem> Items;

        // Specify a table for the categories.
        public Table<AmalTime> Categories;

    }
}
