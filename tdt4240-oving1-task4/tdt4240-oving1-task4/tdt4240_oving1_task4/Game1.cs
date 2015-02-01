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

namespace tdt4240_oving1_task4
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont gameFont;

        public static int screenWidth;
        public static int screenHeight;
        Paddle leftPaddle;
        Paddle rightPaddle;
        Ball ball;
        int leftScore;
        int rightScore;
        public enum Direction { LEFT, RIGHT };
        public static int leftBound;
        public static int rightBound;
        enum GameState { CountDown, InGame };
        GameState gameState;
        DateTime gameStart;
        int secondsUntilGameStarts;
        Random random;
        String winner = null;
        static readonly int maxPoints = 21;

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

            leftBound = 50;
            rightBound = screenWidth - 50;

            random = new Random();

            resetGame();
        }

        public void resetGame()
        {
            Random random = new Random();
            leftPaddle = new Paddle(this, new Vector2(leftBound - Paddle.spriteWidth, screenHeight / 2 - Paddle.spriteHeight / 2));
            leftPaddle.LoadContent();
            rightPaddle = new Paddle(this, new Vector2(rightBound, screenHeight / 2 - Paddle.spriteHeight / 2));
            rightPaddle.LoadContent();
            ball = new Ball(this);
            ball.LoadContent();
            ball.reset();
            
            leftScore = 0;
            rightScore = 0;
            gameState = GameState.CountDown;
            gameStart = DateTime.Now + TimeSpan.FromSeconds(3.99);
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            gameFont = Content.Load<SpriteFont>("SpriteFont1");
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

            Keys[] k = Keyboard.GetState().GetPressedKeys();


            switch (gameState)
            {
                case GameState.InGame:
                    if (k.Contains<Keys>(Keys.W))
                    {
                        leftPaddle.moveUp();
                    }
                    else if (k.Contains<Keys>(Keys.S))
                    {
                        leftPaddle.moveDown();
                    }

                    if (k.Contains<Keys>(Keys.Up))
                    {
                        rightPaddle.moveUp();
                    }
                    else if (k.Contains<Keys>(Keys.Down))
                    {
                        rightPaddle.moveDown();
                    }

                    leftPaddle.Update(gameTime);
                    rightPaddle.Update(gameTime);
                    ball.Update(gameTime);
                    break;
                case GameState.CountDown:
                    secondsUntilGameStarts = (int)(gameStart - DateTime.Now).TotalSeconds;
                    if (secondsUntilGameStarts <= 0)
                    {
                        gameState = GameState.InGame;
                    }
                    break;
                default:
                    break;
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            leftPaddle.Draw(gameTime, spriteBatch);
            rightPaddle.Draw(gameTime, spriteBatch);

            spriteBatch.DrawString(
                gameFont,
                leftScore.ToString(),
                new Vector2(100, 0),
                Color.White
            );
            spriteBatch.DrawString(
                gameFont,
                rightScore.ToString(),
                new Vector2(screenWidth - 100, 0),
                Color.White
            );

            switch (gameState)
            {
                case GameState.InGame:
                    ball.Draw(gameTime, spriteBatch);
                    break;

                case GameState.CountDown:
                    if (null != winner) {
                        var winnerString = winner + " wins!";
                        var scale = (float)(1.5 + 0.5 * Math.Sin(0.008 * (DateTime.Now - gameStart).TotalMilliseconds));
                        var stringWidth = gameFont.MeasureString(winnerString).Length() * scale;
                        spriteBatch.DrawString(
                            gameFont,
                            winnerString,
                            new Vector2(screenWidth / 2 - stringWidth / 2, screenHeight / 2 - 90),
                            Color.White,
                            0,
                            new Vector2(0, 0),
                            scale,
                            SpriteEffects.None,
                            0
                        );
                    }
                    spriteBatch.DrawString(
                        gameFont,
                        secondsUntilGameStarts.ToString(),
                        new Vector2(screenWidth / 2, screenHeight / 2),
                        Color.White
                    );
                    break;

                default:
                    break;
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        public Paddle getLeftPaddle()
        {
            return leftPaddle;
        }

        public Paddle getRightPaddle()
        {
            return rightPaddle;
        }

        public void leftMissesBall()
        {
            rightScore++;
            if (rightScore >= maxPoints)
            {
                winner = "Right";
                resetGame();
            }
            else
            {
                ball.reset();
            }
        }

        public void rightMissesBall()
        {
            leftScore++;
            if (leftScore >= maxPoints)
            {
                winner = "Left";
                resetGame();
            }
            else
            {
                ball.reset();
            }
        }

        public Random getRandom()
        {
            return random;
        }
    }
}
