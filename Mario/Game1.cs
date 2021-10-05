using Mario.Sprites;
using Mario.Sprites.Items;
using Mario.Sprites.Items.Items;
using Mario.Sprites.Enemies;
using Mario.Sprites.Mario;
using Mario.States;
using Mario.Map;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;
using System.Linq;
using System;

namespace Mario
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        public bool IsMenuVisible;
        SuperMario mario;
        FireFlower fireFlower;
        Coin coin;
        Star star;
        FloorBlock floorBlock;
        Koopa koopa;
        Goomba goomba;
        RedMushroom redMushroom;
        GreenMushroom greenMushroom;
        MarioContext context;
        Pipe pipe;
        IController kb;
        IController gp;
        BlockContext questionBlock;
        BlockContext hiddenBlock;
        BlockContext brickBlock;
        Vector2 QuestionBlockLocation;
        Vector2 HiddenBlockLocation;
        Vector2 BrickBlockLocation;
        string [][] mapArray;
        Level map;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            IsMenuVisible = false;
            context = new MarioContext();
            QuestionBlockLocation = new Vector2(100, 250);
            HiddenBlockLocation = new Vector2(150, 250);
            BrickBlockLocation = new Vector2(200, 250);
        }

        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 608;
            graphics.ApplyChanges();
            map = new Level();
            map.Theatre = this;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            Tiles.Content = Content;
            mapArray = File.ReadLines("map.csv").Select(x => x.Split(',')).ToArray();
            map.GenerateMap(mapArray, 16);

            spriteBatch = new SpriteBatch(GraphicsDevice);
            mario = new SuperMario(context, Content.Load<Texture2D>("mario/smallIdleMarioL")) { animated = false };
            mario.LoadContent(this.Content);
            questionBlock = new BlockContext(this, QuestionBlockLocation);
            questionBlock.SetState(new QuestionBlockState());
            hiddenBlock = new BlockContext(this, HiddenBlockLocation);
            hiddenBlock.SetState(new HiddenBlockState());
            brickBlock = new BlockContext(this, BrickBlockLocation);
            brickBlock.SetState(new BrickBlockState());
            kb = new KeyboardInput(mario, questionBlock,hiddenBlock,brickBlock) { GameObj = this };
            gp = new GamepadInput(mario) { GameObj = this };
            fireFlower = new FireFlower();
            goomba = new Goomba();
            koopa = new Koopa();
            floorBlock = new FloorBlock();
            coin = new Coin();
            star = new Star();
            pipe = new Pipe();
            redMushroom = new RedMushroom();
            greenMushroom = new GreenMushroom();
            fireFlower.LoadContent(this.Content);
            coin.LoadContent(this.Content);
            star.LoadContent(this.Content);
            redMushroom.LoadContent(this.Content);
            greenMushroom.LoadContent(this.Content);
            goomba.LoadContent(this.Content);
            koopa.LoadContent(this.Content);
            pipe.LoadContent(this.Content);
            floorBlock.LoadContent(this.Content);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            gp.UpdateInput();
            kb.UpdateInput();
            mario.Update();
            foreach(BlockSprite block in map.CollisionTiles)
            {
                mario.Collision(block.DestinationRectangle, 800, 608);
            }
            fireFlower.Update();
            coin.Update();
            star.Update();
            questionBlock.Update();
            hiddenBlock.Update();
            brickBlock.Update();
            goomba.Update();
            koopa.Update();
            floorBlock.Update();
            map.Update();
            base.Update(gameTime);
            System.Diagnostics.Debug.WriteLine(context.GetActionState().ToString());
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            spriteBatch.Draw(Content.Load<Texture2D>("bg"), new Vector2(0, -250), Color.White);
            mario.Draw(spriteBatch);
            map.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
        public void ExitCommand()
        {
            Exit();
        }
    }
}
