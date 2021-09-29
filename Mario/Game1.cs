using Mario.Sprites;
using Mario.Sprites.Items;
using Mario.Sprites.Items.Items;
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
        RedMushroom redMushroom;
        GreenMushroom greenMushroom;
        MarioContext context;
        IController kb;
        IController gp;
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
            QuestionBlockLocation = new Vector2(20, 20);
            HiddenBlockLocation = new Vector2(80, 20);
            BrickBlockLocation = new Vector2(140, 20);
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();
            //Vector2 BlockLocation = new Vector2(20, 20);
            //block = new BlockContext(this,BlockLocation);
            //System.Diagnostics.Debug.WriteLine(block.ToString());
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
            kb = new KeyboardInput(mario, questionBlock,hiddenBlock,brickBlock) { GameObj = this };
            gp = new GamepadInput(mario) { GameObj = this };
            fireFlower = new FireFlower();
            coin = new Coin();
            star = new Star();
            redMushroom = new RedMushroom();
            greenMushroom = new GreenMushroom();
            fireFlower.LoadContent(this.Content);
            coin.LoadContent(this.Content);
            star.LoadContent(this.Content);
            redMushroom.LoadContent(this.Content);
            greenMushroom.LoadContent(this.Content);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            gp.UpdateInput();
            kb.UpdateInput();
            mario.Update();
            fireFlower.Update();
            coin.Update();
            star.Update();
            redMushroom.Update();
            greenMushroom.Update();
            base.Update(gameTime);
            questionBlock.Update();
            hiddenBlock.Update();
            brickBlock.Update();
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
            redMushroom.Draw(spriteBatch);
            greenMushroom.Draw(spriteBatch);
            questionBlock.Draw(spriteBatch);
            hiddenBlock.Draw(spriteBatch);
            brickBlock.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
        public void ExitCommand()
        {
            Exit();
        }
    }
}
