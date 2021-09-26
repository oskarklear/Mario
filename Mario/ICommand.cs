using System;
using System.Collections.Generic;
using System.Text;

namespace Mario
{
    interface ICommand
    {
        void Execute();
    }

/*    public abstract class MoveLeft : ICommand
    {
        protected Game1 receiver;

        protected MoveLeft(Game1 receiver)
        {
            this.receiver = receiver;
        }
        public abstract void Execute();
    }

    public abstract class MoveRight : ICommand
    {
        protected 
    }*/
}
