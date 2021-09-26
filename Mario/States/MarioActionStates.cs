using System;
using System.Collections.Generic;
using System.Text;

namespace Mario.States
{
    class MarioActionStates
    {
        public abstract class MarioActionState
        {
            protected double yVelocity;
            protected double xVelocity;

            public abstract void Handle();
        }

        public class IdleState : MarioActionState
        {
            public IdleState()
            {
                this.yVelocity = 0;
                this.xVelocity = 0;
            }

            public override void Handle()
            {
                throw new NotImplementedException();
            }
        }

        public class CrouchingState : MarioActionState
        {
            public CrouchingState()
            {
                this.yVelocity = 0;
                this.xVelocity = 0;
            }

            public override void Handle()
            {
                throw new NotImplementedException();
            }
        }

        public class JumpingState : MarioActionState
        {
            //??? Y velocity upwards? Acceleration? Physics? What is going on?
            public JumpingState()
            {
                this.yVelocity = 5;
                this.xVelocity = 0;
            }

            public override void Handle()
            {
                throw new NotImplementedException();
            }
        }

        public class FallingState : MarioActionState
        {
            public FallingState()
            {
                this.yVelocity = -5;
                this.xVelocity = 0;
            }

            public override void Handle()
            {
                throw new NotImplementedException();
            }
        }

        public class RunningState : MarioActionState
        {
            //Left and right? How to do this?
            public RunningState()
            {
                this.yVelocity = 0;
                this.xVelocity = 5;
            }

            public override void Handle()
            {
                throw new NotImplementedException();
            }
        }
    }
}
