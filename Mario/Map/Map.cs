using Mario.Sprites;
using Mario.Sprites.Enemies;
using Mario.Sprites.Items;
using Mario.Sprites.Items;
using Mario.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using Mario.States;
using Mario.Sprites.Mario;
using Mario.Entities;
using System.IO;
using System.Linq;

namespace Mario.Map
{
    public class Level
    {
        private List<ISprite> collisionObjs = new List<ISprite>();
        public List<ISprite> bgObjects = new List<ISprite>();
        public List<ISprite> [] collisionZones = new List<ISprite> [14];
        public List<ISprite> CollisionObjs
        {
            get { return collisionObjs; }
        }
        private List<RedMushroom> redMushroomObjs = new List<RedMushroom>();
        private List<GreenMushroom> greenMushroomObjs = new List<GreenMushroom>();
        private List<FireFlower> fireFlowerObjs = new List<FireFlower>();
        int numOfRedMushrooms = 0;
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
        public DynamicEntities entities;
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
        private const int COINH = 16;
        private const int COINW = 12;
        private const int BLOCK = 16;
        string[][] map;
        public Level(Game1 theatre)
        {
            this.theatre = theatre;
            entities = new DynamicEntities();
            map = File.ReadLines("1-1.csv").Select(x => x.Split(',')).ToArray();
            reset = false;
            camera = new Camera(theatre.Graphics.GraphicsDevice.Viewport);
            camera.Limits = new Rectangle(0, 0, 3584, 272);
            for (int i = 0; i < collisionZones.Length; i++)
            {
                collisionZones[i] = new List<ISprite>();
            }
        }

