using Mario.Sprites;
using Mario.Sprites.Items;
using Mario.Sprites.Items;
using Mario.Sprites.Enemies;
using Mario.Sprites.Mario;
using Mario.States;
using Mario.Map;
using Mario.Entities;
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
        Level map;
        public Camera camera;
        public DynamicEntities entities;



        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            IsMenuVisible = false;
            entities = new DynamicEntities();
        }

        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = MAPW;
            graphics.PreferredBackBufferHeight = MAPH;
            graphics.ApplyChanges();
            map = new Level(this);
            base.Initialize();
            //camera.Limits = new Rectangle(0, 0, 3584, 272);
        }

        protected override void LoadContent()
        {
            //camera = new Camera(graphics.GraphicsDevice.Viewport);
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
            map.Mario.Update();

            foreach(ISprite sprite in map.CollisionObjs)
            {
                map.Mario.Collision(sprite);

                if (sprite is BlockContext)
                    sprite.Collision(map.Mario);
                if (map.Mario.context.ShowHitbox)
                    sprite.ShowHitbox = true;
                else
                    sprite.ShowHitbox = false;
            }

            foreach(ISprite sprite in entities.entityObjs)
            {
                map.Mario.Collision(sprite);
                if (sprite is BlockContext)
                    sprite.Collision(map.Mario);
                if (map.Mario.context.ShowHitbox)
                    sprite.ShowHitbox = true;
                else
                    sprite.ShowHitbox = false;
                /*foreach(ISprite extra in map.CollisionObjs)
                {
                    sprite.Collision(extra, MAPW, MAPH);
                    extra.Collision(sprite, MAPW, MAPH);
                }*/
            }

            map.Update();
            entities.Update();
            base.Update(gameTime);
            System.Diagnostics.Debug.WriteLine(map.Mario.context.GetActionState().ToString());
            //camera.LookAt(map.Mario.position);
            //System.Diagnostics.Debug.WriteLine("LIST SIZE: " + entities.entityObjs.Count);

        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, map.camera.GetViewMatrix(new Vector2(.2f)));
            spriteBatch.Draw(Content.Load<Texture2D>("bg"), new Vector2(0, -250), Color.White);
            spriteBatch.End();
            map.Draw(spriteBatch);
            entities.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public void ExitCommand()
        {
            Exit();
        }
    }
}
