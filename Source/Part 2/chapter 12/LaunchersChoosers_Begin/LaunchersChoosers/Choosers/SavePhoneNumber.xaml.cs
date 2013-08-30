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
    public partial class SavePhoneNumber : PhoneApplicationPage
    {
        SavePhoneNumberTask spn;

        public SavePhoneNumber()
        {
            InitializeComponent();
            txtPhoneNo.InputScope = new InputScope()
            {
                Names = { new InputScopeName() { NameValue = InputScopeNameValue.TelephoneNumber } }
            };

            spn = new SavePhoneNumberTask();
            spn.Completed += new EventHandler<TaskEventArgs>(spn_Completed);
        }

        void spn_Completed(object sender, TaskEventArgs e)
        {
            if (e.TaskResult == TaskResult.OK)
            {
                MessageBox.Show("保存成功!");
            }
            else
            {
                MessageBox.Show("保存失敗!");
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            spn.PhoneNumber = txtPhoneNo.Text;
            spn.Show();
        }
    }
}