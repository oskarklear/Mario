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
    class FireFlower : ISprite
    {
        int timeSinceLastFrame;
        int millisecondsPerFrame;
        int currentFrame;
        int Columns;
        Texture2D texture;
        Vector2 position;
        int endPosition;
        Vector2 velocity;
        bool obtained;
        bool spawning;
        private bool showHitbox;
        public bool ShowHitbox
        {
            get { return showHitbox; }
            set { showHitbox = value; }
        }
        public bool isShell { get; set; }
        public bool delete()
        {
            return false;
        }
        public Vector2 Position
        {
            get { return position; }
        }
        Rectangle hitbox;
        public Rectangle Hitbox
        {
            get { return hitbox; }
            set { hitbox = value; }
        }

        public FireFlower(Game1 theatre, Vector2 location)
        {
            timeSinceLastFrame = 0;
            millisecondsPerFrame = 15;
            currentFrame = 0;
            Columns = 2;
            position = location;
            endPosition = (int)position.Y - 13;
            texture = theatre.Content.Load<Texture2D>("items/fire_flower");
            obtained = false;
            showHitbox = false;
            hitbox = new Rectangle((int)location.X, (int)location.Y, 16, 16);
            velocity.Y = 1f;
            velocity.X = 1f;
            spawning = true;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            int width = texture.Width / Columns;
            int height = texture.Height;
            int row = currentFrame / Columns;
            int column = currentFrame % Columns;

            Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);

            if (!obtained)
            {
                Hitbox = new Rectangle((int)position.X, (int)position.Y, width, height);
                spriteBatch.Draw(texture, Hitbox, sourceRectangle, Color.White);

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

            if (timeSinceLastFrame > millisecondsPerFrame)
            {
                currentFrame++;
                timeSinceLastFrame = 0;
            }
            if (currentFrame == Columns)
                currentFrame = 0;
            timeSinceLastFrame++;

            if (position.Y > endPosition && spawning)
            {
                position.Y -= 2;
                hitbox.Y -= 2;
            }
            else if (position.Y == endPosition && spawning)
            {
                spawning = false;
            }
        }

        public void Collision(ISprite collider)
        {
            if (!spawning)
            {
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

                    if (hitbox.TouchRightOf(collider.Hitbox))
                    {
                        hitbox.X = collider.Hitbox.X + hitbox.Width + 1;
                        position.X = hitbox.X;
                    }

                    if (hitbox.TouchLeftOf(collider.Hitbox))
                    {
                        hitbox.X = collider.Hitbox.X - hitbox.Width - 1;
                        position.X = hitbox.X;
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

