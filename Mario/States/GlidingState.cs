﻿using System;
using System.Collections.Generic;
using System.Text;
using Mario.Movement;
using Microsoft.Xna.Framework.Media;

namespace Mario.States
{
    public class GlidingState : MarioActionState
    {
        public GlidingState(MarioContext context)
        {
            marioContext = context;
            PowerUpState = context.GetPowerUpState();
            kinematics = new Kinematics(context);
        }

        public override void Enter(IMarioActionState previousActionState)
        {
            PreviousActionState = previousActionState;
            marioContext.SetActionState(this);
            kinematics.AccelerateUp();
            marioContext.isFalling = false;
            marioContext.isTouchingTop = false;
            marioContext.capeglide.Play();
            
            
        }

        public override void Exit()
        {
            PreviousActionState.Enter(this);
        }

        public override void StandingTransition()
        {
            marioContext.idleState.Enter(this);
        }

        public override void CrouchingTransition()
        {
            //Does nothing - for now
        }

        public override void WalkingTransition()
        {
            //Does nothing - for now
        }

        public override void RunningTransition()
        {
            //Does nothing - at least, for now we imply that you cannot get into running from jumping
        }
        public override void JumpingTransition()
        {
            /*if (marioContext.jumpHeight > 12 || marioContext.isTouchingBottom)
            {
                FallingTransition();
            }
            else
            {
                kinematics.AccelerateUp(marioContext);
            }
            */
        }

        public override void FallingTransition()
        {
            //marioContext.fallingState.Enter(this);
        }

        public override void FaceLeftTransition()
        {

            if (marioContext.isTouchingRight || marioContext.isTouchingLeft || marioContext.isTouchingTop)
            {
                marioContext.fallingState.Enter(this);
                marioContext.Velocity.X = 0;
            }

            if (marioContext.Velocity.X > 0)
            {
                kinematics.AccelerateUp();
            }
           
            
        }

        public override void FaceRightTransition()
        {
            if (marioContext.isTouchingLeft||marioContext.isTouchingRight || marioContext.isTouchingTop)
            {
                marioContext.fallingState.Enter(this);
                marioContext.Velocity.X = 0;
            }

            if (marioContext.Velocity.X < 0)
            {
                kinematics.AccelerateUp();
            }
        }

        public override void CrouchingDiscontinueTransition()
        {
            //Does nothing
        }

        public override void FaceLeftDiscontinueTransition()
        {
            //kinematics.XDecelerateToRight(marioContext);
            kinematics.AccelerateDownCape(marioContext);
        }

        public override void FaceRightDiscontinueTransition()
        {
            //kinematics.XDecelerateToLeft(marioContext);
            //kinematics.IdleYDecelerate(marioContext);
            kinematics.AccelerateDownCape(marioContext);
        }

        public override void RunningDiscontinueTransition()
        {
            //Does nothing
        }

        public override void JumpingDiscontinueTransition()
        {
            if (marioContext.isTouchingTop)
                marioContext.idleState.Enter(this);
            else
            {
                //if (marioContext.jumpHeight < 8 && !marioContext.isFalling)
                //{
                // JumpingTransition();
                //}
                //else
                //{
                marioContext.fallingState.Enter(this);
                //}
            }
        }

        public override string ToString()
        {
            return ("GlidingState");
        }
    }
}