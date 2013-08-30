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
using System.Collections.ObjectModel;
using System.Windows.Data;

namespace ComboBoxBingds
{
    public partial class MainPage : PhoneApplicationPage
    {
        public ObservableCollection<Recording> MyMusic = new ObservableCollection<Recording>();

        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Add items to the collection.
            MyMusic.Add(new Recording("Chris Sells", "Chris Sells Live",
            new DateTime(2008, 2, 5)));
            MyMusic.Add(new Recording("Luka Abrus",
            "The Road to Redmond", new DateTime(2007, 4, 3)));
            MyMusic.Add(new Recording("Jim Hance",
            "The Best of Jim Hance", new DateTime(2007, 2, 6)));
            // Set the data context
            LayoutRoot.DataContext = new CollectionViewSource { Source = MyMusic };
        }       
    }

    // A simple business object
    public class Recording
    {
        public Recording() { }
        public Recording(string artistName, string cdName, DateTime release)
        {
            Artist = artistName;
            Name = cdName;
            ReleaseDate = release;
        }
        public string Artist { get; set; }
        public string Name { get; set; }
        public DateTime ReleaseDate { get; set; }
        // Override the ToString method.
        public override string ToString()
        {
            return Name + " by " + Artist + ", Released: " + ReleaseDate.ToShortDateString();
        }
    }

    public class StringFormatter : IValueConverter
    {
        // This converts the value object to the string to display.
        // This will work with most simple types.
        public object Convert(object value, Type targetType,
        object parameter, System.Globalization.CultureInfo culture)
        {
            // Retrieve the format string and use it to format the value.
            string formatString = parameter as string;
            if (!string.IsNullOrEmpty(formatString))
            {
                return string.Format(culture, formatString, value);
            }
            // If the format string is null or empty, simply
            // call ToString() on the value.
            return value.ToString();
        }
        // No need to implement converting back on a one-way binding
        public object ConvertBack(object value, Type targetType,
        object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}