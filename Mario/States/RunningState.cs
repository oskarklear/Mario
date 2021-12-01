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
            kinematics = new Kinematics(context);
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
            if (marioContext.isTouchingTop)
                marioContext.crouchingState.Enter(this);
        }

        public override void WalkingTransition()
        {
            //Does nothing - for now
        }

        public override void RunningTransition()
        {
            //Does nothing
        }

        public override void JumpingTransition()
        {
            //System.Diagnostics.Debug.WriteLine("jumping");
            if (!marioContext.jumped&&Math.Abs(marioContext.Velocity.X)>6&&marioContext.GetPowerUpState() is CapeMarioState)
                marioContext.glidingState.Enter(this);
            else if (!marioContext.jumped)
            {
                marioContext.jumpingState.Enter(this);
            }
        }

        public override void FallingTransition()
        {
            if (marioContext.isTouchingTop)
                CrouchingTransition();
            else
                marioContext.fallingState.Enter(this);
        }

        public override void FaceLeftTransition()
        {
            if (marioContext.isTouchingRight)
            {
                marioContext.idleState.Enter(this);
                marioContext.Velocity.X = 0;
            }
            else
            {
                if (!marioContext.facingLeft)
                    marioContext.idleState.Enter(this);
                else
                    kinematics.AccelerateLeft();
            }

            if (!marioContext.isTouchingTop)
                FallingTransition();
        }

        public override void FaceRightTransition()
        {
            if (marioContext.isTouchingLeft)
            {
                marioContext.idleState.Enter(this);
            }
            else
            {
                if (marioContext.facingLeft)
                    marioContext.idleState.Enter(this);
                else
                    kinematics.AccelerateRight();
            }

            if (!marioContext.isTouchingTop)
                FallingTransition();
        }

        public override void CrouchingDiscontinueTransition()
        {
            //Does nothing
        }

        public override void FaceLeftDiscontinueTransition()
        {
            kinematics.XDecelerateToRight();

            if (marioContext.Velocity.X > -0.16 && marioContext.Velocity.X < 0)
            {
                StandingTransition();
            }
        }

        public override void FaceRightDiscontinueTransition()
        {
            kinematics.XDecelerateToLeft();

            if (marioContext.Velocity.X < 0.16 && marioContext.Velocity.X > 0)
            {
                StandingTransition();
            }
        }

        public override void RunningDiscontinueTransition()
        {
            Exit();
        }

        public override void JumpingDiscontinueTransition()
        {
            marioContext.jumped = false;
        }

        public override string ToString()
        {
            return ("RunningState");
        }
    }
}
