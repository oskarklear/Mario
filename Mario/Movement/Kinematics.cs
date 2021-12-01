using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mario.Movement
{
    public class Kinematics
    {
        private MarioContext context;
        private const double HACCEL = 0.2;

        public Kinematics(MarioContext Context)
        {
            this.context = Context;
        }

        public void AccelerateLeft()
        {
            float marioTopLeftSpeed;
            if (context.dashing && !context.isBallooned)
                marioTopLeftSpeed = (float)-6.0;
            else
                marioTopLeftSpeed = (float)-3.0;
            if (context.isBallooned) marioTopLeftSpeed = (float)-2;


            if (context.Velocity.X > marioTopLeftSpeed)
            {
                context.Velocity.X -= (float)HACCEL;
            }
            else if (context.Velocity.X < marioTopLeftSpeed - HACCEL)
            {
                context.Velocity.X += (float)HACCEL;
            }
        }

        public void AccelerateRight()
        {
            float marioTopRightSpeed;
            if (context.dashing && !context.isBallooned)
                marioTopRightSpeed = (float)6.0;
            else
                marioTopRightSpeed = (float)3.0;

            if (context.isBallooned) marioTopRightSpeed = (float)2;

            if (context.Velocity.X < marioTopRightSpeed)
            {
                context.Velocity.X += (float)HACCEL;
            }
            else if (context.Velocity.X > marioTopRightSpeed + HACCEL)
            {
                context.Velocity.X -= (float)HACCEL;
            }
        }

        public void AccelerateUp()
        {

            float marioTopUpSpeed = (float)3.0;
            if (context.isBallooned) marioTopUpSpeed = (float)0.75;

            if (context.Velocity.Y < marioTopUpSpeed)
            {
                if (context.isCape && context.firstJump && context.dashing)
                {
                    context.Velocity.Y += (float)3.0;
                }
                else if (!context.isBallooned)
                {
                    context.Velocity.Y += (float)0.7;
                } else
                {
                    context.Velocity.Y += (float)0.4;
                }
                    
            }
            context.jumpHeight += (float)0.7;
            

        }

        public void AccelerateDown()
        {
            float marioTopDownSpeed = (float)-3.0;
            if (context.Velocity.Y > marioTopDownSpeed)
            {
                context.Velocity.Y -= (float)0.4;              
            }
            context.jumpHeight -= (float)0.4;
        }

        public void AccelerateDownBalloon()
        {
            float marioTopDownSpeed = (float)-0.75;
            if (context.Velocity.Y > marioTopDownSpeed)
            {
                context.Velocity.Y -= (float)0.025;
            }
            context.jumpHeight -= (float)0.4;
        }
        public void AccelerateDownCape()
        {
            float marioTopDownSpeed = (float)-3.0;
            if (context.Velocity.Y > marioTopDownSpeed)
            {
                context.Velocity.Y -= (float)0.2;
            }
            context.jumpHeight -= (float)0.1;
        }
        public void IdleXDecelerate()
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

        public void IdleYDecelerate()
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

        public void XDecelerateToLeft()
        {
            if (context.Velocity.X != 0)
            {
                if (context.Velocity.X > 0)
                    context.Velocity.X -= (float)0.1;
            }
        }

        public void XDecelerateToRight()
        {
            if (context.Velocity.X != 0)
            {
                if (context.Velocity.X < 0)
                    context.Velocity.X += (float)0.1;
            }
        }
        public void YDecelerateToUp()
        {
            if (context.Velocity.Y != 0)
            {
                if (context.Velocity.Y < 0)
                    context.Velocity.Y = 0;
            }

        }
        public void YDecelerateToDown()
        {
            if (context.Velocity.Y != 0)
            {
                if (context.Velocity.Y > 0)
                    context.Velocity.Y = 0;
            }
        }

    }
}
