using System;
using System.Collections.Generic;
using System.Text;
using Mario.Movement;

namespace Mario.States
{
    public class CrouchingState : MarioActionState
    {
        public CrouchingState(MarioContext context)
        {
            kinematics = new Kinematics();
            marioContext = context;
            PowerUpState = context.GetPowerUpState();
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
            marioContext.idleState.Enter(this);
        }
        public override void StandingTransition()
        {
            marioContext.idleState.Enter(this);

        }
        public override void CrouchingTransition()
        {          
        }

        public override void WalkingTransition()
        {
            //Does nothing
        }
        public override void RunningTransition()
        {
            //Does nothing
        }
        public override void JumpingTransition()
        {
            this.StandingTransition();
        }
        public override void FallingTransition()
        {
            kinematics.AccelerateDown(marioContext);
        }

        public override void FaceLeftTransition()
        {
            if (!marioContext.facingLeft)
                marioContext.facingLeft = true;
            else
                kinematics.AccelerateLeft(marioContext);
            marioContext.runningState.Enter(this);
        }
        public override void FaceRightTransition()
        {
            if (marioContext.facingLeft)
                marioContext.facingLeft = false;
            else
                kinematics.AccelerateRight(marioContext);
            marioContext.runningState.Enter(this);

        }

        public override void CrouchingDiscontinueTransition()
        {
            Exit();
            kinematics.AccelerateDown(marioContext);
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
            return ("CrouchingState");
        }
    }
}
