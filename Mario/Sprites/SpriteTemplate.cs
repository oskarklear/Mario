using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Mario.Sprites.Mario;
using Mario.Sprites.Items;
using Mario.States;

namespace Mario.Sprites
{
    public class SpriteTemplate : ISprite
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
        public int rows;
        public int columns;
        public int topCollisionOffset;
        public int rightCollisionOffset;
        public int leftCollisionOffset;
        public int bottomCollisionOffset;
        public int pipeRightCollisionOffset;
        public int pipeLeftCollisionOffset;

        public bool isShell { get; set; }

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public Rectangle Hitbox
        {
            get { return hitbox; }
            set { hitbox = value; }
        }
        public virtual bool ShowHitbox
        {
            get { return showHitbox; }
            set { showHitbox = value; }
        }

        public virtual bool Delete()
        {
            if (obtained) return true;
            else return false;
        }

        public void MakeHitbox(SpriteBatch spriteBatch, bool showHitbox)
        {
            if (showHitbox)
            {
                Texture2D hitboxTextureW = new Texture2D(spriteBatch.GraphicsDevice, hitbox.Width, 1);
                Texture2D hitboxTextureH = new Texture2D(spriteBatch.GraphicsDevice, 1, hitbox.Height);
                Color[] dataW = new Color[hitbox.Width];
                for (int i = 0; i < dataW.Length; i++) dataW[i] = Color.Blue;
                Color[] dataH = new Color[hitbox.Height];
                for (int i = 0; i < dataH.Length; i++) dataH[i] = Color.Blue;
                hitboxTextureW.SetData(dataW);
                hitboxTextureH.SetData(dataH);
                spriteBatch.Draw(hitboxTextureW, new Vector2((int)hitbox.X, (int)hitbox.Y), Color.White);
                spriteBatch.Draw(hitboxTextureW, new Vector2((int)hitbox.X, (int)hitbox.Y + (int)hitbox.Height), Color.White);
                spriteBatch.Draw(hitboxTextureH, new Vector2((int)hitbox.X, (int)hitbox.Y), Color.White);
                spriteBatch.Draw(hitboxTextureH, new Vector2((int)hitbox.X + (int)hitbox.Width, (int)hitbox.Y), Color.White);
            }
        }

        public virtual void Gravity()
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

        public virtual void SetHitbox()
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
            if (position.X < -16 || position.X > 3584 || position.Y > 300)
                obtained = true;
        }

        public virtual void DrawSprite(SpriteBatch spriteBatch, Rectangle sourceRectangle, int width, int height)
        {
            Rectangle destinationRectangle = new Rectangle((int)position.X, (int)position.Y, width, height);

            if (facingLeft) spriteBatch.Draw(textureLeft, destinationRectangle, sourceRectangle, Color.White);
            else if (!facingLeft && textureRight != null) spriteBatch.Draw(textureRight, destinationRectangle, sourceRectangle, Color.White);
            else spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, Color.White);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
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
                    DrawSprite(spriteBatch, sourceRectangle, width, height);
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

        public virtual void MarioCollision(ISprite collider)
        {
            if (collider is SuperMario)
            {
                obtained = true;
                hitbox = Rectangle.Empty;
                velocity.X = 0f;
                velocity.Y = 0f;
            }
        }

        public virtual void TopCollide(ISprite collider)
        {
            if (hitbox.TouchTopOf(collider.Hitbox))
            {
                hitbox.Y = collider.Hitbox.Y - hitbox.Height - topCollisionOffset;
                position.Y = hitbox.Y;
            }
        }

        public virtual void RightCollide(ISprite collider)
        {
            if (hitbox.TouchRightOf(collider.Hitbox))
            {
                hitbox.X = collider.Hitbox.X + hitbox.Width + rightCollisionOffset;
                position.X = hitbox.X;
                if (doesMove) horizontalDirection = !horizontalDirection;
            }
        }

        public virtual void LeftCollide(ISprite collider)
        {
            if (hitbox.TouchLeftOf(collider.Hitbox))
            {
                hitbox.X = collider.Hitbox.X - hitbox.Width - leftCollisionOffset;
                position.X = hitbox.X;
                if (doesMove) horizontalDirection = !horizontalDirection;
            }
        }

        public virtual void BottomCollide(ISprite collider)
        {
            if (hitbox.TouchBottomOf(collider.Hitbox))
            {
                hitbox.Y = collider.Hitbox.Y + hitbox.Height + bottomCollisionOffset;
                position.Y = hitbox.Y;
            }
        }

        public virtual void Collision(ISprite collider)
        {
            if (!spawning)
            {
                MarioCollision(collider);
                TopCollide(collider);
                RightCollide(collider);
                LeftCollide(collider);
                BottomCollide(collider);
            }
        }

        


    }
}
