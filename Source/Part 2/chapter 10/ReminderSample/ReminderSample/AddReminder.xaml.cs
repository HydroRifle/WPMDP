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
using Microsoft.Phone.Scheduler;

namespace ReminderSample
{
    public partial class AddReminder : PhoneApplicationPage
    {
        public AddReminder()
        {
            InitializeComponent();
        }

        private void ApplicationBarSaveButton_Click(object sender, EventArgs e)
        {
            // Generate a unique name for the new reminder. You can choose a
            // name that is meaningful for your app, or just use a GUID.
            String name = System.Guid.NewGuid().ToString();

        

            // Get the begin time for the reminder by combining the DatePicker
            // value and the TimePicker value.
            DateTime date = (DateTime)beginDatePicker.Value;
            DateTime time = (DateTime)beginTimePicker.Value;
            DateTime beginTime = date + time.TimeOfDay;

            // Make sure that the begin time has not already passed.
            if (beginTime < DateTime.Now)
            {
                MessageBox.Show("the begin date must be in the future.");
                return;
            }

            // Get the expiration time for the reminder
            date = (DateTime)expirationDatePicker.Value;
            time = (DateTime)expirationTimePicker.Value;
            DateTime expirationTime = date + time.TimeOfDay;

            // Make sure that the expiration time is after the begin time.
            if (expirationTime < beginTime)
            {
                MessageBox.Show("expiration time must be after the begin time.");
                return;
            }

            // Determine which recurrence radio button is checked.
            RecurrenceInterval recurrence = RecurrenceInterval.None;
            if (dailyRadioButton.IsChecked == true)
            {
                recurrence = RecurrenceInterval.Daily;
            }
            else if (weeklyRadioButton.IsChecked == true)
            {
                recurrence = RecurrenceInterval.Weekly;
            }
            else if (monthlyRadioButton.IsChecked == true)
            {
                recurrence = RecurrenceInterval.Monthly;
            }
            else if (endOfMonthRadioButton.IsChecked == true)
            {
                recurrence = RecurrenceInterval.EndOfMonth;
            }
            else if (yearlyRadioButton.IsChecked == true)
            {
                recurrence = RecurrenceInterval.Yearly;
            }


            // Create a Uri for the page that will be launched if the user
            // taps on the reminder. Use query string parameters to pass 
            // content to the page that is launched.
            string param1Value = param1TextBox.Text;
            string param2Value = param2TextBox.Text;
            string queryString = "";
            if (param1Value != "" && param2Value != "")
            {
                queryString = "?param1=" + param1Value + "&param2=" + param2Value;
            }
            else if(param1Value != "" || param2Value != "")
            {
                queryString = (param1Value!=null) ? "?param1="+param1Value : "?param2="+param2Value;
            }

            Uri navigationUri = new Uri("/ShowParams.xaml" + queryString, UriKind.Relative);

            Reminder reminder = new Reminder(name);
            reminder.Title = titleTextBox.Text;
            reminder.Content = contentTextBox.Text;
            reminder.BeginTime = beginTime;
            reminder.ExpirationTime = expirationTime;
            reminder.RecurrenceType = recurrence;
            reminder.NavigationUri = navigationUri;

            // Register the reminder with the system.
            ScheduledActionService.Add(reminder);

            // Navigate back to the main reminder list page.
            NavigationService.GoBack();

        }
    }
}