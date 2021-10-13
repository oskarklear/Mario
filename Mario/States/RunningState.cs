using System;
using System.Collections.Generic;
using System.Text;
using Mario.Movement;

namespace Mario.States
{
    public class RunningState : MarioActionState
    {
        public RunningState(MarioContext context)
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
            marioContext.idleState.Enter(this);
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
            //Does nothing, we're already in running state
        }

        public override void JumpingTransition()
        {
            marioContext.jumpingState.Enter(this);
        }

        public override void FallingTransition()
        {
            CrouchingTransition();
        }

        public override void FaceLeftTransition()
        {
            if (marioContext.isTouchingRight)
            {
                Exit();
            }
            else
            {
                if (!marioContext.facingLeft)
                    marioContext.idleState.Enter(this);
                else
                    kinematics.AccelerateLeft(marioContext);
                kinematics.IdleYDecelerate(marioContext);
            }        
        }
        public override void FaceRightTransition()
        {
            if (marioContext.isTouchingLeft)
            {
                Exit();
            }
            else
            {
                if (marioContext.facingLeft)
                    marioContext.idleState.Enter(this);
                else
                    kinematics.AccelerateRight(marioContext);
                //kinematics.IdleYDecelerate(marioContext);
            }
        }

        public override void CrouchingDiscontinueTransition()
        {
            //Does nothing
        }

        public override void FaceLeftDiscontinueTransition()
        {
            kinematics.XDecelerateToRight(marioContext);
        }

        public override void FaceRightDiscontinueTransition()
        {
            kinematics.XDecelerateToLeft(marioContext);
        }

        public override void RunningDiscontinueTransition()
        {
            Exit();
        }

        public override void JumpingDiscontinueTransition()
        {
            //Does nothing
        }
        public override string ToString()
        {
            return ("RunningState");
        }
    }
}
