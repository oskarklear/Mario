﻿using Mario.Sprites.Mario;
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
        //Texture2D hitboxTexture;
        public Vector2 position;
        Vector2 velocity;
        bool obtained;
        SuperMario mario;
        MarioContext context;
        Boolean isActive;
        Rectangle hitbox;
        public Rectangle Hitbox 
        {
            get { return hitbox; }
        
        }
        private bool showHitbox;
        public bool ShowHitbox
        {
            get { return showHitbox; }
            set { showHitbox = value; }
        }

        public RedMushroom(Game1 theatre, Vector2 location)
        {
            position = location;
            Theatre = theatre;
            texture = Theatre.Content.Load<Texture2D>("items/red_mushroom");
            obtained = false;
            hitbox = new Rectangle((int)location.X, (int)location.Y, 18, 18);
            showHitbox = false;
            isActive = false;
            //mario = new SuperMario(theatre, mario.position, context);
            
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            //if (isActive)
            //{
                if (!obtained)
                {
                    spriteBatch.Draw(texture, position, Color.White);






                    if (showHitbox)
                    {
                        Texture2D hitboxTextureW = new Texture2D(spriteBatch.GraphicsDevice, hitbox.Width, 1);
                        Texture2D hitboxTextureH = new Texture2D(spriteBatch.GraphicsDevice, 1, hitbox.Height);
                        Color[] dataW = new Color[hitbox.Width];
                        for (int i = 0; i < dataW.Length; i++) dataW[i] = Color.Green;
                        Color[] dataH = new Color[hitbox.Height];
                        for (int i = 0; i < dataH.Length; i++) dataH[i] = Color.Green;
                        hitboxTextureW.SetData(dataW);
                        hitboxTextureH.SetData(dataH);
                        spriteBatch.Draw(hitboxTextureW, new Vector2((int)hitbox.X, (int)hitbox.Y), Color.White);
                        spriteBatch.Draw(hitboxTextureW, new Vector2((int)hitbox.X, (int)hitbox.Y + (int)hitbox.Height), Color.White);
                        spriteBatch.Draw(hitboxTextureH, new Vector2((int)hitbox.X, (int)hitbox.Y), Color.White);
                        spriteBatch.Draw(hitboxTextureH, new Vector2((int)hitbox.X + (int)hitbox.Width, (int)hitbox.Y), Color.White);
                    }
                }
            //}
        }

        public void Update()
        {
            if (obtained)
                hitbox = new Rectangle(-1, -1, 0, 0);
            //if (mario.position.X < position.X)
            //{
                position.X -= 1;
                hitbox.X -= 1;
            //}
            //else
            //{
            //    position.X += 1;
            //    hitbox.X += 1;
            //}
        }

        private void MoveLeft(Vector2 velocity, Vector2 position)
        {

        }

        public void Collision(ISprite collider, int xoffset, int yoffset)
        {
            obtained = true;
            hitbox = new Rectangle(-1, -1, 0, 0);
        }
        
    }
}
