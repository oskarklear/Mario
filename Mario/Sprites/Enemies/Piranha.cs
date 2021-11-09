using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mario.Sprites.Enemies
{
    class Piranha : SpriteTemplate
    {
        bool up;
        int timer;
        int bPosition;
        int tPosition;
        public bool hiding;
        public Piranha(Game1 theatre, Vector2 location)
        {
            gameObj = theatre;
            texture = gameObj.Content.Load<Texture2D>("enemies/piranha");
            textureLeft = gameObj.Content.Load<Texture2D>("enemies/piranha");
            hitbox = new Rectangle((int)location.X + 2, (int)location.Y, 16, 21);
            up = false;
            timer = 100;
            position = location;
            tPosition = (int)position.Y;
            bPosition = (int)position.Y + 30;
            isAnimated = true;
            columns = 2;
            facingLeft = true;
            timeSinceLastFrame = 0;
            millisecondsPerFrame = 25;
            currentFrame = 0;
            hiding = false;
        }
        public override void Move()
        {
            if (hiding && timer <= 0)
            {
                if ((int)position.Y < bPosition)
                    position.Y++;
                else
                    up = true;
                
            }
            else if (!up && (int)position.Y < bPosition && timer <= 0)
            {
                position.Y++;
                if ((int)position.Y == bPosition)
                {
                    up = true;
                    timer = 100;
                }
            }
            else if (up && (int)position.Y > tPosition && timer <= 0)
            {
                position.Y--;
                if ((int)position.Y == tPosition)
                {
                    up = false;
                    timer = 100;
                }
            }
            timer--;
        }
        public override void SetHitbox()
        {
            hitbox = new Rectangle((int)position.X + 2, (int)position.Y, 16, 21);
        }
        public override void Collision(ISprite collider)
        {
            //No collision response
        }

    }
}
