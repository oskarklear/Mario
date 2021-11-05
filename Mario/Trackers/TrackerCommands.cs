using System;
using System.Collections.Generic;
using System.Text;
using Mario.Trackers;

namespace Mario.Trackers
{
    class AddCoinCommand : TrackerCommand
    {
        public AddCoinCommand(StatTracker receiver) : base(receiver)
        {
        }

        public override void Execute()
        {
            receiver.AddCoinCommand();
        }
    }

    class AddLifeCommand : TrackerCommand
    {
        public AddLifeCommand(StatTracker receiver) : base(receiver)
        {
        }

        public override void Execute()
        {
            receiver.AddLifeCommand();
        }
    }

    class RemoveLifeCommand : TrackerCommand
    {
        public RemoveLifeCommand(StatTracker receiver) : base(receiver)
        {
        }

        public override void Execute()
        {
            receiver.RemoveLifeCommand();
        }
    }

/*    class AddPointsCommand : TrackerCommand
    {
        public AddPointsCommand(StatTracker receiver) : base(receiver)
        {
        }

        public override void Execute()
        {
            receiver.AddPointsCommand();
        }
    }*/
}
