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

namespace LaunchersChoosers.Choosers
{
    public partial class EmailAddressChooser : PhoneApplicationPage
    {
        EmailAddressChooserTask eac;

        public EmailAddressChooser()
        {
            InitializeComponent();

            eac = new EmailAddressChooserTask();
            eac.Completed += new EventHandler<EmailResult>(eac_Completed);
        }

        void eac_Completed(object sender, EmailResult e)
        {
            if (e.TaskResult == TaskResult.OK)
            {
                textBox1.Text = e.Email;
            }
        }

        private void btnEmailaddress_Click(object sender, RoutedEventArgs e)
        {
            eac.Show();
        }
    }
}