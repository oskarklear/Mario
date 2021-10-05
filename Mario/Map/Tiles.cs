using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mario.Map
{
    class Tiles
    {
        protected Texture2D texture;
        private Rectangle rectangle;
        public Rectangle Rectangle
        {
            get { return rectangle;}
            protected set { rectangle = value; }
        }
        private static ContentManager content;
        public static ContentManager Content
        {
            protected get { return content; }
            set { content = value; }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, Color.White);
        }
    }

    class CollisionTiles : Tiles
    {
        public CollisionTiles(int i, Rectangle rect)
        {
            switch (i)
            {
                case 1:
                    texture = Content.Load<Texture2D>("obstacles/GroundBlock");
                    break;
                case 2:
                    texture = Content.Load<Texture2D>("obstacles/Brick Block");
                    break;
                case 3:
                    texture = Content.Load<Texture2D>("obstacles/Item Block");
                    break;
            }
            
            this.Rectangle = rect;
        }
    }
}
