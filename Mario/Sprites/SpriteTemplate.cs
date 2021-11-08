using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Mario.Sprites.Mario;
using Mario.States;

namespace Mario.Sprites
{
    class SpriteTemplate : ISprite
    {
        public Game1 gameObj;
        public Texture2D texture;
        public Texture2D textureLeft;
        public Texture2D textureRight;
        public Vector2 position;
        public Vector2 velocity;
        public Rectangle hitbox;
        public bool showHitbox;
        public bool obtained;
        public bool spawning;
        public bool verticalDirection;
        public bool horizontalDirection;
        public bool doesMove;
        public bool facingLeft;
        public bool isAnimated;
        public bool useGravity;
        public bool spawnsFromBlock;
        public int endPosition;
        public int timeSinceLastFrame;
        public int millisecondsPerFrame;
        public int currentFrame;
        public int columns;

        public bool isShell { get; set; }

        public Vector2 Position
        {
            get { return position; }
        }
        public Rectangle Hitbox
        {
            get { return hitbox; }
            set { hitbox = value; }
        }
        public bool ShowHitbox
        {
            get { return showHitbox; }
            set { showHitbox = value; }
        }

        public bool Delete()
        {
            return false;
        }

        private void MakeHitbox(SpriteBatch spriteBatch, bool showHitbox)
        {
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

        public void Gravity()
        {
            if (useGravity) position.Y += velocity.Y;
        }

        public void Animate()
        {
            if (isAnimated)
            {
                if (timeSinceLastFrame > millisecondsPerFrame)
                {
                    currentFrame++;
                    timeSinceLastFrame = 0;
                }
                if (currentFrame == columns)
                    currentFrame = 0;
                timeSinceLastFrame++;
            }
        }

        public void SetHitbox()
        {
            if (!obtained) hitbox = new Rectangle((int)position.X, (int)position.Y, 16, 16);
        }

        public virtual void SpawnFromBlock()
        {
            if (spawnsFromBlock)
            {
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
        }

        public virtual void Move()
        {
            if (doesMove)
            {
                if (horizontalDirection && !spawning)
                {
                    position.X += velocity.X;
                }
                else if (!horizontalDirection && !spawning)
                {
                    position.X -= velocity.X;
                }
            }
        }

        public virtual void Update()
        {
            Gravity();
            Animate();
            SetHitbox();
            SpawnFromBlock();
            Move();
        }

        //public void 

        public void Draw(SpriteBatch spriteBatch)
        {
            if (isAnimated)
            {
                int width = texture.Width / columns;
                int height = texture.Height;
                int row = currentFrame / columns;
                int column = currentFrame % columns;

                Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);

                if (!obtained)
                {
                    Rectangle destinationRectangle = new Rectangle((int)position.X, (int)position.Y, width, height);
                    spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, Color.White);

                    MakeHitbox(spriteBatch, showHitbox);
                }
            }
            else
            {
                if (!obtained)
                {
                    spriteBatch.Draw(texture, position, Color.White);

                    MakeHitbox(spriteBatch, showHitbox);
                }
            }
        }

        public virtual void Collision(ISprite collider)
        {

        }

        


    }
}
