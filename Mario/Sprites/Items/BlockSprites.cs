using System;
using Mario.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mario;
using Mario.States;


namespace Mario.Sprites
{
    public abstract class BlockSprite : ISprite
    {
        protected Texture2D Texture;
        protected int width;
        protected int height;
        protected int row;
        protected int column;
        protected int Rows;
        protected int Columns;
        protected Vector2 Location;
        protected int moveDistance;
        protected int moveRange;
        protected bool moving;
        protected int currentFrame;
        protected int totalFrames;

        protected Rectangle sourceRectangle;
        protected Rectangle destinationRectangle;
        protected BlockContext Context;
        protected int timeSinceLastFrame;
        protected int millisecondsPerFrame;
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            
            
            row = currentFrame / Columns;
            column = currentFrame % Columns;

             sourceRectangle = new Rectangle(width * column, height * row, width, height);
             destinationRectangle = new Rectangle((int)Location.X, (int)Location.Y, width, height);

            
            spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);
            
        }

        public void setMovement(int range)
        {
            moveRange = range;
            moving = true;
        }

        public Vector2 GetLocation()
        {
            return Location;
        }

        public virtual void Update()
        {
            if (moveDistance < moveRange && moving)
            {
                Location.Y++;
                moveDistance++;
            }
            else if (moveDistance == moveRange)
            {
                moving = false;
            }
            else if (moveDistance > 0)
            {
                Location.Y--;
                moveDistance--;
            }
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
    public class BrickBlockSprite : BlockSprite
    {
        public BrickBlockSprite(Game1 theatre, Vector2 location, BlockContext context)
        {
            
            Texture = theatre.Content.Load<Texture2D>("obstacles/Brick Block");
            
            Location = location;
            moveDistance = 0;
            moveRange = 0;
            Rows = 1;
            Columns = 1;
            currentFrame = 0;
            totalFrames = 1;            
            width = Texture.Width;
            height = Texture.Height;
            Context = context;
            timeSinceLastFrame = 0;
            millisecondsPerFrame = 10;


        }        
    }

    public class QuestionBlockSprite : BlockSprite
    {
        public QuestionBlockSprite(Game1 theatre, Vector2 location, BlockContext context)
        {

            Texture = theatre.Content.Load<Texture2D>("obstacles/Item Block");

            Location = location;
            moveDistance = 0;
            moveRange = 0;
            Rows = 1;
            Columns = 4;
            currentFrame = 0;
            totalFrames = 4;
            width = Texture.Width/4;
            height = Texture.Height;
            Context = context;
            timeSinceLastFrame = 0;
            millisecondsPerFrame = 10;
        }
    }
    public class UsedBlockSprite : BlockSprite
    {
        public UsedBlockSprite(Game1 theatre, Vector2 location, BlockContext context)
        {

            Texture = theatre.Content.Load<Texture2D>("obstacles/Used Item Block");

            Location = location;
            moveDistance = 0;
            moveRange = 0;
            Rows = 1;
            Columns = 1;
            currentFrame = 0;
            totalFrames = 1;
            width = Texture.Width;
            height = Texture.Height;
            Context = context;
            timeSinceLastFrame = 0;
            millisecondsPerFrame = 10;
        }
    }
    public class HiddenBlockSprite : BlockSprite
    {
        public HiddenBlockSprite(Game1 theatre, Vector2 location, BlockContext context)
        {

            Texture = theatre.Content.Load<Texture2D>("obstacles/Item Block");

            Location = location;
            moveDistance = 0;
            moveRange = 0;
            Rows = 1;
            Columns = 1;
            currentFrame = 0;
            totalFrames = 1;
            width = Texture.Width;
            height = Texture.Height;
            Context = context;
            timeSinceLastFrame = 0;
            millisecondsPerFrame = 10;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {

        }
    }

    public class BrokenBlockSprite : BlockSprite
    {
        Boolean rubbleActive;
        Game1 Theatre;
        public BrokenBlockSprite(Game1 theatre, Vector2 location, BlockContext context)
        {

            Texture = theatre.Content.Load<Texture2D>("obstacles/Block Debris");

            Location = location;
            moveDistance = 0;
            moveRange = 0;
            Rows = 1;
            Columns = 8;
            currentFrame = 0;
            totalFrames = 8;
            width = Texture.Width/8;
            height = Texture.Height;
            rubbleActive = false;
            Context = context;
            Theatre = theatre;
            timeSinceLastFrame = 0;
            millisecondsPerFrame = 10;

        }
        public override void Update()
        {
            if (rubbleActive)
            {
                currentFrame++;
                if (currentFrame == totalFrames)
                {
                    currentFrame = 0;

                }
                Location.Y--;
                if(Location.Y < Theatre.GraphicsDevice.PresentationParameters.BackBufferHeight)
                {
                    ToggleRubble();
                }
            }
            
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            if(rubbleActive)
            base.Draw(spriteBatch);
        }
        public void ToggleRubble()
        {
            rubbleActive = !rubbleActive;
        }
        
        

        }
        
    }

