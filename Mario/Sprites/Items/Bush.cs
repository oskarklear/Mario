using Mario.Sprites.Mario;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mario.Sprites.Items
{
    class Bush : SpriteTemplate
    {
        public Bush(Game1 theatre, Vector2 location)
        {
            texture = theatre.Content.Load<Texture2D>("BackgroundEntities/bush");
            position = location;
        }
    }
}
