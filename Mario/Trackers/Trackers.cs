using System;
using System.Collections.Generic;
using System.Text;

namespace Mario.Trackers
{
    public class StatTracker
    {
        public int lives;
        public int coins;
        int points;
        int timeRemaining;

        public StatTracker()
        {
            lives = 3;
            coins = 99;
            points = 0;
            timeRemaining = 400;
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
            timeRemaining--;
            //if (timeRemaining == 0)
        }
    }
}
