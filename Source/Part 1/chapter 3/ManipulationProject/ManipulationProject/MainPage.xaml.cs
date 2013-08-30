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

namespace ManipulationProject
{
    public partial class MainPage : PhoneApplicationPage
    {
        private TransformGroup transformGroup;
        private TranslateTransform translation;
        private ScaleTransform scale;

        // Constructor
        public MainPage()
        {
            InitializeComponent();
            this.transformGroup = new TransformGroup();
            this.translation = new TranslateTransform();
            this.scale = new ScaleTransform();

            this.transformGroup.Children.Add(this.scale);
            this.transformGroup.Children.Add(this.translation);
            this.rectangle.RenderTransform = this.transformGroup;

        }

        private void Canvas_ManipulationDelta(object sender, ManipulationDeltaEventArgs e)
        {
            // Scale the rectangle.
            if ((e.DeltaManipulation.Scale.X == 0) || (e.DeltaManipulation.Scale.Y == 0))
            {
                // Increase ScaleX and ScaleY by 5%.
                this.scale.ScaleX *= 1.05;
                this.scale.ScaleY *= 1.05;
            }
            else
            {
                this.scale.ScaleX *= e.DeltaManipulation.Scale.X;
                this.scale.ScaleY *= e.DeltaManipulation.Scale.Y;
            }

            // Move the rectangle.
            this.translation.X += e.DeltaManipulation.Translation.X;
            this.translation.Y += e.DeltaManipulation.Translation.Y;

        }
    }
}