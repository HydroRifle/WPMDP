// ----------------------------------------------------------------------------------
// Microsoft Developer & Platform Evangelism
// 
// Copyright (c) Microsoft Corporation. All rights reserved.
// 
// THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
// EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED WARRANTIES 
// OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
// ----------------------------------------------------------------------------------
// The example companies, organizations, products, domain names,
// e-mail addresses, logos, people, places, and events depicted
// herein are fictitious.  No association with any real company,
// organization, product, domain name, email address, logo, person,
// places, or events is intended or should be inferred.
// ----------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;

namespace ModelViewer
{
    public partial class GamePage : PhoneApplicationPage
    {
        //XNA Setup
        GameTimer timer;
        SpriteBatch spriteBatch;

        //Scene background
        Texture2D background;

        //Model & metadata
        XnaModelWrapper model;
        ModelMetadata modelMetadata;

        //XNA touch setup
        float xRotation = 0.0f;
        float yRotation = 0.0f;
        float? prevLength;
        float cameraFOV = 45; // Initial camera FOV (serves as a zoom level)

        //Silverlight UI rendering
        UIElementRenderer uiRenderer;

        public GamePage()
        {
            InitializeComponent();

            // Create a timer for this page
            timer = new GameTimer();
            timer.UpdateInterval = TimeSpan.FromTicks(333333);
            timer.Update += timer_Update;
            timer.Draw += timer_Draw;

            //Initialize gestures support - Pinch for Zoom and horizontal drag for rotate
            TouchPanel.EnabledGestures = GestureType.FreeDrag | GestureType.Pinch | GestureType.PinchComplete;

            this.LayoutUpdated += GamePage_LayoutUpdated;
            this.DataContext = this;
        }

        private void GamePage_LayoutUpdated(object sender, EventArgs e)
        {
            if(uiRenderer == null || LayoutRoot.ActualWidth > 0 && LayoutRoot.ActualHeight > 0)
                uiRenderer = new UIElementRenderer(LayoutRoot, (int)LayoutRoot.ActualWidth, (int)LayoutRoot.ActualHeight);
        }

        private void timer_Update(object sender, GameTimerEventArgs e)
        {
            HandleInput();

            float yaw = MathHelper.Pi + MathHelper.PiOver2 + xRotation / 100; // rotation around the y-axis
            float pitch = yRotation / 100; // rotation around the x-axis
            float fieldOfView = MathHelper.ToRadians(cameraFOV) / modelMetadata.FieldOfViewDivisor; // zoom

            model.Rotation = modelMetadata.World * Matrix.CreateFromYawPitchRoll(yaw, pitch, 0);
            model.Projection = Matrix.CreatePerspectiveFieldOfView(fieldOfView, modelMetadata.AspectRatio, modelMetadata.NearPlaneDistance, modelMetadata.FarPlaneDistance);
            model.View = modelMetadata.ViewMatrix;
            model.IsTextureEnabled = true;
            model.IsPerPixelLightingEnabled = true;
        }

        private void timer_Draw(object sender, GameTimerEventArgs e)
        {
            SharedGraphicsDeviceManager.Current.GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            spriteBatch.Draw(background, Vector2.Zero, Color.White);
            spriteBatch.End();

            // Set render states.
            SharedGraphicsDeviceManager.Current.GraphicsDevice.DepthStencilState = DepthStencilState.Default;
            SharedGraphicsDeviceManager.Current.GraphicsDevice.BlendState = BlendState.Opaque;
            SharedGraphicsDeviceManager.Current.GraphicsDevice.RasterizerState = RasterizerState.CullNone;
            SharedGraphicsDeviceManager.Current.GraphicsDevice.SamplerStates[0] = SamplerState.LinearWrap;

            // Draw the model
            model.Draw();

            // Update the Silverlight UI
            uiRenderer.Render();

            // Draw the sprite
            spriteBatch.Begin();
            spriteBatch.Draw(uiRenderer.Texture, Vector2.Zero, Color.White);
            spriteBatch.End();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // Set the sharing mode of the graphics device to turn on XNA rendering
            SharedGraphicsDeviceManager.Current.GraphicsDevice.SetSharingMode(true);

            //Initialize SpriteBatch
            spriteBatch = new SpriteBatch(SharedGraphicsDeviceManager.Current.GraphicsDevice);

            App app = (App)Application.Current;
            ContentManager appContentManager = new ContentManager(app, "Content");

            background = appContentManager.Load<Texture2D>("background");

            //Get query string parameter and initialize local metadata variable
            IDictionary<string, string> data = NavigationContext.QueryString;
            modelMetadata = app.ModelsStore[data["ID"]];
            ModelName = modelMetadata.Name;
            ModelDesc = modelMetadata.Description;
            xRotation = modelMetadata.DefaultXRotation;
            yRotation = modelMetadata.DefaultYRotation;

            //Initialize the model
            model = new XnaModelWrapper();
            model.Lights = new bool[] { true, true, true };
            ContentManager contentManager = modelMetadata.IsContent ? appContentManager : new CustomContentManager();
            model.Load(contentManager, modelMetadata.Assets[0]);

            // Start the game timer
            timer.Start();

            base.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            // Stop the game timer
            timer.Stop();

            // Set the sharing mode of the graphics device to turn off XNA rendering
            SharedGraphicsDeviceManager.Current.GraphicsDevice.SetSharingMode(false);

            base.OnNavigatedFrom(e);
        }

        private void HandleInput()
        {
            while (TouchPanel.IsGestureAvailable)
            {
                GestureSample gestureSample = TouchPanel.ReadGesture();
                switch (gestureSample.GestureType)
                {
                    case GestureType.FreeDrag:
                        xRotation += gestureSample.Delta.X;
                        yRotation -= gestureSample.Delta.Y;
                        break;

                    case GestureType.Pinch:
                        float gestureValue = 0;
                        float minFOV = 80;
                        float maxFOV = 20;
                        float gestureLengthToZoomScale = 10;

                        Vector2 gestureDiff = gestureSample.Position - gestureSample.Position2;
                        gestureValue = gestureDiff.Length() / gestureLengthToZoomScale;

                        if (null != prevLength) // Skip the first pinch event
                            cameraFOV -= gestureValue - prevLength.Value;

                        cameraFOV = MathHelper.Clamp(cameraFOV, maxFOV, minFOV);

                        prevLength = gestureValue;
                        break;

                    case GestureType.PinchComplete:
                        prevLength = null;
                        break;

                    default:
                        break;
                }
            }
        }

        public string ModelName
        {
            get { return (string)GetValue(ModelNameProperty); }
            set { SetValue(ModelNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ModelName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ModelNameProperty =
            DependencyProperty.Register("ModelName", typeof(string), typeof(GamePage), null);

        public string ModelDesc
        {
            get { return (string)GetValue(ModelDescProperty); }
            set { SetValue(ModelDescProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ModelDesc.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ModelDescProperty =
            DependencyProperty.Register("ModelDesc", typeof(string), typeof(GamePage), null);
    }
}
