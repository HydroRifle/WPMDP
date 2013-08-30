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

namespace SliderBindings
{
    public partial class MainPage : PhoneApplicationPage
    {
        //public ObservableCollection<Recording> MyMusic = new ObservableCollection<Recording>();

        // Constructor
        public MainPage()
        {
            InitializeComponent();

            //// Add items to the collection.
            //MyMusic.Add(new Recording("Chris Sells", "Chris Sells Live",
            //new DateTime(2008, 2, 5)));
            //MyMusic.Add(new Recording("Luka Abrus",
            //"The Road to Redmond", new DateTime(2007, 4, 3)));
            //MyMusic.Add(new Recording("Jim Hance",
            //"The Best of Jim Hance", new DateTime(2007, 2, 6)));
            //// Set the data context for the combo box.
            //ComboBox1.DataContext = MyMusic;
        }
    }
    //public class Recording
    //{
    //    public Recording() { }
    //    public Recording(string artistName, string cdName, DateTime release)
    //    {
    //        Artist = artistName;
    //        Name = cdName;
    //        ReleaseDate = release;
    //    }
    //    public string Artist { get; set; }
    //    public string Name { get; set; }
    //    public DateTime ReleaseDate { get; set; }
    //    // Override the ToString method.
    //    public override string ToString()
    //    {
    //        return Name + " by " + Artist + ", Released: " + ReleaseDate.ToShortDateString();
    //    }
    //}
}