using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mario.Sprites.Items
{
    class CaveBG : SpriteTemplate
    {
        public CaveBG(Game1 theatre, Vector2 location)
        {
            texture = theatre.Content.Load<Texture2D>("BackgroundEntities/UndergroundBG");
            position = location;
        }
    }
 }


