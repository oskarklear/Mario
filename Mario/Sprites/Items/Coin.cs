using Mario.Sprites.Mario;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using Mario.Trackers;

namespace Mario.Sprites.Items
{
    class BlockCoin : SpriteTemplate
    {
        int disappearPosition;
        bool topPositionReached;
        bool coinActive;

        public BlockCoin(Game1 theatre, Vector2 location)
        {
            texture = theatre.Content.Load<Texture2D>("items/coins");
            position = location;
            velocity.Y = 1f;
            velocity.X = 1f;
            hitbox = new Rectangle((int)position.X, (int)position.Y, 12, 16);
            showHitbox = false;
            obtained = false;
            doesMove = false;
            isAnimated = true;
            useGravity = false;
            endPosition = (int)position.Y - 40;
            timeSinceLastFrame = 0;
            millisecondsPerFrame = 5;
            currentFrame = 0;
            columns = 4;

            disappearPosition = (int)position.Y - 30;
            topPositionReached = false;
            coinActive = true;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            int width = texture.Width / columns;
            int height = texture.Height;
            int row = currentFrame / columns;
            int column = currentFrame % columns;

            Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);

            if (!obtained && coinActive)
            {
                DrawSprite(spriteBatch, sourceRectangle, width, height);
                MakeHitbox(spriteBatch, showHitbox);
            }
        }

        public override void Move()
        {
            if (position.Y > endPosition && !topPositionReached)
            {
                position.Y -= 2;
            }
            else if (position.Y == endPosition)
            {
                topPositionReached = !topPositionReached;
                position.Y += 2;
            }
            else if (position.Y < disappearPosition && topPositionReached)
            {
                position.Y += 2;
            }
            else if (position.Y == disappearPosition && topPositionReached)
            {
                coinActive = false;
            }
        }

        public override void Collision(ISprite collider)
        {
            MarioCollision(collider);
        }
    }


    class MapCoin : SpriteTemplate
    {
        ICommand AddCoinCommand { get; set; }
        public MapCoin(Game1 theatre, Vector2 location)
        {
            texture = theatre.Content.Load<Texture2D>("items/coins");
            position = location;
            hitbox = new Rectangle((int)position.X, (int)position.Y, 12, 16);
            showHitbox = false;
            obtained = false;
            doesMove = false;
            isAnimated = true;
            useGravity = false;
            timeSinceLastFrame = 0;
            millisecondsPerFrame = 5;
            currentFrame = 0;
            columns = 4;

            AddCoinCommand = new AddCoinCommand(theatre.tracker);
        }

        public override void SetHitbox()
        {
        }

        public override void Collision(ISprite collider)
        {
            if (collider is SuperMario)
            {
                obtained = true;
                hitbox = Rectangle.Empty;
                AddCoinCommand.Execute();
            }
        }
    }
}
