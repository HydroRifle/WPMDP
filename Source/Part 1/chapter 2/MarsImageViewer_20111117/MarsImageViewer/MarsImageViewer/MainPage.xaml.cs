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
using Microsoft.Phone.Shell;
using ShakeGestures;


namespace MarsImageViewer
{
    public partial class MainPage : PhoneApplicationPage
    {
        private int imageListIndex = 0;
        private List<string> ImageIdList;
        private string MarsCurrentImageId;

        double initialAngle;
        double initialScale;

        bool _isNewPageInstance = false;

        // storage loaded image
        private Dictionary<string, BitmapImage> loadBitmapDic;
        private const byte MAX_STORAGE_IMAGES = 20;

        /* Microsoft Project Codename “Dallas” changes the way information is exchanged by offering a wide range of content 
        from authoritative commercial & public sources in a single marketplace, making it easier to find and purchase the 
        data that you need to power your applications and analytics. */
        // https://www.sqlazureservices.com/Account.aspx 

        private const string WindowsLiveId = "highcedar@hotmail.com";//"<Your Windows Live ID>";
        private const string AccountKey = "s8YMD6iVqSsK0DENVy33GfUf+ucrHbw83lPkUQmtWXM=";//"<Your account key>";

        //private const string AccountKey = "4jAAma48D6y0bmuznmXyNa+wJwCgTQeWIcdXr56cJ6o=";//"<New account key>";

        // progressBar
        private ProgressIndicator _progressIndicator;

        // Constructor
        public MainPage()
        {
            InitializeComponent();

            _isNewPageInstance = true;

            ImageIdList = new List<string>();

            loadBitmapDic = new Dictionary<string, BitmapImage>();
        }

        private const string CurrImageIdKey = "CurrImageIdKey";
        private const string CurrImageListIndexKey = "CurrImageListIndexKey";

        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
            // If this is a back navigation, the page will be discarded, so there
            // is no need to save state.
            if (e.NavigationMode != System.Windows.Navigation.NavigationMode.Back)
            {
                // current image id
                this.SaveState(CurrImageIdKey, MarsCurrentImageId);
                this.SaveState(CurrImageListIndexKey, imageListIndex);
            }
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            // If _isNewPageInstance is true, the page constuctor has been called, so
            // state may need to be restored
            if (_isNewPageInstance)
            {
                // progressBar
                if (null == _progressIndicator)
                {
                    _progressIndicator = new ProgressIndicator();
                    _progressIndicator.IsVisible = true;
                    SystemTray.ProgressIndicator = _progressIndicator;
                }
                _progressIndicator.IsIndeterminate = true;                

                try
                {
                    // register shake event
                    ShakeGesturesHelper.Instance.ShakeGesture += new EventHandler<ShakeGestureEventArgs>(Instance_ShakeGesture);

                    // optional, set parameters
                    ShakeGesturesHelper.Instance.MinimumRequiredMovesForShake = 2;

                    // start shake detection
                    ShakeGesturesHelper.Instance.Active = true;
                }
                catch
                { 
                }
                
                if (State.Count > 0)
                {
                    if (State.ContainsKey(CurrImageIdKey))
                    {
                        MarsCurrentImageId = this.LoadState<string>(CurrImageIdKey);
                    }
                }
                else
                {
                    foreach (string ImageId in ImageIdList)
                    {
                        MarsCurrentImageId = ImageId;
                        break;
                    }
                }

                if (ImageIdList == null)
                {
                    ImageIdList = new List<string>();
                }
                //Dispatcher.BeginInvoke(() => GetImageIDs()); // ***

                GetImageIDs();

                try
                {
                    LiveTileStart();
                }
                catch
                {
                }

                //else
                //{
                //    // load images
                //    Images = new ObservableCollection<ImageItem>();

                //    ImageService imgService = new ImageService();
                //    IEnumerable<ImageItem> images = imgService.GetImages();
                //    foreach (ImageItem imageItem in images)
                //    {
                //        Images.Add(imageItem);
                //    }
                //}

                //#region RestoreApplicationState

                //// if the application member variable is not empty,
                //// set the page's data object from the application member variable.
                //if ((Application.Current as ExecutionModelSample.App).ApplicationDataObject != null)
                //{
                //    UpdateApplicationDataUI();
                //}
                //else
                //{

                //    // Otherwise, call the method that loads data.
                //    statusTextBlock.Text = "getting data...";
                //    (Application.Current as ExecutionModelSample.App).GetDataAsync();
                //}

                //#endregion
            }

