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
            //MarioActionState context.GetActionState() = context.GetActionState();
            //MarioPowerupState context.GetPowerUpState() = context.GetPowerUpState();
            context.GetActionState().PressLeft(context);
            //context.GetActionState().PressLeft(context);
            if (context.GetPowerUpState().ToString().Equals("StandardMario"))
            {
                if (context.GetActionState().ToString().Equals("IdleStateLeft"))
                {
                    texture = sprites["smallIdleMarioL"];
                }
                else if (context.GetActionState().ToString().Equals("RunningStateLeft"))
                {
                    texture = Content.Load<Texture2D>("mario/smallRunningMarioL");
                }
            }
            else if (context.GetPowerUpState().ToString().Equals("SuperMario"))
            {
                if (context.GetActionState().ToString().Equals("mario/IdleStateLeft"))
                {
                    texture = Content.Load<Texture2D>("bigIdleMarioR");
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
                }
                else if (context.GetActionState().ToString().Equals("RunningStateRight"))
                {
                    texture = Content.Load<Texture2D>("mario/smallRunningMarioR");
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
            Console.WriteLine("Jump");
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
