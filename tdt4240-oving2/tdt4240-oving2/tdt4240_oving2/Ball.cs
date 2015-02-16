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
    class Ball : Microsoft.Xna.Framework.DrawableGameComponent
    {
        private Vector2 position;
        private Vector2 velocity;
        private Game1 game;
        private Texture2D sprite;
        private Rectangle sourceRect = new Rectangle();
        private Rectangle destinationRect = new Rectangle();
        public static int spriteWidth = 24;
        public static int spriteHeight = 24;

        public Ball(Game game)
            : base(game)
        {
            this.game = (Game1)game;
        }

        public void LoadContent()
        {
            sprite = Game.Content.Load<Texture2D>("ball");
            base.LoadContent();
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            sourceRect = new Rectangle(0, 0, spriteWidth, spriteHeight);
            destinationRect = new Rectangle((int)position.X, (int)position.Y, spriteWidth, spriteHeight);

            if (position.Y <= 0)
            {
                position.Y = 0;
                velocity.Y *= -1;
            }
            else if (position.Y >= Game1.screenHeight - spriteHeight)
            {
                position.Y = Game1.screenHeight - spriteHeight;
                velocity.Y *= -1;
            }

            position += velocity;
            velocity *= 1.001f;

            if (position.X <= Game1.leftBound)
            {
                if (collidesWithPaddle(game.getLeftPaddle()))
                {
                    position.X = Game1.leftBound;
                    velocity.X *= -1;
                }
                else
                {
                    game.leftMissesBall();
                }

            }
            else if (position.X + spriteWidth >= Game1.rightBound)
            {
                if (collidesWithPaddle(game.getRightPaddle()))
                {
                    position.X = Game1.rightBound - spriteWidth;
                    velocity.X *= -1;
                }
                else
                {
                    game.rightMissesBall();
                }
            }

            base.Update(gameTime);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, destinationRect, Color.White);

            base.Draw(gameTime);
        }

        public void resetPosition()
        {
            position = new Vector2(Game1.screenWidth / 2 - Ball.spriteWidth / 2, Game1.screenHeight / 2 - Ball.spriteHeight);
            velocity = new Vector2(3, 0);
        }

        public void initializeVelocity()
        {
            velocity = new Vector2(
                (float)(4.5 * (game.getRandom().NextDouble() > 0.5 ? -1 : 1)),
                (float)(1 + 2 * game.getRandom().NextDouble()) * (game.getRandom().NextDouble() > 0.5 ? -1 : 1)
            );
        }

        public void reset()
        {
            resetPosition();
            initializeVelocity();
        }

        public Boolean collidesWithPaddle(Paddle paddle)
        {
            return position.Y + spriteHeight / 2 > paddle.getTopY() && position.Y + spriteHeight / 2 < paddle.getBottomY();
        }
    }
}
