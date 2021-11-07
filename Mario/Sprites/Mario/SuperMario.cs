using System;
using System.Collections.Generic;
using System.Text;
using Mario.Sprites.Enemies;
using Mario.Sprites.Items;
using Mario.States;
using Mario.Movement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Mario.Entities;
using Mario.Sprites.Projectiles;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;

namespace Mario.Sprites.Mario
{
    public class SuperMario : ISprite
    {
        protected const int MAPW = 3584;
        protected const int MAPH = 272;
        protected const int delaytime = 100;
        int fireballCooldown;
        DynamicEntities entities;
        public MarioContext context { get; set; }
        public bool animated { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }
        private int currentFrame;
        private int timeSinceLastFrame;
        private int millisecondsPerFrame;
        int delay;
        Texture2D texture;
        Game1 Theatre;
        public Vector2 position;
        public Vector2 Position
        {
            get { return position; }
        }
        Rectangle hitbox;
        public Kinematics kinematics;
        public bool ShowHitbox
        {
            get { return context.ShowHitbox; }
            set { context.ShowHitbox = value; }
        }
        public Rectangle Hitbox 
        {
            get { return hitbox; }
            set { hitbox = value; }
        }

        public bool isShell { get; set; }
        public bool delete()
        {
            return false;
        }
        private int deathTimer; //Used for the death animation
        private int topHeight; //Used for the death animation
        private bool hitTopHeight; //Used for the death animation


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
            kinematics = new Kinematics();
            delay = 0;
            entities = theatre.map.entities;
            deathTimer = 60;
        }

        public void MoveLeftCommand()
        {
            if (!(context.GetPowerUpState() is DeadMarioState))
            {
                context.GetActionState().FaceLeftTransition();
            }           
        }

        public void MoveRightCommand()
        {
            if (!(context.GetPowerUpState() is DeadMarioState))
            {
                context.GetActionState().FaceRightTransition();
            }
        }

        public void JumpCommand()
        {
            if (!(context.GetPowerUpState() is DeadMarioState))
            {
                context.GetActionState().JumpingTransition();
            }
        }

