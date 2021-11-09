using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Mario.Sprites.Items
{
    class BackgroundHills : SpriteTemplate
    {
        public BackgroundHills(Game1 theatre, Vector2 location)
        {
            texture = theatre.Content.Load<Texture2D>("BackgroundEntities/BackgroundHills");
            position = location;
        }
    }
}
