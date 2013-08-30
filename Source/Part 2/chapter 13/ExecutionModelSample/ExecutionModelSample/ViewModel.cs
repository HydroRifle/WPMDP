using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace ExecutionModelSample
{
    [DataContract]
    public class ViewModel : INotifyPropertyChanged
    {


        private string _textBox1Text;
        private bool _checkBox1IsChecked;
        private bool _radioButton1IsChecked;
        private bool _radioButton2IsChecked;
        private double _slider1Value;


        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            if (null != PropertyChanged)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }


        [DataMember]
        public string TextBox1Text
        {
            get { return _textBox1Text; }
            set
            {
                _textBox1Text = value;
                NotifyPropertyChanged("TextBox1Text");
            }
        }

        [DataMember]
        public bool CheckBox1IsChecked
        {
            get { return _checkBox1IsChecked; }
            set
            {
                _checkBox1IsChecked = value;
                NotifyPropertyChanged("CheckBox1IsChecked");
            }
        }

        [DataMember]
        public double Slider1Value
        {
            get { return _slider1Value; }
            set
            {
                _slider1Value = value;
                NotifyPropertyChanged("Slider1Value");
            }
        }

        [DataMember]
        public bool RadioButton1IsChecked
        {
            get { return _radioButton1IsChecked; }
            set
            {
                _radioButton1IsChecked = value;
                NotifyPropertyChanged("RadioButton1IsChecked");
            }
        }

        [DataMember]
        public bool RadioButton2IsChecked
        {
            get { return _radioButton2IsChecked; }
            set
            {
                _radioButton2IsChecked = value;
                NotifyPropertyChanged("RadioButton2IsChecked");
            }
        }

    }
}
