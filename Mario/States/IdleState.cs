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
            System.Diagnostics.Debug.WriteLine(PowerUpState.ToString());
            kinematics = new Kinematics();
        }

        public override void Enter(IMarioActionState previousActionState)
        {
            PreviousActionState = previousActionState;
            marioContext.SetActionState(this);
        }

        public override void Exit()
        {
        }

        public override void PressNothing(MarioContext context)
        {
            //Does nothing, since we're already pressing nothing
            kinematics.IdleXDecelerate(context);
            if (!marioContext.isTouchingTop)
            {
                marioContext.fallingState.Enter(this);
            }
            else
                kinematics.IdleYDecelerate(context);
        }

        public override void StandingTransition()
        {
            //Does nothing
        }

        public override void CrouchingTransition()
        {
            if (marioContext.GetPowerUpState().ToString() != "DeadMario")
            {
                //if (marioContext.GetPowerUpState().ToString() != "StandardMario")

                marioContext.crouchingState.Enter(this);
                kinematics.IdleXDecelerate(marioContext);

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
            //Does nothing
        }

        public override void FaceRightDiscontinueTransition()
        {
            //Does nothing
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
            return ("IdleState");
        }


    }
}
