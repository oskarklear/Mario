using System;
using System.Collections.Generic;
using System.Text;
using Mario.Sprites.Mario;

namespace Mario.Movement
{
    class MoveLeftCommand : MarioCommand
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

    class MoveRightCommand : MarioCommand
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

    class JumpCommand : MarioCommand
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

    class CrouchCommand : MarioCommand
    {
        private SuperMario mario;
        public CrouchCommand(SuperMario M)
        {
            mario = M;
        }
        public void Execute()
        {
            mario.crouch();
        }
    }
}
