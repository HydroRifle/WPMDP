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
using Microsoft.Devices;
using System.Collections.ObjectModel;
using System.Windows.Navigation;


namespace DeviceStatus
{
    /// <summary>
    /// The main page of the application, presenting the user with information about the device.
    /// </summary>
    public partial class MainPage : PhoneApplicationPage
    {
        /// <summary>
        /// Creates a new instance of the page.
        /// </summary>
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // The application was restored
            if (e.IsNavigationInitiator == false)
            {
                // Simulate a panorama change, in case we are viewing the capabilities item
                PanoStatus_SelectionChanged(this, null);
            }

            base.OnNavigatedTo(e);            
        }

        private void PanoStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PanoStatus.SelectedItem == null)
            {
                return;
            }            
        }        
        
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            // The application was sent to the background
            if (e.IsNavigationInitiator == false)
            {
                ;//UninitializeCamera();
            }

            base.OnNavigatedFrom(e);            
        }        
    }
}