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
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        public bool IsMenuVisible;
        IController kb;
        IController gp1;
        IController gp2;
        IController gb3;
        IController gp4;
        string [][] mapArray;
        Level map;
        

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            IsMenuVisible = false;
            
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
            
            kb = new KeyboardInput(map.Mario) { GameObj = this };
            gp1 = new GamepadInput(map.Mario) { GameObj = this };
            
            //gp2 = new GamepadInput(mario)

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
                

                    
                    map.Mario.Collision(sprite, 800, 608);
                    if (sprite is BlockContext)
                        sprite.Collision(map.Mario, 800, 608);
                    
                
            }
            //collisionDetector.Update(DynamicObjects);
            map.Update();
            base.Update(gameTime);
            System.Diagnostics.Debug.WriteLine(map.Mario.context.GetActionState().ToString());
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            spriteBatch.Draw(Content.Load<Texture2D>("bg"), new Vector2(0, -250), Color.White);
            map.Mario.Draw(spriteBatch);
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
