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
        public SoundEffect win { get; }

        public StatTracker()
        {                  
            lives = 3;
            coins = 0;
            points = 0;
            timeRemaining = FRAMESPERSECOND * 400;
            //win = theatre.Content.Load<SoundEffect>("SoundEffects/course_clear");
            //lifeRemovedAfterTimeRemainingIsZero = false;
        }

        public void AddCoinCommand()
        {
            coins++;
            if (coins % 100 == 0) AddLifeCommand();
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
/*            else if (timeRemaining == 0 && !lifeRemovedAfterTimeRemainingIsZero)
            {
                lifeRemovedAfterTimeRemainingIsZero = true;
                
            }*/
        }

        public void ResetTimeRemainingCommand()
        {
            timeRemaining = FRAMESPERSECOND * 400;
            //lifeRemovedAfterTimeRemainingIsZero = false;
            //RemoveLifeCommand();
        }

        public void ResetPointsCommand()
        {
            points = 0;
        }

        public void Update()
        {          
            //System.Diagnostics.Debug.WriteLine("Coins: " + tracker.coins);
            //System.Diagnostics.Debug.WriteLine("Lives: " + tracker.lives);
            if (timeRemaining % 60 == 0)
                System.Diagnostics.Debug.WriteLine("Time Remaining: " + timeRemaining / 60);
            //System.Diagnostics.Debug.WriteLine(tracker.lifeRemovedAfterTimeRemainingIsZero);
            DecrementTimeCommand();

            //if (timeRemaining == 0) map.Reset();
        }
    }
}
