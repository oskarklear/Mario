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
                System.Diagnostics.Debug.WriteLine("Crouch");
                kinematics.AccelerateDown(marioContext);
                kinematics.IdleXDecelerate(marioContext);

            }
        }
        public override void WalkingTransition()
        {
            //Does nothing - for now
        }

        public override void RunningTransition()
        {
            System.Diagnostics.Debug.WriteLine("RunningTransition");
            marioContext.runningState.Enter(this);
        }

        public override void JumpingTransition()
        {
            System.Diagnostics.Debug.WriteLine("JumpingTransition");
            marioContext.jumpingState.Enter(this);
        }
        public override void FallingTransition()
        {
            System.Diagnostics.Debug.WriteLine("Falling");
            this.CrouchingTransition();
        }

        public override void FaceLeftTransition()
        {
            System.Diagnostics.Debug.WriteLine("FaceLeft");
            if (marioContext.facingLeft)
            {
                System.Diagnostics.Debug.WriteLine("Facingleft, go to running");
                this.RunningTransition();
            }
            else
                marioContext.facingLeft = true;
        }
        public override void FaceRightTransition()
        {
            System.Diagnostics.Debug.WriteLine("FaceRight");
            if (marioContext.facingLeft)
                marioContext.facingLeft = false;
            else
            {
                System.Diagnostics.Debug.WriteLine("Facing right, go to running");
                this.RunningTransition();
            }
        }

        public override void CrouchingDiscontinueTransition()
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
