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
        bool showHitbox;
        public bool ShowHitbox
        {
            get { return showHitbox; }
            set { showHitbox = value; }
        }
        Rectangle hitbox;
        public Rectangle Hitbox
        {
            get { return hitbox; }
            set { hitbox = value; }
        }
        public bool Delete()
        {
            return false;
        }
        public Vector2 Position
        {
            get { return position; }
        }
        public bool isShell { get; set; }
        public Pipe(Game1 theatre, Vector2 location)
        {
            position = location;
            texture = theatre.Content.Load<Texture2D>("obstacles/pipe");
            Hitbox = new Rectangle((int)location.X, (int)location.Y, 32, 33);
            showHitbox = false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
            if (showHitbox)
            {
                Texture2D hitboxTextureW = new Texture2D(spriteBatch.GraphicsDevice, hitbox.Width, 1);
                Texture2D hitboxTextureH = new Texture2D(spriteBatch.GraphicsDevice, 1, hitbox.Height);
                Color[] dataW = new Color[hitbox.Width];
                for (int i = 0; i < dataW.Length; i++) dataW[i] = Color.Blue;
                Color[] dataH = new Color[hitbox.Height];
                for (int i = 0; i < dataH.Length; i++) dataH[i] = Color.Blue;
                hitboxTextureW.SetData(dataW);
                hitboxTextureH.SetData(dataH);
                spriteBatch.Draw(hitboxTextureW, new Vector2((int)hitbox.X, (int)hitbox.Y), Color.White);
                spriteBatch.Draw(hitboxTextureW, new Vector2((int)hitbox.X, (int)hitbox.Y + (int)hitbox.Height), Color.White);
                spriteBatch.Draw(hitboxTextureH, new Vector2((int)hitbox.X, (int)hitbox.Y), Color.White);
                spriteBatch.Draw(hitboxTextureH, new Vector2((int)hitbox.X + (int)hitbox.Width, (int)hitbox.Y), Color.White);
            }
        }

        public void Update()
        {

        }

        public void Collision(ISprite collider)
        {
            //TODO
        }
        
    }
}
