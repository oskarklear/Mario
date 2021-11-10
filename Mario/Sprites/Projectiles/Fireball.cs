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
        Vector2 initPos;

        public Fireball(Game1 theatre, Vector2 location, SuperMario mario, bool xDirection)
        {
            texture = theatre.Content.Load<Texture2D>("projectiles/fireball");
            timeSinceLastFrame = 0;
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
            superMario = mario;
            initPos = position;
        }

        public override void Collision(ISprite collider)
        {
            maxUpwardLength = 30;
            poofStart = 0;

        }

        public void Collision(ISprite collider)
        {
            
            if (hitbox.TouchBottomOf(collider.Hitbox) || hitbox.TouchTopOf(collider.Hitbox))
            {
                if (collider is Goomba || collider is Koopa) isPoof = true;
                else upDown = !upDown;
            }

            if (hitbox.TouchLeftOf(collider.Hitbox) || hitbox.TouchRightOf(collider.Hitbox))
            {
                isPoof = true;
                //deleted = true;
            if (obtained) return true;
            else return false;
        }

        public override void Move()
        {
            if (horizontalDirection)
            {
                position.X -= 2;
            }
            else
            {
                position.X += 2;
            }

            // if fireball going up+right
            if (upDown)
            {
                position.Y += 1;
            }
            // if fireball going down+right
            else
            {
                position.Y -= 1;
            }

            if ((Math.Abs(position.X - initPos.X) > 1000)) obtained = true;

                count += 2;
                if (count > maxUpwardLength && !upDown)
                {
                    upDown = !upDown;
                    count = 0;
                }

                hitbox = new Rectangle((int)position.X + 5, (int)position.Y + 5, 10, 10);
            } else
            {
                poofStart += 1;
                if (poofStart > 20)
                {
                    deleted = true;
                }
            }
        }

        public override void Collision(ISprite collider)
        {
            if (hitbox.TouchBottomOf(collider.Hitbox) || hitbox.TouchTopOf(collider.Hitbox))
            {
                upDown = !upDown;
            }

            if (hitbox.TouchLeftOf(collider.Hitbox) || hitbox.TouchRightOf(collider.Hitbox))
            {
                obtained = true;
            }
        }
    }
}
