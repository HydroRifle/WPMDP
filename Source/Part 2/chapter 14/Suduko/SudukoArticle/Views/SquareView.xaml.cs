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
using SudukoArticle.ViewModels;
using SudukoArticle.Utility;
using Microsoft.Devices;

namespace SudukoArticle.Views
{
    public partial class SquareView : UserControl
    {
        private SquareViewModel _viewModel;
        public SquareView(SquareViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            this.DataContext = _viewModel;
            _viewModel.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(viewModel_PropertyChanged);
            SetColors();
            SetThickness();
        }

        void viewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "CurrentBoxState":
                    SetColors();
                    break;
            }
        }

        private void SetColors()
        {
            switch (_viewModel.CurrentBoxState)
            {
                case BoxStates.UnEditable:
                    MainText.Foreground = (SolidColorBrush)Resources["PhoneDisabledBrush"];
                    LayoutRoot.Background = (SolidColorBrush)Resources["PhoneBackgroundBrush"];
                    break;
                case BoxStates.Invalid:
                    MainText.Foreground = (SolidColorBrush)Resources["PhoneContrastForegroundBrush"];
                    LayoutRoot.Background = (SolidColorBrush)Resources["PhoneContrastBackgroundBrush"];
                    VibrateController.Default.Start(TimeSpan.FromMilliseconds(200));
                    break;
                case BoxStates.Selected:
                    MainText.Foreground = (SolidColorBrush)Resources["PhoneAccentBrush"];
                    LayoutRoot.Background = (SolidColorBrush)Resources["PhoneChromeBrush"];
                    break;
                default:
                    MainText.Foreground = (SolidColorBrush)Resources["PhoneAccentBrush"];
                    LayoutRoot.Background = (SolidColorBrush)Resources["PhoneBackgroundBrush"];
                    break;
            }
        }

        private void SetThickness()
        {
            int topBorder = 1;
            int rightBorder = 1;
            int leftBorder = 1;
            int bottomBorder = 1;
            if (_viewModel.Row % 3 == 0)
                topBorder = 3;
            if (_viewModel.Column % 3 == 0)
                leftBorder = 3;
            if (_viewModel.Row == 8)
                bottomBorder = 3;
            if (_viewModel.Column == 8)
                rightBorder = 3;

            BoxGridBorder.BorderThickness = new Thickness(leftBorder, topBorder, rightBorder, bottomBorder);
        }

        public event EventHandler BoxClicked;

        private void LayoutRoot_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (BoxClicked != null)
                BoxClicked(this, null);
        }
    }
}
