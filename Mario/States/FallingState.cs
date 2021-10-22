﻿using System;
using System.Collections.Generic;
using System.Text;
using Mario.Movement;

namespace Mario.States
{
    public class FallingState : MarioActionState
    {
        public FallingState(MarioContext context)
        {
            marioContext = context;
            PowerUpState = context.GetPowerUpState();
            kinematics = new Kinematics();
        }

        public override void Enter(IMarioActionState previousActionState)
        {
            PreviousActionState = previousActionState;
            marioContext.SetActionState(this);
            kinematics.AccelerateDown(marioContext);
        }

        public override void Exit()
        {
            PreviousActionState.Enter(this);
        }
        public override void PressNothing(MarioContext context)
        {
            if (context.isTouchingTop)
                this.StandingTransition();
            else
                this.FallingTransition();
        }

        public override void StandingTransition()
        {
            marioContext.idleState.Enter(this);
        }

        public override void CrouchingTransition()
        {
            //Does nothing
        }

        public override void WalkingTransition()
        {
            //Does nothing - for now
        }

        public override void RunningTransition()
        {
            marioContext.runningState.Enter(this);
        }

        public override void JumpingTransition()
        {
            marioContext.jumpingState.Enter(this);
        }

        public override void FallingTransition()
        {
            kinematics.AccelerateDown(marioContext);
        }

        public override void FaceLeftTransition()
        {
            if (marioContext.facingLeft)
            {
                this.RunningTransition();
            }
            else
                marioContext.facingLeft = true;
            kinematics = new Kinematics();
        }
        public override void FaceRightTransition()
        {
            if (marioContext.facingLeft)
                marioContext.facingLeft = false;
            else
            {
                this.RunningTransition();
            }
        }

        public override void CrouchingDiscontinueTransition()
        {
            //Does nothing
        }

        public override void FaceLeftDiscontinueTransition()
        {
            //kinematics.XDecelerateRight(marioContext);
        }
        public override void FaceRightDiscontinueTransition()
        {
            //kinematics.XDecelerateLeft(marioContext);
        }
        public override void RunningDiscontinueTransition()
        {
            //Does nothing
        }

        public override void JumpingDiscontinueTransition()
        {
            //Does nothing
        }

        public override string ToString()
        {
            return ("FallingState");
        }
    }
}
