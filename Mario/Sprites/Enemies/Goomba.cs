using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using Mario.States;
using Mario.Sprites.Mario;
using Mario.Sprites.Items;
using Mario.Sprites.Projectiles;

namespace Mario.Sprites.Enemies
{
    class Goomba : SpriteTemplate
    {
        public Goomba(Game1 theatre, Vector2 location)
        {
            gameObj = theatre;
            textureLeft = theatre.Content.Load<Texture2D>("enemies/goomba/goombaLeft");
            textureRight = theatre.Content.Load<Texture2D>("enemies/goomba/goombaRight");
            texture = facingLeft ? textureLeft : textureRight;
            position = location;
            velocity.Y = 1f;
            velocity.X = 0.5f;
            hitbox = new Rectangle((int)location.X + 5, (int)location.Y, 16, 16);
            showHitbox = false;
            obtained = false;
            horizontalDirection = false;
            doesMove = true;
            facingLeft = true;
            isAnimated = true;
            useGravity = true;
            spawnsFromBlock = false;
            timeSinceLastFrame = 0;
            millisecondsPerFrame = 15;
            currentFrame = 0;
            columns = 2;
            topCollisionOffset = 2;
            rightCollisionOffset = 2;
            leftCollisionOffset = 8;
            bottomCollisionOffset = 0;
            pipeRightCollisionOffset = 12;
            pipeLeftCollisionOffset = 10;
        }

        public override void SetHitbox()
        {
            if (!obtained) hitbox = new Rectangle((int)position.X + 5, (int)position.Y, 16, 16);
        }

        public override void Move()
        {
            if (horizontalDirection)
            {
                position.X += velocity.X;
                facingLeft = false;
            }
            else
            {
                position.X -= velocity.X;
                facingLeft = true;
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
                gameObj.tracker.AddPointsCommand(100);
            }
        }

        public override void TopCollide(ISprite collider)
        {
            if (hitbox.TouchTopOf(collider.Hitbox))
            {
                hitbox.Y = collider.Hitbox.Y - hitbox.Height - topCollisionOffset;
                position.Y = hitbox.Y;
                velocity.Y = 0f;
            }
            else
            {
                velocity.Y = 1f;
            }
        }

        public override void RightCollide(ISprite collider)
        {
            if (hitbox.TouchRightOf(collider.Hitbox))
            {
                if (collider is Pipe) hitbox.X = collider.Hitbox.X + hitbox.Width + pipeRightCollisionOffset;
                else hitbox.X = collider.Hitbox.X + hitbox.Width + rightCollisionOffset;
                position.X = hitbox.X;
                horizontalDirection = !horizontalDirection;
                velocity.Y = 0f;
            }
            else
            {
                velocity.Y = 1f;
            }
        }

        public override void LeftCollide(ISprite collider)
        {
            if (hitbox.TouchLeftOf(collider.Hitbox))
            {
                if (collider is Pipe) hitbox.X = collider.Hitbox.X - hitbox.Width - pipeLeftCollisionOffset;
                else hitbox.X = collider.Hitbox.X - hitbox.Width - leftCollisionOffset;
                position.X = hitbox.X;
                horizontalDirection = !horizontalDirection;
            }
        }

        public override void Collision(ISprite collider)
        {
            MarioCollision(collider);

            if (collider is Fireball)
            {
                if (hitbox.TouchTopOf(collider.Hitbox) || hitbox.TouchBottomOf(collider.Hitbox) || hitbox.TouchLeftOf(collider.Hitbox) || hitbox.TouchRightOf(collider.Hitbox))
                {
                    obtained = true;
                    (collider as Fireball).Deleted = true;
                    hitbox = Rectangle.Empty;
                    velocity.X = 0f;
                    velocity.Y = 0f;
                }
            }

            TopCollide(collider);
            RightCollide(collider);
            LeftCollide(collider);
            BottomCollide(collider);
        }
    }
}
