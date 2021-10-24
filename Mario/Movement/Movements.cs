using System;
using System.Collections.Generic;
using System.Text;
using Mario.Sprites.Mario;

namespace Mario.Movement
{
    class MoveLeftCommand : MarioCommand
    {
        public MoveLeftCommand(SuperMario receiver) : base(receiver)
        {
        }

        public override void Execute()
        {
            receiver.MoveLeftCommand();
        }
    }

    class MoveRightCommand : MarioCommand
    {
        public MoveRightCommand(SuperMario receiver) : base(receiver)
        {
        }

        public override void Execute()
        {
            receiver.MoveRightCommand();
        }
    }

    class JumpCommand : MarioCommand
    {
        public JumpCommand(SuperMario receiver) : base(receiver)
        {
        }

        public override void Execute()
        {
            receiver.JumpCommand();
        }
    }

    class CrouchCommand : MarioCommand
    {
        public CrouchCommand(SuperMario receiver) : base(receiver)
        {
        }

        public override void Execute()
        {
            receiver.CrouchCommand();
        }
    }

    class IdleCommand : MarioCommand
    {
        public IdleCommand(SuperMario receiver) : base(receiver)
        {
        }

        public override void Execute()
        {
            receiver.IdleCommand();
        }
    }
    class CrouchingDiscontinueCommand : MarioCommand
    {
        public CrouchingDiscontinueCommand(SuperMario receiver) : base(receiver)
        {
        }

        public override void Execute()
        {
            receiver.CrouchingDiscontinueCommand();
        }
    }
    class JumpingDiscontinueCommand : MarioCommand
    {
        public JumpingDiscontinueCommand(SuperMario receiver) : base(receiver)
        {
        }

        public override void Execute()
        {
            receiver.JumpingDiscontinueCommand();
        }
    }
    class FaceLeftDiscontinueCommand : MarioCommand
    {
        public FaceLeftDiscontinueCommand(SuperMario receiver) : base(receiver)
        {
        }

        public override void Execute()
        {
            receiver.FaceLeftDiscontinueCommand();
        }
    }
    class FaceRightDiscontinueCommand : MarioCommand
    {
        public FaceRightDiscontinueCommand(SuperMario receiver) : base(receiver)
        {
        }

        public override void Execute()
        {
            receiver.FaceRightDiscontinueCommand();
        }
    }

    class FireCommand : MarioCommand
    {
        public FireCommand(SuperMario receiver) : base(receiver)
        {
        }

        public override void Execute()
        {
            receiver.FireCommand();
        }
    }
}
