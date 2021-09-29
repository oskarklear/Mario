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
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            gp.UpdateInput();
            kb.UpdateInput();
            mario.Update();
            base.Update(gameTime);
            questionBlock.Update();
            hiddenBlock.Update();
            brickBlock.Update();
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            mario.Draw(spriteBatch);
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
