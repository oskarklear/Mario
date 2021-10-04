using System;
using System.Collections.Generic;
using System.Text;

namespace Mario.States
{
    public abstract class MarioActionState : IMarioActionState
    {
        public abstract IMarioActionState PreviousActionState { get; }
        public MarioContext Mario { get { return } }
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
    }

    public class IdleStateLeft : MarioActionState
    {
        public IdleStateLeft()
        {
        }
        public override void Enter(IMarioActionState previousActionState)
        {

        }
        public override void Exit()
        {
            throw new NotImplementedException();
        }
        public override void StandingTransition()
        {
            //Does nothing. Already in 
        }

        public override void PressUp(MarioContext context)
        {
            context.SetActionState(new JumpingStateLeft());
        }

        public override void PressDown(MarioContext context)
        {
            string powerUpState = context.GetPowerUpState().ToString();
            if (powerUpState != "StandardMario" && powerUpState != "DeadMario")
            {
                context.SetActionState(new CrouchingStateLeft());
            }
        }
        public override void PressRight(MarioContext context)
        {
            context.SetActionState(new IdleStateRight());
        }
        public override void PressLeft(MarioContext context)
        {
            context.SetActionState(new RunningStateLeft());
        }
        public override string ToString()
        {
            return ("IdleStateLeft");
        }
    }

    public class IdleStateRight : MarioActionState
    {
        public IdleStateRight()
        {
        }

        public override void PressUp(MarioContext context)
        {
            context.SetActionState(new JumpingStateRight());
        }

        public override void PressDown(MarioContext context)
        {
            string powerUpState = context.GetPowerUpState().ToString();
            if (powerUpState != "StandardMario" && powerUpState != "DeadMario")
            {
                context.SetActionState(new CrouchingStateRight());
            }
        }
        public override void PressRight(MarioContext context)
        {
            context.SetActionState(new RunningStateRight());
        }
        public override void PressLeft(MarioContext context)
        {
            context.SetActionState(new IdleStateLeft());
        }
        public override string ToString()
        {
            return ("IdleStateRight");
        }
    }

    public class CrouchingStateLeft : MarioActionState
    {
        public CrouchingStateLeft()
        {
        }

        public override void PressUp(MarioContext context)
        {
            context.SetActionState(new IdleStateLeft());
        }

        public override void PressDown(MarioContext context)
        {
            //Does nothing
        }
        public override void PressRight(MarioContext context)
        {
            //Does nothing
        }
        public override void PressLeft(MarioContext context)
        {
            //Does nothing
        }
        public override string ToString()
        {
            return ("CrouchingStateLeft");
        }
    }

    public class CrouchingStateRight : MarioActionState
    {
        public CrouchingStateRight()
        {
        }

        public override void PressUp(MarioContext context)
        {
            context.SetActionState(new IdleStateRight());
        }

        public override void PressDown(MarioContext context)
        {
            //Does nothing
        }
        public override void PressRight(MarioContext context)
        {
            //Does nothing
        }
        public override void PressLeft(MarioContext context)
        {
            //Does nothing
        }
        public override string ToString()
        {
            return ("CrouchingStateRight");
        }
    }

    public class JumpingStateLeft : MarioActionState
    {
        public JumpingStateLeft()
        {
        }

        public override void PressUp(MarioContext context)
        {
            //Does nothing
        }

        public override void PressDown(MarioContext context)
        {
            context.SetActionState(new IdleStateLeft());
        }
        public override void PressRight(MarioContext context)
        {
            //Does nothing
        }
        public override void PressLeft(MarioContext context)
        {
            //Does nothing
        }
        public override string ToString()
        {
            return ("JumpingStateLeft");
        }
    }

    public class JumpingStateRight : MarioActionState
    {
        public JumpingStateRight()
        {
        }

        public override void PressUp(MarioContext context)
        {
            //Does nothing
        }
        public override void PressDown(MarioContext context)
        {
            context.SetActionState(new IdleStateRight());
        }
        public override void PressRight(MarioContext context)
        {
            //Does nothing
        }
        public override void PressLeft(MarioContext context)
        {
            //Does nothing
        }
        public override string ToString()
        {
            return ("JumpingStateRight");
        }
    }
    public class FallingStateLeft : MarioActionState
    {
        public FallingStateLeft()
        {
        }

        public override void PressUp(MarioContext context)
        {
            //Does nothing
        }
        public override void PressDown(MarioContext context)
        {
            //Does nothing
        }
        public override void PressRight(MarioContext context)
        {
            //Does nothing
        }
        public override void PressLeft(MarioContext context)
        {
            //Does nothing
        }
        public override string ToString()
        {
            return ("FallingStateLeft");
        }
    }

    public class FallingStateRight : MarioActionState
    {
        public FallingStateRight()
        {
        }

        public override void PressUp(MarioContext context)
        {
            //Does nothing
        }
        public override void PressDown(MarioContext context)
        {
            //Does nothing
        }
        public override void PressRight(MarioContext context)
        {
            //Does nothing
        }
        public override void PressLeft(MarioContext context)
        {
            //Does nothing
        }
        public override string ToString()
        {
            return ("FallingStateRight");
        }
    }

    public class RunningStateLeft : MarioActionState
    {
        public RunningStateLeft()
        {
        }

        public override void PressUp(MarioContext context)
        {
            context.SetActionState(new JumpingStateLeft());
        }
        public override void PressDown(MarioContext context)
        {
            //Does nothing
        }
        public override void PressRight(MarioContext context)
        {
            context.SetActionState(new IdleStateLeft());
        }
        public override void PressLeft(MarioContext context)
        {
            //Does nothing
        }
        public override string ToString()
        {
            return ("RunningStateLeft");
        }
    }

    public class RunningStateRight : MarioActionState
    {
        public RunningStateRight()
        {
        }

        public override void PressUp(MarioContext context)
        {
            context.SetActionState(new JumpingStateRight());
        }
        public override void PressDown(MarioContext context)
        {
            //Does nothing

        }
        public override void PressRight(MarioContext context)
        {
            //Does nothing
        }
        public override void PressLeft(MarioContext context)
        {
            context.SetActionState(new IdleStateRight());
        }
        public override string ToString()
        {
            return ("RunningStateRight");
        }
    }
}

