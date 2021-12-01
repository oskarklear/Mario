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
    class CapeFeather : SpriteTemplate
    {
        private float displacement;
        private const int RIGHTLIMIT = 20;
        private const int LEFTLIMIT = -20;
        private const int UPPERLIMIT = 60;
        public CapeFeather(Game1 theatre, Vector2 location, SuperMario mario)
        {
            gameObj = theatre;
            textureLeft = theatre.Content.Load<Texture2D>("items/cape_featherL");
            textureRight = theatre.Content.Load<Texture2D>("items/cape_featherR");
            texture = textureLeft;
            position = location;
            velocity.X = 1f;
            velocity.Y = 0.5f;
            hitbox = new Rectangle((int)location.X, (int)location.Y, 16, 16);
            showHitbox = false;
            obtained = false;
            spawning = true;
            horizontalDirection = false;
            doesMove = true;
            facingLeft = true;
            isAnimated = false;
            useGravity = true;
            spawnsFromBlock = true;
            endPosition = (int)position.Y - UPPERLIMIT;
            rows = 1;
            columns = 1;
            topCollisionOffset = 2;
            rightCollisionOffset = 2;
            leftCollisionOffset = 2;
            bottomCollisionOffset = 0;
            pipeRightCollisionOffset = 10;
            pipeLeftCollisionOffset = 5;
            displacement = 0;
        }

        public override void SpawnFromBlock()
        {
            if (spawnsFromBlock)
            {
                if (position.Y > endPosition && spawning)
                {
                    position.Y -= 5;
                    hitbox.Y -= 5;
                }
                else if (position.Y == endPosition && spawning)
                {
                    spawning = false;
                }
            }
        }

        public override void Move()
        {
            if (doesMove)
            {
                if (horizontalDirection && !spawning)
                {
                    position.X += velocity.X - (displacement / (2*RIGHTLIMIT));
                    displacement += velocity.X - (displacement / (2 * RIGHTLIMIT)) ;
                    if (displacement > RIGHTLIMIT)
                    {
                        horizontalDirection = false;
                    }
                }
                else if (!horizontalDirection && !spawning)
                {
                    position.X -= velocity.X - (-displacement / (2 * RIGHTLIMIT));
                    displacement -= velocity.X - (-displacement / (2 * RIGHTLIMIT));
                    if (displacement < LEFTLIMIT)
                    {
                        horizontalDirection = true;
                    }
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {

            if (!obtained)
            {
                if (horizontalDirection)
                {
                    spriteBatch.Draw(textureRight, position, Color.White);
                }
                else
                {
                    spriteBatch.Draw(textureLeft, position, Color.White);
                }
                MakeHitbox(spriteBatch, showHitbox);
            }
        }

        public override void MarioCollision(ISprite collider)
        {
            if (collider is SuperMario)
            {
                obtained = true;
                hitbox = Rectangle.Empty;
                velocity.X = 0f;
                velocity.Y = 0f;
            }
        }
        public override void Collision(ISprite collider)
        {
            if (!spawning)
            {               
                if (position.Y >= 300)
                {
                    obtained = true;
                }
                MarioCollision(collider);
            }
        }
    }
}
