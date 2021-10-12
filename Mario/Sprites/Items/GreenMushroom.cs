using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mario.Sprites.Items
{
    
    class GreenMushroom : ISprite
    {
        Texture2D texture;
        Vector2 position;
        bool obtained;
        Rectangle hitbox;
        public Rectangle DestinationRectangle
        {
            get { return hitbox; }
            set { hitbox = value; }
        }
        public GreenMushroom(Game1 theatre, Vector2 location)
        {
            texture = theatre.Content.Load<Texture2D>("items/green_mushroom");
            position = location;
            obtained = false;
            hitbox = new Rectangle((int)location.X, (int)location.Y, 16, 16);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (!obtained)
                spriteBatch.Draw(texture, position, Color.White);
        }

        public void Update()
        {

        }
        public void Collision(ISprite collider, int xoffset, int yoffset)
        {
            obtained = true;
            hitbox = new Rectangle(-1, -1, 0, 0);
        }
    }
}
