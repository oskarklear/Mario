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

    class DecrementTimeCommand : TrackerCommand
    {
        public DecrementTimeCommand(StatTracker receiver) : base(receiver)
        {
        }

        public override void Execute()
        {
            receiver.DecrementTimeCommand();
        }
    }

    class ResetTimeRemainingCommand : TrackerCommand
    {
        public ResetTimeRemainingCommand(StatTracker receiver) : base(receiver)
        {
        }

        public override void Execute()
        {
            receiver.ResetTimeRemainingCommand();
        }
    }

    class ResetPointsCommand : TrackerCommand
    {
        public ResetPointsCommand(StatTracker receiver) : base(receiver)
        {
        }

        public override void Execute()
        {
            receiver.ResetPointsCommand();
        }
    }

    class ResetLivesCommand : TrackerCommand
    {
        public ResetLivesCommand(StatTracker receiver) : base(receiver)
        {
        }

        public override void Execute()
        {
            receiver.ResetLivesCommand();
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
