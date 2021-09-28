using System;
using System.Collections.Generic;
using System.Text;
using Mario.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Mario.Sprites.Mario
{
    public class SuperMario
    {
        MarioContext context;
        MarioActionState actionState;
        MarioPowerupState powerupState;
        Texture2D texture;

        public SuperMario(MarioContext context, MarioActionState state, Texture2D texture)
        {
            this.context = context;
            this.actionState = state;
            this.texture = texture;
            powerupState = new StandardMarioState();
        }

        public void MoveLeftCommand()
        {
            actionState.PressLeft(context);
            if (powerupState.ToString().Equals("StandardMario"))
            {
                if (actionState.ToString().Equals("IdleStateLeft"))
                {
                    texture = Content.Load<Texture2D>("smallIdleMarioL");
                }
                else if (actionState.ToString().Equals("RunningStateLeft"))
                {
                    texture = Content.Load<Texture2D>("smallRunningMario");
                }
            }
            else if (powerupState.ToString().Equals("SuperMario"))
            {
                if (actionState.ToString().Equals("IdleStateLeft"))
                {
                    texture = Content.Load<Texture2D>("bigIdleMarioR");
                }
                else if (actionState.ToString().Equals("RunningStateLeft"))
                {
                    texture = Content.Load<Texture2D>("bigRunningMarioL");
                }
            }
            else if (powerupState.ToString().Equals("FireMario"))
            {
                if (actionState.ToString().Equals("IdleStateLeft"))
                {
                    texture = Content.Load<Texture2D>("fireIdleMarioL");
                }
                else if (actionState.ToString().Equals("RunningStateLeft"))
                {
                    texture = Content.Load<Texture2D>("fireRunningMarioL");
                }
            }

            Console.WriteLine("Move Left");
        }

        public void MoveRightCommand()
        {
            actionState.PressRight(context);
            if (powerupState.ToString().Equals("StandardMario"))
            {
                if (actionState.ToString().Equals("IdleStateRight"))
                {
                    texture = Content.Load<Texture2D>("smallIdleMarioR");
                }
                else if (actionState.ToString().Equals("RunningStateRight"))
                {
                    texture = Content.Load<Texture2D>("smallRunningMarioR");
                }
            }
            else if (powerupState.ToString().Equals("SuperMario"))
            {
                if (actionState.ToString().Equals("IdleStateRight"))
                {
                    texture = Content.Load<Texture2D>("bigIdleMarioR");
                }
                else if (actionState.ToString().Equals("RunningStateRight"))
                {
                    texture = Content.Load<Texture2D>("bigRunningMarioR");
                }
            }
            else if (powerupState.ToString().Equals("FireMario"))
            {
                if (actionState.ToString().Equals("IdleStateRight"))
                {
                    texture = Content.Load<Texture2D>("fireIdleMarioR");
                }
                else if (actionState.ToString().Equals("RunningStateRight"))
                {
                    texture = Content.Load<Texture2D>("fireRunningMarioR");
                }
            }
            Console.WriteLine("Move Right");
        }

        public void JumpCommand()
        {
            actionState.PressUp(context);
            if (powerupState.ToString().Equals("StandardMario"))
            {
                if (actionState.ToString().Equals("JumpingStateLeft"))
                {
                    texture = Content.Load<Texture2D>("smallJumpingMarioL");
                }
                else if (actionState.ToString().Equals("JumpingStateRight"))
                {
                    texture = Content.Load<Texture2D>("smallJumpingMarioR");
                }
            }
            else if (powerupState.ToString().Equals("SuperMario"))
            {
                if (actionState.ToString().Equals("JumpingStateLeft"))
                {
                    texture = Content.Load<Texture2D>("bigJumpingMarioL");
                }
                else if (actionState.ToString().Equals("JumpingStateRight"))
                {
                    texture = Content.Load<Texture2D>("bigJumpingMarioR");
                }
            }
            else if (powerupState.ToString().Equals("FireMario"))
            {
                if (actionState.ToString().Equals("JumpingStateLeft"))
                {
                    texture = Content.Load<Texture2D>("fireJumpingMarioL");
                }
                else if (actionState.ToString().Equals("JumpingStateRight"))
                {
                    texture = Content.Load<Texture2D>("fireJumpingMarioR");
                }
            }
            Console.WriteLine("Jump");
        }

        public void CrouchCommand()
        {
            actionState.PressDown(context);
            if (powerupState.ToString().Equals("StandardMario"))
            {
                if (actionState.ToString().Equals("CrouchingStateLeft"))
                {
                    texture = Content.Load<Texture2D>("smallCrouchingMarioL");
                }
                else if (actionState.ToString().Equals("CrouchingStateRight"))
                {
                    texture = Content.Load<Texture2D>("smallCrouchingMarioR");
                }
            }
            else if (powerupState.ToString().Equals("SuperMario"))
            {
                if (actionState.ToString().Equals("CrouchingStateLeft"))
                {
                    texture = Content.Load<Texture2D>("bigCrouchingMarioL");
                }
                else if (actionState.ToString().Equals("CrouchingStateRight"))
                {
                    texture = Content.Load<Texture2D>("bigCrouchingMarioR");
                }
            }
            else if (powerupState.ToString().Equals("FireMario"))
            {
                if (actionState.ToString().Equals("CrouchingStateLeft"))
                {
                    texture = Content.Load<Texture2D>("fireCrouchingMarioL");
                }
                else if (actionState.ToString().Equals("CrouchingStateRight"))
                {
                    texture = Content.Load<Texture2D>("fireCrouchingMarioR");
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
            //TODO
        }
    }
}
