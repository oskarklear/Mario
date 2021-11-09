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
    
    class GreenMushroom : ISprite
    {
        Texture2D texture;
        Vector2 position;
        int endPosition;
        Vector2 velocity;
        bool obtained;
        SuperMario superMario;
        bool direction;
        bool spawning;
        Game1 Theatre;
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
        Rectangle hitbox;
        public Rectangle Hitbox
        {
            get { return hitbox; }
            set { hitbox = value; }
        }
        public bool isShell { get; set; }
        public GreenMushroom(Game1 theatre, Vector2 location, SuperMario mario)
        {
            Theatre = theatre;
            position = location;
            endPosition = (int)position.Y - 13;
            texture = theatre.Content.Load<Texture2D>("items/green_mushroom");
            obtained = false;
            hitbox = new Rectangle((int)location.X, (int)location.Y, 16, 16);
            showHitbox = false;
            superMario = mario;
            direction = mario.position.X < position.X ? true : false;
            velocity.Y = 1f;
            velocity.X = 1f;
            spawning = true;
        }

        public bool delete()
        {
            if (deleted) return true;
            else return false;
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
            position.Y += velocity.Y;
            if (!obtained)
                hitbox = new Rectangle((int)position.X, (int)position.Y, 16, 16);

            if (position.Y > endPosition && spawning)
            {
                position.Y -= 2;
                hitbox.Y -= 2;
            }
            else if (position.Y == endPosition && spawning)
            {
                spawning = false;
            }
            else if (direction)
            {
                position.X += velocity.X;
            }
            else if (!direction)
            {
                position.X -= velocity.X;
            }
        }

        public void Collision(ISprite collider)
        {
            if (!spawning)
            {
                if (collider is SuperMario)
                {
                    obtained = true;
                    deleted = true;
                    hitbox = Rectangle.Empty;
                    velocity.X = 0f;
                    velocity.Y = 0f;
                    Theatre.tracker.AddLifeCommand();
                }

                if (collider is BlockContext || collider is Pipe)
                {
                    if (hitbox.TouchTopOf(collider.Hitbox))
                    {
                        hitbox.Y = collider.Hitbox.Y - hitbox.Height - 2;
                        position.Y = hitbox.Y;
                    }

                    if (hitbox.TouchRightOf(collider.Hitbox))
                    {
                        if (collider is Pipe) hitbox.X = collider.Hitbox.X + hitbox.Width + 10;
                        else hitbox.X = collider.Hitbox.X + hitbox.Width + 2;
                        position.X = hitbox.X;
                        direction = !direction;
                    }

                    if (hitbox.TouchLeftOf(collider.Hitbox))
                    {
                        if (collider is Pipe) hitbox.X = collider.Hitbox.X - hitbox.Width - 5;
                        else hitbox.X = collider.Hitbox.X - hitbox.Width - 2;
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
}
