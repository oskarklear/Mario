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
        public Rectangle DestinationRectangle { get; set; }
        public RedMushroom(Game1 theatre, Vector2 location)
        {
            position = location;
            Theatre = theatre;
            texture = Theatre.Content.Load<Texture2D>("items/red_mushroom");
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }

        public void Update()
        {

        }
        public void LoadContent(ContentManager content)
        {
            Content = content;
            texture = Content.Load<Texture2D>("items/red_mushroom");
        }

        public void Collision(ISprite collider, int xoffset, int yoffset)
        {
            //TODO
        }
    }
}
