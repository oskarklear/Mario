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
    class FireFlower : SpriteTemplate
    {
        public FireFlower(Game1 theatre, Vector2 location)
        {
            gameObj = theatre;
            texture = gameObj.Content.Load<Texture2D>("items/fire_flower");
            position = location;
            hitbox = new Rectangle((int)location.X, (int)location.Y, 16, 16);
            showHitbox = false;
            obtained = false;
            spawning = true;
            doesMove = false;
            isAnimated = true;
            useGravity = false;
            spawnsFromBlock = true;
            endPosition = (int)position.Y - 12;
            timeSinceLastFrame = 0;
            millisecondsPerFrame = 15;
            currentFrame = 0;
            columns = 2;
            topCollisionOffset = 2;
            rightCollisionOffset = 1;
            leftCollisionOffset = 1;
            bottomCollisionOffset = 0;
        }
        public override void MarioCollision(ISprite collider)
        {
            if (collider is SuperMario)
            {
                obtained = true;
                hitbox = Rectangle.Empty;
                gameObj.tracker.AddPointsCommand(1000);
            }
        }
    }
}

