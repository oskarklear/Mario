using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using Mario.States;
using Mario.Sprites.Mario;
using Mario.Sprites.Items;

namespace Mario.Sprites.Enemies
{
    class Koopa : ISprite
    {
        int timeSinceLastFrame;
        int millisecondsPerFrame;
        int currentFrame;
        int Columns;
        Texture2D textureLeft;
        Texture2D textureRight;
        Vector2 position;
        Vector2 velocity;
        bool direction;
        bool facingLeft;
        Game1 Theatre;
        bool dead;
        bool colliding;
        public Vector2 Position
        {
            get { return position; }
        }
        Rectangle hitbox;
        public Rectangle Hitbox
        {
            get { return hitbox; }
            set { hitbox = value; }
        }
        private bool showHitbox;
        public bool ShowHitbox
        {
            get { return showHitbox; }
            set { showHitbox = value; }
        }
        public bool delete()
        {
            return false;
        }

        public Koopa(Game1 theatre, Vector2 location)
        {
            timeSinceLastFrame = 0;
            millisecondsPerFrame = 15;
            currentFrame = 0;
            Columns = 2;
            position = location;
            Theatre = theatre;
            textureLeft = Theatre.Content.Load<Texture2D>("enemies/koopa/koopa_green_leftWalking");
            textureRight = Theatre.Content.Load<Texture2D>("enemies/koopa/koopa_green_rightWalking");
            hitbox = new Rectangle((int)location.X + 7, (int)location.Y, 16, 26);
            dead = false;
            showHitbox = false;
            direction = false;
            facingLeft = true;
            velocity.Y = 1f;
            velocity.X = 0.5f;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            int width = textureLeft.Width / Columns;
            int height = textureLeft.Height;
            int row = currentFrame / Columns;
            int column = currentFrame % Columns;

            Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);

            if (!dead)
            {
                Rectangle destinationRectangle = new Rectangle((int)position.X, (int)position.Y, width, height);
                if (facingLeft) spriteBatch.Draw(textureLeft, destinationRectangle, sourceRectangle, Color.White);
                else spriteBatch.Draw(textureRight, destinationRectangle, sourceRectangle, Color.White);

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

        public void Update()
        {
            position.Y += velocity.Y;
            hitbox = new Rectangle((int)position.X + 7, (int)position.Y, 16, 26);

            System.Diagnostics.Debug.WriteLine("X-VELOCITY: " + velocity.X);
            System.Diagnostics.Debug.WriteLine("Y-VELOCITY: " + velocity.Y);

            if (timeSinceLastFrame > millisecondsPerFrame)
            {
                currentFrame++;
                timeSinceLastFrame = 0;
            }
            if (currentFrame == Columns)
                currentFrame = 0;
            timeSinceLastFrame++;

            if (direction)
            {
                position.X += velocity.X;
                facingLeft = false;
            }
            else
            {
                position.X -= velocity.X;
                facingLeft = true;
            }
        }

        public void Collision(ISprite collider)
        {
            if (collider is SuperMario)
            {
                dead = true;
                hitbox = new Rectangle(-1, -1, 0, 0);
                velocity.X = 0f;
                velocity.Y = 0f;
            }

            if (collider is BlockContext || collider is Pipe)
            {
                if (hitbox.TouchTopOf(collider.Hitbox))
                {
                    hitbox.Y = collider.Hitbox.Y - hitbox.Height - 2;
                    position.Y = hitbox.Y;
                    velocity.Y = 0f;
                    colliding = true;
                }
                else
                    velocity.Y = 1f;

                if (hitbox.TouchRightOf(collider.Hitbox))
                {
                    if (collider is Pipe) hitbox.X = collider.Hitbox.X + hitbox.Width + 10;
                    else hitbox.X = collider.Hitbox.X + hitbox.Width + 2;
                    position.X = hitbox.X;
                    direction = !direction;
                    colliding = true;
                }

                if (hitbox.TouchLeftOf(collider.Hitbox))
                {
                    if (collider is Pipe) hitbox.X = collider.Hitbox.X - hitbox.Width - 10;
                    else hitbox.X = collider.Hitbox.X - hitbox.Width - 2;
                    position.X = hitbox.X;
                    direction = !direction;
                    colliding = true;
                }

                if (hitbox.TouchBottomOf(collider.Hitbox))
                {
                    hitbox.Y = collider.Hitbox.Y + hitbox.Height;
                    position.Y = hitbox.Y;
                    colliding = true;
                }
            }
        }

        public void ToggleHitbox()
        {
            showHitbox = !showHitbox;
        }
    }
}
