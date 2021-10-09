using System;
using System.Collections.Generic;
using System.Text;
using Mario.States;
using Mario.Movement;

namespace Mario.States
{
    public abstract class MarioActionState : IMarioActionState
    {
        public abstract void PressUp(MarioContext context);

        public abstract void PressDown(MarioContext context);

        public abstract void PressRight(MarioContext context);

        public abstract void PressLeft(MarioContext context);

        public abstract void PressNothing(MarioContext context);

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
        Kinematics kinematics;

        public IdleStateLeft()
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
            throw new NotImplementedException();
            kinematics.IdleXDecelerate(context);
            kinematics.AccelerateUp(context);
            context.SetActionState(new JumpingStateLeft());
        }

        public override void PressDown(MarioContext context)
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
            string powerUpState = context.GetPowerUpState().ToString();
            kinematics.AccelerateDown(context);
            kinematics.IdleXDecelerate(context);
            if (powerUpState != "DeadMario")
            {
                //context.jumped = false;
                context.SetActionState(new CrouchingStateLeft());
            }
        }
        public override void WalkingTransition()

        public override void PressRight(MarioContext context)
        {
            //marioContext.SetActionState(new WalkingStateLeft());
            //Does nothing
            kinematics.AccelerateRight(context);
            context.SetActionState(new IdleStateRight());
        }
        public override void RunningTransition()

        public override void PressLeft(MarioContext context)
        {
            System.Diagnostics.Debug.WriteLine("RunningTransition");
            marioContext.runningState.Enter(this);
            System.Diagnostics.Debug.WriteLine(marioContext.GetActionState().ToString());
            kinematics.AccelerateLeft(context);
            context.SetActionState(new RunningStateLeft());
        }
        public override void JumpingTransition()

        public override void PressNothing(MarioContext context)
        {
            kinematics.IdleXDecelerate(context);
            kinematics.IdleYDecelerate(context);
        }

        public override string ToString()
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
    }

    public class IdleStateRight : MarioActionState
    {
        Kinematics kinematics;

        public IdleStateRight()
        {
            kinematics = new Kinematics();
        }

        public override void FaceRightTransition()
        {
            System.Diagnostics.Debug.WriteLine("FaceRight");
            if (marioContext.facingLeft)
                marioContext.facingLeft = false;
            else
                this.RunningTransition();
            kinematics.AccelerateUp(context);
            kinematics.IdleXDecelerate(context);
            //context.jumped = true;
            context.SetActionState(new JumpingStateRight());
        }
        public override void CrouchingDiscontinueTransition()
        {
            //Does nothing

        public override void PressDown(MarioContext context)
        {
            string powerUpState = context.GetPowerUpState().ToString();
            kinematics.AccelerateDown(context);
            kinematics.IdleXDecelerate(context);
            if (powerUpState != "DeadMario")
            {
                //context.jumped = false;
                context.SetActionState(new CrouchingStateRight());
            }
        }
        public override void RunningDiscontinueTransition()

        public override void PressRight(MarioContext context)
        {
            //Does nothing
            kinematics.AccelerateRight(context);
            context.SetActionState(new RunningStateRight());
        }
        public override void JumpingDiscontinueTransition()

        public override void PressLeft(MarioContext context)
        {
            //Does nothing
            kinematics.AccelerateLeft(context);
            context.SetActionState(new IdleStateLeft());
        }

        public override void PressNothing(MarioContext context)
        {
            kinematics.IdleXDecelerate(context);
            kinematics.IdleYDecelerate(context);
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
    public class CrouchingStateLeft : MarioActionState
    {
        Kinematics kinematics;

        public CrouchingStateLeft()
        {
            PreviousActionState.Enter(this);
            kinematics = new Kinematics();
        }

        public override void StandingTransition()
        {
            marioContext.idleState.Enter(this);
            
        }

        public override void CrouchingTransition()
        {
            kinematics.AccelerateDown(context);
        }

        public override void WalkingTransition()

        public override void PressRight(MarioContext context)
        {
            kinematics.AccelerateDown(context);
        }

        public override void RunningTransition()

        public override void PressLeft(MarioContext context)
        {
            kinematics.AccelerateDown(context);
        }

        public override void JumpingTransition()

        public override void PressNothing(MarioContext context)
        {
            context.SetActionState(new IdleStateLeft());
            kinematics.AccelerateDown(context);

        }

        public override string ToString()
        {
            this.StandingTransition();
        }

        public override void FallingTransition()
        {
            marioContext.fallingState.Enter(this);
        }

        public override void FaceLeftTransition()
    public class CrouchingStateRight : MarioActionState
    {
        Kinematics kinematics;

        public CrouchingStateRight()
        {
            if (!marioContext.facingLeft)
                marioContext.facingLeft = true;
            kinematics = new Kinematics();
        }

        public override void FaceRightTransition()
        {
            if (marioContext.facingLeft)
                marioContext.facingLeft = false;
            
        }

        public override void CrouchingDiscontinueTransition()
        {
            Exit();
            kinematics.AccelerateDown(context);
        }

        public override void RunningDiscontinueTransition()

        public override void PressRight(MarioContext context)
        {
            kinematics.AccelerateDown(context);
        }

        public override void JumpingDiscontinueTransition()

        public override void PressLeft(MarioContext context)
        {
            kinematics.AccelerateDown(context);
        }

        public override void PressNothing(MarioContext context)
        {
            kinematics.AccelerateDown(context);
            context.SetActionState(new IdleStateRight());

        }

        public override string ToString()
        {
            return ("CrouchingState");
        }
    }

    public class JumpingState : MarioActionState
    {
        public JumpingState(MarioContext context)
        Kinematics kinematics;

        public JumpingStateLeft()
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
            kinematics.AccelerateUp(context);
        }

        public override void StandingTransition()
        {
            marioContext.idleState.Enter(this);
            //context.jumped = true;
            kinematics.AccelerateDown(context);
            context.SetActionState(new IdleStateLeft());
        }

        public override void CrouchingTransition()

        public override void PressRight(MarioContext context)
        {
            context.SetActionState(new JumpingStateRight());
        }

        public override void WalkingTransition()

        public override void PressLeft(MarioContext context)
        {
            kinematics.AccelerateLeft(context);
        }

        public override void PressNothing(MarioContext context)
        {
            context.SetActionState(new IdleStateLeft());
        }

        public override void RunningTransition()

        public override string ToString()
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
    public class JumpingStateRight : MarioActionState
    {
        Kinematics kinematics;

        public JumpingStateRight()
        {
            kinematics = new Kinematics();
        }

        public override void FaceLeftTransition()
        {
            if (!marioContext.facingLeft)
                marioContext.facingLeft = true;
            kinematics.AccelerateUp(context);
        }

        public override void FaceRightTransition()

        public override void PressDown(MarioContext context)
        {
            if (marioContext.facingLeft)
                marioContext.facingLeft = false;
            //context.jumped = true;
            kinematics.AccelerateDown(context);
            context.SetActionState(new IdleStateRight());
        }

        public override void CrouchingDiscontinueTransition()

        public override void PressRight(MarioContext context)
        {
            kinematics.AccelerateRight(context);
        }

        public override void RunningDiscontinueTransition()

        public override void PressLeft(MarioContext context)
        {
            kinematics.AccelerateLeft(context);
            context.SetActionState(new JumpingStateLeft());
        }

        public override void PressNothing(MarioContext context)
        {
            context.SetActionState(new IdleStateRight());
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

    public class FallingStateLeft : MarioActionState
    {
        public FallingState(MarioContext context)
        Kinematics kinematics;

        public FallingStateLeft()
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

        public override void PressDown(MarioContext context)
        {
            PreviousActionState.Enter(this);
            
        }

        public override void StandingTransition()

        public override void PressRight(MarioContext context)
        {
            marioContext.idleState.Enter(this);
        }

        public override void CrouchingTransition()
        {
            //Does nothing
            context.SetActionState(new FallingStateRight());
        }

        public override void WalkingTransition()

        public override void PressLeft(MarioContext context)
        {
            int marioTopLeftSpeed = -3;
            if (context.Velocity.X > marioTopLeftSpeed)
            {
                context.Velocity.X -= (float)0.15;
            }
        }

        public override void PressNothing(MarioContext context)
        {
            // do nothing
        }

        public override void RunningTransition()

        public override string ToString()
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
    public class FallingStateRight : MarioActionState
    {
        Kinematics kinematics;

        public FallingStateRight()
        {
            if (!marioContext.facingLeft)
                marioContext.facingLeft = true;
            kinematics = new Kinematics();
        }

        public override void FaceRightTransition()
        {
            if (marioContext.facingLeft)
                marioContext.facingLeft = false;
        }

        public override void CrouchingDiscontinueTransition()

        public override void PressDown(MarioContext context)
        {
            //Does nothing
        }

        public override void RunningDiscontinueTransition()

        public override void PressRight(MarioContext context)
        {
            //Does nothing
        }

        public override void JumpingDiscontinueTransition()

        public override void PressLeft(MarioContext context)
        {
            int marioTopLeftSpeed = -3;
            if (context.Velocity.X > marioTopLeftSpeed)
            {
                context.Velocity.X -= (float)0.15;
            }
        }
        public override void PressNothing(MarioContext context)
        {
            // do nothing
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
    public class RunningStateLeft : MarioActionState
    {
        Kinematics kinematics;

        public RunningStateLeft()
        {
            PreviousActionState.Enter(this);
            kinematics = new Kinematics();
        }

        public override void StandingTransition()
        {
            marioContext.idleState.Enter(this);
            kinematics.AccelerateUp(context);
            context.SetActionState(new JumpingStateLeft());
        }

        public override void CrouchingTransition()

        public override void PressDown(MarioContext context)
        {
            //Does nothing???
            context.SetActionState(new CrouchingStateLeft());
        }

        public override void WalkingTransition()

        public override void PressRight(MarioContext context)
        {
            //Does nothing - for now
            
            context.SetActionState(new IdleStateLeft());
        }

        public override void RunningTransition()

        public override void PressLeft(MarioContext context)
        {
            
            kinematics.AccelerateLeft(context);
        }

        public override void PressNothing(MarioContext context)
        {
            kinematics.IdleYDecelerate(context);
            context.SetActionState(new IdleStateLeft());

        }

        public override void JumpingTransition()

        public override string ToString()
        {
            marioContext.jumpingState.Enter(this);
        }

        public override void FallingTransition()
        {
            marioContext.fallingState.Enter(this);
        }

        public override void FaceLeftTransition()
    public class RunningStateRight : MarioActionState
    {
        Kinematics kinematics;

        public RunningStateRight()
        {
            if (!marioContext.facingLeft)
                marioContext.idleState.Enter(this);
            kinematics = new Kinematics();
        }

        public override void FaceRightTransition()
        {
            if (marioContext.facingLeft)
                marioContext.idleState.Enter(this);
            kinematics.AccelerateUp(context);
            context.SetActionState(new JumpingStateRight());
        }

        public override void CrouchingDiscontinueTransition()

        public override void PressDown(MarioContext context)
        {
            //Does nothing
        }
            context.SetActionState(new CrouchingStateRight());

        public override void RunningDiscontinueTransition()
        {
            marioContext.idleState.Enter(this);
        }

        public override void JumpingDiscontinueTransition()

        public override void PressRight(MarioContext context)
        {
            //context.SetActionState(new IdleStateRight());
            
            kinematics.AccelerateRight(context);
        }

        public override void PressLeft(MarioContext context)
        {
            kinematics.AccelerateLeft(context);
            //context.SetActionState(new IdleStateRight());
        }

        public override void PressNothing(MarioContext context)
        {
            kinematics.IdleYDecelerate(context);
            context.SetActionState(new IdleStateRight());

        }

        public override string ToString()
        {
            return ("RunningState");
        }
    }
}


