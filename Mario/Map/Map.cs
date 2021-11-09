using Mario.Sprites;
using Mario.Sprites.Enemies;
using Mario.Sprites.Items;
using Mario.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using Mario.Sprites.Mario;
using Mario.Entities;
using System.IO;
using System.Linq;
using Microsoft.Xna.Framework.Media;
using Mario.Trackers;

namespace Mario.Map
{
    public class Level
    {
        protected const int GROUNDBLOCK = 10;
        protected const int BRICKBLOCK = 11;
        protected const int RMQBLOCK = 12;
        protected const int GMQBLOCK = 13;
        protected const int FLOWERQBLOCK = 14;
        protected const int STARQBLOCK = 15;
        protected const int COINQBLOCK = 16;
        protected const int ONECOINBRICKBLOCK = 17;
        protected const int TENCOINBRICKBLOCK = 18;
        protected const int PIPE = 6;
        public List<ISprite> bgObjects = new List<ISprite>();
        public List<ISprite> [] collisionZones = new List<ISprite> [14];
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
        string[][] overworld;
        string[][] underground;
        Song OverworldTheme;
        Song UndergroundTheme;
        ICommand ResetTimeRemainingCommand { get; set; }
        public Level(Game1 theatre)
        {
            this.theatre = theatre;
            entities = new DynamicEntities();
            overworld = File.ReadLines("1-1.csv").Select(x => x.Split(',')).ToArray();
            underground = File.ReadLines("1-1Sub.csv").Select(x => x.Split(',')).ToArray();
            reset = false;
            camera = new Camera(theatre.Graphics.GraphicsDevice.Viewport);
            camera.Limits = new Rectangle(0, 0, 3584, 272);
            OverworldTheme = theatre.Content.Load<Song>("OverworldTheme");
            UndergroundTheme = theatre.Content.Load<Song>("UndergroundTheme");
            MediaPlayer.IsRepeating = true;
            ResetTimeRemainingCommand = new ResetTimeRemainingCommand(theatre.tracker);
            
            for (int i = 0; i < collisionZones.Length; i++)
            {
                collisionZones[i] = new List<ISprite>();
            }
        }

