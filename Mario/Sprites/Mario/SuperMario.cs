﻿using System;
using System.Collections.Generic;
using System.Text;
using Mario.Sprites.Enemies;
using Mario.Sprites.Items;
using Mario.Sprites.Items.Items;
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
        Game1 Theatre;
        Vector2 position;
        public bool ShowHitbox
        {
            get { return context.ShowHitbox; }
            set { context.ShowHitbox = value; }
        }
        Rectangle hitbox;
        public Rectangle Hitbox 
        {
            get { return hitbox; }
            set { hitbox = value; }
        }
        public SuperMario(Game1 theatre, Vector2 location, MarioContext context)
        {
            this.context = context;
            Rows = 1;
            Columns = 1;
            currentFrame = 0;
            timeSinceLastFrame = 0;
            millisecondsPerFrame = 6;
            position = location;
            Theatre = theatre;
            texture = Theatre.Content.Load<Texture2D>("mario/smallIdleMarioR");
            hitbox = new Rectangle((int)position.X, (int)position.Y, 14, 20);
        }


        public void MoveLeftCommand()
        {
            if (!(context.GetPowerUpState() is DeadMarioState))
            {
                context.GetActionState().FaceLeftTransition();
                //System.Diagnostics.Debug.WriteLine("Left");
                System.Diagnostics.Debug.WriteLine(context.GetActionState().ToString());
            }           
        }

        public void MoveRightCommand()
        {
            if (!(context.GetPowerUpState() is DeadMarioState))
            {
                context.GetActionState().FaceRightTransition();

                //System.Diagnostics.Debug.WriteLine("Right");
                //System.Diagnostics.Debug.WriteLine(context.GetActionState().ToString());
            }
        }

        public void JumpCommand()
        {
            if (!(context.GetPowerUpState() is DeadMarioState))
            {
                context.GetActionState().JumpingTransition();

                //System.Diagnostics.Debug.WriteLine("Up");
                //System.Diagnostics.Debug.WriteLine(context.GetActionState().ToString());
            }
        }

        public void CrouchCommand()
        {
            if (!(context.GetPowerUpState() is DeadMarioState))
            {
                context.GetActionState().FallingTransition();

                //System.Diagnostics.Debug.WriteLine("Down");
                //System.Diagnostics.Debug.WriteLine(context.GetActionState().ToString());
            }
        }
        public void CrouchingDiscontinueCommand()
        {
            context.GetActionState().CrouchingDiscontinueTransition();
        }
        public void JumpingDiscontinueCommand()
        {
            context.GetActionState().JumpingDiscontinueTransition();
        }
        public void FaceLeftDiscontinueCommand()
        {
            context.GetActionState().FaceLeftDiscontinueTransition();

            //System.Diagnostics.Debug.WriteLine("Down");
            //System.Diagnostics.Debug.WriteLine(context.GetActionState().ToString());
        }
        public void FaceRightDiscontinueCommand()
        {
            context.GetActionState().FaceRightDiscontinueTransition();

            //System.Diagnostics.Debug.WriteLine("Down");
            //System.Diagnostics.Debug.WriteLine(context.GetActionState().ToString());
        }

        public void IdleCommand()
        {
            context.GetActionState().PressNothing(context);

            //System.Diagnostics.Debug.WriteLine("Nothing");
            //System.Diagnostics.Debug.WriteLine(context.GetActionState().ToString());
        }

        public void Update()
        {
            System.Diagnostics.Debug.WriteLine("X: " + context.Velocity.X);
            System.Diagnostics.Debug.WriteLine("Y: " + context.Velocity.Y);

            if (context.GetPowerUpState().ToString().Equals("StandardMario"))
            {
                switch (context.GetActionState().ToString())
                {
                    case "IdleState":
                        if (context.facingLeft)
                            texture = Theatre.Content.Load<Texture2D>("mario/smallIdleMarioL");
                        else
                            texture = Theatre.Content.Load<Texture2D>("mario/smallIdleMarioR");
                        Columns = 1;
                        animated = false;
                        break;
                    case "CrouchingState":
                        if (context.facingLeft)
                            texture = Theatre.Content.Load<Texture2D>("mario/smallCrouchingMarioL");
                        else
                            texture = Theatre.Content.Load<Texture2D>("mario/smallCrouchingMarioR");
                        Columns = 1;
                        animated = false;
                        break;
                    case "JumpingState":
                        if (context.facingLeft)
                            texture = Theatre.Content.Load<Texture2D>("mario/smallJumpingMarioL");
                        else
                            texture = Theatre.Content.Load<Texture2D>("mario/smallJumpingMarioR");
                        Columns = 1;
                        animated = false;
                        break;
                    case "FallingState":
                        if (context.facingLeft)
                            texture = Theatre.Content.Load<Texture2D>("mario/smallFallingMarioL");
                        else
                            texture = Theatre.Content.Load<Texture2D>("mario/smallFallingMarioR");
                        Columns = 1;
                        animated = false;
                        break;
                    case "RunningState":
                        if (context.facingLeft)
                            texture = Theatre.Content.Load<Texture2D>("mario/smallRunningMarioL");
                        else
                            texture = Theatre.Content.Load<Texture2D>("mario/smallRunningMarioR");
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
                            texture = Theatre.Content.Load<Texture2D>("mario/bigIdleMarioL");
                        else
                            texture = Theatre.Content.Load<Texture2D>("mario/bigIdleMarioR");
                        Columns = 1;
                        animated = false;
                        break;
                    case "CrouchingState":
                        if (context.facingLeft)
                            texture = Theatre.Content.Load<Texture2D>("mario/bigCrouchingMarioL");
                        else
                            texture = Theatre.Content.Load<Texture2D>("mario/bigCrouchingMarioR");
                        Columns = 1;
                        animated = false;
                        break;
                    case "JumpingState":
                        if (context.facingLeft)
                            texture = Theatre.Content.Load<Texture2D>("mario/bigJumpingMarioL");
                        else
                            texture = Theatre.Content.Load<Texture2D>("mario/bigJumpingMarioR");
                        Columns = 1;
                        animated = false;
                        break;
                    case "FallingState":
                        if (context.facingLeft)
                            texture = Theatre.Content.Load<Texture2D>("mario/bigFallingMarioL");
                        else
                            texture = Theatre.Content.Load<Texture2D>("mario/bigFallingMarioR");
                        Columns = 1;
                        animated = false;
                        break;
                    case "RunningState":
                        if (context.facingLeft)
                            texture = Theatre.Content.Load<Texture2D>("mario/bigRunningMarioL");
                        else
                            texture = Theatre.Content.Load<Texture2D>("mario/bigRunningMarioR");
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
                            texture = Theatre.Content.Load<Texture2D>("mario/fireIdleMarioL");
                        else
                            texture = Theatre.Content.Load<Texture2D>("mario/fireIdleMarioR");
                        Columns = 1;
                        animated = false;
                        break;
                    case "CrouchingState":
                        if (context.facingLeft)
                            texture = Theatre.Content.Load<Texture2D>("mario/fireCrouchingMarioL");
                        else
                            texture = Theatre.Content.Load<Texture2D>("mario/fireCrouchingMarioR");
                        Columns = 1;
                        animated = false;
                        break;
                    case "JumpingState":
                        if (context.facingLeft)
                            texture = Theatre.Content.Load<Texture2D>("mario/fireJumpingMarioL");
                        else
                            texture = Theatre.Content.Load<Texture2D>("mario/fireJumpingMarioR");
                        Columns = 1;
                        animated = false;
                        break;
                    case "FallingState":
                        if (context.facingLeft)
                            texture = Theatre.Content.Load<Texture2D>("mario/fireFallingMarioL");
                        else
                            texture = Theatre.Content.Load<Texture2D>("mario/fireFallingMarioR");
                        Columns = 1;
                        animated = false;
                        break;
                    case "RunningState":
                        if (context.facingLeft)
                            texture = Theatre.Content.Load<Texture2D>("mario/fireRunningMarioL");
                        else
                            texture = Theatre.Content.Load<Texture2D>("mario/fireRunningMarioR");
                        Columns = 3;
                        animated = true;
                        break;
                }
            }

            if (context.GetPowerUpState().ToString().Equals("DeadMario"))
            {
                texture = Theatre.Content.Load<Texture2D>("mario/deadMario");
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
            if (Math.Abs(context.Velocity.X) > 0 || Math.Abs(context.Velocity.Y) > 0)
            {
                context.isTouchingLeft = false;
                context.isTouchingRight = false;
            }
            if (context.GetPowerUpState().ToString().Equals("StandardMario"))
                hitbox = new Rectangle((int)position.X, (int)position.Y, 14, 20);
            else
                hitbox = new Rectangle((int)position.X, (int)position.Y, 15, 28);
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
            if (ShowHitbox)
            {
                Texture2D hitboxTextureW = new Texture2D(spriteBatch.GraphicsDevice, hitbox.Width, 1);
                Texture2D hitboxTextureH = new Texture2D(spriteBatch.GraphicsDevice, 1, hitbox.Height);
                Color[] dataW = new Color[hitbox.Width];
                for (int i = 0; i < dataW.Length; i++) dataW[i] = Color.Yellow;
                Color[] dataH = new Color[hitbox.Height];
                for (int i = 0; i < dataH.Length; i++) dataH[i] = Color.Yellow;
                hitboxTextureW.SetData(dataW);
                hitboxTextureH.SetData(dataH);
                spriteBatch.Draw(hitboxTextureW, new Vector2((int)hitbox.X, (int)hitbox.Y), Color.White);
                spriteBatch.Draw(hitboxTextureW, new Vector2((int)hitbox.X, (int)hitbox.Y + (int)hitbox.Height), Color.White);
                spriteBatch.Draw(hitboxTextureH, new Vector2((int)hitbox.X, (int)hitbox.Y), Color.White);
                spriteBatch.Draw(hitboxTextureH, new Vector2((int)hitbox.X + (int)hitbox.Width, (int)hitbox.Y), Color.White);
            }
        

    }

        public void Collision(ISprite collider, int xOffset, int yOffset)
        {
            //Blocks
            if (collider is BlockContext || collider is Pipe || collider is Goomba || collider is Koopa)
            {
                if (hitbox.TouchTopOf(collider.Hitbox))
                {
                    hitbox.Y = collider.Hitbox.Y - hitbox.Height - 1;
                    position.Y = hitbox.Y;
                    context.Velocity.Y = 0f;
                    System.Diagnostics.Debug.WriteLine("mario hit the top of something");
                    if (collider is Goomba || collider is Koopa)
                        collider.Collision(null, -1, -1);
                }
                if (hitbox.TouchLeftOf(collider.Hitbox))
                {
                    hitbox.X = collider.Hitbox.X - hitbox.Width - 4;
                    position.X = hitbox.X;
                    System.Diagnostics.Debug.WriteLine("mario hit the left of something");
                    if (collider is Goomba || collider is Koopa)
                        context.TakeDamage();
                    context.isTouchingLeft = true;
                }
                if (hitbox.TouchRightOf(collider.Hitbox))
                {
                    hitbox.X = collider.Hitbox.X + hitbox.Width + 4;
                    position.X = hitbox.X;
                    System.Diagnostics.Debug.WriteLine("mario hit the right of something");
                    if (collider is Goomba || collider is Koopa)
                        context.TakeDamage();
                    context.isTouchingRight = true;
                }
                if (hitbox.TouchBottomOf(collider.Hitbox))
                {
                    hitbox.Y = collider.Hitbox.Y + hitbox.Height + 1;
                    position.Y = hitbox.Y;
                    if (collider is Goomba || collider is Koopa)
                        context.TakeDamage();

                    //context.Velocity.Y = 0f;
                    System.Diagnostics.Debug.WriteLine("mario hit the bottom of something");
                }
            }
            //Pickups
            else
            {
                if (hitbox.TouchTopOf(collider.Hitbox) || hitbox.TouchRightOf(collider.Hitbox)
                    || hitbox.TouchLeftOf(collider.Hitbox) || hitbox.TouchBottomOf(collider.Hitbox))
                {
                    if (collider is FireFlower)
                    {
                        collider.Collision(null, -1, -1);
                        context.GetFireFlower();
                    }
                    if (collider is RedMushroom)
                    {
                        collider.Collision(null, -1, -1);
                        context.GetMushroom();
                    }
                    if (collider is Coin)
                    {
                        collider.Collision(null, -1, -1);
                    }
                    if (collider is GreenMushroom)
                    {
                        collider.Collision(null, -1, -1);
                    }
                    if (collider is Star)
                    {
                        collider.Collision(null, -1, -1);
                    }
                }
            }
            if (position.X < 0)
                position.X = 0;

            if (position.X > xOffset - hitbox.Width)
                position.X = xOffset - hitbox.Width;

            if (position.Y < 0)
                position.Y = 0;

            if (position.Y > yOffset - hitbox.Height)
                position.Y = yOffset - hitbox.Height;

        }
    }
}
