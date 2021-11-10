using System;
using System.Collections.Generic;
using System.Text;
using Mario.Sprites.Mario;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Mario.Sprites.Mario
{
    public class MarioIcon : SpriteTemplate
    {
        public MarioIcon(Game1 theatre, Vector2 location)
        {
            texture = theatre.Content.Load<Texture2D>("mario/smallIdleMarioR");
            position = location;
            currentFrame = 0;
            columns = 1;
        }
    }
}
