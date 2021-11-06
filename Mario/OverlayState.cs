using System;

public abstract class OverlayState
{
	public void Draw(Spritebatch spritebatch);

	public Boolean isActive();
}

public class GameOverState :OverlayState
{
	public void Draw(Spritebatch spritebatch)
    {

    }

	public Boolean isActive()
    {
        return true;
    }
}
public class PauseState : OverlayState
{
    public void Draw(Spritebatch spritebatch)
    {

    }

    public Boolean isActive()
    {
        return true;
    }
}
public class WinState : OverlayState
{
    public void Draw(Spritebatch spritebatch)
    {

    }

    public Boolean isActive()
    {
        return true;
    }
}
public class NoOverlayState : OverlayState
{
    public void Draw(Spritebatch spritebatch)
    {

    }

    public Boolean isActive()
    {
        return false;
    }
}