using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mario.Movement
{
    
    public class Kinematics
    {

        public Kinematics()
        {

        }
        public void AccelerateLeft(MarioContext context)
        {
            float marioTopLeftSpeed = (float)-3.0;
            if (context.Velocity.X > marioTopLeftSpeed)
            {
                context.Velocity.X -= (float)0.2;
            }
        }

        public void AccelerateRight(MarioContext context)
        {
            float marioTopRightSpeed = (float)3.0;
            if (context.Velocity.X < marioTopRightSpeed)
            {
                context.Velocity.X += (float)0.2;
            }
        }

        public void AccelerateUp(MarioContext context)
        {
            float marioTopUpSpeed = (float)3.0;
            if (context.Velocity.Y < marioTopUpSpeed)
            {
                context.Velocity.Y += (float)0.7;              
            }
            context.jumpHeight += (float)0.7;
        }

        public void AccelerateDown(MarioContext context)
        {
            float marioTopDownSpeed = (float)-3.0;
            if (context.Velocity.Y > marioTopDownSpeed)
            {
                context.Velocity.Y -= (float)0.4;              
            }
            context.jumpHeight -= (float)0.4;
        }

        public void IdleXDecelerate(MarioContext context)
        {
            if (context.Velocity.X != 0)
            {
                if (context.Velocity.X < 0)
                {
                    context.Velocity.X += (float)0.2;
                }
                else
                {
                    context.Velocity.X -= (float)0.2;
                }
            }

            // if there's leftover speed from shitty code, zero it
            if (Math.Abs(context.Velocity.X) < 0.16)
            {
                context.Velocity.X = 0;
            }

        }

        public void IdleYDecelerate(MarioContext context)
        {
            if (context.Velocity.Y != 0)
            {
                if (context.Velocity.Y < 0)
                {
                    context.Velocity.Y += (float)0.3;
                }
                else
                {
                    context.Velocity.Y -= (float)0.3;
                }
            }

            if (Math.Abs(context.Velocity.Y) < 0.16)
            {
                context.Velocity.Y = 0;
            }
        }

        public void XDecelerateToLeft(MarioContext context)
        {
            if (context.Velocity.X != 0)
            {
                if (context.Velocity.X > 0)
                    context.Velocity.X -= (float)0.1;
            }
        }

        public void XDecelerateToRight(MarioContext context)
        {
            if (context.Velocity.X != 0)
            {
                if (context.Velocity.X < 0)
                    context.Velocity.X += (float)0.1;
            }
        }
        public void YDecelerateToUp(MarioContext context)
        {
            if (context.Velocity.Y != 0)
            {
                if (context.Velocity.Y < 0)
                    context.Velocity.Y = 0;
            }

        }
        public void YDecelerateToDown(MarioContext context)
        {
            if (context.Velocity.Y != 0)
            {
                if (context.Velocity.Y > 0)
                    context.Velocity.Y = 0;
            }
        }

    }
}
