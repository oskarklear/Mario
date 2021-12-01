using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using Mario.States;
using Mario.Sprites.Mario;
using Mario.Sprites.Items;
using Mario.Map;
using Mario.Sprites.Projectiles;

namespace Mario.Sprites.Enemies
{
    class Koopa : SpriteTemplate
    {
        Texture2D shellTexture;
        bool isMoving;
        bool falling;
        int shellDirection;
        int shellSpeed;
        bool dead;
        int invincibility;

        public Koopa(Game1 theatre, Vector2 location)
        {
            gameObj = theatre;
            textureLeft = theatre.Content.Load<Texture2D>("enemies/koopa/koopa_green_leftWalking");
            textureRight = theatre.Content.Load<Texture2D>("enemies/koopa/koopa_green_rightWalking");
            shellTexture = theatre.Content.Load<Texture2D>("enemies/koopa/koopa_shell_green_init");
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
            isShell = false;
            dead = false;
            shellDirection = 1;
            shellSpeed = 2;
            invincibility = 10;
            topCollisionOffset = 2;
            rightCollisionOffset = 2;
            leftCollisionOffset = 8;
            bottomCollisionOffset = 0;
            pipeRightCollisionOffset = 12;
            pipeLeftCollisionOffset = 10;
        }

        public override void DrawSprite(SpriteBatch spriteBatch, Rectangle sourceRectangle, int width, int height)
        {
            Rectangle destinationRectangle = new Rectangle((int)position.X, (int)position.Y, width, height);

            if (!isShell)
            {
                if (facingLeft) spriteBatch.Draw(textureLeft, destinationRectangle, sourceRectangle, Color.White);
                else spriteBatch.Draw(textureRight, destinationRectangle, sourceRectangle, Color.White);

                MakeHitbox(spriteBatch, showHitbox);
            }
            else
            {
                columns = 1;
                spriteBatch.Draw(shellTexture, position, Color.White);
            }

        }

        public override void SetHitbox()
        {
            if (!obtained) hitbox = new Rectangle((int)position.X + 7, (int)position.Y, 16, 26);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            int width = texture.Width / columns;
            int height = texture.Height;
            int row = currentFrame / columns;
            int column = currentFrame % columns;

            Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);

            if (!obtained)
            {
                DrawSprite(spriteBatch, sourceRectangle, width, height);
            }
        }

        public override void Move()
        {
            //System.Diagnostics.Debug.WriteLine("Koopa: " + hitbox.X + ", " + hitbox.Y);
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

        public override void Update()
        {
            if (position.X < -16 || position.X > 3584 || position.Y > 300)
                obtained = true;
            if (invincibility > 0)
            {
                invincibility--;
            }

            if (!isShell)
            {
                Gravity();
                Animate();
                SetHitbox();
                Move();
            }
            else
            {
                if (isMoving)
                {
                    position.Y += velocity.Y;
                    position.X += shellDirection * shellSpeed;
                    hitbox = new Rectangle((int)position.X, (int)position.Y, 8, 8);
                }
            }
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
                    dead = true;
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

        public override void MarioCollision(ISprite collider)
        {
            if (collider is SuperMario && invincibility <= 0)
            {
                obtained = true;
                hitbox = Rectangle.Empty;
                velocity.X = 0f;
                velocity.Y = 0f;
                gameObj.map.entities.NewKoopaShell(position);
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
