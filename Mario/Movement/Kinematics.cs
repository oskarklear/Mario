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

        public void AccelerateRight(MarioContext context)
        {
            int marioTopRightSpeed = 3;
            if (context.xVelocity < marioTopRightSpeed)
            {
                context.xVelocity += (float)0.15;
            }
        }

        public void AccelerateUp(MarioContext context)
        {
            int marioTopUpSpeed = 1;
            if (context.yVelocity < marioTopUpSpeed)
            {
                context.yVelocity += (float)0.1;
            }
        }

        public void MoveUp(MarioContext context)
        {
            context.yVelocity = 2;
        }

        public void MoveDown(MarioContext context)
        {
            context.yVelocity = -2;
        }


    }
}
