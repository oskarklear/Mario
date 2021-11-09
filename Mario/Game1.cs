﻿using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Mario.Map;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Mario.Trackers;

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
        SpriteFont HeadsUpDisplay;
        Vector2 HUDPosition;
        public StatTracker tracker;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            IsMenuVisible = false;
            tracker = new StatTracker();
        }

        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 272;
            graphics.ApplyChanges();
            map = new Level(this);
            HUDPosition.X = 10;
            HUDPosition.Y = 10;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            map.GenerateMap();

            spriteBatch = new SpriteBatch(GraphicsDevice);
            kb = new KeyboardInput(map) { GameObj = this };
            gp1 = new GamepadInput(map.Mario) { GameObj = this };
            HeadsUpDisplay = Content.Load<SpriteFont>("HUD");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            gp1.UpdateInput();
            kb.UpdateInput();
            map.Update();
            base.Update(gameTime);
            tracker.Update();
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Bisque);
            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, map.camera.GetViewMatrix(new Vector2(.2f)));            
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, map.camera.GetViewMatrix(new Vector2(1f)));
            spriteBatch.DrawString(HeadsUpDisplay, "Time: " + (tracker.timeRemaining / 60).ToString(), map.camera.Position, Color.Blue);
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