            //// shake
            //try
            //{
            //    _shakeSensor = new AccelerometerShake();
            //    Loaded += (sender, args) =>
            //    {
            //        _shakeSensor.ShakeDetected += ShakeDetected;
            //        _shakeSensor.Start();
            //    };
            //}
            //catch
            //{
            //}

            // Set _isNewPageInstance to false. If the user navigates back to this page
            // and it has remained in memory, this value will continue to be false.
            _isNewPageInstance = false;
        }

        //private void ShakeDetected(object sender, ShakeStateEventArgs e)
        //{
        //    if (e.shakingState == true)
        //    {
        //        isShaking = true;

        //        //Dispatcher.BeginInvoke(() => flickData.Text = "Shaking.");
        //    }
        //    else if ((true == isShaking) && (e.shakeStoppedState == true))
        //    {
        //        isShaking = false;

        //        // shake stop then get new image
        //        //Dispatcher.BeginInvoke(() => GetNextImage());
        //        Deployment.Current.Dispatcher.BeginInvoke(delegate { GetNextImage(); });
        //    }
        //}

        private void Instance_ShakeGesture(object sender, ShakeGestureEventArgs e)
        {
            //_lastUpdateTime.Dispatcher.BeginInvoke(
            //    () =>
            //    {
            //        _lastUpdateTime.Text = DateTime.Now.ToString();
            //        CurrentShakeType = e.ShakeType;
            //    });
            Deployment.Current.Dispatcher.BeginInvoke(delegate { GetNextImage(); });

            PageTitleCollapse();

            if (System.Diagnostics.Debugger.IsAttached)
            {
                System.Diagnostics.Debug.WriteLine(DateTime.Now.Minute + ":" + DateTime.Now.Second + ":" + DateTime.Now.Millisecond + "  " + e.ShakeType);
            }
        }

        private void PageTitleCollapse()
        {
            PageTitle.Dispatcher.BeginInvoke(
               () =>
               {
                   PageTitle.Visibility = System.Windows.Visibility.Collapsed;
               });
        }

