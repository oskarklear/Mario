using System;
using System.Collections.Generic;
using System.Text;
using Mario.Movement;

namespace Mario.States
{
    public class JumpingState : MarioActionState
    {
        public JumpingState(MarioContext context)
        {
            marioContext = context;
            PowerUpState = context.GetPowerUpState();
            kinematics = new Kinematics();
        }

        public override void Enter(IMarioActionState previousActionState)
        {
            PreviousActionState = previousActionState;
            marioContext.SetActionState(this);
            kinematics.AccelerateUp(marioContext);
            marioContext.isFalling = false;
            marioContext.isTouchingTop = false;
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
            System.Diagnostics.Debug.WriteLine("HEIGHT: " + marioContext.jumpHeight);
            if (marioContext.jumpHeight > 10 || marioContext.isTouchingBottom)
            {
                System.Diagnostics.Debug.WriteLine("WHY");
                FallingTransition();
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Accelerateup");
                kinematics.AccelerateUp(marioContext); 
            }
        }

        public override void FallingTransition()
        {
            System.Diagnostics.Debug.WriteLine("FallingTransition");
            marioContext.fallingState.Enter(this);
        }    

        public override void FaceLeftTransition()
        {
            if (!marioContext.facingLeft)
                marioContext.facingLeft = true;
            else
                kinematics.AccelerateLeft(marioContext);
        }

        public override void FaceRightTransition()
        {
            if (marioContext.facingLeft)
                marioContext.facingLeft = false;
            else
                kinematics.AccelerateRight(marioContext);
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
            //Does nothing
        }

        public override void JumpingDiscontinueTransition()
        {
            if (marioContext.isTouchingTop)
                marioContext.idleState.Enter(this);
            else
            {
                if (marioContext.jumpHeight < 8 && !marioContext.isFalling)
                {
                    System.Diagnostics.Debug.WriteLine("TEMP");
                    JumpingTransition();
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("HELLO: " + marioContext.isFalling);
                    FallingTransition();
                }
            }
        }
        public override string ToString()
        {
            return ("JumpingState");
        }
    }
}
