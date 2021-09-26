using System;
using System.Collections.Generic;
using System.Text;
using Mario.Sprites.Mario;

namespace Mario.Movement
{
    class MoveLeftCommand : MovementCommand
    {
        private SuperMario mario;
        public MoveLeftCommand(SuperMario M)
        {
            mario = M;
        }
        public void Execute()
        {
            mario.moveLeft();
        }
    }

    class MoveRightCommand : MovementCommand
    {
        private SuperMario mario;
        public MoveRightCommand(SuperMario M)
        {
            mario = M;
        }
        public void Execute()
        {
            mario.moveRight();
        }
    }

    class JumpCommand : MovementCommand
    {
        private SuperMario mario;
        public JumpCommand(SuperMario M)
        {
            mario = M;
        }
        public void Execute()
        {
            mario.jump();
        }
    }
}
