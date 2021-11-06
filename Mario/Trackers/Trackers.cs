using System;
using System.Collections.Generic;
using System.Text;

namespace Mario.Trackers
{
    public class StatTracker
    {
        protected const int FRAMESPERSECOND = 60;

        public int lives;
        public int coins;
        int points;
        public int timeRemaining;
        public bool lifeRemovedAfterTimeRemainingIsZero;

        public StatTracker()
        {
            lives = 3;
            coins = 0;
            points = 0;
            timeRemaining = FRAMESPERSECOND * 5;
            lifeRemovedAfterTimeRemainingIsZero = false;
        }

        public void AddCoinCommand()
        {
            coins++;
            if (coins % 100 == 0) AddLifeCommand();
        }

        public void AddLifeCommand()
        {
            lives++;
        }

        public void RemoveLifeCommand()
        {
            lives--;
        }

        public void AddPointsCommand(int pointsToAdd)
        {
            points += pointsToAdd;
        }

        public void DecrementTimeCommand()
        {
            if (timeRemaining > 0) timeRemaining--;
/*            else if (timeRemaining == 0 && !lifeRemovedAfterTimeRemainingIsZero)
            {
                lifeRemovedAfterTimeRemainingIsZero = true;
                
            }*/
        }

        public void ResetTimeRemainingCommand()
        {
            timeRemaining = FRAMESPERSECOND * 400;
            //lifeRemovedAfterTimeRemainingIsZero = false;
            RemoveLifeCommand();
        }
    }
}
