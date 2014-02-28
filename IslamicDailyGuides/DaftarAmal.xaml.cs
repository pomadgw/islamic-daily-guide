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

namespace IslamicDailyGuides
{
    public partial class DaftarAmal : PhoneApplicationPage
    {
        public DaftarAmal()
        {
            InitializeComponent();
            this.DataContext = App.ViewModel;
        }

        private void bukaAmalButton_Click(object sender, RoutedEventArgs e)
        {
            // Cast the parameter as a button.
            var button = sender as Button;

            if (button != null)
            {
                // Get a handle for the to-do item bound to the button.
                AmalItem amalToOpen = button.DataContext as AmalItem;

                // App.ViewModel.DeleteAmalItem(amalToOpen);
                NavigationService.Navigate(new Uri("/LamanAmal1.xaml?id=" + amalToOpen.AmalItemId, UriKind.Relative));
            }

            // Put the focus back to the main page.
            this.Focus();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            string index;
            if (NavigationContext.QueryString.ContainsKey("page"))
            {
                index = NavigationContext.QueryString["page"];
                pivotControl.SelectedIndex = Convert.ToInt32(index);
            }
            base.OnNavigatedTo(e);
        }
    }
}