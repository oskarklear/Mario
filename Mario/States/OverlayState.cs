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
    }

    public class GameOverState : OverlayState
    {
        public override void Draw(SpriteBatch spritebatch)
        {

        }

        public override bool isActive()
        {
            return true;
        }

        public override void Pause(Overlay context)
        {
            //does nothing
        }
    }
    public class PauseState : OverlayState
    {
        public override  void Draw(SpriteBatch spritebatch)
        {

        }

        public override bool isActive()
        {
            return true;
        }

        public override void Pause(Overlay context)
        {
            context.SwitchOverlay(new NoOverlayState());
        }
    }
    public class WinState : OverlayState
    {
        public override void Draw(SpriteBatch spritebatch)
        {

        }

        public override bool isActive()
        {
            return true;
        }

        public override void Pause(Overlay context)
        {
            //does nothing
        }

    }
    public class NoOverlayState : OverlayState
    {
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
            context.SwitchOverlay(new PauseState());
        }
    }
}