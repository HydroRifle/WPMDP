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
    public partial class SaveEmailAddress : PhoneApplicationPage
    {
        SaveEmailAddressTask sea;

        public SaveEmailAddress()
        {
            InitializeComponent();
            txtEmail.InputScope = new InputScope()
            {
                Names = { new InputScopeName() { NameValue = InputScopeNameValue.EmailNameOrAddress } }
            };

            sea = new SaveEmailAddressTask();
            sea.Completed += new EventHandler<TaskEventArgs>(sea_Completed);
        }

        void sea_Completed(object sender, TaskEventArgs e)
        {
            if (e.TaskResult == TaskResult.OK)
            {
                //Success
                MessageBox.Show("保存成功!");
            }
            else
            {
                MessageBox.Show("保存失敗!");
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            sea.Email = txtEmail.Text;
            sea.Show();
        }
    }
}