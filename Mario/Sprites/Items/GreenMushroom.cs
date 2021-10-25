using Mario.Sprites.Mario;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mario.Sprites.Items
{
    
    class GreenMushroom : ISprite
    {
        Texture2D texture;
        Vector2 position;
        int endPosition;
        bool obtained;
        SuperMario superMario;
        bool direction;
        bool useGravity;
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
        Rectangle hitbox;
        public Rectangle Hitbox
        {
            get { return hitbox; }
            set { hitbox = value; }
        }
        public GreenMushroom(Game1 theatre, Vector2 location, SuperMario mario)
        {
            position = location;
            endPosition = (int)position.Y - 13;
            texture = theatre.Content.Load<Texture2D>("items/green_mushroom");
            obtained = false;
            hitbox = new Rectangle((int)location.X, (int)location.Y, 16, 16);
            showHitbox = false;
            superMario = mario;
            direction = mario.position.X < position.X ? true : false;
            useGravity = false;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (!obtained)
            {
                spriteBatch.Draw(texture, position, Color.White);
                if (showHitbox)
                {
                    Texture2D hitboxTextureW = new Texture2D(spriteBatch.GraphicsDevice, hitbox.Width, 1);
                    Texture2D hitboxTextureH = new Texture2D(spriteBatch.GraphicsDevice, 1, hitbox.Height);
                    Color[] dataW = new Color[hitbox.Width];
                    for (int i = 0; i < dataW.Length; i++) dataW[i] = Color.Green;
                    Color[] dataH = new Color[hitbox.Height];
                    for (int i = 0; i < dataH.Length; i++) dataH[i] = Color.Green;
                    hitboxTextureW.SetData(dataW);
                    hitboxTextureH.SetData(dataH);
                    spriteBatch.Draw(hitboxTextureW, new Vector2((int)hitbox.X, (int)hitbox.Y), Color.White);
                    spriteBatch.Draw(hitboxTextureW, new Vector2((int)hitbox.X, (int)hitbox.Y + (int)hitbox.Height), Color.White);
                    spriteBatch.Draw(hitboxTextureH, new Vector2((int)hitbox.X, (int)hitbox.Y), Color.White);
                    spriteBatch.Draw(hitboxTextureH, new Vector2((int)hitbox.X + (int)hitbox.Width, (int)hitbox.Y), Color.White);
                }
            }
            
        }

        public void Update()
        {
            System.Diagnostics.Debug.WriteLine("useGravity: " + useGravity);
            System.Diagnostics.Debug.WriteLine("Y-position: " + position.Y);
            System.Diagnostics.Debug.WriteLine("X-position: " + position.X);

            if (position.Y > endPosition)
            {
                position.Y -= 1;
                hitbox.Y -= 1;
            }
            else if (direction)
            {
                position.X += 1;
                hitbox.X += 1;
            }
            else
            {
                position.X -= 1;
                hitbox.X -= 1;
            }

            if (useGravity)
            {
                position.Y += 2;
                hitbox.Y += 2;
            }
        }

        public void Collision(ISprite collider)
        {
            obtained = true;
            if (collider is SuperMario)
            {
                hitbox = new Rectangle(-1, -1, 0, 0);
            }
            /*else if (!Hitbox.TouchTopOf(collider.Hitbox))
            {
                useGravity = true;
            }
            else
            {
                useGravity = false;
            }*/
            
        }
       
    }
}
