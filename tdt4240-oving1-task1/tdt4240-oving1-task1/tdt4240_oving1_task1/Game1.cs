using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace tdt4240_oving1_task1
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D helicopterTexture;
        Vector2 helicopterVelocity;
        Vector2 helicopterPosition;
        SpriteEffects helicopterEffects;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            helicopterVelocity = new Vector2(3, 0);
            helicopterPosition = new Vector2(100, 100);
            helicopterEffects = SpriteEffects.FlipHorizontally;
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

            base.Initialize();
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

            helicopterTexture = Content.Load<Texture2D>("helicopter");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
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

            if (helicopterPosition.X < 0)
            {
                helicopterVelocity.X = 3;
            }
            else if (helicopterPosition.X > GraphicsDevice.Viewport.Width - helicopterTexture.Width)
            {
                helicopterVelocity.X = -3;
            }

            helicopterPosition.X += helicopterVelocity.X;
            helicopterPosition.Y += helicopterVelocity.Y;

            if (helicopterVelocity.X > 0)
            {
                helicopterEffects = SpriteEffects.FlipHorizontally;
            }
            else
            {
                helicopterEffects = SpriteEffects.None;
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

            spriteBatch.Begin();
            spriteBatch.Draw(
                helicopterTexture,
                new Rectangle(
                    (int)helicopterPosition.X,
                    (int)helicopterPosition.Y,
                    helicopterTexture.Width,
                    helicopterTexture.Height
                ),
                null,
                Color.White,
                0,
                new Vector2(0, 0),
                helicopterEffects,
                0
            );
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
