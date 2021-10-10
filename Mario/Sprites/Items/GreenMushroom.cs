﻿using Microsoft.Xna.Framework;
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
        public Rectangle DestinationRectangle { get; set; }
        public GreenMushroom(Game1 theatre, Vector2 location)
        {
            texture = theatre.Content.Load<Texture2D>("items/green_mushroom");
            position = location;
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
            texture = Content.Load<Texture2D>("items/green_mushroom");
        }

        public void Collision(ISprite collider, int xoffset, int yoffset)
        {
           //TODO
        }
    }
}
