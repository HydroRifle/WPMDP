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

namespace LaunchersChoosers.Launchers
{
    public partial class PhoneCall : PhoneApplicationPage
    {
        public PhoneCall()
        {
            InitializeComponent();
            txtPhoneNo.InputScope = new InputScope
            {
                Names = { new InputScopeName { NameValue = InputScopeNameValue.TelephoneNumber } }
            };
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PhoneCallTask pct = new PhoneCallTask();
            pct.DisplayName = "Test Call";
            pct.PhoneNumber = txtPhoneNo.Text;
            pct.Show();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }
    }
}