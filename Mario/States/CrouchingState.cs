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
            kinematics = new Kinematics(context);
            marioContext = context;
            PowerUpState = context.GetPowerUpState();
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
            //Does nothing
        }
        public override void RunningTransition()
        {
            //Does nothing
        }
        public override void JumpingTransition()
        {
            //Does nothing
        }

        public override void FallingTransition()
        {
            kinematics.IdleXDecelerate();
        }

        public override void FaceLeftTransition()
        {
            if (!marioContext.facingLeft)
                marioContext.facingLeft = true;
        }

        public override void FaceRightTransition()
        {
            if (marioContext.facingLeft)
                marioContext.facingLeft = false;
        }

        public override void CrouchingDiscontinueTransition()
        {
            kinematics.YDecelerateToUp();
            if (marioContext.Velocity.X > 0)
                Exit();
            else
                StandingTransition();
        }

        public override void FaceLeftDiscontinueTransition()
        {
            kinematics.XDecelerateToRight();
        }

        public override void FaceRightDiscontinueTransition()
        {
            kinematics.XDecelerateToLeft();
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
