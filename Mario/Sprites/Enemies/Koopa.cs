using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using Mario.States;
using Mario.Sprites.Mario;
using Mario.Sprites.Items;
using Mario.Map;

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
        Texture2D shellTexture;
        Vector2 position;
        Vector2 velocity;
        bool direction;
        bool facingLeft;
        Game1 Theatre;
        bool dead;
        bool falling;
        bool colliding;
        bool deleted;
        bool isMoving;
        int shellSpeed;
        int shellDirection;

        public Vector2 Position
        {
            get { return position; }
        }
        public Rectangle hitbox;
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
        public bool isShell { get; set; }
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
            shellTexture = Theatre.Content.Load<Texture2D>("enemies/koopa/koopa_shell_green_init");
            hitbox = new Rectangle((int)location.X + 7, (int)location.Y, 16, 26);
            dead = false;
            showHitbox = false;
            direction = false;
            facingLeft = true;
            velocity.Y = 1f;
            velocity.X = 0.5f;
            isShell = false;
            shellDirection = 1;
            shellSpeed = 2;

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
                if (!isShell)
                {
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
                else
                {
                    
                    Columns = 1;
                    spriteBatch.Draw(shellTexture, position, Color.White);
                }
            }
        }

        public void Update()
        {
            if (!isShell)
            {
                position.Y += velocity.Y;
                hitbox = new Rectangle((int)position.X + 7, (int)position.Y, 16, 26);

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
            else
            {
                
                if (isMoving)
                {
                    System.Diagnostics.Debug.WriteLine("BIG ASSSSS");
                    position.X += shellDirection * shellSpeed;
                }
            }
        }

        public void Collision(ISprite collider)
        {
            if (collider is SuperMario)
            {
                //dead = true;
                //hitbox = new Rectangle(-1, -1, 0, 0);
                System.Diagnostics.Debug.WriteLine("KOOPA TOP: " + hitbox.Top);
                System.Diagnostics.Debug.WriteLine("MARIO BOTTOM: " + collider.Hitbox.Bottom);
                System.Diagnostics.Debug.WriteLine("KOOPA RIGHT: " + hitbox.Right);
                System.Diagnostics.Debug.WriteLine("MARIO LEFT: " + collider.Hitbox.Left);
                if (hitbox.TouchTopOf(collider.Hitbox))
                {
                    //isShell = true;
                }
                if (hitbox.Top <= collider.Hitbox.Bottom + 1 &&
                hitbox.Top >= collider.Hitbox.Bottom - 2 &&
                hitbox.Right >= collider.Hitbox.Left + (collider.Hitbox.Width / 5) &&
                hitbox.Left <= collider.Hitbox.Right - (collider.Hitbox.Width / 5))
                {
                    System.Diagnostics.Debug.WriteLine("FUCK");
                    velocity.X = 0f;
                    velocity.Y = 0f;
                    isShell = true;
                } 
                if (hitbox.Right <= collider.Hitbox.Right &&
                hitbox.Right >= collider.Hitbox.Left - 4 &&
                hitbox.Top <= collider.Hitbox.Bottom - (collider.Hitbox.Width / 4) &&
                hitbox.Bottom >= collider.Hitbox.Top + (collider.Hitbox.Width / 4))
                {
                    System.Diagnostics.Debug.WriteLine("FUCK");
                    isMoving = true;
                    shellDirection = 1;
                }
                if (hitbox.Left >= collider.Hitbox.Left &&
                hitbox.Left <= collider.Hitbox.Right &&
                hitbox.Top <= collider.Hitbox.Bottom - (collider.Hitbox.Width / 4) &&
                hitbox.Bottom >= collider.Hitbox.Top + (collider.Hitbox.Width / 4))
                {
                    System.Diagnostics.Debug.WriteLine("FUCK");
                    isMoving = true;
                    shellDirection = -1;
                }

            }

            if (collider is BlockContext || collider is Pipe)
            {
                if (hitbox.TouchTopOf(collider.Hitbox))
                {
                    hitbox.Y = collider.Hitbox.Y - hitbox.Height - 2;
                    position.Y = hitbox.Y;
                    velocity.Y = 0f;
                    falling = false;
                }
                else
                {
                    velocity.Y = 1f;
                    falling = true;
                }

                if (hitbox.TouchRightOf(collider.Hitbox))
                {
                    if (collider is Pipe) hitbox.X = collider.Hitbox.X + hitbox.Width + 10;
                    else hitbox.X = collider.Hitbox.X + hitbox.Width + 2;
                    position.X = hitbox.X;
                    //if (!falling)
                        direction = !direction;
                }

                if (hitbox.TouchLeftOf(collider.Hitbox))
                {
                    if (collider is Pipe) hitbox.X = collider.Hitbox.X - hitbox.Width - 10;
                    else hitbox.X = collider.Hitbox.X - hitbox.Width - 8;
                    position.X = hitbox.X;
                    //if (!falling)
                        direction = !direction;
                }

                if (hitbox.TouchBottomOf(collider.Hitbox))
                {
                    hitbox.Y = collider.Hitbox.Y + hitbox.Height;
                    position.Y = hitbox.Y;
                }
            }
        }

        public void ToggleHitbox()
        {
            showHitbox = !showHitbox;
        }
    }
}
