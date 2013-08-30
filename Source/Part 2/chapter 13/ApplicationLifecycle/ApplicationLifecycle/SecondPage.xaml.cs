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

namespace ApplicationLifecycle
{
    public partial class SecondPage : PhoneApplicationPage
    {
        Control focusedElement;
        bool bFocused = false;

        public SecondPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            //Trace the event for debug purposes
            Utils.Trace("Navigated To SecondPage");

            //Check if page state has saved focus and apply it back
            if (State.ContainsKey("FocusedElement"))
            {
                focusedElement = this.FindName(State["FocusedElement"] as string) as Control;
                bFocused = true;

                //if (null != focusedElement)
                //    focusedElement.Focus();
            }
            else
            {
                bFocused = false;
            }

            base.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
            //Trace the event for debug purposes
            Utils.Trace("Navigated From MainPage");

            //Remove focused element from previous time if any
            if (State.ContainsKey("FocusedElement"))
                State.Remove("FocusedElement");

            //If some input control is in focus, save it to the page state
            object obj = FocusManager.GetFocusedElement();
            if (null != obj)
            {
                string focusedControl = (obj as FrameworkElement).Name;
                State.Add("FocusedElement", focusedControl);
            }

            base.OnNavigatedFrom(e);
        }

        private void PhoneApplicationPage_LayoutUpdated(object sender, EventArgs e)
        {
            try
            {
                if (bFocused && (null != focusedElement))
                {
                    bFocused = false;
                    focusedElement.Focus();
                }
            }
            catch (Exception ex)
            {
                //Trace the exception for debug purposes
                Utils.Trace(String.Format("LayoutUpdated occur exception = {0}.", ex.GetType()) + "\n");
            }
        }

        private void ApplicationBarGoBack_Click(object sender, EventArgs e)
        {
            //Navigate go back
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
            else
            {
                NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
            }
        }

        private void ApplicationBarSave_Click(object sender, EventArgs e)
        {
            Utils.SaveTravelReport(
                (App.Current.RootVisual as PhoneApplicationFrame).DataContext as TravelReportInfo,
                "TravelReportInfo.dat",
                false);
        }

        private void ApplicationBarCancel_Click(object sender, EventArgs e)
        {
            Utils.ClearTravelReport(((App.Current.RootVisual as PhoneApplicationFrame).DataContext as TravelReportInfo));            
        }
    }
}