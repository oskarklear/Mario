using System;
using System.Collections.Generic;
using System.Text;
using Mario.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Mario.Sprites.Mario
{
    public class SuperMario
    {
        public MarioContext context { get; set; }
        public bool animated { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }
        private int currentFrame;
        private int totalFrames;
        private int timeSinceLastFrame;
        private int millisecondsPerFrame;
        Texture2D texture;
        ContentManager Content;
        Vector2 position;
        Dictionary<string, Texture2D> sprites;

        public SuperMario(MarioContext context, Texture2D texture)
        {
            this.context = context;
            this.texture = texture;
            position = new Vector2(100, 100);
            sprites = new Dictionary<string, Texture2D>();
            Rows = 1;
            Columns = 1;
            currentFrame = 0;
            totalFrames = 2;
            timeSinceLastFrame = 0;
            millisecondsPerFrame = 11;
        }
        public void LoadContent(ContentManager content)
        {
            Content = content;
            sprites.Add("smallIdleMarioL", Content.Load<Texture2D>("mario/smallIdleMarioL"));
            sprites.Add("smallIdleMarioR", Content.Load<Texture2D>("mario/smallIdleMarioR"));
        }
        public void MoveLeftCommand()
        {
            //MarioActionState context.GetActionState() = context.GetActionState();
            //MarioPowerupState context.GetPowerUpState() = context.GetPowerUpState();
            context.GetActionState().PressLeft(context);
            //context.GetActionState().PressLeft(context);
            if (context.GetPowerUpState().ToString().Equals("StandardMario"))
            {
                if (context.GetActionState().ToString().Equals("IdleStateLeft"))
                {
                    texture = sprites["smallIdleMarioL"];
                    Columns = 1;
                    animated = false;
                }
                else if (context.GetActionState().ToString().Equals("IdleStateRight"))
                {
                    texture = sprites["smallIdleMarioR"];
                    Columns = 1;
                    animated = false;
                }
                else if (context.GetActionState().ToString().Equals("RunningStateLeft"))
                {
                    texture = Content.Load<Texture2D>("mario/smallRunningMarioL");
                    Columns = 2;
                    animated = true;
                }
            }
            else if (context.GetPowerUpState().ToString().Equals("SuperMario"))
            {
                if (context.GetActionState().ToString().Equals("mario/IdleStateLeft"))
                {
                    texture = Content.Load<Texture2D>("bigIdleMarioL");
                }
                else if (context.GetActionState().ToString().Equals("RunningStateLeft"))
                {
                    texture = Content.Load<Texture2D>("mario/bigRunningMarioL");
                }
            }
            else if (context.GetPowerUpState().ToString().Equals("FireMario"))
            {
                if (context.GetActionState().ToString().Equals("IdleStateLeft"))
                {
                    texture = Content.Load<Texture2D>("mario/fireIdleMarioL");
                }
                else if (context.GetActionState().ToString().Equals("RunningStateLeft"))
                {
                    texture = Content.Load<Texture2D>("mario/fireRunningMarioL");
                }
            }

            System.Diagnostics.Debug.WriteLine("Left");
            System.Diagnostics.Debug.WriteLine(context.GetActionState().ToString());
        }

        public void MoveRightCommand()
        {
            //MarioActionState context.GetActionState() = context.GetActionState();
            //MarioPowerupState context.GetPowerUpState() = context.GetPowerUpState();
            context.GetActionState().PressRight(context);
            //context.GetActionState().PressRight(context);
            if (context.GetPowerUpState().ToString().Equals("StandardMario"))
            {
                if (context.GetActionState().ToString().Equals("IdleStateRight"))
                {
                    texture = sprites["smallIdleMarioR"];
                    Columns = 1;
                    animated = false;
                }
                else if (context.GetActionState().ToString().Equals("IdleStateLeft"))
                {
                    texture = sprites["smallIdleMarioL"];
                    Columns = 1;
                    animated = false;
                }
                else if (context.GetActionState().ToString().Equals("RunningStateRight"))
                {
                    texture = Content.Load<Texture2D>("mario/smallRunningMarioR");
                    Columns = 2;
                    animated = true;
                }
            }
            else if (context.GetPowerUpState().ToString().Equals("SuperMario"))
            {
                if (context.GetActionState().ToString().Equals("IdleStateRight"))
                {
                    texture = Content.Load<Texture2D>("mario/bigIdleMarioR");
                }
                else if (context.GetActionState().ToString().Equals("RunningStateRight"))
                {
                    texture = Content.Load<Texture2D>("mario/bigRunningMarioR");
                }
            }
            else if (context.GetPowerUpState().ToString().Equals("FireMario"))
            {
                if (context.GetActionState().ToString().Equals("IdleStateRight"))
                {
                    texture = Content.Load<Texture2D>("mario/fireIdleMarioR");
                }
                else if (context.GetActionState().ToString().Equals("RunningStateRight"))
                {
                    texture = Content.Load<Texture2D>("mario/fireRunningMarioR");
                }
            }
            System.Diagnostics.Debug.WriteLine("Right");
            System.Diagnostics.Debug.WriteLine(context.GetActionState().ToString());
        }

        public void JumpCommand()
        {
            //MarioActionState context.GetActionState() = context.GetActionState();
            //MarioPowerupState context.GetPowerUpState() = context.GetPowerUpState();
            context.GetActionState().PressUp(context);
            //context.GetActionState().PressUp(context);
            if (context.GetPowerUpState().ToString().Equals("StandardMario"))
            {
                if (context.GetActionState().ToString().Equals("JumpingStateLeft"))
                {
                    texture = Content.Load<Texture2D>("mario/smallJumpingMarioL");
                }
                else if (context.GetActionState().ToString().Equals("JumpingStateRight"))
                {
                    texture = Content.Load<Texture2D>("mario/smallJumpingMarioR");
                }
            }
            else if (context.GetPowerUpState().ToString().Equals("SuperMario"))
            {
                if (context.GetActionState().ToString().Equals("JumpingStateLeft"))
                {
                    texture = Content.Load<Texture2D>("mario/bigJumpingMarioL");
                }
                else if (context.GetActionState().ToString().Equals("JumpingStateRight"))
                {
                    texture = Content.Load<Texture2D>("mario/bigJumpingMarioR");
                }
            }
            else if (context.GetPowerUpState().ToString().Equals("FireMario"))
            {
                if (context.GetActionState().ToString().Equals("JumpingStateLeft"))
                {
                    texture = Content.Load<Texture2D>("mario/fireJumpingMarioL");
                }
                else if (context.GetActionState().ToString().Equals("JumpingStateRight"))
                {
                    texture = Content.Load<Texture2D>("mario/fireJumpingMarioR");
                }
            }
            System.Diagnostics.Debug.WriteLine("Up");
            System.Diagnostics.Debug.WriteLine(context.GetActionState().ToString());
        }

        public void CrouchCommand()
        {
            //MarioActionState context.GetActionState() = context.GetActionState();
            //MarioPowerupState context.GetPowerUpState() = context.GetPowerUpState();
            context.GetActionState().PressDown(context);
            //context.GetActionState().PressDown(context);
            if (context.GetPowerUpState().ToString().Equals("StandardMario"))
            {
                if (context.GetActionState().ToString().Equals("CrouchingStateLeft"))
                {
                    texture = Content.Load<Texture2D>("mario/smallCrouchingMarioL");
                }
                else if (context.GetActionState().ToString().Equals("CrouchingStateRight"))
                {
                    texture = Content.Load<Texture2D>("mario/smallCrouchingMarioR");
                }
                else if (context.GetActionState().ToString().Equals("IdleStateLeft"))
                {
                    texture = Content.Load<Texture2D>("mario/smallIdleMarioL");
                }
                else if (context.GetActionState().ToString().Equals("IdleStateRight"))
                {
                    texture = Content.Load<Texture2D>("mario/smallIdleMarioR");
                }
            }
            else if (context.GetPowerUpState().ToString().Equals("SuperMario"))
            {
                if (context.GetActionState().ToString().Equals("CrouchingStateLeft"))
                {
                    texture = Content.Load<Texture2D>("mario/bigCrouchingMarioL");
                }
                else if (context.GetActionState().ToString().Equals("CrouchingStateRight"))
                {
                    texture = Content.Load<Texture2D>("mario/bigCrouchingMarioR");
                }
                else if (context.GetActionState().ToString().Equals("IdleStateLeft"))
                {
                    texture = Content.Load<Texture2D>("mario/bigIdleMarioL");
                }
                else if (context.GetActionState().ToString().Equals("IdleStateRight"))
                {
                    texture = Content.Load<Texture2D>("mario/bigIdleMarioR");
                }
            }
            else if (context.GetPowerUpState().ToString().Equals("FireMario"))
            {
                if (context.GetActionState().ToString().Equals("CrouchingStateLeft"))
                {
                    texture = Content.Load<Texture2D>("mario/fireCrouchingMarioL");
                }
                else if (context.GetActionState().ToString().Equals("CrouchingStateRight"))
                {
                    texture = Content.Load<Texture2D>("mario/fireCrouchingMarioR");
                }
                else if (context.GetActionState().ToString().Equals("IdleStateLeft"))
                {
                    texture = Content.Load<Texture2D>("mario/fireIdleMarioL");
                }
                else if (context.GetActionState().ToString().Equals("IdleStateRight"))
                {
                    texture = Content.Load<Texture2D>("mario/fireIdleMarioR");
                }
            }
            System.Diagnostics.Debug.WriteLine("Down");
            System.Diagnostics.Debug.WriteLine(context.GetActionState().ToString());
        }

        /*
         * Possibly irrelevant
         */
        public void UpdateTexture(Texture2D texture)
        {
            this.texture = texture;
        }
        public void animate()
        {
            if (animated)
            {
                if (timeSinceLastFrame > millisecondsPerFrame)
                {
                    currentFrame++;
                    timeSinceLastFrame = 0;
                }
                if (currentFrame == totalFrames)
                    currentFrame = 0;
                timeSinceLastFrame++;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            int width = texture.Width / Columns;
            int height = texture.Height / Rows;
            int row = currentFrame / Columns;
            int column = currentFrame % Columns;

            Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
            Rectangle destinationRectangle = new Rectangle((int)position.X, (int)position.Y, width, height);

            spriteBatch.Begin();
            spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, Color.White);
            spriteBatch.End();
        }
    }
}
