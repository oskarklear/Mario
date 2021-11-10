using System;
using System.Collections.Generic;
using System.Text;
using Mario.Sprites.Mario;
using Mario.States;
using Mario.Trackers;

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

    public abstract class TrackerCommand : ICommand
    {
        protected StatTracker receiver;

        protected TrackerCommand(StatTracker receiver)
        {
            this.receiver = receiver;
        }

        public abstract void Execute();
    }
    public abstract class MenuCommand : ICommand
    {
        protected Overlay receiver;

        protected MenuCommand(Overlay receiver)
        {
            this.receiver = receiver;
        }

        public abstract void Execute();
    }

}

