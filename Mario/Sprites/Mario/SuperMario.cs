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
    public class SuperMario : SpriteTemplate
    {
        protected const int MAPW = 3584;
        protected const int MAPH = 272;
        protected const int delaytime = 100;

        DynamicEntities entities;
        Kinematics kinematics;
        int fireballCooldown;
        public bool warp;
        public bool warped;
        public bool isWarpableHorizontal;
        public bool isWarpableVertical;
        public bool overworld;
        int delay;
        public Vector2 spawn;

        public MarioContext context { get; set; }

        public override bool ShowHitbox
        {
            get { return context.ShowHitbox; }
            set { context.ShowHitbox = value; }
        }
        private int deathTimer; //Used for the death animation
        private int topHeight; //Used for the death animation



        public SuperMario(Game1 theatre, Vector2 location, MarioContext context)
        {
            gameObj = theatre;
            texture = theatre.Content.Load<Texture2D>("mario/smallIdleMarioR");
            position = location;
            spawn = location;
            hitbox = new Rectangle((int)position.X, (int)position.Y, 14, 20);
            timeSinceLastFrame = 0;
            millisecondsPerFrame = 6;
            rows = 1;
            columns = 1;
            overworld = true;
            entities = theatre.map.entities;
            this.context = context;
            kinematics = new Kinematics();
            delay = 0;
            deathTimer = 60;
            topHeight = 30;
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
                if (!isWarpableVertical)
                {
                    context.GetActionState().FaceRightTransition();
                } else if (!warped)
                {
                    initiateWarp();
                }
                

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
                if (!isWarpableHorizontal)
                {
                    context.GetActionState().FallingTransition();
                }
                else if (!warped && overworld)
                {
                    initiateWarp();
                }
                
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
                context.fireball.Play();
                entities.fireBallObjs.Add(new Fireball(gameObj, position, this, context.facingLeft));
                fireballCooldown = 0;
            }
        }

        public void IdleCommand()
        {

        }

        int warpAnimCount = 0;

        public override void Update()
        {
            if (!warp)
            {

                fireballCooldown += 1;
                if (context.GetPowerUpState().ToString().Equals("StandardMario"))
                {
                    switch (context.GetActionState().ToString())
                    {
                        case "IdleState":
                            if (context.facingLeft)
                                texture = gameObj.Content.Load<Texture2D>("mario/smallIdleMarioL");
                            else
                                texture = gameObj.Content.Load<Texture2D>("mario/smallIdleMarioR");
                            columns = 1;
                            isAnimated = false;
                            break;
                        case "CrouchingState":
                            if (context.facingLeft)
                                texture = gameObj.Content.Load<Texture2D>("mario/smallCrouchingMarioL");
                            else
                                texture = gameObj.Content.Load<Texture2D>("mario/smallCrouchingMarioR");
                            columns = 1;
                            isAnimated = false;
                            break;
                        case "JumpingState":
                            if (context.facingLeft)
                                texture = gameObj.Content.Load<Texture2D>("mario/smallJumpingMarioL");
                            else
                                texture = gameObj.Content.Load<Texture2D>("mario/smallJumpingMarioR");
                            columns = 1;
                            isAnimated = false;
                            break;
                        case "FallingState":
                            if (context.facingLeft)
                                texture = gameObj.Content.Load<Texture2D>("mario/smallFallingMarioL");
                            else
                                texture = gameObj.Content.Load<Texture2D>("mario/smallFallingMarioR");
                            columns = 1;
                            isAnimated = false;
                            break;
                        case "RunningState":
                            if (context.facingLeft)
                                texture = gameObj.Content.Load<Texture2D>("mario/smallRunningMarioL");
                            else
                                texture = gameObj.Content.Load<Texture2D>("mario/smallRunningMarioR");
                            columns = 3;
                            isAnimated = true;
                            break;
                    }
                }
            fireballCooldown += 1;
            if (context.GetPowerUpState().ToString().Equals("StandardMario"))
            {
                switch (context.GetActionState().ToString())
                {
                    case "IdleState":
                        if (context.facingLeft)
                            texture = gameObj.Content.Load<Texture2D>("mario/smallIdleMarioL");
                        else
                            texture = gameObj.Content.Load<Texture2D>("mario/smallIdleMarioR");
                        columns = 1;
                        isAnimated = false;
                        break;
                    case "CrouchingState":
                        if (context.facingLeft)
                            texture = gameObj.Content.Load<Texture2D>("mario/smallCrouchingMarioL");
                        else
                            texture = gameObj.Content.Load<Texture2D>("mario/smallCrouchingMarioR");
                        columns = 1;
                        isAnimated = false;
                        break;
                    case "JumpingState":
                        if (context.facingLeft)
                            texture = gameObj.Content.Load<Texture2D>("mario/smallJumpingMarioL");
                        else
                            texture = gameObj.Content.Load<Texture2D>("mario/smallJumpingMarioR");
                        columns = 1;
                        isAnimated = false;
                        break;
                    case "FallingState":
                        if (context.facingLeft)
                            texture = gameObj.Content.Load<Texture2D>("mario/smallFallingMarioL");
                        else
                            texture = gameObj.Content.Load<Texture2D>("mario/smallFallingMarioR");
                        columns = 1;
                        isAnimated = false;
                        break;
                    case "RunningState":
                        if (context.facingLeft)
                            texture = gameObj.Content.Load<Texture2D>("mario/smallRunningMarioL");
                        else
                            texture = gameObj.Content.Load<Texture2D>("mario/smallRunningMarioR");
                        columns = 3;
                        isAnimated = true;
                        break;
                }
            }

            if (context.GetPowerUpState().ToString().Equals("SuperMario"))
            {
                switch (context.GetActionState().ToString())
                {
                    case "IdleState":
                        if (context.facingLeft)
                            texture = gameObj.Content.Load<Texture2D>("mario/bigIdleMarioL");
                        else
                            texture = gameObj.Content.Load<Texture2D>("mario/bigIdleMarioR");
                        columns = 1;
                        isAnimated = false;
                        break;
                    case "CrouchingState":
                        if (context.facingLeft)
                            texture = gameObj.Content.Load<Texture2D>("mario/bigCrouchingMarioL");
                        else
                            texture = gameObj.Content.Load<Texture2D>("mario/bigCrouchingMarioR");
                        columns = 1;
                        isAnimated = false;
                        break;
                    case "JumpingState":
                        if (context.facingLeft)
                            texture = gameObj.Content.Load<Texture2D>("mario/bigJumpingMarioL");
                        else
                            texture = gameObj.Content.Load<Texture2D>("mario/bigJumpingMarioR");
                        columns = 1;
                        isAnimated = false;
                        break;
                    case "FallingState":
                        if (context.facingLeft)
                            texture = gameObj.Content.Load<Texture2D>("mario/bigFallingMarioL");
                        else
                            texture = gameObj.Content.Load<Texture2D>("mario/bigFallingMarioR");
                        columns = 1;
                        isAnimated = false;
                        break;
                    case "RunningState":
                        if (context.facingLeft)
                            texture = gameObj.Content.Load<Texture2D>("mario/bigRunningMarioL");
                        else
                            texture = gameObj.Content.Load<Texture2D>("mario/bigRunningMarioR");
                        columns = 3;
                        isAnimated = true;
                        break;
                }
            }

            if (context.GetPowerUpState().ToString().Equals("FireMario"))
            {
                switch (context.GetActionState().ToString())
                {
                    case "IdleState":
                        if (context.facingLeft)
                            texture = gameObj.Content.Load<Texture2D>("mario/fireIdleMarioL");
                        else
                            texture = gameObj.Content.Load<Texture2D>("mario/fireIdleMarioR");
                        columns = 1;
                        isAnimated = false;
                        break;
                    case "CrouchingState":
                        if (context.facingLeft)
                            texture = gameObj.Content.Load<Texture2D>("mario/fireCrouchingMarioL");
                        else
                            texture = gameObj.Content.Load<Texture2D>("mario/fireCrouchingMarioR");
                        columns = 1;
                        isAnimated = false;
                        break;
                    case "JumpingState":
                        if (context.facingLeft)
                            texture = gameObj.Content.Load<Texture2D>("mario/fireJumpingMarioL");
                        else
                            texture = gameObj.Content.Load<Texture2D>("mario/fireJumpingMarioR");
                        columns = 1;
                        isAnimated = false;
                        break;
                    case "FallingState":
                        if (context.facingLeft)
                            texture = gameObj.Content.Load<Texture2D>("mario/fireFallingMarioL");
                        else
                            texture = gameObj.Content.Load<Texture2D>("mario/fireFallingMarioR");
                        columns = 1;
                        isAnimated = false;
                        break;
                    case "RunningState":
                        if (context.facingLeft)
                            texture = gameObj.Content.Load<Texture2D>("mario/fireRunningMarioL");
                        else
                            texture = gameObj.Content.Load<Texture2D>("mario/fireRunningMarioR");
                        columns = 3;
                        isAnimated = true;
                        break;
                }
            }

            if (context.GetPowerUpState().ToString().Equals("DeadMario"))
            {
                context.Velocity.X = 0f;
                context.Velocity.Y = 0f;
                texture = gameObj.Content.Load<Texture2D>("mario/deadMario");
                columns = 2;
                isAnimated = true;
                //hitbox = Rectangle.Empty;
                MediaPlayer.Stop();
                deathTimer--;
                if (deathTimer <= 0 && topHeight > 0)
                {
                    position.Y -= 1;
                    topHeight--;
                }
                else if (deathTimer <= 0)
                {
                    position.Y += 1;
                }
            }

            if (isAnimated)
            {
                if (timeSinceLastFrame > millisecondsPerFrame)
                {
                    currentFrame++;
                    timeSinceLastFrame = 0;
                }
                if (currentFrame == columns)
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
            } else
            {
                // if warping
                hitbox = Rectangle.Empty;
                if (overworld)
                {
                    if (warpAnimCount < 30)
                    {
                        position.Y += 1;
                        //position.X += 10;
                        warpAnimCount++;
                    }
                    else
                    {
                        warped = true;
                        overworld = !overworld;
                        //System.Diagnostics.Debug.WriteLine("BIG ASS");
                    }
                }
                else
                {
                    if (warpAnimCount < 30)
                    {
                        position.X += 1;
                        //position.X += 10;
                        warpAnimCount++;
                    }
                    else
                    {
                        warped = true;
                        overworld = !overworld;
                        //System.Diagnostics.Debug.WriteLine("BIG ASS");
                    }
                }
                

            }
            isWarpableHorizontal = false;
            isWarpableVertical = false;
    }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (delay % 10 == 0)
            {
                if (isAnimated)
                {
                    int width = texture.Width / columns;
                    int height = texture.Height / rows;
                    int row = currentFrame / columns;
                    int column = currentFrame % columns;

                    Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
                    Rectangle destinationRectangle = new Rectangle((int)position.X - 7, (int)position.Y, width, height);
                    spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, Color.White);
                }
                else
                {
                    spriteBatch.Draw(texture, position, Color.White);
                }
            }

            MakeHitbox(spriteBatch, showHitbox);

            if (delay > 0)
            {
                delay--;
            }
        }
       
        private void initiateWarp()
        {

            warp = true;
            warpAnimCount = 0;
            //System.Diagnostics.Debug.WriteLine("fuck");
        }

        public override void Collision(ISprite collider)
        {
            if (collider is BlockContext || collider is Pipe || collider is Goomba || collider is Koopa || collider is Piranha || collider is SidePipe || collider is LongPipe)
            {
                if (collider is Pipe)
                {
                    if (hitbox.TouchTopOf((collider as Pipe).WarpHitbox) && (collider as Pipe).Warpable)
                    {
                        isWarpableHorizontal = true;
                        System.Diagnostics.Debug.WriteLine("big farty");
                    }
                }
                if (collider is SidePipe)
                {
                    if (hitbox.TouchLeftOf((collider as SidePipe).hitbox))
                    {
                        isWarpableVertical = true;
                        System.Diagnostics.Debug.Write("big farty left");
                    }
                }
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
                        if (collider is Piranha)
                        {
                            System.Diagnostics.Debug.WriteLine("OWOWOWOWOWOWOWWOW");
                            if (delay <= 0)
                            {
                                context.TakeDamage();
                                delay = delaytime;
                            }
                        }
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

                        if (collider is Goomba || collider is Piranha)
                        {
                            System.Diagnostics.Debug.WriteLine("OWOWOWOWOWOWOWWOW");
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

                        if (collider is Goomba || collider is Piranha)
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
                            if (collider is Goomba || collider is Koopa || collider is Piranha)
                            {
                                System.Diagnostics.Debug.WriteLine("OWOWOWOWOWOWOWWOW");
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
                    else if (collider is GoalGateMovingPart)
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
    }
}
