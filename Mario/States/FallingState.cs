using System;
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
            kinematics = new Kinematics(context);
        }

        public override void Enter(IMarioActionState previousActionState)
        {
            if (previousActionState is JumpingState)
            {
                marioContext.jumped = true;
            }
            PreviousActionState = previousActionState;
            marioContext.SetActionState(this);
            marioContext.isFalling = true;
            kinematics.AccelerateDown();
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
            marioContext.crouchingState.Enter(this);
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
            if (marioContext.isTouchingTop)
                StandingTransition();

            if (marioContext.isBallooned)
            {
                marioContext.jumpingState.Enter(this); 
            }
        }

        public override void FallingTransition()
        {
            if (marioContext.isTouchingTop)
                CrouchingTransition();
        }

        public override void FaceLeftTransition()
        {
            if (marioContext.isTouchingTop)
                RunningTransition();

            if (!marioContext.facingLeft)
                marioContext.facingLeft = true;
            kinematics.AccelerateLeft();
        }

        public override void FaceRightTransition()
        {
            if (marioContext.isTouchingTop)
                RunningTransition();
            if (marioContext.facingLeft)
                marioContext.facingLeft = false;
            kinematics.AccelerateRight();
        }

        public override void CrouchingDiscontinueTransition()
        {
            if (marioContext.isTouchingTop)
                StandingTransition();
        }

        public override void FaceLeftDiscontinueTransition()
        {
            kinematics.XDecelerateToRight();
            if (marioContext.isTouchingTop)
                StandingTransition();
        }
        public override void FaceRightDiscontinueTransition()
        {
            kinematics.XDecelerateToLeft();
            if (marioContext.isTouchingTop)
                StandingTransition();
        }

        public override void RunningDiscontinueTransition()
        {
            //Does nothing
        }

        public override void JumpingDiscontinueTransition()
        {
            if (marioContext.isTouchingTop)
                StandingTransition();
        }

        public override string ToString()
        {
            return ("FallingState");
        }
    }
}