        public void GenerateMap()
        {
            bgLayerNear = new Layer(camera);
            bgLayerNear.Parallax=new Vector2(.8f);
            bgLayerMid = new Layer(camera);
            bgLayerMid.Parallax = new Vector2(.5f);
            bgLayerFar = new Layer(camera);
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
                                collisionZones[(i * BLOCK) / 256].Add(groundBlock);
                                break;
                            case 11: //Brick Block
                                BlockContext brickBlock = new BlockContext(theatre, new Vector2(i * BLOCK, j * BLOCK));
                                brickBlock.SetState(new BrickBlockState());
                                collisionZones[(i * BLOCK) / 256].Add(brickBlock);
                                break;
                            case 12:  //Red Mushroom Question Block
                                BlockContext redMushroomQuestionBlock = new BlockContext(theatre, new Vector2(i * BLOCK, j * BLOCK));
                                redMushroomQuestionBlock.SetState(new RedMushroomQuestionBlockState());
                                collisionZones[(i * BLOCK) / 256].Add(redMushroomQuestionBlock);
                                break;
                            case 13: //Green Mushroom Question Block
                                BlockContext greenMushroomQuestionBlock = new BlockContext(theatre, new Vector2(i * BLOCK, j * BLOCK));
                                greenMushroomQuestionBlock.SetState(new GreenMushroomQuestionBlockState());
                                collisionZones[(i * BLOCK) / 256].Add(greenMushroomQuestionBlock);
                                break;
                            case 14: //Fire Flower Question Block
                                BlockContext fireFlowerQuestionBlock = new BlockContext(theatre, new Vector2(i * BLOCK, j * BLOCK));
                                fireFlowerQuestionBlock.SetState(new FireFlowerQuestionBlockState());
                                collisionZones[(i * BLOCK) / 256].Add(fireFlowerQuestionBlock);
                                break;
                            case 15: //Star Question Block
                                BlockContext starQuestionBlock = new BlockContext(theatre, new Vector2(i * BLOCK, j * BLOCK));
                                starQuestionBlock.SetState(new StarQuestionBlockState());
                                collisionZones[(i * BLOCK) / 256].Add(starQuestionBlock);
                                break;
                            case 16: //Coin Question Block
                                BlockContext coinQuestionBlock = new BlockContext(theatre, new Vector2(i * BLOCK, j * BLOCK));
                                coinQuestionBlock.SetState(new CoinQuestionBlockState());
                                collisionZones[(i * BLOCK) / 256].Add(coinQuestionBlock);
                                break;
                            case 17: //One Coin Brick Block
                                BlockContext oneCoinBrickBlock = new BlockContext(theatre, new Vector2(i * BLOCK, j * BLOCK));
                                oneCoinBrickBlock.SetState(new OneCoinBrickBlockState());
                                collisionZones[(i * BLOCK) / 256].Add(oneCoinBrickBlock);
                                break;
                            case 18: //Ten Coin Brick Block
                                BlockContext tenCoinBrickBlock = new BlockContext(theatre, new Vector2(i * BLOCK, j * BLOCK));
                                tenCoinBrickBlock.SetState(new TenCoinBrickBlockState());
                                collisionZones[(i * BLOCK) / 256].Add(tenCoinBrickBlock);
                                break;
                            case 19:  //Hidden Block
                                BlockContext hblock = new BlockContext(theatre, new Vector2(i * BLOCK, j * BLOCK));
                                hblock.SetState(new HiddenBlockState());
                                collisionZones[(i * BLOCK) / 256].Add(hblock);
                                break;
                            case 20:  //Used Block
                                BlockContext ublock = new BlockContext(theatre, new Vector2(i * BLOCK, j * BLOCK));
                                ublock.SetState(new UsedBlockState());
                                collisionZones[(i * BLOCK) / 256].Add(ublock);
                                break;
                            case 6: //Pipe
                                collisionZones[(i * 15 + 32) / 256].Add(new Pipe(theatre, new Vector2(i * 16, j * 15)));
                                break;
                            case 118:  //Coin
                                collisionObjs.Add(new MapCoin(theatre, new Vector2(i * COINW, j * COINH)));
                                break;
                            case 111:  //Green Mushroom
                                collisionObjs.Add(new GreenMushroom(theatre, new Vector2(i * MUSHROOM, j * MUSHROOM), Mario));
                                break;
                            case 112:  //Red Mushroom
                                collisionObjs.Add(new RedMushroom(theatre, new Vector2(i * MUSHROOM, j * MUSHROOM), Mario));
                                break;
                            case 133:  //Fire Flower
                                collisionObjs.Add(new FireFlower(theatre, new Vector2(i * BLOCK, j * BLOCK)));
                                break;
                            case 144:  //Star
                                collisionObjs.Add(new Star(theatre, new Vector2(i * BLOCK, j * BLOCK), Mario));
                                break;
                            case 30:  //Goomba
                                collisionZones[(i * GOOMBAW) / 256].Add(new Goomba(theatre, new Vector2(i * BLOCK, j * BLOCK - 15)));
                                break;
                            case 31:  //Koopa
                                collisionZones[(i * KOOPAW) / 256].Add(new Koopa(theatre, new Vector2(i * BLOCK, j * BLOCK - 15)));
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
                            case 99:
                                GoalGate gg = new GoalGate(theatre, new Vector2(i * BLOCK, j * BLOCK - 99));
                                GoalGateMovingPart mp = new GoalGateMovingPart(theatre, new Vector2(i * BLOCK + 10, j * BLOCK));
                                bgObjects.Add(gg);
                                collisionZones[13].Add(mp);
                                break;
                            
                        }
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, camera.GetViewMatrix(new Vector2(1f)));
            foreach (ISprite obj in bgObjects)
                obj.Draw(spriteBatch);
            mario.Draw(spriteBatch);
            for (int i = 0; i < collisionZones.Length; i++)
            {
                foreach (ISprite obj in collisionZones[i])
                    obj.Draw(spriteBatch);
            }
            entities.Draw(spriteBatch);
            spriteBatch.End();
            bgLayerNear.Draw(spriteBatch);
            bgLayerMid.Draw(spriteBatch);
            bgLayerFar.Draw(spriteBatch);
            

        }
        public void Update()
        {
            //Zone behind Mario
            if (mario.position.X > 256)
            {
                foreach (ISprite sprite in collisionZones[((int)(mario.position.X / 256)) - 1])
                {
                    mario.Collision(sprite);

                    if (sprite is BlockContext)
                        sprite.Collision(mario);
                    if (mario.context.ShowHitbox)
                        sprite.ShowHitbox = true;
                    else
                        sprite.ShowHitbox = false;
                }
            }
            //Zone Mario is in
            foreach (ISprite sprite in collisionZones[(int)(mario.position.X / 256)])
            {
                mario.Collision(sprite);

                if (sprite is BlockContext)
                    sprite.Collision(mario);
                if (mario.context.ShowHitbox)
                    sprite.ShowHitbox = true;
                else
                    sprite.ShowHitbox = false;
            }

            foreach (ISprite sprite in collisionZones[(int)(mario.position.X / 256)])
            {

            }

            //Zone ahead of Mario
            if (mario.position.X < 3328)
            {
                foreach (ISprite sprite in collisionZones[((int)(mario.position.X / 256)) + 1])
                {
                    mario.Collision(sprite);

                    if (sprite is BlockContext)
                        sprite.Collision(mario);
                    if (mario.context.ShowHitbox)
                        sprite.ShowHitbox = true;
                    else
                        sprite.ShowHitbox = false;
                }
            }
            for (int i = 0; i < collisionZones.Length; i++)
            {
                foreach (ISprite obj in collisionZones[i])
                    obj.Update();
            }
            camera.LookAt(mario.position);

            foreach (ISprite sprite in entities.entityObjs)
            {
                mario.Collision(sprite);
                
                if (mario.context.ShowHitbox)
                    sprite.ShowHitbox = true;
                else
                    sprite.ShowHitbox = false;
            }

            foreach (ISprite sprite in entities.entityObjs)
            {
                foreach (ISprite block in collisionZones[((int)(sprite.Position.X / 256))])
                {
                    if (block is BlockContext)
                    {
                        sprite.Collision(block);
                        if (mario.context.ShowHitbox)
                            sprite.ShowHitbox = true;
                        else sprite.ShowHitbox = false;
                    }
                }
            }

            for (int i = 0; i < entities.fireBallObjs.Count; i++)
            {
                ISprite sprite = entities.fireBallObjs[i];
                foreach (ISprite block in collisionZones[((int)(sprite.Position.X / 256))])
                {
                    sprite.Collision(block);
                    if (mario.context.ShowHitbox) sprite.ShowHitbox = true;
                    else sprite.ShowHitbox = false;

                    if (sprite.delete())
                    {
                        entities.fireBallObjs.Remove(sprite);
                        sprite = null;
                        break;
                    }

                }
            }


            entities.Update();
        }

        public void Reset()
        {
            collisionObjs.Clear();
            bgObjects.Clear();
            reset = true;
            GenerateMap();
            mario.position = new Vector2(100, 230);
            mario.context.SetPowerUpState(new StandardMarioState());
        }
    }
}
