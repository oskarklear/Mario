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
    
    class Star : ISprite
    {
        int timeSinceLastFrame;
        int millisecondsPerFrame;
        int currentFrame;
        int Columns;
        bool obtained;
        SuperMario superMario;
        bool verticalDirection;
        bool horizontalDirection;
        int count;
        int maxUpwardDistance;
        Vector2 comingFromBlockPosition;
        bool spawning;
        Texture2D texture;
        Vector2 velocity;
        int spawnTime;
        Vector2 position;
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
        public bool delete()
        {
            return false;
        }

        public Star(Game1 theatre, Vector2 location, SuperMario mario)
        {
            timeSinceLastFrame = 0;
            millisecondsPerFrame = 3;
            currentFrame = 0;
            Columns = 3;
            position = location;
            texture = theatre.Content.Load<Texture2D>("items/stars");
            obtained = false;
            hitbox = new Rectangle((int)location.X, (int)location.Y, 16, 16);
            showHitbox = false;
            superMario = mario;
            horizontalDirection = mario.position.X < position.X ? true : false;
            verticalDirection = true;
            maxUpwardDistance = 50;
            comingFromBlockPosition.Y = (int)position.Y - 13;
            spawning = true;
            velocity.Y = 1f;
            velocity.X = 1f;
            spawnTime = 0;
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
            spawnTime += 1;
            hitbox = new Rectangle((int)position.X, (int)position.Y, 16, 16);

            if (timeSinceLastFrame > millisecondsPerFrame)
            {
                currentFrame++;
                timeSinceLastFrame = 0;
            }

            if (currentFrame == Columns)
                currentFrame = 0;
            timeSinceLastFrame++;

            if (position.Y > comingFromBlockPosition.Y && spawning)
            {
                position.Y -= 2;
                hitbox.Y -= 2;
            }
            
            if (spawnTime > 12 )
            {
                spawning = false;
            }
            
            if (horizontalDirection && !spawning)
            {
                position.X += 1;
                hitbox.X += 1;
            }
            else if (!horizontalDirection && !spawning)
            {
                position.X -= 1;
                hitbox.X -= 1;
            }

            if (verticalDirection && !spawning)
            {
                position.Y -= 1;
                hitbox.Y -= 1;
                count++;
            }
            else if (!verticalDirection && !spawning)
            {
                position.Y += 1;
                hitbox.Y += 1;
            }

            if (count > maxUpwardDistance)
            {
                verticalDirection = false;
                count = 0;
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

                if (collider is BlockContext || collider is Pipe)
                {
                    if (hitbox.TouchTopOf(collider.Hitbox))
                    {
                        hitbox.Y = collider.Hitbox.Y - hitbox.Height - 2;
                        position.Y = hitbox.Y;
                        verticalDirection = true;
                    }

                    if (hitbox.TouchRightOf(collider.Hitbox))
                    {
                        if (collider is Pipe) hitbox.X = collider.Hitbox.X + hitbox.Width + 10;
                        else hitbox.X = collider.Hitbox.X + hitbox.Width + 2;
                        position.X = hitbox.X;
                        horizontalDirection = !horizontalDirection;
                    }

                    if (hitbox.TouchLeftOf(collider.Hitbox))
                    {
                        if (collider is Pipe) hitbox.X = collider.Hitbox.X - hitbox.Width - 5;
                        else hitbox.X = collider.Hitbox.X - hitbox.Width - 1;
                        position.X = hitbox.X;
                        horizontalDirection = !horizontalDirection;
                    }

                    if (hitbox.TouchBottomOf(collider.Hitbox))
                    {
                        hitbox.Y = collider.Hitbox.Y + hitbox.Height;
                        position.Y = hitbox.Y;
                        verticalDirection = false;
                    }
                }
            }
        }
    }
}