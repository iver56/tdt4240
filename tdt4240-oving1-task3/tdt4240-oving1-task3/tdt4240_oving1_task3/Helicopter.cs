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

namespace tdt4240_oving1_task3
{
    class Helicopter : Microsoft.Xna.Framework.DrawableGameComponent
    {
        private Vector2 position;
        private Game1 game;
        private Texture2D spriteStrip;
        private int frameCount;
        private Rectangle sourceRect = new Rectangle();
        private Rectangle destinationRect = new Rectangle();
        public static int frameWidth = 130;
        public static int frameHeight = 52;
        private DateTime created;
        private int currentFrame;
        private Vector2 velocity;
        private SpriteEffects spriteEffect;

        public Helicopter(Game game, Vector2 position, Vector2 velocity)
            : base(game)
        {
            this.position = position;
            this.velocity = velocity;
            frameCount = 4;
            this.game = (Game1)game;
            created = DateTime.Now;
        }

        public void LoadContent()
        {
            spriteStrip = Game.Content.Load<Texture2D>("helicopter-sheet");
            base.LoadContent();
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            currentFrame = (int)(DateTime.Now.Subtract(created).TotalMilliseconds / 100) % frameCount;
            sourceRect = new Rectangle(currentFrame * frameWidth, 0, frameWidth, frameHeight);
            destinationRect = new Rectangle((int)position.X, (int)position.Y, frameWidth, frameHeight);
            
            position += velocity;

            if (position.X < 0)
            {
                position.X = 0;
                velocity.X *= -1;
            }
            else if (position.X > Game1.screenWidth - frameWidth)
            {
                position.X = Game1.screenWidth - frameWidth;
                velocity.X *= -1;
            }

            if (position.Y < 0)
            {
                position.Y = 0;
                velocity.Y *= -1;
            }
            else if (position.Y > Game1.screenHeight - frameHeight)
            {
                position.Y = Game1.screenHeight - frameHeight;
                velocity.Y *= -1;
            }

            if (velocity.X > 0)
            {
                spriteEffect = SpriteEffects.FlipHorizontally;
            }
            else if (velocity.X < 0)
            {
                spriteEffect = SpriteEffects.None;
            }

            base.Update(gameTime);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                spriteStrip,
                destinationRect,
                sourceRect,
                Color.White,
                0,
                new Vector2(0, 0),
                spriteEffect,
                0
            );

            base.Draw(gameTime);
        }

        public Rectangle getRectangle()
        {
            return new Rectangle((int)position.X, (int)position.Y, frameWidth, frameHeight);
        }

        public Boolean collidesWith(Helicopter other)
        {
            return this.getRectangle().Intersects(other.getRectangle());
        }

        public Boolean collidesWith(List<Helicopter> others)
        {
            for (var i = 0; i < others.Count; i++)
            {
                if (this.collidesWith(others[i]))
                {
                    return true;
                }
            }
            return false;
        }

        public void collideWith(Helicopter other)
        {
            // TODO: Improve collision behavior, f.ex. http://en.wikipedia.org/wiki/Elastic_collision#Two-Dimensional_Collision_With_Two_Moving_Objects
            this.velocity *= -1;
        }
    }
}
