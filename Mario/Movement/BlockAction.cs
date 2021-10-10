using System;
using System.Collections.Generic;
using System.Text;
using Mario.States;

namespace Mario.Movement
{
    class BumpCommand : BlockCommand
    {
        MarioContext BlockMarioContext;
        public BumpCommand(BlockContext receiver, MarioContext mario) : base(receiver)
        {
            //System.Diagnostics.Debug.WriteLine(receiver.ToString());
            BlockMarioContext = mario;
        }
        public override void Execute()
        {
            receiver.Bump(BlockMarioContext);
        }
    }

}
