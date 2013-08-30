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
    public partial class MainPage : PhoneApplicationPage
    {
        Control focusedElement;
        bool bFocused = false;
        bool bToDateChangedNavigateTo = false;
        bool bFromDateChangedNavigateTo = false;

        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            base.OnBackKeyPress(e);

            //Ask userto preserve data in persistent store
            MessageBoxResult res = MessageBox.Show("Do you want to save your work before?", "You are exiting the application", MessageBoxButton.OKCancel);

            if (res == MessageBoxResult.OK)
                Utils.SaveTravelReport((App.Current.RootVisual as PhoneApplicationFrame).DataContext as TravelReportInfo,
                    "TravelReportInfo.dat", true);
            else
                Utils.ClearTravelReport((App.Current.RootVisual as PhoneApplicationFrame).DataContext as TravelReportInfo);
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            //Trace the event for debug purposes
            Utils.Trace("Navigated To MainPage");

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

            TravelReportInfo travelReportInfo = ((App.Current.RootVisual as PhoneApplicationFrame).DataContext as TravelReportInfo);
            if (State.ContainsKey("txtDestination"))
                travelReportInfo.Destination = State["txtDestination"] as string;

            if (State.ContainsKey("txtJustification"))
            {
                travelReportInfo.Justification = State["txtJustification"] as string;                
            }

            if (State.ContainsKey("txtToDate"))
            {
                if (!bToDateChangedNavigateTo)
                {
                    travelReportInfo.LastDay = DateTime.Parse(State["txtToDate"] as string);
                    txtToDate.Value = travelReportInfo.LastDay;//this operation call txtFromDate_ValueChanged event
                    bToDateChangedNavigateTo = false;
                }
                else
                {
                    bToDateChangedNavigateTo = false;
                }
            }
            else
            {
                //format todata
                txtToDate.Value = travelReportInfo.LastDay;
                bToDateChangedNavigateTo = false;
            }

            if (State.ContainsKey("txtFromDate"))
            {
                if (!bFromDateChangedNavigateTo)
                {
                    travelReportInfo.FirstDay = DateTime.Parse(State["txtFromDate"] as string);
                    txtFromDate.Value = travelReportInfo.FirstDay;//this operation call txtFromDate_ValueChanged event 

                    bFromDateChangedNavigateTo = false;
                }
                else
                {
                    bFromDateChangedNavigateTo = false;
                }
            }
            else
            {
                // format fromdata
                txtFromDate.Value = travelReportInfo.FirstDay;
                bFromDateChangedNavigateTo = false;
            }

            base.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
            //Trace the event for debug purposes
            Utils.Trace("Navigated From MainPage");

            //Remove focused element from previous time if any
            if (State.ContainsKey("FocusedElement"))
            {
                State.Remove("FocusedElement");
            }

            //If some input control is in focus, save it to the page state
            object obj = FocusManager.GetFocusedElement();
            if (null != obj)
            {
                string focusedControl = (obj as FrameworkElement).Name;
                State.Add("FocusedElement", focusedControl);
            }

            if (State.ContainsKey("txtDestination"))
                State.Remove("txtDestination");

            State.Add("txtDestination", txtDestination.Text);

            if (State.ContainsKey("txtJustification"))
                State.Remove("txtJustification");

            State.Add("txtJustification", txtJustification.Text);

            if (State.ContainsKey("txtFromDate"))
                State.Remove("txtFromDate");

            State.Add("txtFromDate", txtFromDate.ValueString);

            if (State.ContainsKey("txtToDate"))
                State.Remove("txtToDate");

            State.Add("txtToDate", txtToDate.ValueString);

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
                    focusedElement = null;
                }
            }
            catch (Exception ex)
            {
                //Trace the exception for debug purposes
                Utils.Trace(String.Format("Exception = {0}.", ex.GetType()) + "\n"); 
            }

            TravelReportInfo travelReportInfo = ((App.Current.RootVisual as PhoneApplicationFrame).DataContext as TravelReportInfo);

            txtFromDate.Value = travelReportInfo.FirstDay;
            bFromDateChangedNavigateTo = false;

            txtToDate.Value = travelReportInfo.LastDay;
            bToDateChangedNavigateTo = false;
        }

        private void txtToDate_ValueChanged(object sender, DateTimeValueChangedEventArgs e)
        {
            TravelReportInfo travelReportInfo = ((App.Current.RootVisual as PhoneApplicationFrame).DataContext as TravelReportInfo);
            travelReportInfo.LastDay = DateTime.Parse(txtToDate.ValueString);

            bToDateChangedNavigateTo = true;
        }

        private void txtFromDate_ValueChanged(object sender, DateTimeValueChangedEventArgs e)
        {
            TravelReportInfo travelReportInfo = ((App.Current.RootVisual as PhoneApplicationFrame).DataContext as TravelReportInfo);
            travelReportInfo.FirstDay = DateTime.Parse(txtFromDate.ValueString);

            bFromDateChangedNavigateTo = true;
        }

        private void ApplicationBarCancel_Click(object sender, EventArgs e)
        {
            Utils.ClearTravelReport(((App.Current.RootVisual as PhoneApplicationFrame).DataContext as TravelReportInfo));
        }

        private void ApplicationBarNext_Click(object sender, EventArgs e)
        {
            if (NavigationService.CanGoForward)
            {
                NavigationService.GoForward();
            }
            else
            {
                //Navigate to second page
                NavigationService.Navigate(new Uri("/SecondPage.xaml", UriKind.Relative));
            }
        }

        private void ApplicationBarSave_Click(object sender, EventArgs e)
        {
            Utils.SaveTravelReport(
                (App.Current.RootVisual as PhoneApplicationFrame).DataContext as TravelReportInfo,
                "TravelReportInfo.dat",
                false);
        }
        public static void PreserveFocusState(IDictionary<string, object> state, FrameworkElement parent)
        {
            // Determine which control currently has focus.
            Control focusedControl = FocusManager.GetFocusedElement() as Control;

            // If no control has focus, store null in the State dictionary.
            if (focusedControl == null)
            {
                state["FocusedControlName"] = null;
            }
            else
            {
                // Find the control within the parent
                Control foundFE = parent.FindName(focusedControl.Name) as Control;

                // If the control isn't found within the parent, store null in the State dictionary.
                if (foundFE == null)
                {
                    state["FocusedControlName"] = null;
                }
                else
                {
                    // otherwise store the name of the control with focus.
                    state["FocusedControlName"] = focusedControl.Name;
                }
            }

        }

    }
}