using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Mario;
using Mario.Map;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Mario.Trackers;
namespace Mario.States
{
    public class Overlay
    {
        OverlayState state;
        SpriteFont MenuFont;
        public StatTracker Stats;


        public Overlay(SpriteFont font,StatTracker stats)
        {
            Stats = stats;
            MenuFont = font;
            state = new NoOverlayState(font,this);
        }

        public void SwitchOverlay(OverlayState newState)
        {
            
            state = newState;
        }

        public void Draw(SpriteBatch spritebatch)
        {
            state.Draw(spritebatch);
        }
        public String toString()
        {
            return state.toString();
        }
        public void Pause()
        {
            state.Pause(this);
        }
        
        

        
    }
}
