using System;
using System.Collections.Generic;
using System.Text;
using Mario.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Mario.Sprites.Mario
{
    public class SuperMario : ISprite
    {
        public MarioContext context { get; set; }
        public bool animated { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }
        private int currentFrame;
        private int timeSinceLastFrame;
        private int millisecondsPerFrame;
        Texture2D texture;
        ContentManager Content;
        Vector2 position;
        Dictionary<string, Texture2D> sprites;
        Rectangle hitbox;
        public Rectangle DestinationRectangle { get; set; }
        public SuperMario(MarioContext context, Texture2D texture)
        {
            this.context = context;
            this.texture = texture;
            position = new Vector2(32, 500);
            sprites = new Dictionary<string, Texture2D>();
            Rows = 1;
            Columns = 1;
            currentFrame = 0;
            timeSinceLastFrame = 0;
            millisecondsPerFrame = 6;
        }

        public void LoadContent(ContentManager content)
        {
            Content = content;
        }

        public void MoveLeftCommand()
        {
            context.GetActionState().PressLeft(context);
            int marioTopLeftSpeed = -3;
            if (context.Velocity.X > marioTopLeftSpeed) 
            {
                context.Velocity.X -= (float)0.15;
            }
            

            System.Diagnostics.Debug.WriteLine("Left");
            System.Diagnostics.Debug.WriteLine(context.GetActionState().ToString());
        }

        public void MoveRightCommand()
        {
            context.GetActionState().PressRight(context);

            System.Diagnostics.Debug.WriteLine("Right");
            System.Diagnostics.Debug.WriteLine(context.GetActionState().ToString());
        }

        public void JumpCommand()
        {
            context.GetActionState().PressUp(context);

            System.Diagnostics.Debug.WriteLine("Up");
            System.Diagnostics.Debug.WriteLine(context.GetActionState().ToString());
        }

        public void CrouchCommand()
        {
            context.GetActionState().PressDown(context);
            
            System.Diagnostics.Debug.WriteLine("Down");
            System.Diagnostics.Debug.WriteLine(context.GetActionState().ToString());
        }

        public void IdleCommand()
        {
            context.GetActionState().PressNothing(context);

            System.Diagnostics.Debug.WriteLine("Nothing");
            System.Diagnostics.Debug.WriteLine(context.GetActionState().ToString());
        }

        public void Update()
        {
            System.Diagnostics.Debug.WriteLine("X: " + context.Velocity.X);
            System.Diagnostics.Debug.WriteLine("Y: " + context.Velocity.Y);

            if (context.GetPowerUpState().ToString().Equals("StandardMario"))
            {
                switch (context.GetActionState().ToString())
                {
                    case "IdleStateLeft":
                        texture = Content.Load<Texture2D>("mario/smallIdleMarioL");
                        Columns = 1;
                        animated = false;
                        break;
                    case "IdleStateRight":
                        texture = Content.Load<Texture2D>("mario/smallIdleMarioR");
                        Columns = 1;
                        animated = false;
                        break;
                    case "CrouchingStateLeft":
                        texture = Content.Load<Texture2D>("mario/smallCrouchingMarioL");
                        Columns = 1;
                        animated = false;
                        break;
                    case "CrouchingStateRight":
                        texture = Content.Load<Texture2D>("mario/smallCrouchingMarioR");
                        Columns = 1;
                        animated = false;
                        break;
                    case "JumpingStateLeft":
                        texture = Content.Load<Texture2D>("mario/smallJumpingMarioL");
                        Columns = 1;
                        animated = false;
                        break;
                    case "JumpingStateRight":
                        texture = Content.Load<Texture2D>("mario/smallJumpingMarioR");
                        Columns = 1;
                        animated = false;
                        break;
                    case "FallingStateLeft":
                        texture = Content.Load<Texture2D>("mario/smallFallingMarioL");
                        Columns = 1;
                        animated = false;
                        break;
                    case "FallingStateRight":
                        texture = Content.Load<Texture2D>("mario/smallFallingMarioR");
                        Columns = 1;
                        animated = false;
                        break;
                    case "RunningStateLeft":
                        texture = Content.Load<Texture2D>("mario/smallRunningMarioL");
                        Columns = 2;
                        animated = true;
                        break;
                    case "RunningStateRight":
                        texture = Content.Load<Texture2D>("mario/smallRunningMarioR");
                        Columns = 2;
                        animated = true;
                        break;
                }
            }

            if (context.GetPowerUpState().ToString().Equals("SuperMario"))
            {
                switch (context.GetActionState().ToString())
                {
                    case "IdleStateLeft":
                        texture = Content.Load<Texture2D>("mario/bigIdleMarioL");
                        Columns = 1;
                        animated = false;
                        break;
                    case "IdleStateRight":
                        texture = Content.Load<Texture2D>("mario/bigIdleMarioR");
                        Columns = 1;
                        animated = false;
                        break;
                    case "CrouchingStateLeft":
                        texture = Content.Load<Texture2D>("mario/bigCrouchingMarioL");
                        Columns = 1;
                        animated = false;
                        break;
                    case "CrouchingStateRight":
                        texture = Content.Load<Texture2D>("mario/bigCrouchingMarioR");
                        Columns = 1;
                        animated = false;
                        break;
                    case "JumpingStateLeft":
                        texture = Content.Load<Texture2D>("mario/bigJumpingMarioL");
                        Columns = 1;
                        animated = false;
                        break;
                    case "JumpingStateRight":
                        texture = Content.Load<Texture2D>("mario/bigJumpingMarioR");
                        Columns = 1;
                        animated = false;
                        break;
                    case "FallingStateLeft":
                        texture = Content.Load<Texture2D>("mario/bigFallingMarioL");
                        Columns = 1;
                        animated = false;
                        break;
                    case "FallingStateRight":
                        texture = Content.Load<Texture2D>("mario/bigFallingMarioR");
                        Columns = 1;
                        animated = false;
                        break;
                    case "RunningStateLeft":
                        texture = Content.Load<Texture2D>("mario/bigRunningMarioL");
                        Columns = 3;
                        animated = true;
                        break;
                    case "RunningStateRight":
                        texture = Content.Load<Texture2D>("mario/bigRunningMarioR");
                        Columns = 3;
                        animated = true;
                        break;
                }
            }

            if (context.GetPowerUpState().ToString().Equals("FireMario"))
            {
                switch (context.GetActionState().ToString())
                {
                    case "IdleStateLeft":
                        texture = Content.Load<Texture2D>("mario/fireIdleMarioL");
                        Columns = 1;
                        animated = false;
                        break;
                    case "IdleStateRight":
                        texture = Content.Load<Texture2D>("mario/fireIdleMarioR");
                        Columns = 1;
                        animated = false;
                        break;
                    case "CrouchingStateLeft":
                        texture = Content.Load<Texture2D>("mario/fireCrouchingMarioL");
                        Columns = 1;
                        animated = false;
                        break;
                    case "CrouchingStateRight":
                        texture = Content.Load<Texture2D>("mario/fireCrouchingMarioR");
                        Columns = 1;
                        animated = false;
                        break;
                    case "JumpingStateLeft":
                        texture = Content.Load<Texture2D>("mario/fireJumpingMarioL");
                        Columns = 1;
                        animated = false;
                        break;
                    case "JumpingStateRight":
                        texture = Content.Load<Texture2D>("mario/fireJumpingMarioR");
                        Columns = 1;
                        animated = false;
                        break;
                    case "FallingStateLeft":
                        texture = Content.Load<Texture2D>("mario/fireFallingMarioL");
                        Columns = 1;
                        animated = false;
                        break;
                    case "FallingStateRight":
                        texture = Content.Load<Texture2D>("mario/fireFallingMarioR");
                        Columns = 1;
                        animated = false;
                        break;
                    case "RunningStateLeft":
                        texture = Content.Load<Texture2D>("mario/fireRunningMarioL");
                        Columns = 3;
                        animated = true;
                        break;
                    case "RunningStateRight":
                        texture = Content.Load<Texture2D>("mario/fireRunningMarioR");
                        Columns = 3;
                        animated = true;
                        break;
                }
            }

            if (context.GetPowerUpState().ToString().Equals("DeadMario"))
            {
                texture = Content.Load<Texture2D>("mario/deadMario");
                Columns = 2;
                animated = true;
            }

            if (animated)
            {
                if (timeSinceLastFrame > millisecondsPerFrame)
                {
                    currentFrame++;
                    timeSinceLastFrame = 0;
                }
                if (currentFrame == Columns)
                    currentFrame = 0;
                timeSinceLastFrame++;
            }

            //set mario's new pos
            position.X += context.Velocity.X;
            position.Y -= context.Velocity.Y;

            hitbox = new Rectangle((int)position.X, (int)position.Y, 14, 20);
            //// if mario idling, then deccelerate
            //if (context.GetActionState().ToString().Equals("IdleStateRight") || context.GetActionState().ToString().Equals("IdleStateLeft"))
            //{
            //    if (context.xVelocity != 0)
            //    {
            //        if (context.xVelocity < 0)
            //        {
            //            context.xVelocity += (float)0.3;
            //        }
            //        else
            //        {
            //            context.xVelocity -= (float)0.3;
            //        }
            //    }

            //    // if there's leftover speed from shitty code, zero it
            //    if (Math.Abs(context.xVelocity) < 0.08)
            //    {
            //        context.xVelocity = 0;
            //    }

            //}

            //if (context.GetActionState().ToString().Equals("JumpingStateLeft") || context.GetActionState().ToString().Equals("JumpingStateRight"))
            //{
            //    if (context.yVelocity != 0)
            //    {
            //        if (context.yVelocity < 0)
            //        {
            //            context.yVelocity -= (float)0.01;
            //        }
            //        else
            //        {
            //            context.yVelocity -= (float)0.01;
            //        }
            //    }

            //    // if there's leftover speed from shitty code, zero it
            //    if (Math.Abs(context.yVelocity) < 0.08)
            //    {
            //        context.xVelocity = 0;
            //    }

            //}




        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (animated)
            {
                int width = texture.Width / Columns;
                int height = texture.Height / Rows;
                int row = currentFrame / Columns;
                int column = currentFrame % Columns;

                Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
                Rectangle destinationRectangle = new Rectangle((int)position.X, (int)position.Y, width, height);
                spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, Color.White);
            }
            else
            {
                spriteBatch.Draw(texture, position, Color.White);
            }

            
        }

        public void Collision(Rectangle newRectangle, int xOffset, int yOffset)
        {
            if (hitbox.TouchTopOf(newRectangle))
            {
                hitbox.Y = newRectangle.Y - hitbox.Height - 1;
                position.Y = hitbox.Y;
                context.Velocity.Y = 0f;
            }
            if (hitbox.TouchLeftOf(newRectangle))
            {
                hitbox.X = newRectangle.X - hitbox.Width - 4;
                position.X = hitbox.X;
            }
            if (hitbox.TouchRightOf(newRectangle))
            {
                hitbox.X = newRectangle.X + hitbox.Width + 4;
                position.X = hitbox.X;
            }
            if (hitbox.TouchBottomOf(newRectangle))
            {
                hitbox.Y = newRectangle.Y + hitbox.Height + 1;
                position.Y = hitbox.Y;
                //context.Velocity.Y = 0f;
            }

            if (position.X < 0)
                position.X = 0;

            if (position.X > xOffset - hitbox.Width)
                position.X = xOffset - hitbox.Width;

            //if (position.Y < 0)
                //context.Velocity.Y = 1f;

            if (position.Y > yOffset - hitbox.Height)
                position.Y = yOffset - hitbox.Height;

        }
    }
}
