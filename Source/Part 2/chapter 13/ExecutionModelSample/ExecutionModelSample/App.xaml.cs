﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Threading;
using System.IO;
using System.IO.IsolatedStorage;

namespace ExecutionModelSample
{
    public partial class App : Application
    {

        #region ApplicationDataObject

        // Declare a private variable to store application state.
        private string _applicationDataObject;

        // Declare an event for when the application data changes.
        public event EventHandler ApplicationDataObjectChanged;

        // Declare a public property to access the application data variable.
        public string ApplicationDataObject
        {
            get { return _applicationDataObject; }
            set
            {
                if (value != _applicationDataObject)
                {
                    _applicationDataObject = value;
                    OnApplicationDataObjectChanged(EventArgs.Empty);
                }
            }
        }

        // Create a method to raise the ApplicationDataObjectChanged event.
        protected void OnApplicationDataObjectChanged(EventArgs e)
        {
            EventHandler handler = ApplicationDataObjectChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        // Declare a public property to store the status of the application data.
        public string ApplicationDataStatus { get; set; }

        #endregion

        /// <summary>
        /// Provides easy access to the root frame of the Phone Application.
        /// </summary>
        /// <returns>The root frame of the Phone Application.</returns>
        public PhoneApplicationFrame RootFrame { get; private set; }

        /// <summary>
        /// Gets whether or not the application was restored from tombstoning.
        /// </summary>
        public static bool WasTombstoned { get; private set; }

        /// <summary>
        /// Constructor for the Application object.
        /// </summary>
        public App()
        {
            // Global handler for uncaught exceptions. 
            UnhandledException += Application_UnhandledException;

            // Standard Silverlight initialization
            InitializeComponent();

            // Phone-specific initialization
            InitializePhoneApplication();

            // Show graphics profiling information while debugging.
            if (System.Diagnostics.Debugger.IsAttached)
            {
                // Display the current frame rate counters.
                Application.Current.Host.Settings.EnableFrameRateCounter = true;

                // Show the areas of the app that are being redrawn in each frame.
                //Application.Current.Host.Settings.EnableRedrawRegions = true;

                // Enable non-production analysis visualization mode, 
                // which shows areas of a page that are handed off to GPU with a colored overlay.
                //Application.Current.Host.Settings.EnableCacheVisualization = true;

                // Disable the application idle detection by setting the UserIdleDetectionMode property of the
                // application's PhoneApplicationService object to Disabled.
                // Caution:- Use this under debug mode only. Application that disable user idle detection will continue to run
                // and consume battery power when the user is not using the phone.
                PhoneApplicationService.Current.UserIdleDetectionMode = IdleDetectionMode.Disabled;
            }

        }

        #region ExecutionModelEvents

        // Code to execute when the application is launching (eg, from Start)
        // This code will not execute when the application is reactivated
        private void Application_Launching(object sender, LaunchingEventArgs e)
        {
        }

        // Code to execute when the application is activated (brought to foreground)
        // This code will not execute when the application is first launched
        private void Application_Activated(object sender, ActivatedEventArgs e)
        {
            if (e.IsApplicationInstancePreserved)
            {
                WasTombstoned = false;
                ApplicationDataStatus = "application instance preserved.";
                return;
            }
            else
            {
                WasTombstoned = true;
            }

            // Check to see if the key for the application state data is in the State dictionary.
            if (PhoneApplicationService.Current.State.ContainsKey("ApplicationDataObject"))
            {
                // If it exists, assign the data to the application member variable.
                ApplicationDataStatus = "data from preserved state.";
                ApplicationDataObject = PhoneApplicationService.Current.State["ApplicationDataObject"] as string;
            }
        }

        // Code to execute when the application is deactivated (sent to background)
        // This code will not execute when the application is closing
        private void Application_Deactivated(object sender, DeactivatedEventArgs e)
        {
            // If there is data in the application member variable...
            if (!string.IsNullOrEmpty(ApplicationDataObject))
            {
                // Store it in the State dictionary.
                PhoneApplicationService.Current.State["ApplicationDataObject"] = ApplicationDataObject;

                // Also store it in Isolated Storage, in case the application is never reactivated.
                SaveDataToIsolatedStorage("myDataFile.txt", ApplicationDataObject);
            }
        }

        // Code to execute when the application is closing (eg, user hit Back)
        // This code will not execute when the application is deactivated
        private void Application_Closing(object sender, ClosingEventArgs e)
        {
            // The application will not be tombstoned, so only save to Isolated Storage
            if (!string.IsNullOrEmpty(ApplicationDataObject))
            {
                SaveDataToIsolatedStorage("myDataFile.txt", ApplicationDataObject);
            }
        }

        #endregion

        #region GetAndSaveDataMethods

        public void GetDataAsync()
        {
            // Call the GetData method on a new thread.
            Thread t = new Thread(new ThreadStart(GetData));
            t.Start();
        }

        private void GetData()
        {
            // Check the time elapsed since data was last saved to Isolated Storage
            TimeSpan TimeSinceLastSave = TimeSpan.FromSeconds(0);
            if (IsolatedStorageSettings.ApplicationSettings.Contains("DataLastSavedTime"))
            {
                DateTime dataLastSaveTime = (DateTime)IsolatedStorageSettings.ApplicationSettings["DataLastSavedTime"];
                TimeSinceLastSave = DateTime.Now - dataLastSaveTime;
            }

            // Check to see if data exists in Isolated Storage and see if the data is fresh.
            // This example uses 30 seconds as the valid time window to make it easy to test. 
            // Real apps will use a larger window.
            IsolatedStorageFile isoStore = IsolatedStorageFile.GetUserStoreForApplication();
            if (isoStore.FileExists("myDataFile.txt") && TimeSinceLastSave.TotalSeconds < 30)
            {
                // This method loads the data from Isolated Storage, if it is available.
                StreamReader sr = new StreamReader(isoStore.OpenFile("myDataFile.txt", FileMode.Open));
                string data = sr.ReadToEnd();
                sr.Close();

                ApplicationDataStatus = "data from isolated storage";
                ApplicationDataObject = data;
            }
            else
            {
                // Otherwise it gets the data from the Web. 
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri("http://windowsteamblog.com/windows_phone/b/windowsphone/rss.aspx"));
                request.BeginGetResponse(HandleWebResponse, request);
            }
        }

