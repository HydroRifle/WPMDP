﻿#pragma checksum "D:\TFS\Sela\WP7\SL\Phase 1\Recipes\Push\Source\WindowsPhone.Recipes.Push.Client\Views\UserLoginView.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "36EC23D53E61AF40DAA06622561480DF"
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
using WindowsPhone.Recipes.Push.Client.Controls;


namespace WindowsPhone.Recipes.Push.Client.Views {
    
    
    public partial class UserLoginView : System.Windows.Controls.UserControl {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal WindowsPhone.Recipes.Push.Client.Controls.ProgressBarWithText progress;
        
        internal System.Windows.Controls.StackPanel login;
        
        internal System.Windows.Controls.TextBox textBoxUserName;
        
        internal System.Windows.Controls.Button buttonLogin;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/PushClient;component/Views/UserLoginView.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.progress = ((WindowsPhone.Recipes.Push.Client.Controls.ProgressBarWithText)(this.FindName("progress")));
            this.login = ((System.Windows.Controls.StackPanel)(this.FindName("login")));
            this.textBoxUserName = ((System.Windows.Controls.TextBox)(this.FindName("textBoxUserName")));
            this.buttonLogin = ((System.Windows.Controls.Button)(this.FindName("buttonLogin")));
        }
    }
}

