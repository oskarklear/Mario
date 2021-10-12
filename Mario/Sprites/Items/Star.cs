using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mario.Sprites.Items
{
    
    class Star : ISprite
    {
        int timeSinceLastFrame;
        int millisecondsPerFrame;
        int currentFrame;
        int Columns;
        bool obtained;
        ContentManager Content;
        Texture2D texture;
        Vector2 position;
        Rectangle hitbox;
        public Rectangle DestinationRectangle
        {
            get { return hitbox; }
            set { hitbox = value; }
        }
        public Star(Game1 theatre, Vector2 location)
        {
            timeSinceLastFrame = 0;
            millisecondsPerFrame = 3;
            currentFrame = 0;
            Columns = 3;
            position = location;
            texture = theatre.Content.Load<Texture2D>("items/stars");
            obtained = false;
            hitbox = new Rectangle((int)location.X, (int)location.Y, 16, 16);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            int width = texture.Width / Columns;
            int height = texture.Height;
            int row = currentFrame / Columns;
            int column = currentFrame % Columns;

            Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
            if (!obtained)
            {
                DestinationRectangle = new Rectangle((int)position.X, (int)position.Y, width, height);
                spriteBatch.Draw(texture, DestinationRectangle, sourceRectangle, Color.White);
            }
        }

        public void Update()
        {
            if (timeSinceLastFrame > millisecondsPerFrame)
            {
                currentFrame++;
                timeSinceLastFrame = 0;
            }
            if (currentFrame == Columns)
                currentFrame = 0;
            timeSinceLastFrame++;
        }
        public void Collision(ISprite collider, int xoffset, int yoffset)
        {
            obtained = true;
            hitbox = new Rectangle(-1, -1, 0, 0);
        }
    }
}
