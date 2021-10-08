using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mario.Movement
{
    
    class Kinematics
    {
        private string prevKinematics;

        public Kinematics()
        {

        }
        public void AccelerateLeft(MarioContext context)
        {
            int marioTopLeftSpeed = -3;
            if (context.Velocity.X > marioTopLeftSpeed)
            {
                context.Velocity.X -= (float)0.15;
            }
        }

        public void AccelerateRight(MarioContext context)
        {
            int marioTopRightSpeed = 3;
            if (context.Velocity.X < marioTopRightSpeed)
            {
                context.Velocity.X += (float)0.15;
            }
        }

        public void AccelerateUp(MarioContext context)
        {
            int marioTopUpSpeed = 2;
            if (context.yVelocity < marioTopUpSpeed)
            {
                context.Velocity.Y += (float)0.1;
            }
        }

        public void AccelerateDown(MarioContext context)
        {
            int marioTopDownSpeed = -2;
            if (context.yVelocity > marioTopDownSpeed)
            {
                context.yVelocity -= (float)0.1;
            }
        }

        public void IdleXDecelerate(MarioContext context)
        {
            if (context.Velocity.X != 0)
            {
                if (context.Velocity.X < 0)
                {
                    context.Velocity.X += (float)0.3;
                }
                else
                {
                    context.Velocity.X -= (float)0.3;
                }
            }

            // if there's leftover speed from shitty code, zero it
            if (Math.Abs(context.xVelocity) < 0.16)
            {
                context.Velocity.X = 0;
            }

        }

        public void IdleYDecelerate(MarioContext context)
        {
            if (context.yVelocity != 0)
            {
                if (context.yVelocity < 0)
                {
                    context.yVelocity += (float)0.3;
                }
                else
                {
                    context.yVelocity -= (float)0.3;
                }
            }

            if (Math.Abs(context.yVelocity) < 0.16)
            {
                context.yVelocity = 0;
            }
        }
    }
}
