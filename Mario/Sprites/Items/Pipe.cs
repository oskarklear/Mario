﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mario.Sprites.Items
{

    class Pipe : ISprite
    {
        ContentManager Content;
        Texture2D texture;
        Vector2 position;
        public Pipe()
        {
            position = new Vector2(400, 200);
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
            texture = Content.Load<Texture2D>("obstacles/pipe");
        }
    }
}