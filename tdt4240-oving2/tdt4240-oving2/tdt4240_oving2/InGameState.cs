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
    class InGameState : GameState
    {
        public InGameState(Game1 game)
        {
            this.game = game;
        }

        public override void Update(GameTime gameTime)
        {
            Keys[] keys = game.getKeys();
            if (keys.Contains<Keys>(Keys.W))
            {
                game.getLeftPaddle().moveUp();
            }
            else if (keys.Contains<Keys>(Keys.S))
            {
                game.getLeftPaddle().moveDown();
            }

            if (keys.Contains<Keys>(Keys.Up))
            {
                game.getRightPaddle().moveUp();
            }
            else if (keys.Contains<Keys>(Keys.Down))
            {
                game.getRightPaddle().moveDown();
            }

            game.getLeftPaddle().Update(gameTime);
            game.getRightPaddle().Update(gameTime);
            game.getBall().Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            game.getBall().Draw(gameTime, spriteBatch);
        }
    }
}
