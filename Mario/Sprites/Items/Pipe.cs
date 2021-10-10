using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mario.Sprites.Items
{

    class Pipe : ISprite
    {
        Texture2D texture;
        Vector2 position;
        public Rectangle DestinationRectangle { get; set; }
        public Pipe(Game1 theatre, Vector2 location)
        {
            position = location;
            texture = theatre.Content.Load<Texture2D>("obstacles/pipe");
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }

        public void Update()
        {

        }

        public void Collision(ISprite collider, int xoffset, int yoffset)
        {
            //TODO
        }
    }
}
