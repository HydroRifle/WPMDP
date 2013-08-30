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
using System.Windows.Media.Imaging;

namespace LaunchersChoosers.Choosers
{
    public partial class PhotoChooser : PhoneApplicationPage
    {
        PhotoChooserTask pc;

        public PhotoChooser()
        {
            InitializeComponent();
            pc = new PhotoChooserTask();
            pc.Completed += new EventHandler<PhotoResult>(pc_Completed);
        }

        void pc_Completed(object sender, PhotoResult e)
        {
            if (e.TaskResult == TaskResult.OK)
            {
                BitmapImage bmpSource = new BitmapImage();
                bmpSource.SetSource(e.ChosenPhoto);
                image1.Source = bmpSource;
                textBlock1.Text = e.OriginalFileName;
            }
            else
            {
                image1.Source = null;
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            //是否裁剪相片，并设定裁剪相片后的最大高度和宽度
            pc.PixelHeight = 30;
            pc.PixelWidth = 80;

            //设定是否出现拍照的按钮（位于Application Bar）
            pc.ShowCamera = true;

            pc.Show();
        }
    }
}