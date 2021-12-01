using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Mario.Sprites.Mario;
using Mario.Sprites.Items;

namespace Mario.Sprites.Enemies
{
    class KoopaShell : SpriteTemplate
    {
        public KoopaShell(Game1 theatre, Vector2 location)
        {
            gameObj = theatre;
            texture = theatre.Content.Load<Texture2D>("enemies/koopa/koopa_shell_green_init");
            position = location;
            velocity.Y = 2f;
            velocity.X = 0f;
            hitbox = new Rectangle((int)location.X, (int)location.Y, 16, 16);
            showHitbox = false;
            obtained = false;
            spawning = false;
            isAnimated = false;
            useGravity = true;
            spawnsFromBlock = false;
            pipeRightCollisionOffset = 17;
            pipeLeftCollisionOffset = 5;
        }

        public override void Move()
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


        public override void MarioCollision(ISprite collider)
        {
        }

        public override void TopCollide(ISprite collider)
        {
            if (hitbox.TouchTopOf(collider.Hitbox))
            {
                hitbox.Y = collider.Hitbox.Y - hitbox.Height - topCollisionOffset;
                position.Y = hitbox.Y;
                if (collider is SuperMario)
                {
                    horizontalDirection = gameObj.map.Mario.Position.X < position.X ? true : false;
                    velocity.X = 2f;
                }
                if (collider is Pipe)
                {
                    horizontalDirection = !horizontalDirection;
                    velocity.X = 2f;
                }
            }
        }

        public override void RightCollide(ISprite collider)
        {
            if (hitbox.TouchRightOf(collider.Hitbox))
            {
                if (collider is Pipe) hitbox.X = collider.Hitbox.X + hitbox.Width + pipeRightCollisionOffset;
                else hitbox.X = collider.Hitbox.X + hitbox.Width + rightCollisionOffset;
                position.X = hitbox.X;
                if (collider is SuperMario)
                {
                    horizontalDirection = true;
                    velocity.X = 2f;
                }
                if (collider is Pipe)
                {
                    horizontalDirection = !horizontalDirection;
                    velocity.X = 2f;
                }
            }
        }

        public override void LeftCollide(ISprite collider)
        {
            if (hitbox.TouchLeftOf(collider.Hitbox))
            {
                if (collider is Pipe) hitbox.X = collider.Hitbox.X - hitbox.Width - pipeLeftCollisionOffset;
                else hitbox.X = collider.Hitbox.X - hitbox.Width - leftCollisionOffset;
                position.X = hitbox.X;
                if (collider is SuperMario)
                {
                    horizontalDirection = false;
                    velocity.X = 2f;
                }
                if (collider is Pipe)
                {
                    horizontalDirection = !horizontalDirection;
                    velocity.X = 2f;
                }
            }
        }
    }
}
