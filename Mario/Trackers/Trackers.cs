using System;
using System.Collections.Generic;
using System.Text;

namespace Mario.Trackers
{
    class StatTracker
    {
        int lives;
        int coins;
        int points;
        int timeRemaining;

        public StatTracker()
        {
            lives = 3;
            coins = 0;
            points = 0;
            timeRemaining = 400;
        }
    }
}
