﻿using System;
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
        private SuperMario mario;
        public CrouchCommand(SuperMario receiver) : base(receiver)
        {
        }
        public override void Execute()
        {
            receiver.CrouchCommand();
        }
    }
}
