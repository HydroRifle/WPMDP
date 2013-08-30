using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;
using Microsoft.Devices.Sensors;

namespace AccelerometerEnabledGame
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D Logo;
        Vector2 LogoPosition;

        Accelerometer gSensor;
        Vector2 LogoVelocity;                                

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.SupportedOrientations = DisplayOrientation.Portrait |
                                        DisplayOrientation.LandscapeLeft |
                                        DisplayOrientation.LandscapeRight;

            graphics.PreferredBackBufferWidth = 480;
            graphics.PreferredBackBufferHeight = 800;
            // Frame rate is 30 fps by default for Windows Phone.
            TargetElapsedTime = TimeSpan.FromTicks(333333);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            gSensor = new Accelerometer();
            gSensor.ReadingChanged += new EventHandler<AccelerometerReadingEventArgs>(gSensor_ReadingChanged);
            gSensor.Start();

            base.Initialize();
        }

        void gSensor_ReadingChanged(object sender, AccelerometerReadingEventArgs e)
        {
            LogoVelocity.X += (float)e.X;
            LogoVelocity.Y += -(float)e.Y;

            LogoPosition += LogoVelocity;            
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            Logo = Content.Load<Texture2D>("xna");
            Viewport viewport = graphics.GraphicsDevice.Viewport;
            LogoPosition = new Vector2(
                (viewport.Width - Logo.Width) / 2,
                (viewport.Height - Logo.Height) / 2);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            gSensor.Stop();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            Viewport viewport = graphics.GraphicsDevice.Viewport;

            if (LogoPosition.X < 0)
            {
                LogoPosition.X = 0;
                LogoVelocity.X = 0;
            }
            else if (LogoPosition.X > viewport.Width - Logo.Width)
            {
                LogoPosition.X = viewport.Width - Logo.Width;
                LogoVelocity.X = 0;
            }

            // keep the sprite on the screen - clamp Y
            if (LogoPosition.Y < 0)
            {
                LogoPosition.Y = 0;
                LogoVelocity.Y = 0;
            }
            else if (LogoPosition.Y > viewport.Height - Logo.Height)
            {
                LogoPosition.Y = viewport.Height - Logo.Height;
                LogoVelocity.Y = 0;
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            spriteBatch.Draw(Logo, LogoPosition,Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
