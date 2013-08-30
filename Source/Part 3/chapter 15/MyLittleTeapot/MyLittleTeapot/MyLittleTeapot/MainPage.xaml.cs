/* 
    Copyright (c) 2011 Microsoft Corporation.  All rights reserved.
    Use of this sample source code is subject to the terms of the Microsoft license 
    agreement under which you licensed this sample source code and is provided AS-IS.
    If you did not accept the terms of the license agreement, you are not authorized 
    to use this sample source code.  For the terms of the license, please see the 
    license agreement between you and Microsoft.
*/
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyLittleTeapot
{
    public partial class MainPage : PhoneApplicationPage
    {
        GameTimer timer;
        ContentManager content;
        SpriteBatch spriteBatch;

        // 3D teapot we display behind the Silverlight page
        TeapotPrimitive teapot;
        Color teapotColor = Color.Black;
        float teapotYaw, teapotPitch;

        // Indicates if the controls are visible
        bool panelVisible = true;

        // For rendering the XAML
        UIElementRenderer elementRenderer;

        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Get the application's ContentManager
            content = (Application.Current as App).Content;

            // Create a timer for this page
            timer = new GameTimer();
            timer.UpdateInterval = TimeSpan.Zero;
            timer.Update += OnUpdate;
            timer.Draw += OnDraw;

            // Use the LayoutUpdate event to know when the page layout has completed so we can
            // create the UIElementRenderer
            LayoutUpdated += new EventHandler(MainPage_LayoutUpdated);
        }

        void MainPage_LayoutUpdated(object sender, EventArgs e)
        {
            // Create the UIElementRenderer to draw the Silverlight page to a texture.

            // Verify the page has a valid size
            if (ActualWidth <= 0 && ActualHeight <= 0)
            {
                return;
            }

            int width = (int)ActualWidth;
            int height = (int)ActualHeight;

            // See if the UIElementRenderer is already the page's size
            if ((elementRenderer != null) &&
                (elementRenderer.Texture != null) &&
                (elementRenderer.Texture.Width == width) &&
                (elementRenderer.Texture.Height == height))
            {
                return;
            }

            // Dispose the UIElementRenderer before creating a new one
            if (elementRenderer != null)
            {
                elementRenderer.Dispose();
            }

            elementRenderer = new UIElementRenderer(this, width, height);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // Set the sharing mode of the graphics device to turn on XNA rendering
            SharedGraphicsDeviceManager.Current.GraphicsDevice.SetSharingMode(true);

            // Create a SpriteBatch for rendering content
            spriteBatch = new SpriteBatch(SharedGraphicsDeviceManager.Current.GraphicsDevice);

            // Create the teapot
            teapot = new TeapotPrimitive(SharedGraphicsDeviceManager.Current.GraphicsDevice);

            // Start the GameTimer
            timer.Start();

            base.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            // Stop the GameTimer
            timer.Stop();

            // Set the sharing mode of the graphics device to turn off XNA rendering
            SharedGraphicsDeviceManager.Current.GraphicsDevice.SetSharingMode(false);

            base.OnNavigatedFrom(e);
        }

        /// <summary>
        /// Allows the page to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        private void OnUpdate(object sender, GameTimerEventArgs e)
        {
            // TODO: Add your update logic here
        }

        /// <summary>
        /// Allows the page to draw itself.
        /// </summary>
        private void OnDraw(object sender, GameTimerEventArgs e)
        {
            // Render the Silverlight controls using the UIElementRenderer
            elementRenderer.Render();

            // Clear the screen to a solid color
            SharedGraphicsDeviceManager.Current.GraphicsDevice.Clear(Color.CornflowerBlue);

            // Draw the teapot
            DrawTeapot(e);

            spriteBatch.Begin();
            // Using the texture from the UIElementRenderer, 
            // draw the Silverlight controls to the screen
            spriteBatch.Draw(elementRenderer.Texture, Vector2.Zero, Color.White);
            spriteBatch.End();
        }

        private void DrawTeapot(GameTimerEventArgs e)
        {
            float aspectRatio = SharedGraphicsDeviceManager.Current.GraphicsDevice.Viewport.AspectRatio;

            // Construct the world, view, and projection matrices
            Matrix world = Matrix.CreateFromYawPitchRoll(teapotYaw, teapotPitch, 0f);
            Matrix view = Matrix.CreateLookAt(new Vector3(0, 0, 2.5f), Vector3.Zero, Vector3.Up);
            Matrix projection = Matrix.CreatePerspectiveFieldOfView(1, aspectRatio, 1, 10);

            // Draw the teapot
            teapot.Draw(world, view, projection, teapotColor);
        }


        #region UI Event Handlers
        // This event handler is hooked up in the XAML 
        private void TeapotManipulationDelta(object sender, ManipulationDeltaEventArgs e)
        {
            teapotYaw = MathHelper.WrapAngle(teapotYaw + (float)e.DeltaManipulation.Translation.X * .01f);
            teapotPitch = MathHelper.WrapAngle(teapotPitch + (float)e.DeltaManipulation.Translation.Y * .01f);
        }

        private void SetTeapotColor(Color c)
        {
            teapotColor = c;
            sliderBlue.Value = c.B;
            sliderGreen.Value = c.G;
            sliderRed.Value = c.R;
        }

        private void redButton_Click(object sender, RoutedEventArgs e)
        {
            SetTeapotColor(Color.Red);
        }

        private void greenButton_Click(object sender, RoutedEventArgs e)
        {
            SetTeapotColor(Color.Lime);
        }

        private void blueButton_Click(object sender, RoutedEventArgs e)
        {
            SetTeapotColor(Color.Blue);
        }

        private void blackButton_Click(object sender, RoutedEventArgs e)
        {
            SetTeapotColor(Color.Black);
        }

        private void slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (sliderRed == null || sliderGreen == null || sliderBlue == null)
                return;

            SetTeapotColor(new Color((int)sliderRed.Value, (int)sliderGreen.Value, (int)sliderBlue.Value));
        }

        private void TogglePanelVisibility(object sender, RoutedEventArgs e)
        {
            if (panelVisible)
            {
                AnimatePanelOut.Completed += new EventHandler(AnimatePanelOut_Completed);
                AnimatePanelOut.Begin();
                panelVisible = false;
            }
            else
            {
                AnimatePanelIn.Begin();
                stackPanel.Visibility = System.Windows.Visibility.Visible;
                panelVisible = true;
            }
        }

        void AnimatePanelOut_Completed(object sender, EventArgs e)
        {
            stackPanel.Visibility = System.Windows.Visibility.Collapsed;
        }
        #endregion
    }
}