using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mario.Movement
{
    
    class Kinematics
    {

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
            int marioTopUpSpeed = 1;
            if (context.Velocity.Y < marioTopUpSpeed)
            {
                context.Velocity.Y += (float)0.1;
            }
        }

        public void MoveUp(MarioContext context)
        {
            if (context.Velocity.Y == 0)
            {
                context.Velocity.Y = 2;
            } else
            {
                context.Velocity.Y = 0;
            }
            
        }

        public void MoveDown(MarioContext context)
        {
            if (context.Velocity.Y == 0)
            {
                context.Velocity.Y = -2;
            } else
            {
                context.Velocity.Y = 0;
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
            if (Math.Abs(context.Velocity.X) < 0.08)
            {
                context.Velocity.X = 0;
            }
        }

    }
}
