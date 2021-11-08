using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Mario;
using Mario.Map;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Mario.States
{
     public abstract class OverlayState
    {
        public abstract void Draw(SpriteBatch spritebatch);

        public abstract  bool isActive();

        public abstract void Pause(Overlay context);

        public abstract String toString();

        
    }

    public class GameOverState : OverlayState
    {
        SpriteFont MenuFont;
        public GameOverState(SpriteFont font)
        {
            MenuFont = font;
        }
        public override void Draw(SpriteBatch spritebatch)
        {
            spritebatch.DrawString(MenuFont, "You lost. Press Q to quit, or R to retry", new Vector2(100, 100), Color.Black);
        }

        public override bool isActive()
        {
            return true;
        }

        public override void Pause(Overlay context)
        {
            //does nothing
        }

        public override string toString()
        {
            return "GameOver";
        }
    }
    public class PauseState : OverlayState
    {
        SpriteFont MenuFont;
        public PauseState(SpriteFont font)
        {
            MenuFont = font;
        }
        public override  void Draw(SpriteBatch spritebatch)
        {
            spritebatch.DrawString(MenuFont, "The Game is Paused. Press Q to quit, or P to unpause", new Vector2(100, 100), Color.Black);
        }

        public override bool isActive()
        {
            return true;
        }

        public override void Pause(Overlay context)
        {
            context.SwitchOverlay(new NoOverlayState(MenuFont));
        }

        public override string toString()
        {
            return "Pause";
        }
    }
    public class WinState : OverlayState
    {
        SpriteFont MenuFont;
        public WinState(SpriteFont font)
        {
            MenuFont = font;
        }
        public override void Draw(SpriteBatch spritebatch)
        {
            spritebatch.DrawString(MenuFont, "You Won! Press Q to quit, or R to restart", new Vector2(100, 100), Color.Black);
        }

        public override bool isActive()
        {
            return true;
        }

        public override void Pause(Overlay context)
        {
            //does nothing
        }

        public override string toString()
        {
            return "Win";
        }
    }
    public class NoOverlayState : OverlayState
    {
        SpriteFont MenuFont;
        public NoOverlayState(SpriteFont font)
        {
            MenuFont = font;
        }
        public override void Draw(SpriteBatch spritebatch)
        {
            //does nothing
        }

        public override bool isActive()
        {
            return false;
        }
        public override void Pause(Overlay context)
        {
            context.SwitchOverlay(new PauseState(MenuFont));
        }

        public override string toString()
        {
            return "NoOverlay";
        }
    }
}