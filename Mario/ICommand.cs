using System;
using System.Collections.Generic;
using System.Text;
using Mario.Sprites.Mario;
using Mario.Sprites.Blocks;

namespace Mario
{
    interface ICommand
    {
        void Execute();
    }

    public abstract class MarioCommand : ICommand
    {
        protected SuperMario receiver;

        protected MarioCommand(SuperMario receiver)
        {
            this.receiver = receiver;
        }
        public abstract void Execute();
    }

    public abstract class BlockCommand : ICommand
    {
        protected BlockSprites receiver;

        protected BlockCommand(BlockSprites receiver)
        {
            this.receiver = receiver;
        }

        public abstract void Execute();
    }
}
