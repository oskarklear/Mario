using Mario.Sprites.Mario;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mario.Sprites.Items
{
    
    class RedMushroom : ISprite
    {
        Game1 Theatre;
        Texture2D texture;
        Vector2 position;
        bool obtained;
        public Rectangle DestinationRectangle { get; set; }
        public RedMushroom(Game1 theatre, Vector2 location)
        {
            position = location;
            Theatre = theatre;
            texture = Theatre.Content.Load<Texture2D>("items/red_mushroom");
            obtained = false;
            DestinationRectangle = new Rectangle((int)location.X, (int)location.Y, 18, 18);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (!obtained)
                spriteBatch.Draw(texture, position, Color.White);
        }

        public void Update()
        {
            if (obtained)
                DestinationRectangle = new Rectangle(-1, -1, 0, 0);
        }

        public void Collision(ISprite collider, int xoffset, int yoffset)
        {
            obtained = true;
            DestinationRectangle = new Rectangle(-1, -1, 0, 0);
        }
    }
}
