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
        public CapeFeather(Game1 theatre, Vector2 location, SuperMario mario)
        {
            gameObj = theatre;
            textureLeft = theatre.Content.Load<Texture2D>("items/cape_featherL");
            textureRight = theatre.Content.Load<Texture2D>("items/cape_featherR");
            texture = facingLeft ? textureLeft : textureRight;
            position = location;
            velocity.X = 1f;
            velocity.Y = 0.5f;
            hitbox = new Rectangle((int)location.X, (int)location.Y, 16, 16);
            showHitbox = false;
            obtained = false;
            spawning = true;
            horizontalDirection = mario.Position.X < position.X ? true : false;
            doesMove = false;
            facingLeft = true;
            isAnimated = false;
            useGravity = true;
            spawnsFromBlock = true;
            endPosition = (int)position.Y - 13;
            topCollisionOffset = 2;
            rightCollisionOffset = 2;
            leftCollisionOffset = 2;
            bottomCollisionOffset = 0;
            pipeRightCollisionOffset = 10;
            pipeLeftCollisionOffset = 5;
        }
        public override void Gravity()
        {
            if (useGravity) position.Y += velocity.Y;
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
                MarioCollision(collider);
            }
        }
    }
}
