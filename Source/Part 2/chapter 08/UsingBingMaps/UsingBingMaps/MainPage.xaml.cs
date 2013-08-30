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
using System.Device.Location;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;

using Microsoft.Phone.Controls;
using Microsoft.Phone.Controls.Maps;
using Microsoft.Phone.Controls.Maps.Core;

using UsingBingMaps.Models;
using UsingBingMaps.Helpers;
using UsingBingMaps.Bing.Route;

namespace UsingBingMaps
{
    public partial class MainPage : PhoneApplicationPage
    {
        #region Consts

        /// <value>Default map zoom level.</value>
        private const double DefaultZoomLevel = 16.0;

        /// <value>Maximum map zoom level allowed.</value>
        private const double MaxZoomLevel = 21.0;

        /// <value>Minimum map zoom level allowed.</value>
        private const double MinZoomLevel = 1.0;

        #endregion

        #region Fields

        /// <value>Provides credentials for the map control.</value>
        private readonly CredentialsProvider _credentialsProvider = new ApplicationIdCredentialsProvider(App.Id);

        /// <value>Default location coordinate.</value>

        //Qingdao Zhanqiao
        private static readonly GeoCoordinate DefaultLocation = new GeoCoordinate(36.05826726951574, 120.31532406806946);
        //Qingdao Wusi Square
        //private static readonly GeoCoordinate DefaultLocation = new GeoCoordinate(36.06126820628312, 120.38010478019714);

        /// <value>Collection of pushpins available on map.</value>
        private readonly ObservableCollection<PushpinModel> _pushpins = new ObservableCollection<PushpinModel>
        {
            new PushpinModel
            {
                Location = DefaultLocation,
                Icon = new Uri("/Resources/Icons/Pushpins/PushpinLocation.png", UriKind.Relative)
            }
        };

        /// <value>Collection of calculated map routes.</value>
        private readonly ObservableCollection<RouteModel> _routes = new ObservableCollection<RouteModel>();

        /// <value>Map zoom level.</value>
        private double _zoom;

        /// <value>Map center coordinate.</value>
        private GeoCoordinate _center;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the credentials provider for the map control.
        /// </summary>
        public CredentialsProvider CredentialsProvider
        {
            get { return _credentialsProvider; }
        }

        /// <summary>
        /// Gets or sets the map zoom level.
        /// </summary>
        public double Zoom
        {
            get { return _zoom; }
            set
            {
                var coercedZoom = Math.Max(MinZoomLevel, Math.Min(MaxZoomLevel, value));
                if (_zoom != coercedZoom)
                {
                    _zoom = value;
                    NotifyPropertyChanged("Zoom");
                }
            }
        }

        /// <summary>
        /// Gets or sets the map center location coordinate.
        /// </summary>
        public GeoCoordinate Center
        {
            get { return _center; }
            set
            {
                if (_center != value)
                {
                    _center = value;
                    NotifyPropertyChanged("Center");
                }
            }
        }

        /// <summary>
        /// Gets a collection of pushpins.
        /// </summary>
        public ObservableCollection<PushpinModel> Pushpins
        {
            get { return _pushpins; }
        }

        /// <summary>
        /// Gets a collection of routes.
        /// </summary>
        public ObservableCollection<RouteModel> Routes
        {
            get { return _routes; }
        }

        /// <summary>
        /// Gets or sets the route destination location.
        /// </summary>
        public string To { get; set; }

        /// <summary>
        /// Gets or sets the route origin location.
        /// </summary>
        public string From { get; set; }

        #endregion

        #region Tasks

        private void InitializeDefaults()
        {
            Zoom = DefaultZoomLevel;
            Center = DefaultLocation;

            From = "Wuqing, TIANJIN, CHINA";
            To = "Qingdao, SHANDONG, CHINA";
        }

        private void ChangeMapMode()
        {
            if (Map.Mode is AerialMode)
            {
                Map.Mode = new RoadMode();
            }
            else
            {
                Map.Mode = new AerialMode(true);
            }
        }

        private void CenterLocation()
        {
            // Center map to default location.
            Center = DefaultLocation;

            // Reset zoom default level.
            Zoom = DefaultZoomLevel;
        }

        private void Pushpin_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var pushpin = sender as Pushpin;

            // Center the map on a pushpin when touched.
            Center = pushpin.Location;
        }

        private void CalculateRoute()
        {
            try
            {
                var routeCalculator = new RouteCalculator(
                    CredentialsProvider,
                    To,
                    From,
                    Dispatcher,
                    result =>
                    {
                        // Clear the route collection to have only one route at a time.
                        Routes.Clear();

                        // Create a new route based on route calculator result,
                        // and add the new route to the route collection.
                        var routeModel = new RouteModel(result.Result.RoutePath.Points);
                        Routes.Add(routeModel);

                        // Set the map to center on the new route.
                        var viewRect = LocationRect.CreateLocationRect(routeModel.Locations);
                        Map.SetView(viewRect);
                    });

                // Display an error message in case of fault.
                routeCalculator.Error += r => MessageBox.Show(r.Reason);

                // Start the route calculation asynchronously.
                routeCalculator.CalculateAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        private void PhoneApplicationPage_OrientationChanged(object sender, OrientationChangedEventArgs e)
        {
            // Switch Map View Border's Height and Width based on an orientation change.
            if ((e.Orientation & PageOrientation.Portrait) == (PageOrientation.Portrait))
            {
                MapView.Height = 768;
                MapView.Width = 480;

                ButtonZoomIn.Margin = new Thickness(8, 180, 0, 0);
                ButtonZoomOut.Margin = new Thickness(8, 260, 0, 0);
            }
            else
            {
                MapView.Height = 480;
                MapView.Width = 768;

                ButtonZoomIn.Margin = new Thickness(320, 0, 0, 0);
                ButtonZoomOut.Margin = new Thickness(400, 0, 0, 0);
            }
        }
    }
}