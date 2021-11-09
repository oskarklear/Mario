using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mario.Sprites.Items
{
    class Pipe : SpriteTemplate
    {
        public Pipe(Game1 theatre, Vector2 location)
        {
            texture = theatre.Content.Load<Texture2D>("obstacles/pipe");
            position = location;
            hitbox = new Rectangle((int)location.X, (int)location.Y, 32, 33);
            showHitbox = false;
        }

        public override void SetHitbox()
        {
        }
    }
}
