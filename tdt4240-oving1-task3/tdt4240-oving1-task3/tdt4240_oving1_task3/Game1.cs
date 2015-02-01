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

namespace tdt4240_oving1_task3
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public static int screenWidth;
        public static int screenHeight;
        List<Helicopter> helicopters;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();
            screenWidth = GraphicsDevice.Viewport.Width;
            screenHeight = GraphicsDevice.Viewport.Height;

            Random random = new Random();
            helicopters = new List<Helicopter>();
            for (var i = 0; i < 4; i++)
            {
                var maxX = screenWidth - Helicopter.frameWidth;
                var maxY = screenHeight - Helicopter.frameHeight;
                var velocity = new Vector2(2f + 2f * (float)random.NextDouble(), 1.5f + 1.5f * (float)random.NextDouble());

                for (var j = 0; j < 20; j++)
                {
                    var position = new Vector2(random.Next(maxX), random.Next(0, maxY));
                    var helicopter = new Helicopter(
                        this,
                        position,
                        velocity
                    );
                    if (!helicopter.collidesWith(helicopters))
                    {
                        helicopter.LoadContent();
                        helicopters.Add(helicopter);
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
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

            for (int i = 0; i < helicopters.Count; i++)
            {
                helicopters[i].Update(gameTime);
                for (int j = i + 1; j < helicopters.Count; j++)
                {
                    if (helicopters[i].collidesWith(helicopters[j]))
                    {
                        helicopters[i].collideWith(helicopters[j]);
                        helicopters[j].collideWith(helicopters[i]);
                    }
                }
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
            for (int i = 0; i < helicopters.Count; i++)
            {
                helicopters[i].Draw(gameTime, spriteBatch);
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
