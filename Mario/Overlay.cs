using System;

public class Overlay
{ 
    OverlayState state;
	public Overlay()
	{
        
	}

    public void SwitchOverlay(OverlayState newState)
    {
        state = newState;
    }

    public void Draw()
    {
        state.Draw();
    }
    public Boolean isActive()
    {
        state.isActive();
    }
}
