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
    public partial class EmailCompose : PhoneApplicationPage
    {
        public EmailCompose()
        {
            InitializeComponent();
            txtEmailAddress.InputScope = new InputScope
            {
                Names = {new InputScopeName() { NameValue = InputScopeNameValue.EmailNameOrAddress }}
            };
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            EmailComposeTask ect = new EmailComposeTask();
            ect.To = txtEmailAddress.Text;
            ect.Subject = txtSubject.Text;
            ect.Body = txtMailBody.Text;
            ect.Show();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }
    }
}