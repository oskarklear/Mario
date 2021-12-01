using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mario.Sprites.Items
{
    class SpikeBlock : SpriteTemplate
    {
        
        public SpikeBlock(Game1 theatre, Vector2 location)
        {
            texture = theatre.Content.Load<Texture2D>("obstacles/SpikeBlock");
            position = location;
            hitbox = new Rectangle((int)location.X, (int)location.Y, 16, 16);
            showHitbox = false;
        }


    }
}