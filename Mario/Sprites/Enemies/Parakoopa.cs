using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Mario.Sprites.Mario;
using Mario.Sprites.Items;
using Mario.Sprites.Projectiles;
using Mario.States;
using Mario.Entities;

namespace Mario.Sprites.Enemies
{
    class Parakoopa : SpriteTemplate
    {
        private int counter;

        public Parakoopa(Game1 theatre, Vector2 location)
        {
            gameObj = theatre;
            textureLeft = theatre.Content.Load<Texture2D>("enemies/parakoopa/parakoopa_left");
            textureRight = theatre.Content.Load<Texture2D>("enemies/parakoopa/parakoopa_right");
            texture = facingLeft ? textureLeft : textureRight;
            position = location;
            velocity.Y = 0.5f;
            velocity.X = 0f;
            hitbox = new Rectangle((int)location.X + 5, (int)location.Y, 16, 16);
            showHitbox = false;
            obtained = false;
            spawning = false;
            verticalDirection = true;
            doesMove = true;
            facingLeft = true;
            isAnimated = true;
            useGravity = false;
            timeSinceLastFrame = 0;
            millisecondsPerFrame = 15;
            currentFrame = 0;
            columns = 2;
            isShell = false;

            counter = 0;
        }

        public override void SetHitbox()
        {
            if (!obtained) hitbox = new Rectangle((int)position.X, (int)position.Y, 16, 26);
        }

        public override void Move()
        {
            if (counter < 200)
            {
                if (verticalDirection)
                {
                    position.Y += velocity.Y;
                }
                else
                {
                    position.Y -= velocity.Y;
                }
                counter++;
            }
            else
            {
                counter = 0;
                verticalDirection = !verticalDirection;
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
                gameObj.map.entities.NewKoopa(position);
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