        public void GenerateMap()
        {
            MediaPlayer.Play(UndergroundTheme);
            bgLayerNear = new Layer(camera);
            bgLayerNear.Parallax=new Vector2(.8f);
            bgLayerMid = new Layer(camera);
            bgLayerMid.Parallax = new Vector2(.5f);
            bgLayerFar = new Layer(camera);
            bgLayerFar.Parallax = new Vector2(.2f);
            for (int i = 0; i < 50; i++)
            {
                for (int j = 0; j < 17; j++)
                {
                    int number = Int32.Parse(underground[j][i]);

                    if (number > 0)
                    {
                        switch (number)
                        {
                            case GROUNDBLOCK: //Ground Block
                                BlockContext groundBlock = new BlockContext(theatre, new Vector2((i * (BLOCK)), j * BLOCK));
                                groundBlock.SetState(new GroundBlockState());
                                collisionZones[(i * BLOCK) / 256].Add(groundBlock);
                                break;
                            case BRICKBLOCK: //Brick Block
                                BlockContext brickBlock = new BlockContext(theatre, new Vector2(i * BLOCK, j * BLOCK));
                                brickBlock.SetState(new BrickBlockState());
                                collisionZones[(i * BLOCK) / 256].Add(brickBlock);
                                break;
                            case RMQBLOCK:  //Red Mushroom Question Block
                                BlockContext redMushroomQuestionBlock = new BlockContext(theatre, new Vector2(i * BLOCK, j * BLOCK));
                                redMushroomQuestionBlock.SetState(new RedMushroomQuestionBlockState());
                                collisionZones[(i * BLOCK) / 256].Add(redMushroomQuestionBlock);
                                break;
                            case GMQBLOCK: //Green Mushroom Question Block
                                BlockContext greenMushroomQuestionBlock = new BlockContext(theatre, new Vector2(i * BLOCK, j * BLOCK));
                                greenMushroomQuestionBlock.SetState(new GreenMushroomQuestionBlockState());
                                collisionZones[(i * BLOCK) / 256].Add(greenMushroomQuestionBlock);
                                break;
                            case FLOWERQBLOCK: //Fire Flower Question Block
                                BlockContext fireFlowerQuestionBlock = new BlockContext(theatre, new Vector2(i * BLOCK, j * BLOCK));
                                fireFlowerQuestionBlock.SetState(new FireFlowerQuestionBlockState());
                                collisionZones[(i * BLOCK) / 256].Add(fireFlowerQuestionBlock);
                                break;
                            case STARQBLOCK: //Star Question Block
                                BlockContext starQuestionBlock = new BlockContext(theatre, new Vector2(i * BLOCK, j * BLOCK));
                                starQuestionBlock.SetState(new StarQuestionBlockState());
                                collisionZones[(i * BLOCK) / 256].Add(starQuestionBlock);
                                break;
                            case COINQBLOCK: //Coin Question Block
                                BlockContext coinQuestionBlock = new BlockContext(theatre, new Vector2(i * BLOCK, j * BLOCK));
                                coinQuestionBlock.SetState(new CoinQuestionBlockState());
                                collisionZones[(i * BLOCK) / 256].Add(coinQuestionBlock);
                                break;
                            case ONECOINBRICKBLOCK: //One Coin Brick Block
                                BlockContext oneCoinBrickBlock = new BlockContext(theatre, new Vector2(i * BLOCK, j * BLOCK));
                                oneCoinBrickBlock.SetState(new OneCoinBrickBlockState());
                                collisionZones[(i * BLOCK) / 256].Add(oneCoinBrickBlock);
                                break;
                            case TENCOINBRICKBLOCK: //Ten Coin Brick Block
                                BlockContext tenCoinBrickBlock = new BlockContext(theatre, new Vector2(i * BLOCK, j * BLOCK));
                                tenCoinBrickBlock.SetState(new TenCoinBrickBlockState());
                                collisionZones[(i * BLOCK) / 256].Add(tenCoinBrickBlock);
                                break;
                            case 19:  //Hidden Block
                                BlockContext hblock = new BlockContext(theatre, new Vector2(i * BLOCK, j * BLOCK));
                                hblock.SetState(new HiddenBlockState());
                                collisionZones[(i * BLOCK) / 256].Add(hblock);
                                break;
                            case 81:  //Underground Ground Block
                                BlockContext uGroundBlock = new BlockContext(theatre, new Vector2(i * BLOCK, j * BLOCK));
                                uGroundBlock.SetState(new UGroundBlockState());
                                collisionZones[(i * BLOCK) / 256].Add(uGroundBlock);
                                break;
                            case 82:  //Underground Brick Block
                                BlockContext uBrickBlock = new BlockContext(theatre, new Vector2(i * BLOCK, j * BLOCK));
                                uBrickBlock.SetState(new UBrickBlockState());
                                collisionZones[(i * BLOCK) / 256].Add(uBrickBlock);
                                break;
                            case 83:  //Hard Block
                                BlockContext hardblock = new BlockContext(theatre, new Vector2(i * BLOCK, j * BLOCK));
                                hardblock.SetState(new HardBlockState());
                                collisionZones[(i * BLOCK) / 256].Add(hardblock);
                                break;
                            case 20:  //Used Block
                                BlockContext ublock = new BlockContext(theatre, new Vector2(i * BLOCK, j * BLOCK));
                                ublock.SetState(new UsedBlockState());
                                collisionZones[(i * BLOCK) / 256].Add(ublock);
                                break;
                            case PIPE: //Pipe
                                collisionZones[(i * 15 + 32) / 256].Add(new Pipe(theatre, new Vector2(i * 16, j * 15)));
                                break;
                            case 61:  //Coin
                                entities.entityObjs.Add(new MapCoin(theatre, new Vector2(i * COINW, j * COINH)));
                                break;
                            case 111:  //Green Mushroom
                                entities.entityObjs.Add(new GreenMushroom(theatre, new Vector2(i * MUSHROOM, j * MUSHROOM), Mario));
                                break;
                            case 112:  //Red Mushroom
                                entities.entityObjs.Add(new RedMushroom(theatre, new Vector2(i * MUSHROOM, j * MUSHROOM), Mario));
                                break;
                            case 133:  //Fire Flower
                                entities.entityObjs.Add(new FireFlower(theatre, new Vector2(i * BLOCK, j * BLOCK)));
                                break;
                            case 144:  //Star
                                entities.entityObjs.Add(new Star(theatre, new Vector2(i * BLOCK, j * BLOCK), Mario));
                                break;
                            case 30:  //Goomba
                                entities.enemyObjs.Add(new Goomba(theatre, new Vector2(i * BLOCK + 10, j * BLOCK - 15)));
                                break;
                            case 31:  //Koopa
                                entities.enemyObjs.Add(new Koopa(theatre, new Vector2(i * BLOCK, j * BLOCK - 15)));
                                break;
                            case 32:  //Piranha
                                entities.enemyObjs.Add(new Piranha(theatre, new Vector2(i * BLOCK + 5, j * BLOCK - 17)));
                                break;
                            case 51: //Cloud
                                bgLayerFar.Sprites.Add(new Cloud(Theatre, new Vector2(i * 16, j * 7)));
                                break;
                            case 52: //Bush
                                bgLayerNear.Sprites.Add(new Bush(Theatre, new Vector2(i * 14, (j * 13))));
                                break;
                            case 53: //Background Hills
                                bgLayerFar.Sprites.Add(new BackgroundHills(Theatre, new Vector2(i * 16, j * 16)));
                                break;
                            case 41: //Mario
                                if (!reset)
                                    mario = new SuperMario(theatre, new Vector2(i * 10, j * 16), new MarioContext(theatre)) { animated = false };
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
            bgLayerFar.Draw(spriteBatch);
            bgLayerMid.Draw(spriteBatch);
            bgLayerNear.Draw(spriteBatch);
            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, camera.GetViewMatrix(new Vector2(1f)));
            foreach (ISprite obj in bgObjects)
                obj.Draw(spriteBatch);
            mario.Draw(spriteBatch);
            entities.Draw(spriteBatch);
            for (int i = 0; i < collisionZones.Length; i++)
            {
                foreach (ISprite obj in collisionZones[i])
                    obj.Draw(spriteBatch);
            }
            
            spriteBatch.End();
            
            

        }
        public void Update()
        {
            //entities.Update();
            mario.Update();
            //Zone behind Mario
            if (mario.Position.X > 256)
            {
                foreach (ISprite sprite in collisionZones[((int)(mario.Position.X / 256)) - 1])
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
            foreach (ISprite sprite in collisionZones[(int)(mario.Position.X / 256)])
            {
                mario.Collision(sprite);

                if (sprite is BlockContext)
                    sprite.Collision(mario);
                if (mario.context.ShowHitbox)
                    sprite.ShowHitbox = true;
                else
                    sprite.ShowHitbox = false;
            }

            //Zone ahead of Mario
            if (mario.Position.X < 3328)
            {
                foreach (ISprite sprite in collisionZones[((int)(mario.Position.X / 256)) + 1])
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
            camera.LookAt(mario.Position);

            foreach (ISprite sprite in entities.entityObjs)
            {
                mario.Collision(sprite);
                if (mario.context.ShowHitbox)
                    sprite.ShowHitbox = true;
                else
                    sprite.ShowHitbox = false;
            }

            for (int i = 0; i < entities.entityObjs.Count; i++)
            {
                ISprite sprite = entities.entityObjs[i];
                //if (sprite.Position.X < 0 && sprite.Position.Y < 0)
                //    entities.entityObjs.RemoveAt(i);
                if (sprite.Position.X > 256)
                {
                    foreach (ISprite block in collisionZones[((int)(sprite.Position.X / 256)) - 1])
                    {
                        if (block is BlockContext || block is Pipe)
                        {
                            sprite.Collision(block);
                            if (mario.context.ShowHitbox)
                                sprite.ShowHitbox = true;
                            else sprite.ShowHitbox = false;
                        }
                    }
                }
                foreach (ISprite block in collisionZones[((int)(sprite.Position.X / 256))])
                {
                    if (block is BlockContext || block is Pipe)
                    {
                        sprite.Collision(block);
                        if (mario.context.ShowHitbox)
                            sprite.ShowHitbox = true;
                        else sprite.ShowHitbox = false;
                    }
                }
                if (sprite.Position.X < 3328)
                {
                    foreach (ISprite block in collisionZones[((int)(sprite.Position.X / 256)) + 1])
                    {
                        if (block is BlockContext || block is Pipe)
                        {
                            sprite.Collision(block);
                            if (mario.context.ShowHitbox)
                                sprite.ShowHitbox = true;
                            else sprite.ShowHitbox = false;
                        }
                    }
                }
            }

            for (int i = 0; i < entities.enemyObjs.Count; i++)
            {
                ISprite sprite = entities.enemyObjs[i];
                //sprite.Collision(mario);
                if (sprite is Piranha)
                {
                    if (Math.Abs(sprite.Position.X - mario.Position.X) < 32)
                    {
                        (sprite as Piranha).hiding = true;
                        //System.Diagnostics.Debug.WriteLine("HIDING NOW :OOOOO");
                    }
                    else
                    {
                        (sprite as Piranha).hiding = false;
                    }
                }
                if (sprite.Position.X < 0 && sprite.Position.Y < 0)
                    entities.enemyObjs.RemoveAt(i);
                foreach(ISprite fireball in entities.fireBallObjs)
                {
                    sprite.Collision(fireball);
                }
                //World collision
                if (sprite.Position.X > 256)
                {
                    foreach (ISprite block in collisionZones[((int)(sprite.Position.X / 256)) - 1])
                    {
                        if (block is BlockContext || block is Pipe)
                        {
                            sprite.Collision(block);
                            if (mario.context.ShowHitbox)
                                sprite.ShowHitbox = true;
                            else sprite.ShowHitbox = false;
                        }
                    }
                }
                foreach (ISprite block in collisionZones[((int)(sprite.Position.X / 256))])
                {
                    if (block is BlockContext || block is Pipe)
                    {
                        sprite.Collision(block);
                        if (mario.context.ShowHitbox)
                            sprite.ShowHitbox = true;
                        else sprite.ShowHitbox = false;
                    }
                }
                if (sprite.Position.X < 3328)
                {
                    foreach (ISprite block in collisionZones[((int)(sprite.Position.X / 256)) + 1])
                    {
                        if (block is BlockContext || block is Pipe)
                        {
                            sprite.Collision(block);
                            if (mario.context.ShowHitbox)
                                sprite.ShowHitbox = true;
                            else sprite.ShowHitbox = false;
                        }
                    }
                }
                mario.Collision(sprite);
            }

            for (int i = 0; i < entities.fireBallObjs.Count; i++)
            {
                ISprite sprite = entities.fireBallObjs[i];
                if (sprite.Position.X > 0 && sprite.Position.X < 3584)
                {
                    foreach (ISprite block in collisionZones[((int)(sprite.Position.X / 256))])
                    {
                        sprite.Collision(block);
                        if (mario.context.ShowHitbox) sprite.ShowHitbox = true;
                        else sprite.ShowHitbox = false;

                        if (sprite.Delete())
                        {
                            entities.fireBallObjs.Remove(sprite);
                            sprite = null;
                            break;
                        }

                    }
                }
                else
                {
                        entities.fireBallObjs.Remove(sprite);
                        sprite = null;
                }
            }
            entities.Update();
        }

        public void Reset()
        {
            entities.enemyObjs.Clear();
            entities.entityObjs.Clear();
            entities.fireBallObjs.Clear();
            bgObjects.Clear();
            reset = true;
            GenerateMap();
            mario.Position = new Vector2(100, 230);
            mario.context.SetPowerUpState(new StandardMarioState());
            ResetTimeRemainingCommand.Execute();
        }
    }
}
