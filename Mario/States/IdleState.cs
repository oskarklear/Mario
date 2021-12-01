using System;
using System.Collections.Generic;
using System.Text;
using Mario.Movement;

namespace Mario.States
{
    public class IdleState : MarioActionState
    {
        public IdleState(MarioContext context)
        {
            marioContext = context;
            PowerUpState = context.GetPowerUpState();
            kinematics = new Kinematics(context);
        }

        public override void Enter(IMarioActionState previousActionState)
        {
            PreviousActionState = previousActionState;
            marioContext.SetActionState(this);
            marioContext.firstJump = true;
            if (marioContext.Velocity.Y == 0)
                marioContext.jumpHeight = 0;
        }

        public override void Exit()
        {

        }

        public override void StandingTransition()
        {
            //Does nothing
        }

        public override void CrouchingTransition()
        {
            if (marioContext.GetPowerUpState().ToString() != "DeadMario")
            {
                marioContext.crouchingState.Enter(this);
                kinematics.IdleXDecelerate();

            }
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
            if (!marioContext.jumped)
                marioContext.jumpingState.Enter(this);
        }

        public override void FallingTransition()
        {
            this.CrouchingTransition();
        }

        public override void FaceLeftTransition()
        {
            if (!marioContext.isTouchingRight)
            {
                if (marioContext.facingLeft)
                {
                    this.RunningTransition();
                }
                else
                    marioContext.facingLeft = true;
            }
        }

        public override void FaceRightTransition()
        {
            if (!marioContext.isTouchingLeft)
            {
                if (marioContext.facingLeft)
                    marioContext.facingLeft = false;
                else
                {
                    this.RunningTransition();
                }
            }          
        }

        public override void CrouchingDiscontinueTransition()
        {
            //Does nothing
        }

        public override void FaceLeftDiscontinueTransition()
        {
            kinematics.IdleXDecelerate();
        }

        public override void FaceRightDiscontinueTransition()
        {
            kinematics.IdleXDecelerate();
        }

        public override void RunningDiscontinueTransition()
        {
            //Does nothing
        }

        public override void JumpingDiscontinueTransition()
        {
            if (!marioContext.isTouchingTop)
                marioContext.fallingState.Enter(this);
            marioContext.jumped = false;
        }

        public override string ToString()
        {
            return ("IdleState");
        }


    }
}
