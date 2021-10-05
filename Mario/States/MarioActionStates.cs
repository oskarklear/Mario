using System;
using System.Collections.Generic;
using System.Text;
using Mario.Movement;

namespace Mario.States
{
    public abstract class MarioActionState
    {

        public abstract void PressUp(MarioContext context);

        public abstract void PressDown(MarioContext context);

        public abstract void PressRight(MarioContext context);

        public abstract void PressLeft(MarioContext context);

        public abstract void PressNothing(MarioContext context);
        public override abstract string ToString();



    }


    public class IdleStateLeft : MarioActionState
    {
        Kinematics kinematics;
        public IdleStateLeft()
        {
            kinematics = new Kinematics();
        }

        public override void PressUp(MarioContext context)
        {
            kinematics.MoveUp(context);
            context.SetActionState(new JumpingStateLeft());
        }

        public override void PressDown(MarioContext context)
        {
            string powerUpState = context.GetPowerUpState().ToString();
            if (powerUpState != "StandardMario" && powerUpState != "DeadMario")
            {
                kinematics.MoveDown(context);
                context.SetActionState(new CrouchingStateLeft());
            }
        }
        public override void PressRight(MarioContext context)
        {
            kinematics.AccelerateRight(context);
            context.SetActionState(new IdleStateRight());
        }
        public override void PressLeft(MarioContext context)
        {
            kinematics.AccelerateLeft(context);
            context.SetActionState(new RunningStateLeft());
        }

        public override void PressNothing(MarioContext context)
        {
            kinematics.IdleXDecelerate(context);
        }

        public override string ToString()
        {
            return ("IdleStateLeft");
        }
    }

    public class IdleStateRight : MarioActionState
    {
        Kinematics kinematics;
        public IdleStateRight()
        {
            kinematics = new Kinematics();
        }

        public override void PressUp(MarioContext context)
        {
            kinematics.MoveUp(context);
            context.SetActionState(new JumpingStateRight());
        }

        public override void PressDown(MarioContext context)
        {
            string powerUpState = context.GetPowerUpState().ToString();
            if (powerUpState != "StandardMario" && powerUpState != "DeadMario")
            {
                kinematics.MoveDown(context);
                context.SetActionState(new CrouchingStateRight());
            }
        }
        public override void PressRight(MarioContext context)
        {
            kinematics.AccelerateRight(context);
            context.SetActionState(new RunningStateRight());
        }
        public override void PressLeft(MarioContext context)
        {
            kinematics.AccelerateLeft(context);
            context.SetActionState(new IdleStateLeft());
        }

        public override void PressNothing(MarioContext context)
        {
            kinematics.IdleXDecelerate(context);
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

        public override void PressNothing(MarioContext context)
        {
            context.SetActionState(new IdleStateLeft());

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

        public override void PressNothing(MarioContext context)
        {
            context.SetActionState(new IdleStateRight());

        }
        public override string ToString()
        {
            return ("CrouchingStateRight");
        }
    }

    public class JumpingStateLeft : MarioActionState
    {
        Kinematics kinematics;
        public JumpingStateLeft()
        {
            kinematics = new Kinematics();
        }

        public override void PressUp(MarioContext context)
        {
            kinematics.MoveUp(context);
            //context.SetActionState(new JumpingStateLeft());
        }

        public override void PressDown(MarioContext context)
        {
            kinematics.MoveDown(context);
            context.SetActionState(new IdleStateLeft());
        }
        public override void PressRight(MarioContext context)
        {
            kinematics.AccelerateRight(context);
            context.SetActionState(new JumpingStateRight());
        }
        public override void PressLeft(MarioContext context)
        {
            kinematics.AccelerateLeft(context);
        }

        public override void PressNothing(MarioContext context)
        {
            //do nothing
        }
        public override string ToString()
        {
            return ("JumpingStateLeft");
        }
    }

    public class JumpingStateRight : MarioActionState
    {
        Kinematics kinematics;
        public JumpingStateRight()
        {
            kinematics = new Kinematics();
        }

        public override void PressUp(MarioContext context)
        {
            kinematics.MoveUp(context);
            //context.SetActionState(new JumpingStateRight());
        }
        public override void PressDown(MarioContext context)
        {
            kinematics.MoveDown(context);
            context.SetActionState(new IdleStateRight());
        }
        public override void PressRight(MarioContext context)
        {
            kinematics.AccelerateRight(context);
        }
        public override void PressLeft(MarioContext context)
        {
            kinematics.AccelerateLeft(context);
            context.SetActionState(new JumpingStateLeft());
        }

        public override void PressNothing(MarioContext context)
        {
            //do nothing
        }
        public override string ToString()
        {
            return ("JumpingStateRight");
        }
    }
    public class FallingStateLeft : MarioActionState
    {
        Kinematics kinematics;
        public FallingStateLeft()
        {
            kinematics = new Kinematics();
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
            kinematics.AccelerateLeft(context);
        }

        public override void PressNothing(MarioContext context)
        {
            // do nothing
        }
        public override string ToString()
        {
            return ("FallingStateLeft");
        }
    }

    public class FallingStateRight : MarioActionState
    {
        Kinematics kinematics;

        public FallingStateRight()
        {
            kinematics = new Kinematics();
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
            kinematics.AccelerateLeft(context);
        }

        public override void PressNothing(MarioContext context)
        {
            // do nothing
        }
        public override string ToString()
        {
            return ("FallingStateRight");
        }
    }

    public class RunningStateLeft : MarioActionState
    {
        Kinematics kinematics;
        public RunningStateLeft()
        {
            kinematics = new Kinematics();
        }

        public override void PressUp(MarioContext context)
        {
            kinematics.MoveUp(context);
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
            kinematics.AccelerateLeft(context);
        }

        public override void PressNothing(MarioContext context)
        {
            context.SetActionState(new IdleStateLeft());

        }
        public override string ToString()
        {
            return ("RunningStateLeft");
        }
    }

    public class RunningStateRight : MarioActionState
    {
        Kinematics kinematics;
        public RunningStateRight()
        {
            kinematics = new Kinematics();
        }

        public override void PressUp(MarioContext context)
        {
            kinematics.MoveUp(context);
            context.SetActionState(new JumpingStateRight());
        }
        public override void PressDown(MarioContext context)
        {
            //Does nothing

        }
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
            context.SetActionState(new IdleStateRight());

        }
        public override string ToString()
        {
            return ("RunningStateRight");
        }
    }
}

