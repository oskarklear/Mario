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
            //horizontalDirection = false;
            doesMove = true;
            facingLeft = true;
            isAnimated = true;
            useGravity = false;
            //spawnsFromBlock = false;
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
            //System.Diagnostics.Debug.WriteLine("Koopa: " + hitbox.X + ", " + hitbox.Y);
            if (counter < 100)
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
            //System.Diagnostics.Debug.WriteLine("hello");
            if (collider is SuperMario)
            {
                obtained = true;
                hitbox = Rectangle.Empty;
                velocity.X = 0f;
                velocity.Y = 0f;
                gameObj.map.entities.NewKoopa(position);
                System.Diagnostics.Debug.WriteLine("TOUCHING");
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

        /*public override void Collision(ISprite collider)
        {
            if (collider is SuperMario)
            {
                //dead = true;
                //hitbox = new Rectangle(-1, -1, 0, 0);
                gameObj.tracker.AddPointsCommand(100);
                System.Diagnostics.Debug.WriteLine("KOOPA TOP: " + hitbox.Top);
                System.Diagnostics.Debug.WriteLine("MARIO BOTTOM: " + collider.Hitbox.Bottom);
                System.Diagnostics.Debug.WriteLine("KOOPA RIGHT: " + hitbox.Right);
                System.Diagnostics.Debug.WriteLine("MARIO LEFT: " + collider.Hitbox.Left);
                if (hitbox.TouchTopOf(collider.Hitbox))
                {
                    //isShell = true;
                }
                if (hitbox.Top <= collider.Hitbox.Bottom &&
                hitbox.Top >= collider.Hitbox.Bottom - 2 &&
                hitbox.Right >= collider.Hitbox.Left + (collider.Hitbox.Width / 5) &&
                hitbox.Left <= collider.Hitbox.Right - (collider.Hitbox.Width / 5))
                {
                    System.Diagnostics.Debug.WriteLine("");
                    velocity.X = 0f;
                    velocity.Y = 0f;
                    isShell = true;
                }
                if (hitbox.Right <= collider.Hitbox.Right &&
                hitbox.Right >= collider.Hitbox.Left - 4 &&
                hitbox.Top <= collider.Hitbox.Bottom - (collider.Hitbox.Width / 4) &&
                hitbox.Bottom >= collider.Hitbox.Top + (collider.Hitbox.Width / 4))
                {
                    System.Diagnostics.Debug.WriteLine("");
                    isMoving = true;
                    shellDirection = -1;
                }
                if (hitbox.Left >= collider.Hitbox.Left &&
                hitbox.Left <= collider.Hitbox.Right &&
                hitbox.Top <= collider.Hitbox.Bottom - (collider.Hitbox.Width / 4) &&
                hitbox.Bottom >= collider.Hitbox.Top + (collider.Hitbox.Width / 4))
                {
                    System.Diagnostics.Debug.WriteLine("");
                    isMoving = true;
                    shellDirection = 1;
                }

            }

            if (collider is Fireball)
            {
                if (hitbox.TouchTopOf(collider.Hitbox) || hitbox.TouchBottomOf(collider.Hitbox) || hitbox.TouchLeftOf(collider.Hitbox) || hitbox.TouchRightOf(collider.Hitbox))
                {
                    //dead = true;
                    obtained = true;
                    (collider as Fireball).Deleted = true;
                    velocity.X = 0f;
                    velocity.Y = 0f;
                }

            }

            if (collider is BlockContext || collider is Pipe)
            {
                if (hitbox.TouchTopOf(collider.Hitbox))
                {
                    hitbox.Y = collider.Hitbox.Y - hitbox.Height - 2;
                    position.Y = hitbox.Y;
                    velocity.Y = 0f;
                    falling = false;
                }
                else
                {
                    velocity.Y = 1f;
                    falling = true;
                }

                if (hitbox.TouchRightOf(collider.Hitbox))
                {
                    if (collider is Pipe)
                    {
                        if (!isShell)
                            hitbox.X = collider.Hitbox.X + hitbox.Width + 10;
                        else
                            hitbox.X = collider.Hitbox.X + hitbox.Width + 30;
                    }
                    else hitbox.X = collider.Hitbox.X + hitbox.Width + 2;
                    position.X = hitbox.X;
                    //if (!falling)
                    horizontalDirection = !horizontalDirection;
                }

                if (hitbox.TouchLeftOf(collider.Hitbox))
                {
                    if (collider is Pipe)
                    {
                        if (!isShell)
                            hitbox.X = collider.Hitbox.X - hitbox.Width - 10;
                        else
                            hitbox.X = collider.Hitbox.X - hitbox.Width - 30;
                    }
                    else hitbox.X = collider.Hitbox.X - hitbox.Width - 8;
                    position.X = hitbox.X;
                    //if (!falling)
                    horizontalDirection = !horizontalDirection;
                }

                if (hitbox.TouchBottomOf(collider.Hitbox))
                {
                    hitbox.Y = collider.Hitbox.Y + hitbox.Height;
                    position.Y = hitbox.Y;
                }
            }
        }*/
    }
}
