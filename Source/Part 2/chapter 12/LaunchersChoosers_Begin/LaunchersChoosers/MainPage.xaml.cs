using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using System.Windows.Navigation;

namespace LaunchersChoosers
{
    public partial class MainPage : PhoneApplicationPage
    {
        const string panoramaIndexStateKey = "panoramaIndex";

        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Set the data context of the listbox control to the sample data
            DataContext = App.ViewModel;
            this.Loaded += new RoutedEventHandler(MainPage_Loaded);
        }

        // Load data for the ViewModel Items and ChoosersItems
        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (!App.ViewModel.IsDataLoaded)
            {
                App.ViewModel.LoadData();
            }
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            if (State.ContainsKey(panoramaIndexStateKey))
                State.Remove(panoramaIndexStateKey);

            State.Add(panoramaIndexStateKey, panorama.SelectedIndex);

            base.OnNavigatedFrom(e);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (State.ContainsKey(panoramaIndexStateKey))
            {
                int index = (int)State[panoramaIndexStateKey];
                if (index != panorama.SelectedIndex)
                    panorama.DefaultItem = panorama.Items[index];
                State.Remove(panoramaIndexStateKey);
            }

            base.OnNavigatedTo(e);
        }
    }
}