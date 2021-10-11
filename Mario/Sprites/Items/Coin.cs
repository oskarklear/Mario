using Mario.Sprites.Mario;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mario.Sprites.Items
{
    class Coin : ISprite
    {
        int timeSinceLastFrame;
        int millisecondsPerFrame;
        int currentFrame;
        int Columns;
        Texture2D texture;
        Vector2 position;
        public Rectangle DestinationRectangle { get; set; }
        bool obtained;
        public Coin(Game1 theatre, Vector2 location)
        {
            timeSinceLastFrame = 0;
            millisecondsPerFrame = 5;
            currentFrame = 0;
            Columns = 4;
            position = location;
            texture = theatre.Content.Load<Texture2D>("items/coins");
            obtained = false;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            int width = texture.Width / Columns;
            int height = texture.Height;
            int row = currentFrame / Columns;
            int column = currentFrame % Columns;

            Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
            if (!obtained)
                DestinationRectangle = new Rectangle((int)position.X, (int)position.Y, width, height);
            else
                DestinationRectangle = new Rectangle(-1, -1, 0, 0);
            spriteBatch.Draw(texture, DestinationRectangle, sourceRectangle, Color.White);
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
            if (DestinationRectangle.TouchTopOf(collider.DestinationRectangle) || DestinationRectangle.TouchRightOf(collider.DestinationRectangle)
                || DestinationRectangle.TouchLeftOf(collider.DestinationRectangle) || DestinationRectangle.TouchBottomOf(collider.DestinationRectangle))
            {
                //System.Diagnostics.Debug.WriteLine("collision");
                if (collider is SuperMario)
                {
                    obtained = true;
                }
            }
         }
     }
}
