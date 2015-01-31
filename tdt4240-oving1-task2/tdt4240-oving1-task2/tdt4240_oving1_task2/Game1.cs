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

namespace tdt4240_oving1_task2
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D helicopterTexture;
        Vector2 helicopterVelocity;
        Vector2 helicopterPosition;
        SpriteEffects helicopterEffects;
        float helicopterRotation;
        float helicopterRotationT;
        Vector2 helicopterSize;
        SpriteFont gameFont;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            helicopterVelocity = new Vector2(0, 0);
            helicopterPosition = new Vector2(100, 100);
            helicopterEffects = SpriteEffects.None;
            helicopterRotation = 0;
            helicopterRotationT = 0;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            helicopterTexture = Content.Load<Texture2D>("helicopter");
            helicopterSize = new Vector2(helicopterTexture.Width, helicopterTexture.Height);
            gameFont = Content.Load<SpriteFont>("SpriteFont1");
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            Keys[] k = Keyboard.GetState().GetPressedKeys();


            if (k.Contains<Keys>(Keys.Right))
            {
                helicopterVelocity.X = 3;
            }
            else if (k.Contains<Keys>(Keys.Left))
            {
                helicopterVelocity.X = -3;
            }
            else
            {
                helicopterVelocity.X = 0;
            }

            if (k.Contains<Keys>(Keys.Up))
            {
                helicopterVelocity.Y = -2;
            }
            else if (k.Contains<Keys>(Keys.Down))
            {
                helicopterVelocity.Y = 2;
            }
            else
            {
                helicopterVelocity.Y = 0;
            }

            helicopterPosition.X += helicopterVelocity.X;
            helicopterPosition.Y += helicopterVelocity.Y;

            if (helicopterPosition.X < 50)
            {
                helicopterPosition.X = 50;
            }
            else if (helicopterPosition.X > GraphicsDevice.Viewport.Width - helicopterSize.X - 50)
            {
                helicopterPosition.X = GraphicsDevice.Viewport.Width - helicopterSize.X - 50;
            }

            if (helicopterPosition.Y < 50)
            {
                helicopterPosition.Y = 50;
            }
            else if (helicopterPosition.Y > GraphicsDevice.Viewport.Height - helicopterSize.Y - 50)
            {
                helicopterPosition.Y = GraphicsDevice.Viewport.Height - helicopterSize.Y - 50;
            }

            if (helicopterVelocity.X > 0)
            {
                helicopterEffects = SpriteEffects.FlipHorizontally;
            }
            else if (helicopterVelocity.X < 0)
            {
                helicopterEffects = SpriteEffects.None;
            }

            if (k.Contains<Keys>(Keys.S))
            {
                Random random = new Random();
                float factor = 0.95f + 0.1f * (float)random.NextDouble();
                helicopterSize *= factor;
            }

            if (k.Contains<Keys>(Keys.R))
            {
                helicopterRotationT += 0.05f;
                helicopterRotation = 0.2f * (float)Math.Sin(helicopterRotationT);
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            spriteBatch.Draw(
                helicopterTexture,
                new Rectangle(
                    (int)helicopterPosition.X + (int)helicopterSize.X / 2,
                    (int)helicopterPosition.Y + (int)helicopterSize.Y / 2,
                    (int)helicopterSize.X,
                    (int)helicopterSize.Y
                ),
                null,
                Color.White,
                helicopterRotation,
                helicopterSize / 2,
                helicopterEffects,
                0
            );
            spriteBatch.DrawString(
                gameFont,
                "Position: (" + helicopterPosition.X + ", " + helicopterPosition.Y + ")",
                new Vector2(0, 0),
                Color.Black
            );
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
