using Mario;
using Mario.States;
using System;

public class Pause : MenuCommand
{
	public Pause(Overlay receiver) : base(receiver)
	{
	}

    public override void Execute()
    {
        receiver.Pause();
    }
}

