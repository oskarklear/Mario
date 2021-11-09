using Mario.Sprites.Mario;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mario.Sprites.Items
{
    class Cloud : SpriteTemplate
    {
        public Cloud(Game1 theatre, Vector2 location)
        {
            texture = theatre.Content.Load<Texture2D>("BackgroundEntities/cloudBoi");
            position = location;
        }
    }
}
