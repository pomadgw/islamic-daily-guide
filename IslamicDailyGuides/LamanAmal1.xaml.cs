using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using IslamicDailyGuides.Model;
using IslamicDailyGuides.ViewModels;
using System.Windows.Media.Imaging;

namespace IslamicDailyGuides
{
    public partial class LamanAmal1 : PhoneApplicationPage
    {
        int id;
        private AmalDataContext AmalDB;

        public LamanAmal1()
        {
            InitializeComponent();
            AmalDB = App.ViewModel.AmalDB;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            string index;
            if (NavigationContext.QueryString.ContainsKey("id"))
            {
                index = NavigationContext.QueryString["id"];
                id = Convert.ToInt32(index);
                LoadItem(id);
            }

            base.OnNavigatedTo(e);
        }

        private void LoadItem(int id)
        {
            var amalTerpilih = from AmalItem amal in AmalDB.Items where amal.AmalItemId == id select amal;

            foreach (AmalItem amal in amalTerpilih)
            {
                string kategori = amal.Category.Name.ToLower();
                if (amal.IsTherePrayImage)
                {
                    prayImageHeader.Visibility = System.Windows.Visibility.Visible;
                    prayImage.Visibility = System.Windows.Visibility.Visible;
                    prayImage.Source = new BitmapImage(new Uri("/Images/" + kategori + "/" + amal.PrayImagePath, UriKind.Relative));
                }

                headerText.Text = "amal " + kategori;
                amalName.Text = amal.ItemName;
                dalilImage.Source = new BitmapImage(new Uri("/Images/" + kategori + "/" + amal.DalilImagePath, UriKind.Relative));
                dalilText.Text = amal.Dalil;
                riwayatText.Text = amal.Riwayat;
            }
        }
    }
}