        private void HandleWebResponse(IAsyncResult result)
        {
            // Put this in a try block in case the Web request was unsuccessful.
            try
            {
                // Get the request from the IAsyncResult
                HttpWebRequest request = (HttpWebRequest)(result.AsyncState);

                // Read the response stream from the response.
                StreamReader sr = new StreamReader(request.EndGetResponse(result).GetResponseStream());
                string data = sr.ReadToEnd();
                data = data.Substring(0, 255);

                // Use the Dispatcher to call SetData on the UI thread, passing the retrieved data.
                //Dispatcher.BeginInvoke(() => { SetData(data, "web"); });
                ApplicationDataStatus = "data from web.";
                ApplicationDataObject = data;
            }
            catch
            {
                // If the data request fails, alert the user
                ApplicationDataStatus = "Unable to get data from Web.";
                ApplicationDataObject = null;
            }
        }

        private void SaveDataToIsolatedStorage(string isoFileName, string value)
        {
            IsolatedStorageFile isoStore = IsolatedStorageFile.GetUserStoreForApplication();
            StreamWriter sw = new StreamWriter(isoStore.OpenFile(isoFileName, FileMode.OpenOrCreate));
            sw.Write(value);
            sw.Close();
            IsolatedStorageSettings.ApplicationSettings["DataLastSaveTime"] = DateTime.Now;
        }

        #endregion

        // Code to execute if a navigation fails
        private void RootFrame_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            if (System.Diagnostics.Debugger.IsAttached)
            {
                // A navigation has failed; break into the debugger
                System.Diagnostics.Debugger.Break();
            }
        }

        // Code to execute on Unhandled Exceptions
        private void Application_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            if (System.Diagnostics.Debugger.IsAttached)
            {
                // An unhandled exception has occurred; break into the debugger
                System.Diagnostics.Debugger.Break();
            }
        }

        #region Phone application initialization

        // Avoid double-initialization
        private bool phoneApplicationInitialized = false;

        // Do not add any additional code to this method
        private void InitializePhoneApplication()
        {
            if (phoneApplicationInitialized)
                return;

            // Create the frame but don't set it as RootVisual yet; this allows the splash
            // screen to remain active until the application is ready to render.
            RootFrame = new PhoneApplicationFrame();
            RootFrame.Navigated += CompleteInitializePhoneApplication;

            // Handle navigation failures
            RootFrame.NavigationFailed += RootFrame_NavigationFailed;

            // Ensure we don't initialize again
            phoneApplicationInitialized = true;
        }

        // Do not add any additional code to this method
        private void CompleteInitializePhoneApplication(object sender, NavigationEventArgs e)
        {
            // Set the root visual to allow the application to render
            if (RootVisual != RootFrame)
                RootVisual = RootFrame;

            // Remove this handler since it is no longer needed
            RootFrame.Navigated -= CompleteInitializePhoneApplication;
        }

        #endregion
    }
}