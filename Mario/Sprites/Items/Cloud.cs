using Mario.Sprites.Mario;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mario.Sprites.Items
{

    class Cloud : ISprite
    {
        Game1 Theatre;
        Texture2D texture;
        Vector2 position;
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
        public Vector2 Position
        {
            get { return position; }
        }

        public bool delete()
        {
            return false;
        }
        public Cloud(Game1 theatre, Vector2 location)
        {
            position = location;
            Theatre = theatre;
            texture = Theatre.Content.Load<Texture2D>("BackgroundEntities/cloudBoi");

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }

        public void Update()
        {

        }

        public void Collision(ISprite collider)
        {
            //Does nothing
        }
    }
}
