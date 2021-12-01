using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Mario.Sprites.Items;
using Microsoft.Xna.Framework.Audio;

namespace Mario.Trackers
{
    public class StatTracker
    {
        protected const int FRAMESPERSECOND = 60;

        public int lives;
        public int coins;
        public int points;
        public int timeRemaining;
        public bool levelComplete;

        public StatTracker()
        {                  
            lives = 3;
            coins = 0;
            points = 0;
            timeRemaining = FRAMESPERSECOND * 400;
            levelComplete = false;
        }

        public void AddCoinCommand()
        {
            coins++;
            if (coins % 100 == 0)
            {
                AddLifeCommand();
                coins = 0;
            }
            AddPointsCommand(200);
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
        }

        public void ResetTimeRemainingCommand()
        {
            timeRemaining = FRAMESPERSECOND * 400;
        }
        public void ConvertTimeToPoints()
        {
            points += timeRemaining / 60 * 50;
            //timeRemaining = 0;
        }

        public void ResetPointsCommand()
        {
            points = 0;
        }

        public void ResetLivesCommand()
        {
            lives = 3;
        }

        public void Update()
        {          
            if (timeRemaining % 60 == 0)
                System.Diagnostics.Debug.WriteLine("Time Remaining: " + timeRemaining / 60);
            if (!levelComplete)
            {
                DecrementTimeCommand();
            }
            else
            {
                timeRemaining = 0;
            }
        }
    }
}
