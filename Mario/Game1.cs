using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Mario.Map;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Mario.States;

namespace Mario
{
    public class Game1 : Game
    {
        private const int MAPH = 272;
        private const int MAPW = 800;
        private GraphicsDeviceManager graphics;
        public GraphicsDeviceManager Graphics
        {
            get { return graphics; }
        }
        private SpriteBatch spriteBatch;
        public bool IsMenuVisible;
        IController kb;
        IController gp1;
        public Level map;
        public Camera camera;
        public Overlay menu;
        public SpriteFont font;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            IsMenuVisible = false;
            font = Content.Load <SpriteFont>("HUD");
            menu = new Overlay(font);
        }

        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = MAPW;
            graphics.PreferredBackBufferHeight = MAPH;
            graphics.ApplyChanges();
            map = new Level(this);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            map.GenerateMap();

            spriteBatch = new SpriteBatch(GraphicsDevice);
            
            kb = new KeyboardInput(map) { GameObj = this };
            gp1 = new GamepadInput(map.Mario) { GameObj = this };
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            gp1.UpdateInput();
            kb.UpdateInput();
            map.Update();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Bisque);
            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, map.camera.GetViewMatrix(new Vector2(.2f)));
            spriteBatch.End();
            map.Draw(spriteBatch);
            base.Draw(gameTime);
        }

        public void ExitCommand()
        {
            Exit();
        }
    }
}
