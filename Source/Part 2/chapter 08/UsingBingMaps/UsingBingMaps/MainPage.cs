// ----------------------------------------------------------------------------------
// Microsoft Developer & Platform Evangelism
// 
// Copyright (c) Microsoft Corporation. All rights reserved.
// 
// THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
// EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED WARRANTIES 
// OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
// ----------------------------------------------------------------------------------
// The example companies, organizations, products, domain names,
// e-mail addresses, logos, people, places, and events depicted
// herein are fictitious.  No association with any real company,
// organization, product, domain name, email address, logo, person,
// places, or events is intended or should be inferred.
// ----------------------------------------------------------------------------------

using System;
using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;
using System.Windows.Controls.Primitives;

using UsingBingMaps.Helpers;

namespace UsingBingMaps
{
    /// <summary>
    /// This partial class contains startup code for implementing the bing maps lab,
    /// so you can concentrate on the Bing map control and services and not on the
    /// UI behavior or application flow.
    /// <remarks>
    /// User code should be added to the MainPage.xaml.cs file only.
    /// </remarks>
    public partial class MainPage : INotifyPropertyChanged
    {
        #region Fields

        /// <value>Helper for handling mouse (touch) events.</value>
        private readonly TouchBehavior _touchBehavior;

        /// <value>Helper for handling visual states.</value>
        private readonly VisualStates _visualStates;

        #endregion

        #region Ctor

        public MainPage()
        {
            InitializeDefaults();
            InitializeComponent();

            _touchBehavior = new TouchBehavior(this);
            _touchBehavior.Tap += touchBehavior_Tap;
            _touchBehavior.Move += touchBehavior_Move;

            _visualStates = new VisualStates(this);

            DataContext = this;
        }

        #endregion

        #region Event Handlers

        private void ButtonLocation_Click(object sender, EventArgs e)
        {
            CenterLocation();  
        }

        private void ButtonRoute_Click(object sender, EventArgs e)
        {
            // Display or hide route panel.
            if (_visualStates.RouteState == VisualStates.HideRoute)
            {
                _visualStates.RouteState = VisualStates.ShowRoute;
            }
            else
            {
                _visualStates.RouteState = VisualStates.HideRoute;
            }
        }

        private void ButtonMode_Click(object sender, EventArgs e)
        {
            ChangeMapMode();         
        }

        private void touchBehavior_Tap(object sender, TouchBehaviorEventArgs e)
        {
            // Add control code.
        }

        private void touchBehavior_Move(object sender, TouchBehaviorEventArgs e)
        {
            // Add control code.
        }

        private void ButtonGo_Click(object sender, RoutedEventArgs e)
        {
            CalculateRoute();

            // Display or hide route panel.
            if (_visualStates.RouteState == VisualStates.ShowRoute)
            {
                _visualStates.RouteState = VisualStates.HideRoute;
            }       
        }

        private void ButtonZoomIn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Zoom += 1;
        }

        private void ButtonZoomOut_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Zoom -= 1;
        }

        #endregion

        #region Property Changed

        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        #endregion

        #region Visual States
        /// <summary>
        /// An internal helper class for handling MainPage visual state transitions.
        /// </summary>
        private class VisualStates
        {
            #region Predefined Visual States
            // Route States
            public const string ShowRoute = "ShowRoute";
            public const string HideRoute = "HideRoute";
            #endregion

            #region Fields
            private readonly Control _control;
            private string _routeState = VisualStates.HideRoute;
            #endregion

            #region Properties
            /// <summary>
            /// Change the route panel visual state.
            /// </summary>
            public string RouteState
            {
                get { return _routeState; }
                set
                {
                    if (_routeState != value)
                    {
                        _routeState = value;
                        VisualStateManager.GoToState(_control, value, true);
                    }
                }
            }

            #endregion

            #region Ctor
            /// <summary>
            /// Initializes a new instance of this class.
            /// </summary>
            /// <param name="control">The target control.</param>
            public VisualStates(Control control)
            {
                _control = control;
            }
            #endregion
        }
        #endregion
    }
}