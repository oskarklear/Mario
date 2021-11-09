using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Content;

namespace Mario.Sprites
{
    
    public interface ISprite
    {
        public Rectangle Hitbox { get; }
        public bool ShowHitbox { get; set; }
        public Vector2 Position { get; }
        public void Update();

        public void Draw(SpriteBatch spriteBatch);

        public void Collision(ISprite collider);
        public bool isShell { get; set; }

        public bool Delete();

    }
}
