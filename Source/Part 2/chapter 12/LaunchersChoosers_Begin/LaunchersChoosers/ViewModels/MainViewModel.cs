using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections.ObjectModel;


namespace LaunchersChoosers
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel()
        {
            this.Items = new ObservableCollection<ItemViewModel>();
            this.ChoosersItems = new ObservableCollection<ItemViewModel>();
        }

        /// <summary>
        /// A collection for ItemViewModel objects.
        /// </summary>
        public ObservableCollection<ItemViewModel> Items { get; private set; }
        public ObservableCollection<ItemViewModel> ChoosersItems { get; private set; }

        private string _sampleProperty = "Sample Runtime Property Value";
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding
        /// </summary>
        /// <returns></returns>
        public string SampleProperty
        {
            get
            {
                return _sampleProperty;
            }
            set
            {
                if (value != _sampleProperty)
                {
                    _sampleProperty = value;
                    NotifyPropertyChanged("SampleProperty");
                }
            }
        }

        public bool IsDataLoaded
        {
            get;
            private set;
        }

        /// <summary>
        /// Creates and adds a few ItemViewModel objects into the Items collection.
        /// </summary>
        public void LoadData()
        {
            // Sample data; replace with real data
            this.Items.Add(new ItemViewModel() { LineOne="EmailCompose", LineTwo="Email Compose Task", LineThree="/Launchers/EmailCompose.xaml" });
            this.Items.Add(new ItemViewModel() { LineOne="PhoneCallTask", LineTwo="Phone Call Task", LineThree="/Launchers/PhoneCall.xaml" });
            this.Items.Add(new ItemViewModel() { LineOne="Search", LineTwo="Search Task", LineThree="/Launchers/Search.xaml" });
            this.Items.Add(new ItemViewModel() { LineOne="Smscompose", LineTwo="Smscompose Task", LineThree="/Launchers/Smscompose.xaml" });
            this.Items.Add(new ItemViewModel() { LineOne="WebSearch", LineTwo="Web Search Task", LineThree="/Launchers/WebBrowser.xaml" });
            this.Items.Add(new ItemViewModel() { LineOne="MediaPlayer", LineTwo="Media Player Launcher", LineThree="/Launchers/MediaLauncher.xaml" });
            this.Items.Add(new ItemViewModel() { LineOne="Marketplace", LineTwo="MarketPlaceDetail Task and MarketplaceHub Task", LineThree="/Launchers/MarketPlace.xaml" });

            this.ChoosersItems.Add(new ItemViewModel() { ChoosersLineOne = "Camer", ChoosersLineTwo = "Camer chooser task", ChoosersLineThree = "/Choosers/CamerChooser.xaml" });
            this.ChoosersItems.Add(new ItemViewModel() { ChoosersLineOne="Email Address", ChoosersLineTwo="email address chooser task", ChoosersLineThree="/Choosers/EmailAddressChooser.xaml" });
            this.ChoosersItems.Add(new ItemViewModel() { ChoosersLineOne="Phone Number", ChoosersLineTwo="phone number chooser task", ChoosersLineThree="/Choosers/PhoneNumberChooser.xaml" });
            this.ChoosersItems.Add(new ItemViewModel() { ChoosersLineOne="Photo", ChoosersLineTwo="photo chooser task", ChoosersLineThree="/Choosers/PhotoChooser.xaml" });
            this.ChoosersItems.Add(new ItemViewModel() { ChoosersLineOne="Save Email Address", ChoosersLineTwo="Save email address task", ChoosersLineThree="/Choosers/SaveEmailAddress.xaml" });
            this.ChoosersItems.Add(new ItemViewModel() { ChoosersLineOne="Save Phone number", ChoosersLineTwo="Save Phone number task", ChoosersLineThree="/Choosers/SavePhoneNumber.xaml" });

            this.IsDataLoaded = true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}