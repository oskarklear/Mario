using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Mario.Sprites.Mario;
using Mario.Sprites.Enemies;


namespace Mario.Sprites.Projectiles
{
    class Fireball : ISprite
    {
        int poofStart;
        bool isPoof;
        int timeSinceLastFrame;
        int millisecondsPerFrame;
        int currentFrame;
        int columns;
        Texture2D texture;
        Texture2D poofTexture;
        Vector2 initPos;
        public bool ShowHitbox { get; set; }
        public Vector2 position;
        Rectangle hitbox;
        SuperMario superMario;
        int maxUpwardLength;
        int count;
        public Rectangle Hitbox
        {
            get { return hitbox; }
        }

        public bool Deleted
        {
            set { isPoof = value; }
        }

        bool upDown;
        bool leftRight;
        bool deleted;
        public Vector2 Position
        {
            get { return position; }
        }
        public bool isShell { get; set; }
        private bool showHitbox;



        public Fireball(Game1 theatre, Vector2 location, SuperMario mario, bool xDirection)
        {
            timeSinceLastFrame = 0;
            millisecondsPerFrame = 5;
            currentFrame = 0;
            columns = 4;
            position = location;
            initPos = position;
            texture = theatre.Content.Load<Texture2D>("projectiles/fireball");
            poofTexture = theatre.Content.Load<Texture2D>("projectiles/poof");
            hitbox = new Rectangle((int)location.X + 5, (int)location.Y + 5, 10, 10);
            showHitbox = false;
            superMario = mario;
            upDown = true;
            deleted = false;
            count = 0;
            leftRight = xDirection;
            maxUpwardLength = 30;
            poofStart = 0;

        }

        public void Collision(ISprite collider)
        {
            
            if (hitbox.TouchBottomOf(collider.Hitbox) || hitbox.TouchTopOf(collider.Hitbox))
            {
                if (collider is Goomba || collider is Koopa) isPoof = true;
                else upDown = !upDown;
            }

            if (hitbox.TouchLeftOf(collider.Hitbox) || hitbox.TouchRightOf(collider.Hitbox))
            {
                isPoof = true;
                //deleted = true;
            }

        }

        public bool delete()
        {
            if (deleted) return true;
            else return false;
        }

        public void Update()
        {

            if (timeSinceLastFrame > millisecondsPerFrame)
            {
                currentFrame++;
                timeSinceLastFrame = 0;
            }
            if (currentFrame == columns)
                currentFrame = 0;
            timeSinceLastFrame++;

            if (!isPoof)
            {
                if (leftRight)
                {
                    position.X -= 5;
                }
                else
                {
                    position.X += 5;
                }

                // if fireball going up+right
                if (upDown)
                {
                    position.Y += 2;
                }
                // if fireball going down+right
                else
                {
                    position.Y -= 2;
                }

                if ((Math.Abs(position.X - initPos.X) > 250)) isPoof = true;

                count += 2;
                if (count > maxUpwardLength && !upDown)
                {
                    upDown = !upDown;
                    count = 0;
                }

                hitbox = new Rectangle((int)position.X + 5, (int)position.Y + 5, 10, 10);
            } else
            {
                poofStart += 1;
                if (poofStart > 20)
                {
                    deleted = true;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {

            int width = texture.Width / columns;
            int height = texture.Height;
            int row = currentFrame / columns;
            int column = currentFrame % columns;
            Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);

            if (!deleted)
            {
                if (!isPoof)
                {
                    Rectangle DestinationRectangle = new Rectangle((int)position.X, (int)position.Y, width, height);
                    spriteBatch.Draw(texture, DestinationRectangle, sourceRectangle, Color.White);
                } else
                {
                    Rectangle DestinationRectangle = new Rectangle((int)position.X, (int)position.Y, width, height);
                    spriteBatch.Draw(poofTexture, DestinationRectangle, sourceRectangle, Color.White);
                }
            }

            if (showHitbox)
            {
                Texture2D hitboxTextureW = new Texture2D(spriteBatch.GraphicsDevice, hitbox.Width, 1);
                Texture2D hitboxTextureH = new Texture2D(spriteBatch.GraphicsDevice, 1, hitbox.Height);
                Color[] dataW = new Color[hitbox.Width];
                for (int i = 0; i < dataW.Length; i++) dataW[i] = Color.Red;
                Color[] dataH = new Color[hitbox.Height];
                for (int i = 0; i < dataH.Length; i++) dataH[i] = Color.Red;
                hitboxTextureW.SetData(dataW);
                hitboxTextureH.SetData(dataH);
                spriteBatch.Draw(hitboxTextureW, new Vector2((int)hitbox.X, (int)hitbox.Y), Color.White);
                spriteBatch.Draw(hitboxTextureW, new Vector2((int)hitbox.X, (int)hitbox.Y + (int)hitbox.Height), Color.White);
                spriteBatch.Draw(hitboxTextureH, new Vector2((int)hitbox.X, (int)hitbox.Y), Color.White);
                spriteBatch.Draw(hitboxTextureH, new Vector2((int)hitbox.X + (int)hitbox.Width, (int)hitbox.Y), Color.White);
            }
        }
    }
}
