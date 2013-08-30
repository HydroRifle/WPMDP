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

using System.Xml.Linq;
using System.IO;
using System.Windows.Media.Imaging;
using System.Net.Browser;
using System.Xml;

namespace MarsImageViewer
{
    public partial class MainPage : PhoneApplicationPage
    {
        private int index;
        private List<string> ImageIdList;
        private string MarsCurrentImageId;
        private BitmapImage imgsrc;

        /* Microsoft Project Codename “Dallas” changes the way information is exchanged by offering a wide range of content 
        from authoritative commercial & public sources in a single marketplace, making it easier to find and purchase the 
        data that you need to power your applications and analytics. */
        // https://www.sqlazureservices.com/Account.aspx 

        private const string WindowsLiveId = "<Your Windows Live ID>";
        private const string AccountKey = "<Your account key>";

        // Constructor
        public MainPage()
        {
            InitializeComponent();

            index = 0;
            ImageIdList = new List<string>();
            imgsrc = new BitmapImage();

            getImageIDs();
        }

        private void getImageIDs()
        {
            //Uri serviceUri = new Uri("https://api.sqlazureservices.com/NasaService.svc/MER/Images?missionId=1&$format=raw");
            Uri serviceUri = new Uri("https://api.sqlazureservices.com/NasaService.svc/MER/Images?missionId=1&$format=atom10");

            // create the request to the dallas           
            WebRequest recDownloader = (HttpWebRequest)WebRequestCreator.ClientHttp.Create(serviceUri);

            recDownloader.Credentials = new NetworkCredential(WindowsLiveId, AccountKey);

            // request the data from the service.
            recDownloader.BeginGetResponse(ar =>
            {
                // get the request.
                HttpWebRequest r = (HttpWebRequest)ar.AsyncState;

                // get the response from the service.
                HttpWebResponse response = null;

                try
                {
                    response = (HttpWebResponse)r.EndGetResponse(ar);
                }

                catch (Exception ex)
                {
                    // returned an error.
                    this.Dispatcher.BeginInvoke(delegate() { MessageBox.Show(ex.Message); });
                    return;
                }

                // read the response.
                using (StreamReader reader = new StreamReader(response.GetResponseStream(), true))
                {
                    // Image ID list
                    XmlReader xmlReader = XmlReader.Create(reader);
                    xmlReader.Read();

                    while (xmlReader.ReadState != ReadState.EndOfFile)
                    {
                        xmlReader.ReadToFollowing("entry");
                        if (xmlReader.ReadState != ReadState.EndOfFile)
                        {
                            xmlReader.ReadToFollowing("m:properties");
                            xmlReader.ReadToFollowing("d:ImageId");
                            ImageIdList.Add(xmlReader.ReadElementContentAsString());
                        }
                    }

                    xmlReader.Close();
                    reader.Close();
              
                    foreach (string ImageId in ImageIdList)
                    {
                        MarsCurrentImageId = ImageId;                        
                        break;
                    }

                    Deployment.Current.Dispatcher.BeginInvoke(() =>
                    {
                        // get image by ID
                        if (MarsCurrentImageId != null)
                        {
                            getImage(MarsCurrentImageId);
                        }
                    });
                }
            }, recDownloader);
        }

        private void getImage(string ID)
        {
            if (ID == null)
            {
                return;
            }

            Uri serviceUri = new Uri("https://api.sqlazureservices.com/NasaService.svc/MER/Images/" + ID + "?$format=raw");

            WebRequest imgDownloader = (HttpWebRequest)WebRequestCreator.ClientHttp.Create(serviceUri);

            imgDownloader.Credentials = new NetworkCredential(WindowsLiveId, AccountKey);

            // request the data from the service.
            imgDownloader.BeginGetResponse(br =>
            {
                // get the request.
                HttpWebRequest r = (HttpWebRequest)br.AsyncState;

                // get the response from the service.
                HttpWebResponse response = null;

                try
                {
                    response = (HttpWebResponse)r.EndGetResponse(br);
                }
                catch (Exception ex)
                {
                    // returned an error
                    this.Dispatcher.BeginInvoke(delegate() { MessageBox.Show(ex.Message); });
                    return;
                }

                Stream ImageStream = response.GetResponseStream();
                ShowImage(ImageStream, response.ContentLength);

            }, imgDownloader);
        }

        private void ShowImage(Stream imageStream, long imageSize)
        {
            BinaryReader br = new BinaryReader(imageStream);
            byte[] ImageBytes = new byte[imageSize];
            br.Read(ImageBytes, 0, ImageBytes.Length);
            MemoryStream msa = new MemoryStream(ImageBytes);

            Deployment.Current.Dispatcher.BeginInvoke(delegate
            {
                imgsrc.SetSource(msa);
                MarsImage.Source = imgsrc;
            });
        }

       private void appbar_BackButton_Click(object sender, EventArgs e)
        {
            if (index > 0)
            {
                index--;
                MarsCurrentImageId = ImageIdList[index];
                getImage(MarsCurrentImageId);
            }
        }

        private void appbar_ForwardButton_Click(object sender, EventArgs e)
        {
            if ((index + 1) < ImageIdList.Count)
            {
                index++;
                MarsCurrentImageId = ImageIdList[index];
                getImage(MarsCurrentImageId);
            }
        }

        private void appbar_AboutButton_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/About.xaml ", UriKind.Relative));
        }
    }
}
