using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace tdt4240_oving2
{
    class GameScore
    {
        private static GameScore INSTANCE = new GameScore();
        private static readonly int MAX_SCORE = 21;

        private int leftScore;
        private int rightScore;

        public static GameScore getInstance()
        {
            return INSTANCE;
        }

        public int getLeftScore()
        {
            return leftScore;
        }

        public int getRightScore()
        {
            return rightScore;
        }

        public void reset()
        {
            leftScore = 0;
            rightScore = 0;
        }

        public void incrementLeftScore()
        {
            leftScore++;
        }

        public void incrementRightScore()
        {
            rightScore++;
        }

        public Boolean hasLeftWon()
        {
            return leftScore >= MAX_SCORE;
        }

        public Boolean hasRightWon()
        {
            return rightScore >= MAX_SCORE;
        }
    }
}
