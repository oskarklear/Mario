using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Mario.Sprites.Mario;
using Mario.Sprites.Enemies;


namespace Mario.Sprites.Projectiles
{
    class Fireball : SpriteTemplate
    {
        bool upDown;
        int count;
        int maxUpwardLength;
        int poofStart;
        bool isPoof;
        Vector2 initPos;
        public bool Deleted
        {
            set { obtained = value; }
        }
        Texture2D poofTexture;

        public Fireball(Game1 theatre, Vector2 location, SuperMario mario, bool xDirection)
        {
            texture = theatre.Content.Load<Texture2D>("projectiles/fireball");
            poofTexture = theatre.Content.Load<Texture2D>("projectiles/poof");
            timeSinceLastFrame = 0;
            millisecondsPerFrame = 15;
            currentFrame = 0;
            columns = 4;
            isAnimated = true;
            position = location;
            hitbox = new Rectangle((int)location.X + 5, (int)location.Y + 5, 10, 10);
            showHitbox = false;
            obtained = false;
            spawning = false;
            horizontalDirection = xDirection;
            doesMove = true;
            useGravity = false;
            upDown = true;
            count = 0;
            maxUpwardLength = 30;
            showHitbox = false;
            initPos = position;
            maxUpwardLength = 30;
            poofStart = 0;
        }

        public override void Collision(ISprite collider)
        {

            if (hitbox.TouchBottomOf(collider.Hitbox) || hitbox.TouchTopOf(collider.Hitbox))
            {
                if (collider is Goomba || collider is Koopa) isPoof = true;
                else upDown = !upDown;
            }

            if (hitbox.TouchLeftOf(collider.Hitbox) || hitbox.TouchRightOf(collider.Hitbox))
            {
                isPoof = true;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
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
                    if (!isPoof)
                    {
                        Rectangle DestinationRectangle = new Rectangle((int)position.X, (int)position.Y, width, height);
                        spriteBatch.Draw(texture, DestinationRectangle, sourceRectangle, Color.White);
                    }
                    else
                    {
                        Rectangle DestinationRectangle = new Rectangle((int)position.X, (int)position.Y, width, height);
                        spriteBatch.Draw(poofTexture, DestinationRectangle, sourceRectangle, Color.White);
                    }
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

        public override void Move()
        {
            if (timeSinceLastFrame > millisecondsPerFrame)
            {
                currentFrame++;
                timeSinceLastFrame = 0;
            }
            if (currentFrame == columns)
                currentFrame = 0;
            timeSinceLastFrame++;

            if (!isPoof)
            {
                if (horizontalDirection)
                {
                    position.X -= 5;
                }
                else
                {
                    position.X += 5;
                }

                // if fireball going up+right
                if (upDown)
                {
                    position.Y += 2;
                }
                // if fireball going down+right
                else
                {
                    position.Y -= 2;
                }

                if ((Math.Abs(position.X - initPos.X) > 250)) isPoof = true;

                count += 2;
                if (count > maxUpwardLength && !upDown)
                {
                    upDown = !upDown;
                    count = 0;
                }

                hitbox = new Rectangle((int)position.X + 5, (int)position.Y + 5, 10, 10);
            }
            else
            {
                poofStart += 1;
                if (poofStart > 20)
                {
                    obtained = true;
                }
            }
        }
    }
}

