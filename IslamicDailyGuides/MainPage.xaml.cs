using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace IslamicDailyGuides
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Set the data context of the listbox control to the sample data
            DataContext = App.ViewModel;
        }

        // Load data for the ViewModel Items
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            
        }

        private void moringButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/DaftarAmal.xaml?page=1", UriKind.Relative));
        }

        private void dayButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/DaftarAmal.xaml?page=2", UriKind.Relative));
        }

        private void nightButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/DaftarAmal.xaml?page=3", UriKind.Relative));
        }

        private void otherButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/DaftarAmal.xaml?page=4", UriKind.Relative));
        }
    }
}