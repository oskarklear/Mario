using System;
using System.Collections.Generic;
using System.Text;
using Mario.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Mario.Sprites.Mario
{
    public class SuperMario :ISprite
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

        public SuperMario(MarioContext context, Texture2D texture)
        {
            this.context = context;
            this.texture = texture;
            position = new Vector2(400, 300);
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
            context.GetActionState().FaceLeftTransition();
            System.Diagnostics.Debug.WriteLine("Left");
            System.Diagnostics.Debug.WriteLine(context.GetActionState().ToString());
        }

        public void MoveRightCommand()
        {
            context.GetActionState().FaceRightTransition();
            
            System.Diagnostics.Debug.WriteLine("Right");
            System.Diagnostics.Debug.WriteLine(context.GetActionState().ToString());
        }

        public void JumpCommand()
        {
            context.GetActionState().JumpingTransition();
            System.Diagnostics.Debug.WriteLine("Up");
            System.Diagnostics.Debug.WriteLine(context.GetActionState().ToString());
        }

        public void CrouchCommand()
        {
            context.GetActionState().FallingTransition();
            //context.GetActionState().PressDown(context);
            
            System.Diagnostics.Debug.WriteLine("Down");
            System.Diagnostics.Debug.WriteLine(context.GetActionState().ToString());
        }

        public void Update()
        {
            if (context.GetPowerUpState().ToString().Equals("StandardMario"))
            {
                switch (context.GetActionState().ToString())
                {
                    case "IdleState":
                        if (context.facingLeft)
                            texture = Content.Load<Texture2D>("mario/smallIdleMarioL");
                        else
                            texture = Content.Load<Texture2D>("mario/smallIdleMarioR");
                        Columns = 1;
                        animated = false;
                        break;
                    case "CrouchingState":
                        if (context.facingLeft)
                            texture = Content.Load<Texture2D>("mario/smallCrouchingMarioL");
                        else
                            texture = Content.Load<Texture2D>("mario/smallCrouchingMarioR");
                        Columns = 1;
                        animated = false;
                        break;
                    case "JumpingState":
                        if (context.facingLeft)
                            texture = Content.Load<Texture2D>("mario/smallJumpingMarioL");
                        else
                            texture = Content.Load<Texture2D>("mario/smallJumpingMarioR");
                        Columns = 1;
                        animated = false;
                        break;
                    case "FallingState":
                        if (context.facingLeft)
                            texture = Content.Load<Texture2D>("mario/smallFallingMarioL");
                        else
                            texture = Content.Load<Texture2D>("mario/smallFallingMarioR");
                        Columns = 1;
                        animated = false;
                        break;
                    case "RunningState":
                        if (context.facingLeft)
                            texture = Content.Load<Texture2D>("mario/smallRunningMarioL");
                        else
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
                    case "IdleState":
                        if (context.facingLeft)
                            texture = Content.Load<Texture2D>("mario/bigIdleMarioL");
                        else
                            texture = Content.Load<Texture2D>("mario/bigIdleMarioR");
                        Columns = 1;
                        animated = false;
                        break;
                    case "CrouchingState":
                        if (context.facingLeft)
                            texture = Content.Load<Texture2D>("mario/bigCrouchingMarioL");
                        else
                            texture = Content.Load<Texture2D>("mario/bigCrouchingMarioR");
                        Columns = 1;
                        animated = false;
                        break;
                    case "JumpingState":
                        if (context.facingLeft)
                            texture = Content.Load<Texture2D>("mario/bigJumpingMarioL");
                        else
                            texture = Content.Load<Texture2D>("mario/bigJumpingMarioR");
                        Columns = 1;
                        animated = false;
                        break;
                    case "FallingState":
                        if (context.facingLeft)
                            texture = Content.Load<Texture2D>("mario/bigFallingMarioL");
                        else
                            texture = Content.Load<Texture2D>("mario/bigFallingMarioR");
                        Columns = 1;
                        animated = false;
                        break;
                    case "RunningState":
                        if (context.facingLeft)
                            texture = Content.Load<Texture2D>("mario/bigRunningMarioL");
                        else
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
                    case "IdleState":
                        if (context.facingLeft)
                            texture = Content.Load<Texture2D>("mario/fireIdleMarioL");
                        else
                            texture = Content.Load<Texture2D>("mario/fireIdleMarioR");
                        Columns = 1;
                        animated = false;
                        break;
                    case "CrouchingState":
                        if (context.facingLeft)
                            texture = Content.Load<Texture2D>("mario/fireCrouchingMarioL");
                        else
                            texture = Content.Load<Texture2D>("mario/fireCrouchingMarioR");
                        Columns = 1;
                        animated = false;
                        break;
                    case "JumpingState":
                        if (context.facingLeft)
                            texture = Content.Load<Texture2D>("mario/fireJumpingMarioL");
                        else
                            texture = Content.Load<Texture2D>("mario/fireJumpingMarioR");
                        Columns = 1;
                        animated = false;
                        break;
                    case "FallingState":
                        if (context.facingLeft)
                            texture = Content.Load<Texture2D>("mario/fireFallingMarioL");
                        else
                            texture = Content.Load<Texture2D>("mario/fireFallingMarioR");
                        Columns = 1;
                        animated = false;
                        break;
                    case "RunningState":
                        if (context.facingLeft)
                            texture = Content.Load<Texture2D>("mario/fireRunningMarioL");
                        else
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
    }
}
