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
            kinematics = new Kinematics();
        }

        public override void Enter(IMarioActionState previousActionState)
        {
            PreviousActionState = previousActionState;
            marioContext.SetActionState(this);
        }

        public override void Exit()
        {
            PreviousActionState.Enter(this);
        }
        public override void PressNothing(MarioContext context)
        {
            if (marioContext.isTouchingTop)
            {
                StandingTransition();
            }          
        }

        public override void StandingTransition()
        {
            marioContext.idleState.Enter(this);
        }

        public override void CrouchingTransition()
        {
            //Does nothing - should not be crouching in the air
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
            //Does nothing
        }

        public override void FaceLeftTransition()
        {
            if (!marioContext.facingLeft)
                marioContext.facingLeft = true;
            kinematics.AccelerateLeft(marioContext);
        }
        public override void FaceRightTransition()
        {
            if (marioContext.facingLeft)
                marioContext.facingLeft = false;
            kinematics.AccelerateRight(marioContext);
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
            if (marioContext.isTouchingTop)
                StandingTransition();
        }

        public override string ToString()
        {
            return ("FallingState");
        }
    }
}