        public void CrouchCommand()
        {          
            if (!(context.GetPowerUpState() is DeadMarioState))
            {
                context.GetActionState().FallingTransition();
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
        }

        public void FaceRightDiscontinueCommand()
        {
            context.GetActionState().FaceRightDiscontinueTransition();
        }

        public void FireCommand()
        {
            if (context.GetPowerUpState() is FireMarioState && entities.fireBallObjs.Count < 2 && fireballCooldown > 20)
            {
                entities.fireBallObjs.Add(new Fireball(Theatre, position, this, context.facingLeft));
                fireballCooldown = 0;
            }
        }

        public void IdleCommand()
        {

        }

        public void Update()
        {
            fireballCooldown += 1;
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
                        Columns = 3;
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
                context.Velocity.X = 0f;
                context.Velocity.Y = 0f;
                texture = Theatre.Content.Load<Texture2D>("mario/deadMario");
                Columns = 2;
                animated = true;
                //hitbox = Rectangle.Empty;
                MediaPlayer.Stop();
                topHeight = (int)position.X + 8;
                deathTimer--;
                if (deathTimer <= 0 && position.Y > topHeight && !hitTopHeight)
                {
                    position.Y -= 1;
                    if (position.Y >= topHeight)
                        hitTopHeight = true;
                }
                else if (deathTimer <= 0 && hitTopHeight)
                {
                    position.Y += 1;
                }
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

            //If mario moves to the right or left, he cannot be touching anything
            if (Math.Abs(context.Velocity.X) > 0 || Math.Abs(context.Velocity.Y) > 0)
            {
                context.isTouchingLeft = false;
                context.isTouchingRight = false;
            }

            //If mario is not touching ground, GRAVITY
            if (!context.isTouchingTop)
            {
                //kinematics.AccelerateDown(context);
                if (context.GetActionState() is FallingState)
                {
                    kinematics.AccelerateDown(context);
                }
                else
                {
                    if (context.isFalling)
                        context.GetActionState().FallingTransition();
                }                
                if (context.Velocity.Y < 0)
                {
                    context.isFalling = true;
                }
            }
            else
            {
                if (context.isFalling)
                    context.jumpHeight = 0;
                context.isFalling = false;
            }

            if (context.GetPowerUpState().ToString().Equals("StandardMario"))
                hitbox = new Rectangle((int)position.X, (int)position.Y, 14, 20);
            //else if (context.GetPowerUpState().ToString().Equals("DeadMario"))
                //hitbox = Rectangle.Empty;
            else
            {
                if (context.GetActionState().ToString().Equals("CrouchingState"))
                    hitbox = new Rectangle((int)position.X, (int)position.Y + 12, 15, 15);
                else
                    hitbox = new Rectangle((int)position.X, (int)position.Y, 15, 28);
            }

            //Reset collision
            context.isTouchingTop = false;
            context.isTouchingBottom = false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (delay % 10 == 0)
            {
                if (animated)
                {
                    int width = texture.Width / Columns;
                    int height = texture.Height / Rows;
                    int row = currentFrame / Columns;
                    int column = currentFrame % Columns;

                    Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
                    Rectangle destinationRectangle = new Rectangle((int)position.X - 7, (int)position.Y, width, height);
                    spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, Color.White);
                }
                else
                {
                    spriteBatch.Draw(texture, position, Color.White);
                }
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

            if (delay > 0)
            {
                delay--;
            }
        }

        public void Collision(ISprite collider)
        {
            if (collider is BlockContext || collider is Pipe || collider is Goomba || collider is Koopa)
            {
                if (collider is BlockContext && ((collider as BlockContext).GetState() is HiddenBlockState))
                {
                    if (hitbox.TouchBottomOf(collider.Hitbox))
                    {
                        hitbox.Y = collider.Hitbox.Y - hitbox.Height;
                        position.Y = hitbox.Y;
                    }
                }
                else
                {
                    if (hitbox.TouchTopOf(collider.Hitbox))
                    {
                        hitbox.Y = collider.Hitbox.Y - hitbox.Height - 1;
                        position.Y = hitbox.Y;
                        context.Velocity.Y = 0f;                        
                        if (collider is Goomba)
                        {
                            //collider.Collision(this);
                            context.stomp.Play();
                            context.Velocity.Y = 4f;                           
                            collider.Collision(this);
                            context.jumpHeight = 0;
                            System.Diagnostics.Debug.WriteLine(context.GetActionState().ToString());
                            context.jumpingState.Enter(context.GetActionState());
                        }

                        if (collider is Koopa)
                        {
                            //collider.Collision(this);
                            context.stomp.Play();
                            collider.Collision(this);
                            context.Velocity.Y = 4f;
                            context.jumpHeight = 0;
                            System.Diagnostics.Debug.WriteLine(context.GetActionState().ToString());
                            context.jumpingState.Enter(context.GetActionState());
                        }
                        context.isTouchingTop = true;
                    }

                    if (hitbox.TouchLeftOf(collider.Hitbox))
                    {
                        hitbox.X = collider.Hitbox.X - hitbox.Width;
                        position.X = hitbox.X;

                        if (collider is Goomba)
                        {
                            if (delay <= 0)
                            {
                                context.TakeDamage();
                                if (!context.GetPowerUpState().ToString().Equals("DeadMario"))
                                    delay = delaytime;
                            }
                        }
                        if (collider is Koopa)
                        {
                            if (collider.isShell)
                            {
                                collider.Collision(this);
                            }
                            else
                            {
                                context.TakeDamage();
                                if (!context.GetPowerUpState().ToString().Equals("DeadMario"))
                                    delay = delaytime;
                            }
                        }
                        context.isTouchingLeft = true;
                    }

                    if (hitbox.TouchRightOf(collider.Hitbox))
                    {
                        if (!(collider is Pipe))
                            hitbox.X = collider.Hitbox.X + hitbox.Width + 5;
                        else
                            hitbox.X = collider.Hitbox.X + hitbox.Width + 18;
                        position.X = hitbox.X;

                        if (collider is Goomba)
                        {
                            if (delay <= 0)
                            {
                                context.TakeDamage();
                                if (!context.GetPowerUpState().ToString().Equals("DeadMario"))
                                    delay = delaytime;
                            }
                        }
                        if (collider is Koopa)
                        {
                            if (collider.isShell)
                            {
                                collider.Collision(this);
                            }
                            else
                            {
                                context.TakeDamage();
                                if (!context.GetPowerUpState().ToString().Equals("DeadMario"))
                                    delay = delaytime;
                            }
                        }
                        context.isTouchingRight = true;
                    }

                    if (hitbox.TouchBottomOf(collider.Hitbox))
                    {
                        if (context.Velocity.Y > 0)
                        {
                            hitbox.Y = collider.Hitbox.Y + hitbox.Height;
                            position.Y = hitbox.Y;
                            if (collider is Goomba || collider is Koopa)
                            {
                                if (delay <= 0)
                                {
                                    context.TakeDamage();
                                    if (!context.GetPowerUpState().ToString().Equals("DeadMario"))
                                        delay = delaytime;
                                }
                            }
                        }                      
                        context.GetActionState().FallingTransition();                   
                    }
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
                        collider.Collision(this);
                        context.GetFireFlower();
                    }
                    else if (collider is RedMushroom)
                    {
                        collider.Collision(this);
                        context.GetMushroom();
                    }
                    else if (collider is MapCoin)
                    {
                        context.coin.Play();
                        collider.Collision(this);
                    }
                    else if (collider is BlockCoin)
                    {
                        collider.Collision(this);
                    }
                    else if (collider is GreenMushroom)
                    {
                        context.oneup.Play();
                        collider.Collision(this);
                    }
                    else if (collider is Star)
                    {
                        collider.Collision(this);
                    }
                }
            }

            if (position.X < 0)
                position.X = 0;

            if (position.X > MAPW - hitbox.Width)
                position.X = MAPW - hitbox.Width;

            if (position.Y < 0)
                position.Y = 0;

            if (position.Y > MAPH - hitbox.Height)
            {
                context.DieInPit();
                position.Y = MAPH - hitbox.Height;
            }
        }
        private void Death()
        {

        }
    }
}
