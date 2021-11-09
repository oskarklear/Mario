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
    public class Overlay
    {
        OverlayState state;
        SpriteFont MenuFont;


        public Overlay(SpriteFont font)
        {
            MenuFont = font;
            state = new NoOverlayState(font);
        }

        public void SwitchOverlay(OverlayState newState)
        {
            
            state = newState;
            //System.Diagnostics.Debug.WriteLine("changed menu state to" + state.toString());
        }

        public void Draw(SpriteBatch spritebatch)
        {
            //System.Diagnostics.Debug.WriteLine(state.toString());
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
