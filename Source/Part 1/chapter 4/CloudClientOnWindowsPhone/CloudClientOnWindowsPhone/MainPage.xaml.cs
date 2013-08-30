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

namespace CloudClientOnWindowsPhone
{
public partial class MainPage : PhoneApplicationPage
{
    CloudService.Service1Client sc;

    // Constructor
    public MainPage()
    {
        InitializeComponent();
    }

    void sc_GetServerTimeCompleted(object sender, CloudService.GetServerTimeCompletedEventArgs e)
    {
        this.textBlock1.Text = e.Result.ToLongDateString();
        this.textBlock2.Text = e.Result.ToShortTimeString();
    }

    private void PhoneApplicationPage_MouseLeave(object sender, MouseEventArgs e)
    {
        if (sc == null)
        {
            sc = new CloudService.Service1Client();
            sc.GetServerTimeCompleted +=
                new EventHandler<CloudService.GetServerTimeCompletedEventArgs>
                    (sc_GetServerTimeCompleted);
        }
        this.textBlock1.Text = "";
        this.textBlock2.Text = "Checking...";
        sc.GetServerTimeAsync();
    }
}
}
