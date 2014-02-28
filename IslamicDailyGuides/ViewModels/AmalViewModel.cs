using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Microsoft.Phone.Scheduler;

// Directive for the data model.
using IslamicDailyGuides.Model;

namespace IslamicDailyGuides.ViewModels
{
    public class AmalViewModel : INotifyPropertyChanged
    {
        // LINQ to SQL data context for the local database.
        private AmalDataContext _amalDB;

        public AmalDataContext AmalDB
        {
            get
            {
                return _amalDB;
            }
        }

        // Class constructor, create the data context object.
        public AmalViewModel(string AmalDBConnectionString)
        {
            _amalDB = new AmalDataContext(AmalDBConnectionString);
        }

        //
        // Amal: Add collections, list, and methods here.
        //

        // Write changes in the data context to the database.
        public void SaveChangesToDB()
        {
            _amalDB.SubmitChanges();
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        // Used to notify the app that a property has changed.
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

        // All to-do items.
        private ObservableCollection<AmalItem> _allAmalItems;
        public ObservableCollection<AmalItem> AllAmalItems
        {
            get { return _allAmalItems; }
            set
            {
                _allAmalItems = value;
                NotifyPropertyChanged("AllAmalItems");
            }
        }

        // To-do items associated with the home category.
        private ObservableCollection<AmalItem> _pagiAmalItems;
        public ObservableCollection<AmalItem> PagiAmalItems
        {
            get { return _pagiAmalItems; }
            set
            {
                _pagiAmalItems = value;
                NotifyPropertyChanged("PagiAmalItems");
            }
        }

        // To-do items associated with the work category.
        private ObservableCollection<AmalItem> _siangAmalItems;
        public ObservableCollection<AmalItem> SiangAmalItems
        {
            get { return _siangAmalItems; }
            set
            {
                _siangAmalItems = value;
                NotifyPropertyChanged("SiangAmalItems");
            }
        }

        // To-do items associated with the hobbies category.
        private ObservableCollection<AmalItem> _malamAmalItems;
        public ObservableCollection<AmalItem> MalamAmalItems
        {
            get { return _malamAmalItems; }
            set
            {
                _malamAmalItems = value;
                NotifyPropertyChanged("MalamAmalItems");
            }
        }

        // To-do items associated with the hobbies category.
        private ObservableCollection<AmalItem> _lainAmalItems;
        public ObservableCollection<AmalItem> LainAmalItems
        {
            get { return _lainAmalItems; }
            set
            {
                _lainAmalItems = value;
                NotifyPropertyChanged("LainAmalItems");
            }
        }

        // A list of all categories, used by the add task page.
        private List<AmalTime> _categoriesList;
        public List<AmalTime> CategoriesList
        {
            get { return _categoriesList; }
            set
            {
                _categoriesList = value;
                NotifyPropertyChanged("CategoriesList");
            }
        }
        // Query database and load the collections and list used by the pivot pages.
        public void LoadCollectionsFromDatabase()
        {

            // Specify the query for all to-do items in the database.
            var toDoItemsInDB = from AmalItem todo in _amalDB.Items
                                select todo;

            // Query the database and load all to-do items.
            // TODO: error minta dibuka dengan mode read-write karena mau melakukan
            //       perubahan terhadap database
            AllAmalItems = new ObservableCollection<AmalItem>(toDoItemsInDB);

            // Add database time for amal as reminders
            addAllToReminder(AllAmalItems);

            // Specify the query for all categories in the database.
            var toDoCategoriesInDB = from AmalTime category in _amalDB.Categories
                                     select category;


            // Query the database and load all associated items to their respective collections.
            foreach (AmalTime category in toDoCategoriesInDB)
            {
                switch (category.Name)
                {
                    case "Pagi":
                        PagiAmalItems = new ObservableCollection<AmalItem>(category.Amals);
                        break;
                    case "Siang":
                        SiangAmalItems = new ObservableCollection<AmalItem>(category.Amals);
                        break;
                    case "Malam":
                        MalamAmalItems = new ObservableCollection<AmalItem>(category.Amals);
                        break;
                    case "Lain":
                        LainAmalItems = new ObservableCollection<AmalItem>(category.Amals);
                        break;
                    default:
                        break;
                }
            }

            // Load a list of all categories.
            CategoriesList = _amalDB.Categories.ToList();
        }

        // Add a to-do item to the database and collections.
        public void AddAmalItem(AmalItem newToDoItem)
        {
            // Add a to-do item to the data context.
            _amalDB.Items.InsertOnSubmit(newToDoItem);

            // Save changes to the database.
            _amalDB.SubmitChanges();

            // Add a to-do item to the "all" observable collection.
            AllAmalItems.Add(newToDoItem);

            // Add a to-do item to the appropriate filtered collection.
            switch (newToDoItem.Category.Name)
            {
                case "Pagi":
                    PagiAmalItems.Add(newToDoItem);
                    break;
                case "Siang":
                    SiangAmalItems.Add(newToDoItem);
                    break;
                case "Malam":
                    MalamAmalItems.Add(newToDoItem);
                    break;
                case "Lain":
                    LainAmalItems.Add(newToDoItem);
                    break;
                default:
                    break;
            }
        }


        // Remove a to-do task item from the database and collections.
        public void DeleteAmalItem(AmalItem amalForDelete)
        {

            // Remove the to-do item from the "all" observable collection.
            AllAmalItems.Remove(amalForDelete);

            // Remove the to-do item from the data context.
            _amalDB.Items.DeleteOnSubmit(amalForDelete);

            // Remove the to-do item from the appropriate category.   
            switch (amalForDelete.Category.Name)
            {
                case "Pagi":
                    PagiAmalItems.Remove(amalForDelete);
                    break;
                case "Siang":
                    SiangAmalItems.Remove(amalForDelete);
                    break;
                case "Malam":
                    MalamAmalItems.Remove(amalForDelete);
                    break;
                case "Lain":
                    LainAmalItems.Remove(amalForDelete);
                    break;
                default:
                    break;
            }

            // Save changes to the database.
            _amalDB.SubmitChanges();
        }

        public void addAllToReminder(ObservableCollection<AmalItem> daftarAmal)
        {
            foreach (var item in daftarAmal)
            {
                if (item.IsEnabled)
                {
                    RecurrenceInterval recurrence = RecurrenceInterval.None;
                    //if (item.DaftarHari == (AmalItem.SENIN | AmalItem.SELASA | AmalItem.RABU | AmalItem.KAMIS | AmalItem.JUMAT | AmalItem.SABTU | AmalItem.MINGGU))
                    if (item.DaftarHari == 127)
                    {
                        recurrence = RecurrenceInterval.Daily;
                    }
                    else
                    {
                        recurrence = RecurrenceInterval.Weekly;
                    }

                    /**if (recurrence == RecurrenceInterval.Weekly)
                    {
                    
                    }*/

                    addAmalToReminder(item, recurrence);
                }
            }
        }

        private void addAmalToReminder(AmalItem item, RecurrenceInterval recurrence)
        {
            DateTime now = DateTime.Now.Date;

            DateTime beginTime = now + item.WaktuAmal.TimeOfDay;
            DateTime expireTime = now + item.WaktuAmal.TimeOfDay + new TimeSpan(1, 0, 0);

            String title;
            String name;

            if (item.ItemName.Length > 63)
            {
                title = item.ItemName.Substring(0, 60) + "...";
                name = item.ItemName.Substring(0, 63);
            }
            else
            {
                title = name = item.ItemName;
            }

            if (ScheduledActionService.Find(name) == null)
            {
                Reminder reminder = new Reminder(name);


                reminder.Title = title;
                reminder.Content = "Jangan lupa amalnya!";// item.ItemName;
                reminder.BeginTime = moveTime(beginTime, recurrence);
                reminder.ExpirationTime = moveTime(expireTime, reminder.BeginTime, recurrence);
                reminder.RecurrenceType = recurrence;
                reminder.NavigationUri = new Uri("/LamanAmal1.xaml?id=" + item.AmalItemId, UriKind.Relative);

                // Register the reminder with the system.
                ScheduledActionService.Add(reminder);
            }
            else
            {
                Reminder reminder = ScheduledActionService.Find(name) as Reminder;
                reminder.BeginTime = moveTime(beginTime, recurrence);
                reminder.ExpirationTime = moveTime(expireTime, reminder.BeginTime, recurrence);

                ScheduledActionService.Replace(reminder);
            }

        }

        private DateTime moveTime(DateTime time, RecurrenceInterval rec)
        {
            return moveTime(time, DateTime.Now, rec);
        }

        private DateTime moveTime(DateTime time, DateTime now, RecurrenceInterval rec)
        {
            TimeSpan add = new TimeSpan(0, 0, 0);

            if (time.Date < now.Date || time.TimeOfDay < now.TimeOfDay)
            {
                if (rec == RecurrenceInterval.Daily)
                {
                    add = new TimeSpan(1, 0, 0, 0);
                }
                else if (rec == RecurrenceInterval.Weekly)
                {
                    add = new TimeSpan(7, 0, 0, 0);
                }
            }

            return time + add;
        }
    }
}
