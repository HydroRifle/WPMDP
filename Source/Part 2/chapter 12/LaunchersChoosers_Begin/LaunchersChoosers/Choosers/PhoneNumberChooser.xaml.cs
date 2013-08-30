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
    public partial class PhoneNumberChooser : PhoneApplicationPage
    {
        PhoneNumberChooserTask pnc;

        public PhoneNumberChooser()
        {
            InitializeComponent();
            pnc = new PhoneNumberChooserTask();
            pnc.Completed += new EventHandler<PhoneNumberResult>(pnc_Completed);
        }

        void pnc_Completed(object sender, PhoneNumberResult e)
        {
            if (e.TaskResult == TaskResult.OK)
            {
                textBox1.Text = e.PhoneNumber;
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            pnc.Show();
        }
    }
}