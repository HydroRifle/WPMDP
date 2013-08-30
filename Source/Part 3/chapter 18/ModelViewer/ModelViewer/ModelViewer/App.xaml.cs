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
using System.Windows;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace ModelViewer
{
    // The App implements IServiceProvider for ContentManagers and other types to
    // be able to access the graphics device services.
    public partial class App : Application, IServiceProvider
    {
        private readonly Dictionary<string, ModelMetadata> modelsStore;
        public Dictionary<string, ModelMetadata> ModelsStore
        {
            get { return modelsStore; }
        }

        /// <summary>
        /// Provides easy access to the root frame of the Phone Application.
        /// </summary>
        /// <returns>The root frame of the Phone Application.</returns>
        public PhoneApplicationFrame RootFrame { get; private set; }

        /// <summary>
        /// Provides access to a ContentManager for the application.
        /// </summary>
        public ContentManager Content { get; private set; }

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

            // XNA initialization
            InitializeXnaApplication();

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

            modelsStore = new Dictionary<string, ModelMetadata>();
            InitializeModelsMetadata();
        }

        private void InitializeModelsMetadata()
        {
            modelsStore.Clear();

            //ScottGu Model
            ModelMetadata metadata = new ModelMetadata()
            {
                ID = "polo",
                Name = "T-Shirt",
                Description = "Polo T-Shirt model",
                IsLocal = true,
                IsContent = true,
                DownloadProgress = 100.0f,
                Assets = new string[] { "polo" },
                World = Matrix.CreateRotationY(MathHelper.Pi) * Matrix.CreateTranslation(0f, -0.15f, 0f),
                ViewMatrix = Matrix.CreateLookAt(new Vector3(1.0f, 0f, 0f), Vector3.Zero, Vector3.Up),
                FieldOfViewDivisor = 2f,
                AspectRatio = 2.0f,
                NearPlaneDistance = 0.01f,
                FarPlaneDistance = 1000f,
            };
            ModelsStore.Add(metadata.ID, metadata);

            //Tank Model
            metadata = new ModelMetadata()
            {
                ID = "tank",
                Name = "Tank",
                Description = "Tank model",
                IsLocal = false,
                IsContent = false,
                Assets = new string[] { "tank", "engine_diff_tex_0", "turret_alt_diff_tex_0" },
                World = Matrix.CreateRotationY(MathHelper.Pi) * Matrix.CreateTranslation(new Vector3(0f, -150f, 0f)),
                ViewMatrix = Matrix.CreateLookAt(new Vector3(1500f, 1000f, 0f), Vector3.Zero, Vector3.Up),
                FieldOfViewDivisor = 1f,
                AspectRatio = 1f,
                NearPlaneDistance = 100f,
                FarPlaneDistance = 2000f,
            };
            ModelsStore.Add(metadata.ID, metadata);

            //Spaceship Model
            metadata = new ModelMetadata()
            {
                ID = "spaceship",
                Name = "Spaceship",
                Description = "Alien spaceship model",
                IsLocal = false,
                IsContent = false,
                Assets = new string[] { "spaceship", "enemy_0" },
                World = Matrix.Identity,
                ViewMatrix = Matrix.CreateLookAt(new Vector3(3500, 400, 0) + new Vector3(0, 250, 0), new Vector3(0, 250, 0), Vector3.Up),
                FieldOfViewDivisor = 1f,
                AspectRatio = 1.66666663f,
                NearPlaneDistance = 10f,
                FarPlaneDistance = 20000f,
            };
            ModelsStore.Add(metadata.ID, metadata);

            //Marble Model
            metadata = new ModelMetadata()
            {
                ID = "marble",
                Name = "Marble",
                Description = "Marble model",
                IsLocal = false,
                IsContent = false,
                Assets = new string[] { "marble" },
                World = Matrix.CreateRotationX(-MathHelper.PiOver2) * Matrix.CreateRotationZ(MathHelper.Pi),
                ViewMatrix = Matrix.CreateLookAt(new Vector3(50f, 0f, 0f), Vector3.Zero, Vector3.Up),
                FieldOfViewDivisor = 1f,
                AspectRatio = 1.65f,
                NearPlaneDistance = 0.01f,
                FarPlaneDistance = 1000f,
            };
            ModelsStore.Add(metadata.ID, metadata);
        }

        // Code to execute when the application is launching (eg, from Start)
        // This code will not execute when the application is reactivated
        private void Application_Launching(object sender, LaunchingEventArgs e)
        {
            //Downloader.RemoveAllRequests();
        }

        // Code to execute when the application is activated (brought to foreground)
        // This code will not execute when the application is first launched
        private void Application_Activated(object sender, ActivatedEventArgs e)
        {
        }

        // Code to execute when the application is deactivated (sent to background)
        // This code will not execute when the application is closing
        private void Application_Deactivated(object sender, DeactivatedEventArgs e)
        {
        }

        // Code to execute when the application is closing (eg, user hit Back)
        // This code will not execute when the application is deactivated
        private void Application_Closing(object sender, ClosingEventArgs e)
        {
        }

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

        #region XNA application initialization

        // Performs initialization of the XNA types required for the application.
        private void InitializeXnaApplication()
        {
            // Create the ContentManager so the application can load precompiled assets
            Content = new ContentManager(this, "Content");

            // Create a GameTimer to pump the XNA FrameworkDispatcher
            GameTimer frameworkDispatcherTimer = new GameTimer();
            frameworkDispatcherTimer.FrameAction += FrameworkDispatcherFrameAction;
            frameworkDispatcherTimer.Start();
        }

        // An event handler that pumps the FrameworkDispatcher each frame.
        // FrameworkDispatcher is required for a lot of the XNA events and
        // for certain functionality such as SoundEffect playback.
        private void FrameworkDispatcherFrameAction(object sender, EventArgs e)
        {
            FrameworkDispatcher.Update();
        }

        #endregion

        #region IServiceProvider Members

        /// <summary>
        /// Gets a service from the ApplicationLifetimeObjects collection.
        /// </summary>
        /// <param name="serviceType">The type of service to retrieve.</param>
        /// <returns>The first item in the ApplicationLifetimeObjects collection of the requested type.</returns>
        public object GetService(Type serviceType)
        {
            // Find the first item that matches the requested type
            foreach (object item in ApplicationLifetimeObjects)
            {
                if (serviceType.IsAssignableFrom(item.GetType()))
                    return item;
            }

            // Throw an exception if there was no matching item
            throw new InvalidOperationException("No object in the ApplicationLifetimeObjects is assignable to " + serviceType);
        }

        #endregion
    }
}
