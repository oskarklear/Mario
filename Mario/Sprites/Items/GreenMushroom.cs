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
    class GreenMushroom : SpriteTemplate
    {
        public GreenMushroom(Game1 theatre, Vector2 location, SuperMario mario)
        {
            gameObj = theatre;
            texture = theatre.Content.Load<Texture2D>("items/green_mushroom");
            position = location;
            velocity.X = 1f;
            velocity.Y = 1f;
            hitbox = new Rectangle((int)location.X, (int)location.Y, 16, 16);
            showHitbox = false;
            obtained = false;
            spawning = true;
            horizontalDirection = mario.Position.X < position.X ? true : false;
            doesMove = true;
            isAnimated = false;
            useGravity = true;
            spawnsFromBlock = true;
            endPosition = (int)position.Y - 13;
            topCollisionOffset = 2;
            rightCollisionOffset = 2;
            leftCollisionOffset = 2;
            bottomCollisionOffset = 0;
            pipeRightCollisionOffset = 10;
            pipeLeftCollisionOffset = 5;
        }

        public override void MarioCollision(ISprite collider)
        {
            if (collider is SuperMario)
            {
                obtained = true;
                hitbox = Rectangle.Empty;
                velocity.X = 0f;
                velocity.Y = 0f;
                gameObj.tracker.AddLifeCommand();
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
    }
}
