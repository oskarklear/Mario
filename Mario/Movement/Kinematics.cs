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
            if (context.yVelocity == 0)
            {
                context.yVelocity = 2;
            } else
            {
                context.yVelocity = 0;
            }
            
        }

        public void MoveDown(MarioContext context)
        {
            if (context.yVelocity == 0)
            {
                context.yVelocity = -2;
            } else
            {
                context.yVelocity = 0;
            }
        }

        public void IdleXDecelerate(MarioContext context)
        {
            if (context.xVelocity != 0)
            {
                if (context.xVelocity < 0)
                {
                    context.xVelocity += (float)0.3;
                }
                else
                {
                    context.xVelocity -= (float)0.3;
                }
            }

            // if there's leftover speed from shitty code, zero it
            if (Math.Abs(context.xVelocity) < 0.08)
            {
                context.xVelocity = 0;
            }
        }

    }
}
