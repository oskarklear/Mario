using Mario.Sprites.Mario;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using Mario.States;

namespace Mario.Sprites.Items
{
    
    class RedMushroom : ISprite
    {
        //Game1 Theatre;
        Texture2D texture;
        //Texture2D hitboxTexture;
        Vector2 position;
        int endPosition;
        Vector2 velocity;
        bool obtained;
        SuperMario superMario;
        bool direction;
        bool spawning;
        //int velocity;
        //MarioContext context;
        //Boolean isActive;
        Rectangle hitbox;
        public Rectangle Hitbox 
        {
            get { return hitbox; }
        
        }
        public Vector2 Position
        {
            get { return position; }
        }

/*        public bool Obtained
        {
            get { return obtained; }
        }*/
        private bool showHitbox;
        public bool ShowHitbox
        {
            get { return showHitbox; }
            set { showHitbox = value; }
        }

        public RedMushroom(Game1 theatre, Vector2 location, SuperMario mario)
        {
            position = location;
            endPosition = (int)position.Y - 15;
            //Theatre = theatre;
            texture = theatre.Content.Load<Texture2D>("items/red_mushroom");
            obtained = false;
            hitbox = new Rectangle((int)location.X, (int)location.Y, 18, 18);
            showHitbox = false;
            superMario = mario;
            direction = mario.position.X > position.X ? true : false;
            velocity.Y = 3f;
            velocity.X = 1f;
            spawning = true;
            //isActive = false;
            //mario = new SuperMario(theatre, mario.position, context);
            
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            //if (isActive)
            //{
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
            //}
        }

        public void Update()
        {
            position.Y += velocity.Y;
            hitbox = new Rectangle((int)position.X, (int)position.Y, 16, 16);
            //hitbox.Y += velocity.Y;
            //if (obtained)
            //   hitbox = new Rectangle(-1, -1, 0, 0);
            //if (mario.position.X < position.X)
            //{
            //System.Diagnostics.Debug.WriteLine("Y-position: " + position.Y);
            //System.Diagnostics.Debug.WriteLine("EndPosition: " + endPosition);
            System.Diagnostics.Debug.WriteLine("X-VELOCITY: " + velocity.X);
            System.Diagnostics.Debug.WriteLine("Y-VELOCITY: " + velocity.Y);
            if (position.Y > endPosition && spawning) 
            {
                position.Y -= 2;
                hitbox.Y -= 2;
            }
            else if (position.Y == endPosition && spawning)
            {
                spawning = false;
            }
            
            if (direction /*&& !spawning*/)
            {
                position.X += velocity.X;
            } 
            else if (!direction /*&& !spawning*/)
            {
                position.X -= velocity.X;
            }
        }

        public void Collision(ISprite collider)
        {
            //velocity.X = 1f;
            if (collider is SuperMario)
            {
                obtained = true;
                hitbox = new Rectangle(-1, -1, 0, 0);
                velocity.X = 0f;
                velocity.Y = 0f;
            }

            if (collider is BlockContext)
            {
                if (hitbox.TouchTopOf(collider.Hitbox))
                {
                    hitbox.Y = collider.Hitbox.Y - hitbox.Height - 2;
                    position.Y = hitbox.Y;
                    
                }
                else
                {
                    velocity.X = 0f;
                }
        
                if (hitbox.TouchRightOf(collider.Hitbox))
                {
                    hitbox.X = collider.Hitbox.X + hitbox.Width + 1;
                    position.X = hitbox.X;
                    direction = !direction;
                }

                if (hitbox.TouchLeftOf(collider.Hitbox))
                {
                    hitbox.X = collider.Hitbox.X - hitbox.Width;
                    position.X = hitbox.X;
                    direction = !direction;
                }

                if (hitbox.TouchBottomOf(collider.Hitbox))
                {
                    hitbox.Y = collider.Hitbox.Y + hitbox.Height;
                    position.Y = hitbox.Y;
                }
            }
            
            
        }
        
    }
}
