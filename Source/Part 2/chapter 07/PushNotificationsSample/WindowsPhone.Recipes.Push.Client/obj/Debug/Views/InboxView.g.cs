﻿#pragma checksum "D:\TFS\Sela\WP7\SL\Phase 1\Recipes\Push\Source\WindowsPhone.Recipes.Push.Client\Views\InboxView.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "49E9DB4F6B188E5B865296A0ECA9F059"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace WindowsPhone.Recipes.Push.Client.Views {
    
    
    public partial class InboxView : System.Windows.Controls.UserControl {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.VisualStateGroup Views;
        
        internal System.Windows.VisualState NormalView;
        
        internal System.Windows.VisualState ScheduleView;
        
        internal System.Windows.Controls.Border TileScheduleView;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/PushClient;component/Views/InboxView.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.Views = ((System.Windows.VisualStateGroup)(this.FindName("Views")));
            this.NormalView = ((System.Windows.VisualState)(this.FindName("NormalView")));
            this.ScheduleView = ((System.Windows.VisualState)(this.FindName("ScheduleView")));
            this.TileScheduleView = ((System.Windows.Controls.Border)(this.FindName("TileScheduleView")));
        }
    }
}

