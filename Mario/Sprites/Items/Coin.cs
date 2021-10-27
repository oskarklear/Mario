using Mario.Sprites.Mario;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mario.Sprites.Items
{
    class BlockCoin : ISprite
    {
        int timeSinceLastFrame;
        int millisecondsPerFrame;
        int currentFrame;
        int Columns;
        Texture2D texture;
        Vector2 position;
        Vector2 velocity;
        int topPosition;
        bool topPositionReached;
        int disappearPosition;
        bool coinActive;
        bool obtained;
        Rectangle hitbox;

        public Rectangle Hitbox
        {
            get { return hitbox; }
            set { hitbox = value; }
        }
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
        
        public BlockCoin(Game1 theatre, Vector2 location)
        {
            timeSinceLastFrame = 0;
            millisecondsPerFrame = 5;
            currentFrame = 0;
            Columns = 4;
            position = location;
            topPosition = (int)position.Y - 40;
            disappearPosition = (int)position.Y - 30;
            topPositionReached = false;
            texture = theatre.Content.Load<Texture2D>("items/coins");
            obtained = false;
            coinActive = true;
            hitbox = new Rectangle((int)position.X, (int)position.Y, 12, 16);
            velocity.Y = 1f;
            velocity.X = 1f;
            showHitbox = false;
        }

        public bool delete()
        {
            return false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            int width = texture.Width / Columns;
            int height = texture.Height;
            int row = currentFrame / Columns;
            int column = currentFrame % Columns;

            Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);

            if (!obtained && coinActive)
            {
                Rectangle destinationRectangle = new Rectangle((int)position.X, (int)position.Y, width, height);
                spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, Color.White);

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
            if (timeSinceLastFrame > millisecondsPerFrame)
            {
                currentFrame++;
                timeSinceLastFrame = 0;
            }
            if (currentFrame == Columns)
                currentFrame = 0;
            timeSinceLastFrame++;

            if (position.Y > topPosition && !topPositionReached)
            {
                position.Y -= 2;
            }
            else if (position.Y == topPosition)
            {
                topPositionReached = !topPositionReached;
                position.Y += 2;
            }
            else if (position.Y < disappearPosition && topPositionReached)
            {
                position.Y += 2;
            }
            else if (position.Y == disappearPosition && topPositionReached)
            {
                coinActive = false;
            }
        }

        public void Collision(ISprite collider)
        {
            if (collider is SuperMario)
            {
                obtained = true;
                hitbox = new Rectangle(-1, -1, 0, 0);
                velocity.X = 0f;
                velocity.Y = 0f;
            }
        }
    }

    class MapCoin : ISprite
    {
        int timeSinceLastFrame;
        int millisecondsPerFrame;
        int currentFrame;
        int Columns;
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
            set { hitbox = value; }
        }
        private bool showHitbox;
        public bool ShowHitbox
        {
            get { return showHitbox; }
            set { showHitbox = value; }
        }
        bool obtained;

        public MapCoin(Game1 theatre, Vector2 location)
        {
            timeSinceLastFrame = 0;
            millisecondsPerFrame = 5;
            currentFrame = 0;
            Columns = 4;
            position = location;
            texture = theatre.Content.Load<Texture2D>("items/coins");
            obtained = false;
            hitbox = new Rectangle((int)position.X, (int)position.Y, 12, 16);
            showHitbox = false;
        }

        public bool delete()
        {
            return false;
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
                Rectangle destinationRectangle = new Rectangle((int)position.X, (int)position.Y, width, height);
                spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, Color.White);
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
            if (timeSinceLastFrame > millisecondsPerFrame)
            {
                currentFrame++;
                timeSinceLastFrame = 0;
            }
            if (currentFrame == Columns)
                currentFrame = 0;
            timeSinceLastFrame++;
        }

        public void Collision(ISprite collider)
        {
            obtained = true;
        }
    }
}
