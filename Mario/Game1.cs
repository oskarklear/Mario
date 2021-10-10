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
        IController gp1;
        IController gp2;
        IController gb3;
        IController gp4;
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
            mario = new SuperMario(context, Content.Load<Texture2D>("mario/smallIdleMarioL")) { animated = false };
            mario.LoadContent(this.Content);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            Tiles.Content = Content;
            mapArray = File.ReadLines("map.csv").Select(x => x.Split(',')).ToArray();
            map.GenerateMap(mapArray, 16);

            spriteBatch = new SpriteBatch(GraphicsDevice);
            
            kb = new KeyboardInput(mario) { GameObj = this };
            gp1 = new GamepadInput(mario) { GameObj = this };
            //gp2 = new GamepadInput(mario)
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            gp1.UpdateInput();
            kb.UpdateInput();
            mario.Update();
            foreach(ISprite sprite in map.CollisionObjs)
            {
                mario.Collision(sprite, 800, 608);
            }
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
