using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Mario.Sprites.Mario;


namespace Mario.Sprites.Projectiles
{
    class Fireball : ISprite
    {
        Texture2D texture;
        public Vector2 position;
        Rectangle hitbox;
        SuperMario superMario;
        int maxUpwardLength;
        int count;
        public Rectangle Hitbox
        {
            get { return hitbox; }
        }
        //false == down, true == up
        bool direction;
        bool deleted;

        private bool showHitbox;
        public bool ShowHitbox
        {
            get { return showHitbox; }
            set { showHitbox = value; }
        }
        public Fireball(Game1 theatre, Vector2 location, SuperMario mario)
        {
            position = location;
            texture = theatre.Content.Load<Texture2D>("projectiles/fireball");
            hitbox = new Rectangle((int)location.X, (int)location.Y, 10, 10);
            showHitbox = false;
            superMario = mario;
            direction = true;
            deleted = false;
            count = 0;
            maxUpwardLength = 30;

        }

        public void Collision(ISprite collider, int xoffset, int yoffset)
        {
            
            if (hitbox.TouchBottomOf(collider.Hitbox) || hitbox.TouchTopOf(collider.Hitbox))
            {
                direction = !direction;
            }

            if (hitbox.TouchLeftOf(collider.Hitbox) || hitbox.TouchRightOf(collider.Hitbox))
            {
                deleted = true;
            }

        }

        public void Update()
        {
            position.X += 2;
            // if fireball going up+right
            if (direction)
            {
                position.Y += 1;
            }
            // if fireball going down+right
            else
            {
                position.Y -= 1;
            }

            count += 2;
            if (count > maxUpwardLength && !direction)
            {
                direction = !direction;
                count = 0;
            }

            hitbox = new Rectangle((int)position.X, (int)position.Y, 10, 10);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (!deleted)
            {
                spriteBatch.Draw(texture, position, Color.White);
            }
            
        }
    }
}
