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
    class CountDownState : GameState
    {
        private int secondsUntilGameStarts;

        public CountDownState(Game1 game)
        {
            this.game = game;
        }

        public override void Update(GameTime gameTime)
        {
            secondsUntilGameStarts = (int)(game.getGameStart() - DateTime.Now).TotalSeconds;
            if (secondsUntilGameStarts <= 0)
            {
                game.setInGameState();
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (null != game.getWinner())
            {
                var winnerString = game.getWinner() + " wins!";
                var scale = (float)(1.5 + 0.5 * Math.Sin(0.008 * (DateTime.Now - game.getGameStart()).TotalMilliseconds));
                var stringWidth = game.getGameFont().MeasureString(winnerString).Length() * scale;
                spriteBatch.DrawString(
                    game.getGameFont(),
                    winnerString,
                    new Vector2(Game1.screenWidth / 2 - stringWidth / 2, Game1.screenHeight / 2 - 90),
                    Color.White,
                    0,
                    new Vector2(0, 0),
                    scale,
                    SpriteEffects.None,
                    0
                );
            }
            spriteBatch.DrawString(
                game.getGameFont(),
                secondsUntilGameStarts.ToString(),
                new Vector2(Game1.screenWidth / 2, Game1.screenHeight / 2),
                Color.White
            );
        }
    }
}