        private void GetImageIDs()
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
                catch (Exception)
                {
                    // returned an error.
                    //this.Dispatcher.BeginInvoke(delegate() { MessageBox.Show(ex.Message + "1"); });
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

                    if (!ImageIdList.Contains(MarsCurrentImageId))
                    {
                        foreach (string ImageId in ImageIdList)
                        {
                            MarsCurrentImageId = ImageId;
                            break;
                        }
                    }

                    try
                    {
                        int iGetIndex = ImageIdList.IndexOf(MarsCurrentImageId);
                        if (imageListIndex != iGetIndex)
                        {
                            imageListIndex = iGetIndex;
                        }

                        if (imageListIndex <= 0)
                        {
                            bool isEnable = false;
                            Dispatcher.BeginInvoke(() => SetLastApplicationBar(isEnable));
                            //SetLastApplicationBar(isEnable);
                        }
                    }
                    catch (Exception)
                    {
                        // returned an error.
                        //this.Dispatcher.BeginInvoke(delegate() { MessageBox.Show(ex.Message + "2"); });
                        return;
                    }

                    try
                    {
                        Dispatcher.BeginInvoke(() => GetImage(MarsCurrentImageId));
                        //GetImage(MarsCurrentImageId);
                    }
                    catch (Exception)
                    {
                        // returned an error.
                        //this.Dispatcher.BeginInvoke(delegate() { MessageBox.Show(ex.Message + "3"); });
                        return;
                    }
                }
            }, recDownloader);
        }

        private void GetImage(string ID)
        {
            if (string.IsNullOrEmpty(ID))
            {
                return;
            }
            // progressBar
            _progressIndicator.IsIndeterminate = true;

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
                catch (Exception)
                {
                    // returned an error
                    //this.Dispatcher.BeginInvoke(delegate() { MessageBox.Show(ex.Message); });
                    return;
                }

                Stream ImageStream = response.GetResponseStream();
                //Save_ShowImage(ID, ImageStream, response.ContentLength);
                Save_ShowImage(ID, ImageStream);

            }, imgDownloader);
        }

        //private void Save_ShowImage(string imageID, Stream imageStream, long imageSize)
        private void Save_ShowImage(string imageID, Stream imageStream)
        {
            //BinaryReader br = new BinaryReader(imageStream);
            //byte[] ImageBytes = new byte[imageSize];
            //br.Read(ImageBytes, 0, ImageBytes.Length);
            //MemoryStream msa = new MemoryStream(ImageBytes);

            Deployment.Current.Dispatcher.BeginInvoke(delegate
            {
                //    BitmapImage imgsrc = new BitmapImage();
                //    imgsrc.SetSource(msa);
                //    MarsImage.Source = imgsrc;
                //});
                try
                {
                    BitmapImage imgsrc = new BitmapImage();
                    imgsrc.SetSource(imageStream);
                    MarsImage.Source = imgsrc;

                    // progressBar
                    _progressIndicator.IsIndeterminate = false;

                    // save image in Dictionary
                    // storage max (MAX_STORAGE_IMAGES = 20) images
                    if ((loadBitmapDic != null)
                        && (!loadBitmapDic.ContainsKey(imageID)))
                    {
                        if (loadBitmapDic.Count < MAX_STORAGE_IMAGES)
                        {
                            loadBitmapDic.Add(imageID, imgsrc);
                        }
                        else
                        {
                            // remove images
                            int removeIndex = imageListIndex % MAX_STORAGE_IMAGES;

                            bool isFindIndex = false;
                            string removeImgId = string.Empty;
                            foreach (String s in ImageIdList)
                            {
                                if (ImageIdList.IndexOf(s) == removeIndex)
                                {
                                    removeImgId = s;
                                    isFindIndex = true;
                                }
                            }

                            // add images
                            if (isFindIndex)
                            {
                                try
                                {
                                    // remove old image
                                    loadBitmapDic.Remove(removeImgId);
                                    // add new image
                                    loadBitmapDic.Add(imageID, imgsrc);
                                }
                                catch
                                { }
                            }
                        }
                    }
                }
                catch
                {
                }
            });
        }

        private void appbar_BackButton_Click(object sender, EventArgs e)
        {
            GetLastImage();
        }

        private void appbar_ForwardButton_Click(object sender, EventArgs e)
        {
            GetNextImage();
        }

        private void appbar_AboutButton_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/About.xaml ", UriKind.Relative));
        }

        private void GetLastImage()
        {
            try
            {
                bool isLastEnable = true;
                bool isNextEnable = true;

                if ((imageListIndex > 0) && (imageListIndex < ImageIdList.Count))
                {
                    imageListIndex--;
                    MarsCurrentImageId = ImageIdList[imageListIndex];
                    if (loadBitmapDic.ContainsKey(MarsCurrentImageId))
                    {

                        BitmapImage imgsrc = new BitmapImage();
                        loadBitmapDic.TryGetValue(MarsCurrentImageId, out imgsrc);

                        MarsImage.Source = imgsrc;
                    }
                    else
                    {
                        //GetImage(MarsCurrentImageId);
                        try
                        {
                            Dispatcher.BeginInvoke(() => GetImage(MarsCurrentImageId));
                            //GetImage(MarsCurrentImageId);
                        }
                        catch
                        { 
                        }
                    }
                }
                else
                {
                    isLastEnable = false;
                }

                SetApplicationBar(isLastEnable, isNextEnable);
            }
            catch
            { }
        }

        private void GetNextImage()
        {
            try
            {
                bool isLastEbable = true;
                bool isNextEnable = true;
                if (imageListIndex < (ImageIdList.Count - 1))
                {
                    imageListIndex++;
                    MarsCurrentImageId = ImageIdList[imageListIndex];
                    if (loadBitmapDic.ContainsKey(MarsCurrentImageId))
                    {
                        BitmapImage imgsrc = new BitmapImage();
                        loadBitmapDic.TryGetValue(MarsCurrentImageId, out imgsrc);

                        MarsImage.Source = imgsrc;
                    }
                    else
                    {
                        //GetImage(MarsCurrentImageId);
                        try
                        {
                            Dispatcher.BeginInvoke(() => GetImage(MarsCurrentImageId));
                            //GetImage(MarsCurrentImageId);
                        }
                        catch
                        { 
                        }
                    }
                }
                else
                {
                    isNextEnable = false;
                }

                SetApplicationBar(isLastEbable, isNextEnable);
            }
            catch
            {
            }
        }

        private void SetLastApplicationBar(bool isEnable)
        {
            if (ApplicationBar != null)
            {
                foreach (ApplicationBarIconButton btn in ApplicationBar.Buttons)
                {
                    if (btn.Text.Equals("Back"))
                    {
                        btn.IsEnabled = isEnable;
                    }
                }
            }
        }

        private void SetNextApplicationBar(bool isEnable)
        {
            if (ApplicationBar != null)
            {
                foreach (ApplicationBarIconButton btn in ApplicationBar.Buttons)
                {
                    if (btn.Text.Equals("Next"))
                    {
                        btn.IsEnabled = isEnable;
                    }
                }
            }
        }

        private void SetApplicationBar(bool isLastEnable, bool isNextEnable)
        {
            if (ApplicationBar != null)
            {
                foreach (ApplicationBarIconButton btn in ApplicationBar.Buttons)
                {
                    if (btn.Text.Equals("Next"))
                    {
                        btn.IsEnabled = isNextEnable;
                    }
                    else if (btn.Text.Equals("Back"))
                    {
                        btn.IsEnabled = isLastEnable;
                    }
                }
            }
        }

        #region Gesture control

        private void OnFlick(object sender, FlickGestureEventArgs e)
        {          
            //string.Format("{0} Flick: Angle {1} Velocity {2},{3}",
            //    e.Direction, Math.Round(e.Angle), e.HorizontalVelocity, e.VerticalVelocity);
            if (e.Direction == System.Windows.Controls.Orientation.Horizontal)
            {
                if (e.HorizontalVelocity < 0)
                {
                    GetNextImage();
                }
                else if (e.HorizontalVelocity > 0)
                {
                    GetLastImage();
                }
            }
            else if (e.Direction == System.Windows.Controls.Orientation.Vertical)
            {
                if (e.VerticalVelocity > 0)
                {
                    GetNextImage();
                }
                else if (e.VerticalVelocity < 0)
                {
                    GetLastImage();
                }
            }

            // Collapse PageTitle
            PageTitleCollapse();
        }

        private void OnTap(object sender, Microsoft.Phone.Controls.GestureEventArgs e)
        {
            //transform.TranslateX = transform.TranslateY = 0;
            // Collapse PageTitle
            PageTitleCollapse();
        }

        private void OnDoubleTap(object sender, Microsoft.Phone.Controls.GestureEventArgs e)
        {
            //transform.ScaleX = transform.ScaleY = 1;
            // Collapse PageTitle
            PageTitleCollapse();
        }

        private void OnHold(object sender, Microsoft.Phone.Controls.GestureEventArgs e)
        {
            //transform.TranslateX = transform.TranslateY = 0;
            transform.ScaleX = transform.ScaleY = 1;
            transform.Rotation = 0;

            //flickData.Text = string.Format("OnHoldGestureEvent");
            // Collapse PageTitle
            PageTitleCollapse();
        }

        private void OnDragStarted(object sender, DragStartedGestureEventArgs e)
        {
            //flickData.Text = string.Format("{0} OnDragStarted",
            //    e.Direction);
        }

        private void OnDragDelta(object sender, DragDeltaGestureEventArgs e)
        {
            //flickData.Text = string.Format("{0} OnDragDelta: \nHorizontalChange = {1} ,\nVerticalChange = {2}.",
            //    e.Direction, e.HorizontalChange, e.VerticalChange);
        }

        private void OnDragCompleted(object sender, DragCompletedGestureEventArgs e)
        {
            if (e.Direction == System.Windows.Controls.Orientation.Horizontal)
            {
                if (e.HorizontalChange < 0)
                {
                    GetNextImage();
                }
                else if (e.HorizontalChange > 0)
                {
                    GetLastImage();
                }
            }
            else if (e.Direction == System.Windows.Controls.Orientation.Vertical)
            {
                if (e.VerticalChange > 0)
                {
                    GetNextImage();
                }
                else if (e.VerticalChange < 0)
                {
                    GetLastImage();
                }
            }
            //flickData.Text = string.Format("{0} OnDragCompleted: \nHorizontalChange = {1} ,\nVerticalChange = {2}.",
            //    e.Direction, e.HorizontalChange, e.VerticalChange);

            // Collapse PageTitle
            PageTitleCollapse();
        }

        private void OnPinchStarted(object sender, PinchStartedGestureEventArgs e)
        {
            initialAngle = transform.Rotation;
            initialScale = transform.ScaleX;

            //flickData.Text = string.Format(" OnPinchStarted: Angle = {0}, Distance = {1}",
            //   Math.Round(e.Angle), e.Distance);
        }

        private void OnPinchDelta(object sender, PinchGestureEventArgs e)
        {
            transform.Rotation = initialAngle + e.TotalAngleDelta;
            transform.ScaleX = transform.ScaleY = initialScale * e.DistanceRatio;

            //flickData.Text = string.Format(" OnPinchDelta: Angle = {0}, Distance = {1}",
            //  e.DistanceRatio, e.TotalAngleDelta);
        }

        private void OnPinchCompleted(object sender, PinchGestureEventArgs e)
        {
            //flickData.Text = string.Format(" OnPinchCompleted: Angle = {0}, Distance = {1}",
            //  e.DistanceRatio, e.TotalAngleDelta);

            // Collapse PageTitle
            PageTitleCollapse();
        }

        #endregion

        private void PhoneApplicationPage_OrientationChanged(object sender, OrientationChangedEventArgs e)
        {
            // Switch Map View Border's Height and Width based on an orientation change.
            if ((e.Orientation & PageOrientation.Portrait) == (PageOrientation.Portrait))
            {                
                ContentPanel.Width = 480;
                ContentPanel.Height = 768;
                border.Width = 480;
                border.Height = 768;
                //MarsImage.Width = 480;
                //MarsImage.Height = 768;
            }
            else
            {
                ContentPanel.Width = 768;
                ContentPanel.Height = 480;
                border.Width = 768;
                border.Height = 480;
                //MarsImage.Width = 768;
                //MarsImage.Height = 480;               
            }
        }

        private void LiveTileStart()
        {
            // Application Tile is always the first Tile, even if it is not pinned to Start.
            ShellTile TileToFind = ShellTile.ActiveTiles.First();

            // Application should always be found
            if (TileToFind != null)
            {
                // Set the properties to update for the Application Tile.
                // Empty strings for the text values and URIs will result in the property being cleared.
                StandardTileData NewTileData = new StandardTileData
                {
                    Title = "SHAKE MARS",
                    BackgroundImage = new Uri("Background.png", UriKind.Relative),
                    Count = 0,
                    BackTitle = "SHAKE MARS",
                    BackBackgroundImage = new Uri("BackBackground.png", UriKind.Relative),
                    BackContent = "探索火星"
                };

                // Update the Application Tile
                TileToFind.Update(NewTileData);
            }
        }
    }
}
