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

using System.Windows;

using Microsoft.Phone.Controls;

namespace UsingBingMaps
{
    public partial class MainPage : PhoneApplicationPage
    {
        #region Consts
        #endregion

        #region Fields
        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the map zoom level.
        /// </summary>
        public double Zoom
        {
            get;
            set;
        }

        public bool HasDirections
        {
            get
            {
                // TODO : Return true only if has directions.

                return true;
            }
        }

        #endregion

        #region Tasks

        private void InitializeDefaults()
        {
            // TODO : Initialize default properties.
        }

        private void ChangeMapMode()
        {
            // TODO : Change map view mode.
        }

        private void IncreaseZoomLevel()
        {
            // TODO : Increases zoom level.
        }

        private void DecreaseZoomLevel()
        {
            // TODO : Decreases zoom level.
        }

        private void CenterLocation()
        {
            // TODO : Center current location.
        }

        private void CenterPushpinsPopup(Point touchPoint)
        {
            // TODO : Center pushpins popup to the touch point.
        }

        private void CreateNewPushpin(object selectedItem, Point point)
        {
            // TODO : Create a new pushpin.
        }

        private void CalculateRoute()
        {
            // TODO : Calculate a route.
        }        

        #endregion
    }
}