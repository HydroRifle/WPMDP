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
using System.Diagnostics;
using System.Windows.Navigation;

namespace LaunchersChoosers.Launchers
{
    public partial class WebBrowser : PhoneApplicationPage
    {
        public WebBrowser()
        {
            InitializeComponent();
            txtInput.InputScope = new InputScope
            {
                Names = { new InputScopeName { NameValue = InputScopeNameValue.Url } }
            };
        }

        const string inputStateKey = "input";

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            Debug.WriteLine("***\t In OnNavigatedFrom function of WebSearchPage\t ***");

            if (State.ContainsKey(inputStateKey))
                State.Remove(inputStateKey);

            State.Add(inputStateKey, txtInput.Text);

            base.OnNavigatedFrom(e);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Debug.WriteLine("***\t In OnNavigatedTo function of WebSearchPage\t ***");

            if (State.ContainsKey(inputStateKey))
            {
                string input = (string)State[inputStateKey];
                txtInput.Text = input;
                State.Remove(inputStateKey);
            }

            base.OnNavigatedTo(e);
        }

        private void btnGo_Click(object sender, RoutedEventArgs e)
        {
            WebBrowserTask webBrowserTask = new WebBrowserTask();
            const string url = "http://m.bing.com/search?q={0}&a=results";
            webBrowserTask.Uri = new Uri(string.Format(url, txtInput.Text));
            webBrowserTask.Show();
        }    

        private void btnCancel_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }
    }
}