using Mario.Sprites.Mario;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mario.Sprites
{

    class GoalGate : ISprite
    {
        Game1 Theatre;
        Texture2D texture;
        Vector2 position;
        public Vector2 Position
        {
            get { return position; }
        }
        Rectangle hitbox;
        public Rectangle Hitbox
        {
            get { return hitbox; }
        }
        private bool showHitbox;
        public bool ShowHitbox
        {
            get { return showHitbox; }
            set { showHitbox = value; }
        }

        public bool delete()
        {
            return false;
        }

        public GoalGate(Game1 theatre, Vector2 location)
        {
            position = location;
            Theatre = theatre;
            texture = Theatre.Content.Load<Texture2D>("GoalGate");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, Color.White, 0f, Vector2.Zero, 0.5f, SpriteEffects.None, 0f);
        }

        public void Update()
        {

        }

        public void Collision(ISprite collider)
        {
            //Does nothing
        }
    }

    class GoalGateMovingPart : ISprite
    {
        Game1 Theatre;
        Texture2D texture;
        Vector2 position;
        public Vector2 Position
        {
            get { return position; }
        }
        Rectangle hitbox;
        public Rectangle Hitbox
        {
            get { return hitbox; }
        }
        private bool showHitbox;
        public bool ShowHitbox
        {
            get { return showHitbox; }
            set { showHitbox = value; }
        }
        bool up;

        public GoalGateMovingPart(Game1 theatre, Vector2 location)
        {
            position = location;
            Theatre = theatre;
            texture = Theatre.Content.Load<Texture2D>("GoalGateMovingPiece");
            up = true;
        }

        public bool delete()
        {
            return false;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, Color.White, 0f, Vector2.Zero, 0.5f, SpriteEffects.None, 0f);
        }

        public void Update()
        {
            if (up)
            {
                position.Y = position.Y - 1;
                if (position.Y <= 141)
                    up = false;
            }
            else
            {
                position.Y = position.Y + 1;
                if (position.Y >= 240)
                    up = true;
            }
        }

        public void Collision(ISprite collider)
        {
            //Does nothing
        }
    }
}
