using System;
using Mario.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mario;


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

        public virtual void Update()
        {
            if (moveDistance < moveRange && moving)
            {
                Location.X++;
            }
            else if (moveDistance == moveRange)
            {
                moving = false;
            }
            else if (moveDistance > 0)
            {
                Location.X--;
            }
            currentFrame++;
            if (currentFrame == totalFrames)
            {
                currentFrame = 0;

            }
        }
    }
    public class BrickBlockSprite : BlockSprite
    {               
        public BrickBlockSprite(Game1 theatre, Vector2 location)
        {
            
            Texture = theatre.Content.Load<Texture2D>("Brick Block");
            
            Location = location;
            moveDistance = 0;
            moveRange = 0;
            Rows = 1;
            Columns = 1;
            currentFrame = 0;
            totalFrames = 1;            
            width = Texture.Width;
            height = Texture.Height;
            

        }        
    }

    public class QuestionBlockSprite : BlockSprite
    {
        public QuestionBlockSprite(Game1 theatre, Vector2 location)
        {

            Texture = theatre.Content.Load<Texture2D>("Item Block");

            Location = location;
            moveDistance = 0;
            moveRange = 0;
            Rows = 1;
            Columns = 4;
            currentFrame = 0;
            totalFrames = 4;
            width = Texture.Width/4;
            height = Texture.Height;
        }
    }
    public class UsedBlockSprite : BlockSprite
    {
        public UsedBlockSprite(Game1 theatre, Vector2 location)
        {

            Texture = theatre.Content.Load<Texture2D>("Used Item Block");

            Location = location;
            moveDistance = 0;
            moveRange = 0;
            Rows = 1;
            Columns = 1;
            currentFrame = 0;
            totalFrames = 1;
            width = Texture.Width;
            height = Texture.Height;
        }
    }
    public class HiddenBlockSprite : BlockSprite
    {
        public HiddenBlockSprite(Game1 theatre, Vector2 location)
        {

            Texture = theatre.Content.Load<Texture2D>("Item Block");

            Location = location;
            moveDistance = 0;
            moveRange = 0;
            Rows = 1;
            Columns = 1;
            currentFrame = 0;
            totalFrames = 1;
            width = Texture.Width;
            height = Texture.Height;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {

        }
    }

    public class BrokenBlockSprite : BlockSprite
    {
        public BrokenBlockSprite(Game1 theatre, Vector2 location)
        {

            Texture = theatre.Content.Load<Texture2D>("Item Block");

            Location = location;
            moveDistance = 0;
            moveRange = 0;
            Rows = 1;
            Columns = 8;
            currentFrame = 0;
            totalFrames = 8;
            width = Texture.Width/8;
            height = Texture.Height;
        }
        public override void Update()
        {
            currentFrame++;
            if (currentFrame == totalFrames)
            {
                currentFrame = 0;

            }
            Location.Y--;

        }
    }
}
