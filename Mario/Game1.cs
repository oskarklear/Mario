using Mario.Sprites;
using Mario.Sprites.Items;
using Mario.Sprites.Items.Items;
using Mario.Sprites.Enemies;
using Mario.Sprites.Mario;
using Mario.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

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

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 800;  
            graphics.PreferredBackBufferHeight = 600;   
            graphics.ApplyChanges();
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
            // TODO: Add your initialization logic here
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            mario = new SuperMario(context, Content.Load<Texture2D>("mario/smallIdleMarioL")) { animated = false };
            mario.LoadContent(this.Content);
            questionBlock = new BlockContext(this, QuestionBlockLocation);
            questionBlock.SetState(new QuestionBlockState());
            hiddenBlock = new BlockContext(this, HiddenBlockLocation);
            hiddenBlock.SetState(new HiddenBlockState());
            brickBlock = new BlockContext(this, BrickBlockLocation);
            brickBlock.SetState(new BrickBlockState());
            kb = new KeyboardInput(mario, questionBlock, hiddenBlock, brickBlock) { GameObj = this };
            gp1 = new GamepadInput(mario) { GameObj = this };
            //gp2 = new GamepadInput(mario)
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
            gp1.UpdateInput();
            kb.UpdateInput();
            mario.Update();
            fireFlower.Update();
            coin.Update();
            star.Update();
            base.Update(gameTime);
            questionBlock.Update();
            hiddenBlock.Update();
            brickBlock.Update();
            goomba.Update();
            koopa.Update();
            floorBlock.Update();

            System.Diagnostics.Debug.WriteLine(context.GetActionState().ToString());
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            spriteBatch.Draw(Content.Load<Texture2D>("bg"), new Vector2(0, -350), Color.White);
            mario.Draw(spriteBatch);
            
            fireFlower.Draw(spriteBatch);
            coin.Draw(spriteBatch);
            star.Draw(spriteBatch);
            goomba.Draw(spriteBatch);
            koopa.Draw(spriteBatch);
            redMushroom.Draw(spriteBatch);
            greenMushroom.Draw(spriteBatch);
            questionBlock.Draw(spriteBatch);
            hiddenBlock.Draw(spriteBatch);
            brickBlock.Draw(spriteBatch);
            pipe.Draw(spriteBatch);
            floorBlock.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
        public void ExitCommand()
        {
            Exit();
        }
    }
}
