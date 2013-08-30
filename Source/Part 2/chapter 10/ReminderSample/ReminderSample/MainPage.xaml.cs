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
    public partial class MainPage : PhoneApplicationPage
    {
        IEnumerable<Reminder> reminders;

        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        private void ResetItemsList()
        {
            // Use GetActions to retrieve all of the scheduled actions
            // stored for this application. The type <Reminder> is specified
            // to retrieve only Reminder objects.
            reminders = ScheduledActionService.GetActions<Reminder>();

            // If there are 1 or more reminders, hide the "no reminders"
            // TextBlock. IF there are zero reminders, show the TextBlock.
            if (reminders.Count<Reminder>() > 0)
            {
                EmptyTextBlock.Visibility = Visibility.Collapsed;
            }
            else
            {
                EmptyTextBlock.Visibility = Visibility.Visible;
            }

            // Update the ReminderListBox with the list of reminders.
            // A full MVVM implementation can automate this step.
            ReminderListBox.ItemsSource = reminders;
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            // Reset the ReminderListBox items when the page is navigated to.
            ResetItemsList();
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            // The scheduled action name is stored in the Tag property
            // of the delete button for each reminder.
            string name = (string)((Button)sender).Tag;

            // Call Remove to unregister the scheduled action with the service.
            ScheduledActionService.Remove(name);

            // Reset the ReminderListBox items
            ResetItemsList();
        }

        private void ApplicationBarAddButton_Click(object sender, EventArgs e)
        {
            // Navigate to the AddReminder page when the add button is clicked.
            NavigationService.Navigate(new Uri("/AddReminder.xaml", UriKind.RelativeOrAbsolute));
        }
    }
}