using System;
using System.Collections.Generic;
using System.Text;
using Mario.States;
using Mario.Movement;

namespace Mario.States
{
    public abstract class MarioActionState : IMarioActionState
    {
        public MarioContext marioContext;
        public MarioPowerupState PowerUpState;
        public IMarioActionState PreviousActionState;
        public IMarioActionState CurrentActionState;
        public Kinematics kinematics;

        public override abstract string ToString();
        public abstract void Enter(IMarioActionState previousActionState);
        public abstract void Exit();
        public abstract void PressNothing(MarioContext context);
        public abstract void StandingTransition();
        public abstract void CrouchingTransition();
        public abstract void WalkingTransition();
        public abstract void RunningTransition();
        public abstract void JumpingTransition();
        public abstract void FallingTransition();
        public abstract void FaceLeftTransition();
        public abstract void FaceRightTransition();
        public abstract void CrouchingDiscontinueTransition();
        //public abstract void WalkingDiscontinueTransition();
        public abstract void RunningDiscontinueTransition();
        public abstract void JumpingDiscontinueTransition();
    }

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
            kinematics.IdleXDecelerate(marioContext);
            kinematics.AccelerateUp(marioContext);
            marioContext.jumpingState.Enter(this);
        }
        public override void PressNothing(MarioContext context)
        {
            //Does nothing, since we're already pressing nothing
            //kinematics.IdleXDecelerate(context);
            //kinematics.IdleYDecelerate(context);
        }
        public override void StandingTransition()
        {
            //Does nothing
        }
        public override void CrouchingTransition()
        {
            if (marioContext.GetPowerUpState().ToString() != "DeadMario")
            {
                if (marioContext.GetPowerUpState().ToString() != "StandardMario")
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

        /*public override void PressRight(MarioContext context)
        {
            //marioContext.SetActionState(new WalkingStateLeft());
            //Does nothing
            kinematics.AccelerateRight(context);
            context.SetActionState(new IdleStateRight());
        }*/
        public override void RunningTransition()
        {
            System.Diagnostics.Debug.WriteLine("RunningTransition");
            marioContext.runningState.Enter(this);
        }

        /*public override void PressLeft(MarioContext context)
        {
            System.Diagnostics.Debug.WriteLine("RunningTransition");
            marioContext.runningState.Enter(this);
            System.Diagnostics.Debug.WriteLine(marioContext.GetActionState().ToString());
            kinematics.AccelerateLeft(context);
        }*/
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
        }
        public override void Exit()
        {
            PreviousActionState.Enter(this);
        }
        public override void PressNothing(MarioContext context)
        {
            marioContext.idleState.Enter(this);
            kinematics.AccelerateDown(context);

        }
        public override void StandingTransition()
        {
            marioContext.idleState.Enter(this);

        }
        public override void CrouchingTransition()
        {
            //kinematics.AccelerateDown(context);
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
            marioContext.fallingState.Enter(this);
        }

        public override void FaceLeftTransition()
        {
            System.Diagnostics.Debug.WriteLine("FaceLeft");
            if (!marioContext.facingLeft)
                marioContext.facingLeft = true;
        }
        public override void FaceRightTransition()
        {
            System.Diagnostics.Debug.WriteLine("FaceRight");
            if (marioContext.facingLeft)
                marioContext.facingLeft = false;
            
        }

        public override void CrouchingDiscontinueTransition()
        {
            Exit();
            kinematics.AccelerateDown(context);
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
        }

        public override void Exit()
        {
            PreviousActionState.Enter(this);
            //kinematics.AccelerateUp(context);
        }
        public override void PressNothing(MarioContext context)
        {
            marioContext.idleState.Enter(this);
        }
        public override void StandingTransition()
        {
            marioContext.idleState.Enter(this);
            //context.jumped = true;
            //kinematics.AccelerateDown(context);
            //context.SetActionState(new IdleStateLeft());
        }
        public override void CrouchingTransition()
        {
            //Does nothing - for now
        }

        /*public override void PressRight(MarioContext context)
        {
            context.SetActionState(new JumpingStateRight());
        }*/

        public override void WalkingTransition()
        {
            //Does nothing - for now
        }

        /*public override void PressLeft(MarioContext context)
        {
            kinematics.AccelerateLeft(context);
        }*/
        public override void RunningTransition()
        {
            //Does nothing - at least, for now we imply that you cannot get into running from jumping
        }
        public override void JumpingTransition()
        {
            //Does nothing
        }

        public override void FallingTransition()
        {
            //PreviousActionState.Enter(this)
            marioContext.idleState.Enter(this);
    public class JumpingStateRight : MarioActionState
    {
        Kinematics kinematics;

        public JumpingStateRight()
        {
            kinematics = new Kinematics();
        }
        public override void FaceLeftTransition()
        {
            System.Diagnostics.Debug.WriteLine("FaceLeft");
            if (!marioContext.facingLeft)
                marioContext.facingLeft = true;
            kinematics.AccelerateUp(context);
        }
        public override void FaceRightTransition()

        public override void PressDown(MarioContext context)
        {
            System.Diagnostics.Debug.WriteLine("FaceRight");
            if (marioContext.facingLeft)
                marioContext.facingLeft = false;
            //context.jumped = true;
            kinematics.AccelerateDown(context);
            context.SetActionState(new IdleStateRight());
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
            Exit();
        }
        public override string ToString()
        {
            return ("JumpingState");
        }
    }
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
            // do nothing
        }

        /*public override void PressDown(MarioContext context)
        {
            PreviousActionState.Enter(this);

        }*/

        public override void StandingTransition()
        {
            marioContext.idleState.Enter(this);
        }

        /*public override void PressRight(MarioContext context)
        {
            marioContext.idleState.Enter(this);
        }*/

        public override void CrouchingTransition()
        {
            //Does nothing
        }

        public override void WalkingTransition()
        {
            //Does nothing - for now
        }

        /*public override void PressLeft(MarioContext context)
        {
            int marioTopLeftSpeed = -3;
            if (context.Velocity.X > marioTopLeftSpeed)
            {
                context.Velocity.X -= (float)0.15;
            }
        }*/

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
            System.Diagnostics.Debug.WriteLine("FaceLeft");
            if (marioContext.facingLeft)
            {
                System.Diagnostics.Debug.WriteLine("Facingleft, go to running");
                this.RunningTransition();
            }
            else
                marioContext.facingLeft = true;
            kinematics = new Kinematics();
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
            return ("FallingState");
        }
    }

    public class RunningState : MarioActionState
    {
        public RunningState(MarioContext context)
        {
            marioContext = context;
            PowerUpState = context.GetPowerUpState();
            kinematics = new Kinematics();
        }

        public override void StandingTransition()
        {
            PreviousActionState = previousActionState;
            marioContext.SetActionState(this);
            kinematics.AccelerateRight(marioContext);
        }

        public override void Exit()
        {
            PreviousActionState.Enter(this);
        }
        public override void PressNothing(MarioContext context)
        {
            kinematics.IdleYDecelerate(context);
            //context.SetActionState(new IdleStateLeft());

        }
        public override void StandingTransition()
        {
            marioContext.idleState.Enter(this);
            //kinematics.AccelerateUp(context);
        }

        public override string ToString()
        {
            //Does nothing - for now
        }

        public override void WalkingTransition()
        {
            marioContext.fallingState.Enter(this);
        }

        public override void RunningTransition()
        {
            //Does nothing, we're already in running state
        }

        /*public override void PressLeft(MarioContext context)
        {

            kinematics.AccelerateLeft(context);
        }*/

        public override void JumpingTransition()
        {
            marioContext.jumpingState.Enter(this);
        }

        public override void FallingTransition()
        {
            marioContext.fallingState.Enter(this);
        }

        public override void RunningDiscontinueTransition()
        {
            marioContext.idleState.Enter(this);
        }

        public override void FaceRightTransition()
        {
            if (marioContext.facingLeft)
                marioContext.idleState.Enter(this);
        }

        public override void CrouchingDiscontinueTransition()
        {
            //Does nothing
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
