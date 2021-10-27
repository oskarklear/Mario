using System;
using System.Collections.Generic;
using System.Text;
using Mario.Sprites.Mario;
using Mario.States;

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
        protected BlockContext receiver;

        protected BlockCommand(BlockContext receiver)
        {
            this.receiver = receiver;
        }

        public abstract void Execute();
    }

    public abstract class GameCommand : ICommand
    {
        protected Game1 receiver;

        protected GameCommand(Game1 receiver)
        {
            this.receiver = receiver;
        }

        public abstract void Execute();
    }

}

