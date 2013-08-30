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

using Microsoft.Phone.Tasks;
using System.Windows.Navigation;
using System.Diagnostics;

namespace LaunchersChoosers.Launchers
{
    public partial class Search : PhoneApplicationPage
    {
        const string inputStateKey = "input";

        public Search()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SearchTask searchTask = new SearchTask();
            searchTask.SearchQuery = txtKeyword.Text;
            searchTask.Show();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }
        
        
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            Debug.WriteLine("***\t In OnNavigatedFrom function of SearchTaskPage\t ***");

            if (State.ContainsKey(inputStateKey))
                State.Remove(inputStateKey);

            State.Add(inputStateKey, txtKeyword.Text);

            base.OnNavigatedFrom(e);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Debug.WriteLine("***\t In OnNavigatedTo function of SearchTaskPage\t ***");

            if (State.ContainsKey(inputStateKey))
            {
                string input = (string)State[inputStateKey];
                txtKeyword.Text = input;
                State.Remove(inputStateKey);
            }

            base.OnNavigatedTo(e);
        }
    }
}