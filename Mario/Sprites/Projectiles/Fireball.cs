using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Mario.Sprites.Mario;


namespace Mario.Sprites.Projectiles
{
    public class Fireball : ISprite
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

        bool upDown;
        bool leftRight;
        bool deleted;
        public Vector2 Position
        {
            get { return position; }
        }
        private bool showHitbox;
        public bool ShowHitbox
        {
            get { return showHitbox; }
            set { showHitbox = value; }
        }
        public Fireball(Game1 theatre, Vector2 location, SuperMario mario, bool xDirection)
        {
            position = location;
            texture = theatre.Content.Load<Texture2D>("projectiles/fireball");
            hitbox = new Rectangle((int)location.X + 5, (int)location.Y + 5, 10, 10);
            showHitbox = false;
            superMario = mario;
            upDown = true;
            deleted = false;
            count = 0;
            leftRight = xDirection;
            maxUpwardLength = 30;

        }

        public void Collision(ISprite collider)
        {
            
            if (hitbox.TouchBottomOf(collider.Hitbox) || hitbox.TouchTopOf(collider.Hitbox))
            {
                upDown = !upDown;
            }

            if (hitbox.TouchLeftOf(collider.Hitbox) || hitbox.TouchRightOf(collider.Hitbox))
            {
                deleted = true;
            }

        }

        public bool delete()
        {
            return true;
        }

        public void Update()
        {
            if (leftRight)
            {
                position.X -= 2;
            } else
            {
                position.X += 2;
            }
            
            // if fireball going up+right
            if (upDown)
            {
                position.Y += 1;
            }
            // if fireball going down+right
            else
            {
                position.Y -= 1;
            }

            count += 2;
            if (count > maxUpwardLength && !upDown)
            {
                upDown = !upDown;
                count = 0;
            }

            hitbox = new Rectangle((int)position.X + 5, (int)position.Y + 5, 10, 10);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (!deleted)
            {
                spriteBatch.Draw(texture, position, Color.White);
            }

            if (showHitbox)
            {
                Texture2D hitboxTextureW = new Texture2D(spriteBatch.GraphicsDevice, hitbox.Width, 1);
                Texture2D hitboxTextureH = new Texture2D(spriteBatch.GraphicsDevice, 1, hitbox.Height);
                Color[] dataW = new Color[hitbox.Width];
                for (int i = 0; i < dataW.Length; i++) dataW[i] = Color.Red;
                Color[] dataH = new Color[hitbox.Height];
                for (int i = 0; i < dataH.Length; i++) dataH[i] = Color.Red;
                hitboxTextureW.SetData(dataW);
                hitboxTextureH.SetData(dataH);
                spriteBatch.Draw(hitboxTextureW, new Vector2((int)hitbox.X, (int)hitbox.Y), Color.White);
                spriteBatch.Draw(hitboxTextureW, new Vector2((int)hitbox.X, (int)hitbox.Y + (int)hitbox.Height), Color.White);
                spriteBatch.Draw(hitboxTextureH, new Vector2((int)hitbox.X, (int)hitbox.Y), Color.White);
                spriteBatch.Draw(hitboxTextureH, new Vector2((int)hitbox.X + (int)hitbox.Width, (int)hitbox.Y), Color.White);
            }

        }
    }
}
