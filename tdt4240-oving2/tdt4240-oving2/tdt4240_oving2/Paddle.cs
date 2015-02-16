using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace tdt4240_oving2
{
    public class Paddle : Microsoft.Xna.Framework.DrawableGameComponent
    {
        private Vector2 position;
        private Game1 game;
        private Texture2D sprite;
        private Rectangle sourceRect = new Rectangle();
        public static int spriteWidth = 5;
        public static int spriteHeight = 130;

        public Paddle(Game game, Vector2 position)
            : base(game)
        {
            this.position = position;
            this.game = (Game1)game;
        }

        public void LoadContent()
        {
            sprite = Game.Content.Load<Texture2D>("paddle");
            base.LoadContent();
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            sourceRect = new Rectangle(0, 0, spriteWidth, spriteHeight);

            if (position.Y < 0)
            {
                position.Y = 0;
            }
            else if (position.Y > Game1.screenHeight - spriteHeight)
            {
                position.Y = Game1.screenHeight - spriteHeight;
            }

            base.Update(gameTime);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, new Rectangle((int)position.X, (int)position.Y, spriteWidth, spriteHeight), Color.White);

            base.Draw(gameTime);
        }

        public void moveUp()
        {
            position.Y -= 5;
        }

        public void moveDown()
        {
            position.Y += 5;
        }

        public float getTopY()
        {
            return position.Y;
        }

        public float getBottomY()
        {
            return position.Y + spriteHeight;
        }
    }
}
