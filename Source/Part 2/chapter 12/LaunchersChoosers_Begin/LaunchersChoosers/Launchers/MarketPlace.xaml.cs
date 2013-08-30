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
    public partial class MarketPlaceDetail : PhoneApplicationPage
    {
        public MarketPlaceDetail()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MarketplaceDetailTask mdt = new MarketplaceDetailTask();
            //ContentType设定为Applcation
            mdt.ContentType = MarketplaceContentType.Applications;
            //当没有指定值则以目前的应用程序为目标
            //如果格式检查不符合GUID的格式则会抛出exception
            mdt.ContentIdentifier = txtID.Text;
            mdt.Show();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            MarketplaceHubTask mht = new MarketplaceHubTask();
            //mht.ContentType = MarketplaceContentType.Applications;
            mht.ContentType = MarketplaceContentType.Music;
            mht.Show();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            MarketplaceReviewTask mrt = new MarketplaceReviewTask();
            mrt.Show();
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            MarketplaceSearchTask mst = new MarketplaceSearchTask();
            //mst.ContentType = MarketplaceContentType.Music;
            mst.ContentType = MarketplaceContentType.Applications;
            mst.SearchTerms = txtSearchTerms.Text;
            mst.Show();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }
    }
}