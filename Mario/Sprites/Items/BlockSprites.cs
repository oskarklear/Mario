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
        public Game1 Theatre;
        public bool moving;
        public bool animated;
        protected int currentFrame;
        protected int totalFrames;
        protected int timer;
        public bool spinning;
        Vector2 position;
        protected Rectangle sourceRectangle;
        protected Rectangle destinationRectangle;
        public Rectangle Hitbox
        {
            get { return destinationRectangle;}
        }
        public Vector2 Position
        {
            get { return position; }
        }
        private bool showHitbox;
        public bool ShowHitbox
        {
            get { return showHitbox; }
            set { showHitbox = value; }
        }
        public bool isShell { get; set; }
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
            if (showHitbox)
            {
                Texture2D hitboxTextureW = new Texture2D(spriteBatch.GraphicsDevice, Hitbox.Width, 1);
                Texture2D hitboxTextureH = new Texture2D(spriteBatch.GraphicsDevice, 1, Hitbox.Height);
                Color[] dataW = new Color[Hitbox.Width];
                for (int i = 0; i < dataW.Length; i++) dataW[i] = Color.Blue;
                Color[] dataH = new Color[Hitbox.Height];
                for (int i = 0; i < dataH.Length; i++) dataH[i] = Color.Blue;
                hitboxTextureW.SetData(dataW);
                hitboxTextureH.SetData(dataH);
                spriteBatch.Draw(hitboxTextureW, new Vector2((int)Hitbox.X, (int)Hitbox.Y), Color.White);
                spriteBatch.Draw(hitboxTextureW, new Vector2((int)Hitbox.X, (int)Hitbox.Y + (int)Hitbox.Height), Color.White);
                spriteBatch.Draw(hitboxTextureH, new Vector2((int)Hitbox.X, (int)Hitbox.Y), Color.White);
                spriteBatch.Draw(hitboxTextureH, new Vector2((int)Hitbox.X + (int)Hitbox.Width, (int)Hitbox.Y), Color.White);
            }
        }

        public void setMovement(int range)
        {
            moveRange = range;
            moving = true;
        }

        public bool Delete()
        {
            return false;
        }

        public void Spin()
        {
            this.Texture = Theatre.Content.Load<Texture2D>("spinningblock");
            this.destinationRectangle = Rectangle.Empty;
            this.Columns = 4;
            this.totalFrames = 4;
            spinning = true;
        }

        public Vector2 GetLocation()
        {
            return Location;
        }

        public virtual void Update()
        {
            if (spinning)
                timer--;
            if (timer < 0 && this is BrickBlockSprite)
            {
                spinning = false;
            }
            if (!spinning && this is BrickBlockSprite)
            {
                this.Texture = Theatre.Content.Load<Texture2D>("obstacles/Brick Block");
                timer = 350;
                spinning = false;
                //this.destinationRectangle
                this.Columns = 1;
                this.totalFrames = 1;
            }

            if (moveDistance < moveRange && moving)
            {
                Location.Y-=3;
                moveDistance++;
            }
            else if (moveDistance == moveRange&&moving==true)
            {
                moving = false;
            }
            else if (moveDistance > 0)
            {
                Location.Y+=3;
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

        public void Collision(ISprite collider)
        {

        }
    }

    public class BrickBlockSprite : BlockSprite
    {
        public BrickBlockSprite(Game1 theatre, Vector2 location, BlockContext context)
        {
            Theatre = theatre;
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
            ShowHitbox = false;
            timer = 350;
        }        
    }

    public class GroundBlockSprite : BlockSprite
    {
        public GroundBlockSprite(Game1 theatre, Vector2 location, BlockContext context)
        {
            Texture = theatre.Content.Load<Texture2D>("obstacles/GroundBlock");
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
            ShowHitbox = false;
        }
    }

    public class QuestionBlockSprite : BlockSprite
    {
        public QuestionBlockSprite(Game1 theatre, Vector2 location, BlockContext context)
        {
            Texture = theatre.Content.Load<Texture2D>("obstacles/Item Block");
            ShowHitbox = false;
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

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }

    public class UsedBlockSprite : BlockSprite
    {
        public UsedBlockSprite(Game1 theatre, Vector2 location, BlockContext context)
        {
            Texture = theatre.Content.Load<Texture2D>("obstacles/Used Item Block");
            ShowHitbox = false;
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
            ShowHitbox = false;
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
    public class UGroundBlockSprite : BlockSprite
    {
        public UGroundBlockSprite(Game1 theatre, Vector2 location, BlockContext context)
        {

            Texture = theatre.Content.Load<Texture2D>("obstacles/UGroundBlock");
            ShowHitbox = false;
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
    public class HardBlockSprite : BlockSprite
    {
        public HardBlockSprite(Game1 theatre, Vector2 location, BlockContext context)
        {

            Texture = theatre.Content.Load<Texture2D>("obstacles/HardBlock");
            ShowHitbox = false;
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
    public class UBrickBlockSprite : BlockSprite
    {
        public UBrickBlockSprite(Game1 theatre, Vector2 location, BlockContext context)
        {
            Texture = theatre.Content.Load<Texture2D>("obstacles/UBrickBlock");
            ShowHitbox = false;
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

    public class BrokenBlockSprite : BlockSprite
    {
        Boolean rubbleActive;
        Game1 Theatre;
        public BrokenBlockSprite(Game1 theatre, Vector2 location, BlockContext context)
        {
            Texture = theatre.Content.Load<Texture2D>("obstacles/Block Debris");
            ShowHitbox = false;
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
                Location.Y++;
                if(Location.Y > Theatre.GraphicsDevice.PresentationParameters.BackBufferHeight)
                {
                    ToggleRubble();
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (rubbleActive)
            {
                base.Draw(spriteBatch);
            }
        }

        public void ToggleRubble()
        {
            rubbleActive = !rubbleActive;
        }
    }    
}

