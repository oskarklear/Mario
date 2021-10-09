using System;
using System.Collections.Generic;
using System.Text;
using Mario.States;

namespace Mario.States
{
    public abstract class MarioActionState : IMarioActionState
    {
        public MarioContext marioContext;
        public MarioPowerupState PowerUpState;
        public IMarioActionState PreviousActionState;
        public IMarioActionState CurrentActionState;

        public override abstract string ToString();
        public abstract void Enter(IMarioActionState previousActionState);
        public abstract void Exit();
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
        }
        public override void Enter(IMarioActionState previousActionState)
        {
            PreviousActionState = previousActionState;
            marioContext.SetActionState(this);
            
        }
        public override void Exit()
        {
            throw new NotImplementedException();
        }
        public override void StandingTransition()
        {
            //Does nothing. Already in 
        }
        public override void CrouchingTransition()
        {
            if ((marioContext.GetPowerUpState().ToString() != "StandardMario") && (marioContext.GetPowerUpState().ToString() != "DeadMario"))
            {
                System.Diagnostics.Debug.WriteLine("Crouch");
                marioContext.crouchingState.Enter(this);
                //Enter crouching transition? crouching state not instantiated yet tho?
            }
        }
        public override void WalkingTransition()
        {
            //marioContext.SetActionState(new WalkingStateLeft());
            //Does nothing
        }
        public override void RunningTransition()
        {
            System.Diagnostics.Debug.WriteLine("RunningTransition");
            marioContext.runningState.Enter(this);
            System.Diagnostics.Debug.WriteLine(marioContext.GetActionState().ToString());
        }
        public override void JumpingTransition()
        {
            marioContext.jumpingState.Enter(this);
        }
        public override void FallingTransition()
        {
            System.Diagnostics.Debug.WriteLine(PowerUpState.ToString());
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
                this.RunningTransition();
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
            this.StandingTransition();
        }

        public override void FallingTransition()
        {
            marioContext.fallingState.Enter(this);
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
            Exit();
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
            marioContext.runningState.Enter(this);
        }

        public override void JumpingTransition()
        {
            //Does nothing
        }

        public override void FallingTransition()
        {
            //PreviousActionState.Enter(this)
            marioContext.idleState.Enter(this);
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
        }

        public override void FaceRightTransition()
        {
            if (marioContext.facingLeft)
                marioContext.facingLeft = false;
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
            //Does nothing???
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
            marioContext.jumpingState.Enter(this);
        }

        public override void FallingTransition()
        {
            marioContext.fallingState.Enter(this);
        }

        public override void FaceLeftTransition()
        {
            if (!marioContext.facingLeft)
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
            marioContext.idleState.Enter(this);
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


