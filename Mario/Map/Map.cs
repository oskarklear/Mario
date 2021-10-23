using Mario.Sprites;
using Mario.Sprites.Enemies;
using Mario.Sprites.Items;
using Mario.Sprites.Items.Items;
using Mario.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using Mario.States;
using Mario.Sprites.Mario;
using System.IO;
using System.Linq;

namespace Mario.Map
{
    class Level
    {
        private List<ISprite> collisionObjs = new List<ISprite>();
        public List<ISprite> bgObjects = new List<ISprite>();
        public List<ISprite> CollisionObjs
        {
            get { return collisionObjs; }
        }
        private Game1 theatre;
        public Game1 Theatre
        {
            get { return theatre; }
            set { theatre = value; }
        }
        private SuperMario mario;
        public SuperMario Mario
        {
            get { return mario; }
        }
        Layer bgLayerMid;
        Layer bgLayerNear;
        Layer bgLayerFar;
        bool reset;
        public Camera camera;
        private const int KOOPAH = 26;
        private const int KOOPAW = 16;
        private const int GOOMBAH = 16;
        private const int GOOMBAW = 16;
        private const int MUSHROOM = 18;
        private const int FLOWER = 16;
        private const int COINH = 16;
        private const int COINW = 12;
        private const int BLOCK = 16;
        string[][] map;
        public Level(Game1 theatre)
        {
            this.theatre = theatre;
            map = File.ReadLines("1-1.csv").Select(x => x.Split(',')).ToArray();
            reset = false;
            camera = new Camera(theatre.Graphics.GraphicsDevice.Viewport);
            camera.Limits = new Rectangle(0, 0, 3584, 272);
        }

        public void GenerateMap()
        {
            bgLayerNear = new Layer(Theatre.camera);
            bgLayerNear.Parallax=new Vector2(.8f);
            bgLayerMid = new Layer(Theatre.camera);
            bgLayerMid.Parallax = new Vector2(.5f);
            bgLayerFar = new Layer(Theatre.camera);
            bgLayerFar.Parallax = new Vector2(.2f);
            for (int i = 0; i < 224; i++)
            {
                for (int j = 0; j < 17; j++)
                {
                    int number = Int32.Parse(map[j][i]);

                    if (number > 0)
                    {
                        switch (number)
                        {
                            case 10: //Ground Block
                                BlockContext groundBlock = new BlockContext(theatre, new Vector2(i * BLOCK, j * BLOCK));
                                groundBlock.SetState(new GroundBlockState());
                                collisionObjs.Add(groundBlock);
                                break;
                            case 11: //Brick Block
                                BlockContext brickBlock = new BlockContext(theatre, new Vector2(i * BLOCK, j * BLOCK));
                                brickBlock.SetState(new BrickBlockState());
                                collisionObjs.Add(brickBlock);
                                break;
                            case 12:  //Question Block
                                BlockContext qblock = new BlockContext(theatre, new Vector2(i * BLOCK, j * BLOCK));
                                qblock.SetState(new QuestionBlockState());
                                collisionObjs.Add(qblock);
                                break;
                            case 4:  //Hidden Block
                                BlockContext hblock = new BlockContext(theatre, new Vector2(i * BLOCK, j * BLOCK));
                                hblock.SetState(new HiddenBlockState());
                                collisionObjs.Add(hblock);
                                break;
                            case 5:  //Used Block
                                BlockContext ublock = new BlockContext(theatre, new Vector2(i * BLOCK, j * BLOCK));
                                ublock.SetState(new UsedBlockState());
                                collisionObjs.Add(ublock);
                                break;
                            case 6: //Pipe
                                collisionObjs.Add(new Pipe(theatre, new Vector2(i * 15 + 32, j * 15)));
                                break;
                            case 118:  //Coin
                                collisionObjs.Add(new Coin(theatre, new Vector2(i * COINW, j * COINH)));
                                break;
                            case 1188:  //Green Mushroom
                                collisionObjs.Add(new GreenMushroom(theatre, new Vector2(i * MUSHROOM, j * MUSHROOM)));
                                break;
                            case 128:  //Red Mushroom
                                collisionObjs.Add(new RedMushroom(theatre, new Vector2(i * MUSHROOM, j * MUSHROOM)));
                                break;
                            case 13:  //Fire Flower
                                collisionObjs.Add(new FireFlower(theatre, new Vector2(i * FLOWER, j * FLOWER)));
                                break;
                            case 14:  //Star
                                collisionObjs.Add(new Star(theatre, new Vector2(i * BLOCK, j * BLOCK)));
                                break;
                            case 30:  //Goomba
                                collisionObjs.Add(new Goomba(theatre, new Vector2(i * GOOMBAW, j * GOOMBAH - BLOCK + 2)));
                                break;
                            case 31:  //Koopa
                                collisionObjs.Add(new Koopa(theatre, new Vector2(i * KOOPAW, j * (KOOPAH - 11) + 19)));
                                break;
                            case 51: //Cloud
                                bgLayerMid.Sprites.Add(new Cloud(Theatre, new Vector2(i * 16, j * 7)));
                                break;
                            case 52: //Bush
                                bgLayerNear.Sprites.Add(new Bush(Theatre, new Vector2(i * 13, (j * 5) + 350)));
                                break;
                            case 41: //Mario
                                if (!reset)
                                    mario = new SuperMario(theatre, new Vector2(i * 10, j * 16), new MarioContext()) { animated = false };
                                //collisionObjs.Add(mario);
                                break;
                            
                        }
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            mario.Draw(spriteBatch);
            
            foreach (ISprite obj in collisionObjs)
                obj.Draw(spriteBatch);
            spriteBatch.End();
            bgLayerNear.Draw(spriteBatch);
            bgLayerMid.Draw(spriteBatch);
            bgLayerFar.Draw(spriteBatch);
            
        }
        public void Update()
        {
            foreach (ISprite obj in collisionObjs)
                obj.Update();
            camera.LookAt(mario.position);
        }

        public void Reset()
        {
            collisionObjs.Clear();
            bgObjects.Clear();
            reset = true;
            GenerateMap();
            mario.position = new Vector2(100, 256);
            mario.context.SetPowerUpState(new StandardMarioState());
        }
    }
}
