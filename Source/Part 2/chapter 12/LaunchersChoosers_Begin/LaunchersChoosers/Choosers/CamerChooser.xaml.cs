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
    public partial class CamerChooser : PhoneApplicationPage
    {
        CameraCaptureTask cct ;

        public CamerChooser()
        {
            InitializeComponent();
            //建议在初始化完成之后就加载事件处理函数,这与application lift cycle有关
            cct = new CameraCaptureTask();
            cct.Completed += new EventHandler<PhotoResult>(cct_Completed);
        }

        private void btnShot_Click(object sender, RoutedEventArgs e)
        {
            //呼叫Chooser
            cct.Show();
        }

        void cct_Completed(object sender, PhotoResult e)
        {
            //判定结果是否成功
            if (e.TaskResult == TaskResult.OK)
            {
                BitmapImage bmpSource = new BitmapImage();
                bmpSource.SetSource(e.ChosenPhoto);
                image1.Source = bmpSource;
            }
            else
            {
                image1.Source = null;
            }
        }
    }
}