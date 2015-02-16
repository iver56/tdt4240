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

namespace tdt4240_oving2
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        public static int screenWidth;
        public static int screenHeight;
        public static int leftBound;
        public static int rightBound;

        public enum Direction { LEFT, RIGHT };

        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private SpriteFont gameFont;
        private Paddle leftPaddle;
        private Paddle rightPaddle;
        private Ball ball;
        private GameState gameState;
        private GameState countDownState;
        private GameState inGameState;
        private Keys[] keys;
        private DateTime gameStart;
        private Random random;
        private String winner = null;
        private GameScore gameScore;

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

            gameScore = GameScore.getInstance();
            countDownState = new CountDownState(this);
            inGameState = new InGameState(this);

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

            gameScore.reset();
            setCountDownState();
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

            keys = Keyboard.GetState().GetPressedKeys();

            gameState.Update(gameTime);

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
                gameScore.getLeftScore().ToString(),
                new Vector2(100, 0),
                Color.White
            );
            spriteBatch.DrawString(
                gameFont,
                gameScore.getRightScore().ToString(),
                new Vector2(screenWidth - 100, 0),
                Color.White
            );

            gameState.Draw(gameTime, spriteBatch);

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

        public Ball getBall()
        {
            return ball;
        }

        public void setCountDownState() {
            gameState = countDownState;
        }

        public void setInGameState()
        {
            gameState = inGameState;
        }

        public void leftMissesBall()
        {
            gameScore.incrementRightScore();
            if (gameScore.hasRightWon())
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
            gameScore.incrementLeftScore();
            if (gameScore.hasLeftWon())
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

        public SpriteFont getGameFont()
        {
            return gameFont;
        }

        public Keys[] getKeys()
        {
            return keys;
        }

        public DateTime getGameStart()
        {
            return gameStart;
        }

        public String getWinner()
        {
            return winner;
        }
    }
}
