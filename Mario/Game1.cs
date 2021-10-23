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
using System.Collections.Generic;

namespace Mario
{
    public class Game1 : Game
    {
        private const int MAPH = 608;
        private const int MAPW = 800;
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        public bool IsMenuVisible;
        IController kb;
        IController gp1;
        string [][] mapArray;
        Level map;
        public Camera camera;
        
        
        
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            IsMenuVisible = false;
        }

        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = MAPW;
            graphics.PreferredBackBufferHeight = MAPH;
            graphics.ApplyChanges();
            map = new Level();
            map.Theatre = this;
            base.Initialize();
            camera.Limits = new Rectangle(0, 0, 1000, 608);
        }

        protected override void LoadContent()
        {
            camera = new Camera(graphics.GraphicsDevice.Viewport);
            mapArray = File.ReadLines("map.csv").Select(x => x.Split(',')).ToArray();
            map.GenerateMap(mapArray);

            spriteBatch = new SpriteBatch(GraphicsDevice);
            
            kb = new KeyboardInput(map.Mario) { GameObj = this };
            gp1 = new GamepadInput(map.Mario) { GameObj = this };
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            gp1.UpdateInput();
            kb.UpdateInput();
            map.Mario.Update();

            foreach(ISprite sprite in map.CollisionObjs)
            {
                map.Mario.Collision(sprite, MAPW, MAPH);

                if (sprite is BlockContext)
                    sprite.Collision(map.Mario, MAPW, MAPH);
                if (map.Mario.context.ShowHitbox)
                    sprite.ShowHitbox = true;
                else
                    sprite.ShowHitbox = false;
            }

            map.Update();
            base.Update(gameTime);
            System.Diagnostics.Debug.WriteLine(map.Mario.context.GetActionState().ToString());
            camera.LookAt(map.Mario.position);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, camera.GetViewMatrix(new Vector2(.2f)));
            spriteBatch.Draw(Content.Load<Texture2D>("bg"), new Vector2(0, -250), Color.White);
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
