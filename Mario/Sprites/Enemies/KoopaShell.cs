using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Mario.Sprites.Enemies
{
    /*class koopaShell : ISprite
    {
        Texture2D texture;
        public Vector2 position;
        Rectangle hitbox;
        bool deleted;
        bool isMoving;
        int direction;
        int speed;

        public Rectangle Hitbox
        {
            get { return hitbox; }
        }

        public Vector2 Position
        {
            get { return position; }
        }

        private bool showHitbox;
        public bool ShowHitbox
        {
            get { return showHitbox; }
            set { showHitbox = value; }
        }
        public bool isShell { get; set; }

        public koopaShell(Game1 theatre, Vector2 location, int color)
        {
            position = location;

            if (color == 0) texture = theatre.Content.Load<Texture2D>("enemies/koopa/koopa_shell_green_init");
            else texture = theatre.Content.Load<Texture2D>("enemies/koopa/red_init");

            hitbox = new Rectangle((int)location.X, (int)location.Y, 10, 10);
            showHitbox = false;

            direction = 1;
            isMoving = false;
            speed = 2;
            //deleted = true;
        }

        public void Collision(ISprite collider)
        {
            if (hitbox.TouchTopOf(collider.Hitbox))
            {
                if (isMoving)
                {
                    isMoving = true;
                    direction = 1;
                } else
                {
                    isMoving = false;
                }
        
            }

            if (hitbox.TouchLeftOf(collider.Hitbox))
            {
                isMoving = true;
                direction = -1;
            }

            if (hitbox.TouchRightOf(collider.Hitbox))
            {
                isMoving = true;
                direction = 1;
            }

        }

        public bool Delete()
        {
            return false;
        }

        public void Update()
        {
            if (isMoving)
            {
                position.X += direction * speed;
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (!deleted)
            {
                spriteBatch.Draw(texture, position, Color.White);
            }
        }

    }*/

    class KoopaShell : SpriteTemplate
    {
        public KoopaShell(Game1 theatre, Vector2 location)
        {
            gameObj = theatre;
            texture = theatre.Content.Load<Texture2D>("enemies/koopa/koopa_shell_green_init");
            position = location;
            velocity.Y = 1f;
            velocity.X = 0f;
            hitbox = new Rectangle((int)location.X, (int)location.Y, 16, 16);
            showHitbox = false;
            obtained = false;
            spawning = false;
            isAnimated = false;
            useGravity = true;
            spawnsFromBlock = false;
        }

        public override void TopCollide(ISprite collider)
        {
            if (hitbox.TouchTopOf(collider.Hitbox))
            {
                hitbox.Y = collider.Hitbox.Y - hitbox.Height - topCollisionOffset;
                position.Y = hitbox.Y;
                horizontalDirection = gameObj.map.Mario.Position.X < position.X ? true : false;
                velocity.X = 2f;
            }
        }

        public override void RightCollide(ISprite collider)
        {
            if (hitbox.TouchRightOf(collider.Hitbox))
            {
                hitbox.X = collider.Hitbox.X + hitbox.Width + rightCollisionOffset;
                position.X = hitbox.X;
                horizontalDirection = true;
                velocity.X = 2f;
            }
        }

        public override void LeftCollide(ISprite collider)
        {
            if (hitbox.TouchLeftOf(collider.Hitbox))
            {
                hitbox.X = collider.Hitbox.X - hitbox.Width - leftCollisionOffset;
                position.X = hitbox.X;
                horizontalDirection = false;
                velocity.X = 2f;
            }
        }
    }
}
