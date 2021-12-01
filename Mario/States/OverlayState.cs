using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Mario;
using Mario.Map;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

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
        Overlay context;
        public GameOverState(SpriteFont font,Overlay Context)
        {
            context = Context;
            MenuFont = font;
        }
        public override void Draw(SpriteBatch spritebatch)
        {
            spritebatch.DrawString(MenuFont, "You lost. Press Q to quit, or R to retry\n Score: "+context.Stats.points, new Vector2(100, 100), Color.Black);
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
        Overlay context;
        public PauseState(SpriteFont font, Overlay Context)
        {
            context = Context;
            MenuFont = font;
        }
        public override  void Draw(SpriteBatch spritebatch)
        {
            //System.Diagnostics.Debug.WriteLine("drawing pause menu");
            spritebatch.DrawString(MenuFont, "The Game is Paused. Press Q to quit, or P to unpause", new Vector2(100, 100), Color.Black);
        }

        public override bool isActive()
        {
            return true;
        }

        public override void Pause(Overlay context)
        {
            MediaPlayer.Resume();
            context.SwitchOverlay(new NoOverlayState(MenuFont,context));
        }

        public override string toString()
        {
            return "Pause";
        }
    }
    public class WinState : OverlayState
    {
        SpriteFont MenuFont;
        Overlay context;
        public WinState(SpriteFont font, Overlay Context)
        {
            context = Context;
            MenuFont = font;
        }
        public override void Draw(SpriteBatch spritebatch)
        {
            spritebatch.DrawString(MenuFont, "Level Complete!\nQ: Quit \nEnter: Next Level \nScore:"+context.Stats.points, new Vector2(100, 100), Color.Black);
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
        Overlay context;
        public NoOverlayState(SpriteFont font, Overlay Context)
        {
            context = Context;
            MenuFont = font;
        }
        public override void Draw(SpriteBatch spritebatch)
        {
            //System.Diagnostics.Debug.WriteLine("drawing no menu");
            //does nothing
        }

        public override bool isActive()
        {
            return false;
        }
        public override void Pause(Overlay context)
        {
            MediaPlayer.Pause();
            context.SwitchOverlay(new PauseState(MenuFont,context));
        }

        public override string toString()
        {
            return "NoOverlay";
        }
    }
}