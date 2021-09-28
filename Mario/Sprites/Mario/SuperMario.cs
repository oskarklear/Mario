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
        MarioContext context;
        Texture2D texture;
        ContentManager Content;
        Vector2 position;
        Dictionary<string, Texture2D> sprites;

        public SuperMario(MarioContext context, Texture2D texture)
        {
            this.context = context;
            this.texture = texture;
            position = new Vector2(0, 0);
            sprites = new Dictionary<string, Texture2D>();
        }
        public void LoadContent(ContentManager content)
        {
            Content = content;
            sprites.Add("smallIdleMarioL", Content.Load<Texture2D>("mario/smallIdleMarioL"));
            sprites.Add("smallIdleMarioR", Content.Load<Texture2D>("mario/smallIdleMarioR"));
        }
        public void MoveLeftCommand()
        {
            MarioActionState actionState = context.GetActionState();
            MarioPowerupState powerupState = context.GetPowerUpState();
            //context.GetActionState().PressLeft(context);
            actionState.PressLeft(context);
            if (powerupState.ToString().Equals("StandardMario"))
            {
                if (actionState.ToString().Equals("IdleStateLeft"))
                {
                    texture = sprites["smallIdleMarioL"];
                }
                else if (actionState.ToString().Equals("RunningStateLeft"))
                {
                    texture = Content.Load<Texture2D>("mario/smallRunningMarioL");
                }
            }
            else if (powerupState.ToString().Equals("SuperMario"))
            {
                if (actionState.ToString().Equals("mario/IdleStateLeft"))
                {
                    texture = Content.Load<Texture2D>("bigIdleMarioR");
                }
                else if (actionState.ToString().Equals("RunningStateLeft"))
                {
                    texture = Content.Load<Texture2D>("mario/bigRunningMarioL");
                }
            }
            else if (powerupState.ToString().Equals("FireMario"))
            {
                if (actionState.ToString().Equals("IdleStateLeft"))
                {
                    texture = Content.Load<Texture2D>("mario/fireIdleMarioL");
                }
                else if (actionState.ToString().Equals("RunningStateLeft"))
                {
                    texture = Content.Load<Texture2D>("mario/fireRunningMarioL");
                }
            }

            Console.WriteLine("Move Left");
        }

        public void MoveRightCommand()
        {
            MarioActionState actionState = context.GetActionState();
            MarioPowerupState powerupState = context.GetPowerUpState();
            //context.GetActionState().PressLeft(context);
            actionState.PressRight(context);
            if (powerupState.ToString().Equals("StandardMario"))
            {
                if (actionState.ToString().Equals("IdleStateRight"))
                {
                    texture = sprites["smallIdleMarioR"];
                }
                else if (actionState.ToString().Equals("RunningStateRight"))
                {
                    texture = Content.Load<Texture2D>("mario/smallRunningMarioR");
                }
            }
            else if (powerupState.ToString().Equals("SuperMario"))
            {
                if (actionState.ToString().Equals("IdleStateRight"))
                {
                    texture = Content.Load<Texture2D>("mario/bigIdleMarioR");
                }
                else if (actionState.ToString().Equals("RunningStateRight"))
                {
                    texture = Content.Load<Texture2D>("mario/bigRunningMarioR");
                }
            }
            else if (powerupState.ToString().Equals("FireMario"))
            {
                if (actionState.ToString().Equals("IdleStateRight"))
                {
                    texture = Content.Load<Texture2D>("mario/fireIdleMarioR");
                }
                else if (actionState.ToString().Equals("RunningStateRight"))
                {
                    texture = Content.Load<Texture2D>("mario/fireRunningMarioR");
                }
            }
            Console.WriteLine("Move Right");
        }

        public void JumpCommand()
        {
            MarioActionState actionState = context.GetActionState();
            MarioPowerupState powerupState = context.GetPowerUpState();
            //context.GetActionState().PressLeft(context);
            actionState.PressUp(context);
            if (powerupState.ToString().Equals("StandardMario"))
            {
                if (actionState.ToString().Equals("JumpingStateLeft"))
                {
                    texture = Content.Load<Texture2D>("mario/smallJumpingMarioL");
                }
                else if (actionState.ToString().Equals("JumpingStateRight"))
                {
                    texture = Content.Load<Texture2D>("mario/smallJumpingMarioR");
                }
            }
            else if (powerupState.ToString().Equals("SuperMario"))
            {
                if (actionState.ToString().Equals("JumpingStateLeft"))
                {
                    texture = Content.Load<Texture2D>("mario/bigJumpingMarioL");
                }
                else if (actionState.ToString().Equals("JumpingStateRight"))
                {
                    texture = Content.Load<Texture2D>("mario/bigJumpingMarioR");
                }
            }
            else if (powerupState.ToString().Equals("FireMario"))
            {
                if (actionState.ToString().Equals("JumpingStateLeft"))
                {
                    texture = Content.Load<Texture2D>("mario/fireJumpingMarioL");
                }
                else if (actionState.ToString().Equals("JumpingStateRight"))
                {
                    texture = Content.Load<Texture2D>("mario/fireJumpingMarioR");
                }
            }
            Console.WriteLine("Jump");
        }

        public void CrouchCommand()
        {
            MarioActionState actionState = context.GetActionState();
            MarioPowerupState powerupState = context.GetPowerUpState();
            //context.GetActionState().PressLeft(context);
            actionState.PressDown(context);
            if (powerupState.ToString().Equals("StandardMario"))
            {
                if (actionState.ToString().Equals("CrouchingStateLeft"))
                {
                    texture = Content.Load<Texture2D>("mario/smallCrouchingMarioL");
                }
                else if (actionState.ToString().Equals("CrouchingStateRight"))
                {
                    texture = Content.Load<Texture2D>("mario/smallCrouchingMarioR");
                }
            }
            else if (powerupState.ToString().Equals("SuperMario"))
            {
                if (actionState.ToString().Equals("CrouchingStateLeft"))
                {
                    texture = Content.Load<Texture2D>("mario/bigCrouchingMarioL");
                }
                else if (actionState.ToString().Equals("CrouchingStateRight"))
                {
                    texture = Content.Load<Texture2D>("mario/bigCrouchingMarioR");
                }
            }
            else if (powerupState.ToString().Equals("FireMario"))
            {
                if (actionState.ToString().Equals("CrouchingStateLeft"))
                {
                    texture = Content.Load<Texture2D>("mario/fireCrouchingMarioL");
                }
                else if (actionState.ToString().Equals("CrouchingStateRight"))
                {
                    texture = Content.Load<Texture2D>("mario/fireCrouchingMarioR");
                }
            }
            Console.WriteLine("Crouch");
        }

        /*
         * Possibly irrelevant
         */
        public void UpdateTexture(Texture2D texture)
        {
            this.texture = texture;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(texture, position, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            spriteBatch.End();
        }
    }
}
