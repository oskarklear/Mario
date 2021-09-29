using System;
using System.Collections.Generic;
using System.Text;

namespace Mario.States
{
    public abstract class MarioActionState
    {

        public abstract void PressUp(MarioContext context);

        public abstract void PressDown(MarioContext context);

        public abstract void PressRight(MarioContext context);

        public abstract void PressLeft(MarioContext context);
        public override abstract string ToString();
    }

    public class IdleStateLeft : MarioActionState
    {
        public IdleStateLeft()
        {
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

