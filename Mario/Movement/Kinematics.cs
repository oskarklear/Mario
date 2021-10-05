using System;
using System.Collections.Generic;
using System.Text;

namespace Mario.Movement
{
    class Kinematics
    {

        public void AccelerateLeft(MarioContext context)
        {
            int marioTopLeftSpeed = -3;
            if (context.xVelocity > marioTopLeftSpeed)
            {
                context.xVelocity -= (float)0.15;
            }
        }

    }
}
