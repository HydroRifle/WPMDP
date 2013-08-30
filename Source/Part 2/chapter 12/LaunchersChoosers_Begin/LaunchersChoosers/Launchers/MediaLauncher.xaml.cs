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
using Microsoft.Phone.Tasks;

namespace LaunchersChoosers.Launchers
{
    public partial class MediaLauncher : PhoneApplicationPage
    {
        public MediaLauncher()
        {
            InitializeComponent();
        }

        private void btnPlayVideo_Click(object sender, RoutedEventArgs e)
        {
            MediaPlayerLauncher mediaPlayerLauncher = new MediaPlayerLauncher();
            if (chkUseExternalMedia.IsChecked.Value)
            {
                mediaPlayerLauncher.Media = new Uri("http://ecn.channel9.msdn.com/o9/ch9/4/1/9/7/4/5/WPMetroDesignOverivew_ch9.wmv", UriKind.Absolute);
            }
            else
            {
                //means is a resource of the app, otherwise it will try to resolve it in Data (IsolatedStorage) for application
                mediaPlayerLauncher.Location = MediaLocationType.Install;

                mediaPlayerLauncher.Media = new Uri("Media/mymovie.wmv", UriKind.Relative);
            }
            mediaPlayerLauncher.Show();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }
    }
}