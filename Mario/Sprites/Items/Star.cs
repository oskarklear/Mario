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
    class Star : SpriteTemplate
    {
        int maxUpwardDistance;
        int spawnTime;
        int count;

        public Star(Game1 theatre, Vector2 location, SuperMario mario)
        {
            gameObj = theatre;
            texture = theatre.Content.Load<Texture2D>("items/stars");
            position = location;
            velocity.X = 1f;
            velocity.Y = 1f;
            hitbox = new Rectangle((int)location.X, (int)location.Y, 16, 16);
            showHitbox = false;
            obtained = false;
            spawning = true;
            verticalDirection = true;
            horizontalDirection = mario.Position.X < position.X ? true : false;
            doesMove = true;
            isAnimated = true;
            useGravity = true;
            spawnsFromBlock = true;
            endPosition = (int)position.Y - 13;
            timeSinceLastFrame = 0;
            millisecondsPerFrame = 3;
            currentFrame = 0;
            columns = 3;
            topCollisionOffset = 2;
            rightCollisionOffset = 2;
            leftCollisionOffset = 1;
            bottomCollisionOffset = 0;
            pipeRightCollisionOffset = 10;
            pipeLeftCollisionOffset = 5;

            maxUpwardDistance = 50;
            spawnTime = 0;
        }

        public override void SpawnFromBlock()
        {
            if (position.Y > endPosition && spawning)
            {
                position.Y -= 2;
                hitbox.Y -= 2;
            }

            if (spawnTime > 12)
            {
                spawning = false;
            }
        }

        public override void Move()
        {
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

        public override void Update()
        {
            spawnTime += 1;
            hitbox = new Rectangle((int)position.X, (int)position.Y, 16, 16);

            Animate();
            SetHitbox();
            SpawnFromBlock();
            Move();
        }

        public override void MarioCollision(ISprite collider)
        {
            if (collider is SuperMario)
            {
                obtained = true;
                hitbox = Rectangle.Empty;
                velocity.X = 0f;
                velocity.Y = 0f;
                gameObj.tracker.AddPointsCommand(1000);
            }
        }
        public override void TopCollide(ISprite collider)
        {
            if (hitbox.TouchTopOf(collider.Hitbox))
            {
                hitbox.Y = collider.Hitbox.Y - hitbox.Height - topCollisionOffset;
                position.Y = hitbox.Y;
                verticalDirection = true;
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

        public override void BottomCollide(ISprite collider)
        {
            if (hitbox.TouchBottomOf(collider.Hitbox))
            {
                hitbox.Y = collider.Hitbox.Y + hitbox.Height + bottomCollisionOffset;
                position.Y = hitbox.Y;
                verticalDirection = false;
            }
        }
    }
}