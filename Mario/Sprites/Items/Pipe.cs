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
        Rectangle warpHitbox;
        public Rectangle WarpHitbox
        {
            get { return warpHitbox; }
        }
        private bool warpable;
        public bool Warpable
        {
            get { return warpable; }
        }
        public Pipe(Game1 theatre, Vector2 location, bool warpable)
        {
            texture = theatre.Content.Load<Texture2D>("obstacles/pipe");
            position = location;
            hitbox = new Rectangle((int)location.X, (int)location.Y, 32, 33);
            warpHitbox = new Rectangle((int)location.X + 14, (int)location.Y, 1, 0);
            showHitbox = false;
            this.warpable = warpable;
        }

        public override void SetHitbox()
        {
        }
    }

    class SidePipe : SpriteTemplate
    {
        public SidePipe(Game1 theatre, Vector2 location)
        {
            texture = theatre.Content.Load<Texture2D>("obstacles/sidepipe");
            position = location;
            hitbox = new Rectangle((int)location.X, (int)location.Y, 33, 32);
            showHitbox = false;
        }

        public override void SetHitbox()
        {
        }
    }
    class UpsidedownPipe : SpriteTemplate
    {
        public UpsidedownPipe(Game1 theatre, Vector2 location)
        {
            texture = theatre.Content.Load<Texture2D>("obstacles/upsidedownpipe");
            position = location;
            hitbox = new Rectangle((int)location.X, (int)location.Y, 32, 33);
            showHitbox = false;
        }

        public override void SetHitbox()
        {
        }
    }

    class LongPipe : SpriteTemplate
    {
        public LongPipe(Game1 theatre, Vector2 location)
        {
            texture = theatre.Content.Load<Texture2D>("obstacles/longpipe");
            position = location;
            hitbox = new Rectangle((int)location.X, (int)location.Y, 33, 272);
            showHitbox = false;
        }

        public override void SetHitbox()
        {
        }
    }
}
