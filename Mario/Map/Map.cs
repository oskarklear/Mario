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
using Microsoft.Xna.Framework.Audio;

namespace Mario.Map
{
    public class Level
    {
        protected Vector2 CHECKPOINT = new Vector2(2624, 220);
        protected const int OVERWORLDWIDTH = 224; //In columns
        protected const int UNDERGROUNDWIDTH = 50; //In columns
        protected const int GROUNDBLOCK = 10;
        protected const int BRICKBLOCK = 11;
        protected const int RMQBLOCK = 12;
        protected const int GMQBLOCK = 13;
        protected const int FLOWERQBLOCK = 14;       
        protected const int STARQBLOCK = 15;
        protected const int COINQBLOCK = 16;
        protected const int ONECOINBRICKBLOCK = 17;
        protected const int TENCOINBRICKBLOCK = 18;
        protected const int BALLOONQBLOCK = 21;
        protected const int FEATHERQBLOCK = 22;
        protected const int PIPE = 6;
        public List<ISprite> bgObjects = new List<ISprite>();
        public List<ISprite>[] collisionZones = new List<ISprite>[14];
        private Game1 theatre;
        public Overlay menu;
        public SpriteFont font;
        public Game1 Theatre
        {
            get { return theatre; }
            set { theatre = value; }
        }
        private int levelnum;
        public int Levelnum
        {
            get { return levelnum; }
            set { levelnum = value; }
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
        List<string[][]> levels;
        string[][] level1;
        string[][] level2;
        string[][] level3;
        string[][] underground;
        bool inOverworld;
        Song OverworldTheme;
        Song UndergroundTheme;
        SoundEffect TimeWarning;
        public StatTracker tracker;
        Vector2 spawnPos;
        ICommand ResetTimeRemainingCommand { get; set; }
        ICommand ResetPointsCommand { get; set; }
        ICommand ResetLivesCommand { get; set; }
        int resetCooldown;
        public Level(Game1 theatre)
        {
            this.theatre = theatre;
            tracker = this.theatre.tracker;
            entities = new DynamicEntities(theatre);
            levels = new List<string[][]>();
            levelnum = 0;
            level1 = File.ReadLines("1-1.csv").Select(x => x.Split(',')).ToArray();
            level2 = File.ReadLines("1-2.csv").Select(x => x.Split(',')).ToArray();
            level3 = File.ReadLines("1-3.csv").Select(x => x.Split(',')).ToArray();
            levels.Add(level1); levels.Add(level2); levels.Add(level3);
            underground = File.ReadLines("1-1Sub.csv").Select(x => x.Split(',')).ToArray();
            reset = false;
            camera = new Camera(theatre.Graphics.GraphicsDevice.Viewport);
            camera.Limits = new Rectangle(0, 0, 3584, 272);
            OverworldTheme = theatre.Content.Load<Song>("OverworldTheme");
            UndergroundTheme = theatre.Content.Load<Song>("UndergroundTheme");
            TimeWarning = theatre.Content.Load<SoundEffect>("SoundEffects/time_warning");
            MediaPlayer.IsRepeating = true;
            ResetTimeRemainingCommand = new ResetTimeRemainingCommand(theatre.tracker);
            ResetPointsCommand = new ResetPointsCommand(theatre.tracker);
            ResetLivesCommand = new ResetLivesCommand(theatre.tracker);
            font = theatre.Content.Load<SpriteFont>("HUD");
            menu = new Overlay(font, theatre.tracker);
            inOverworld = true;
            for (int i = 0; i < collisionZones.Length; i++)
            {
                collisionZones[i] = new List<ISprite>();
            }
        }

        public void GenerateMap()
        {
            int width;
            string[][] map;
            if (inOverworld)
            {
                MediaPlayer.Play(OverworldTheme);
                width = OVERWORLDWIDTH;
                map = levels[levelnum];
            }
            else
            {
                MediaPlayer.Play(UndergroundTheme);
                width = UNDERGROUNDWIDTH;
                map = underground;
            }
            bgLayerNear = new Layer(camera);
            bgLayerNear.Parallax = new Vector2(.8f);
            bgLayerMid = new Layer(camera);
            bgLayerMid.Parallax = new Vector2(.5f);
            bgLayerFar = new Layer(camera);
            bgLayerFar.Parallax = new Vector2(.2f);
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < 17; j++)
                {
                    int number = Int32.Parse(map[j][i]);

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
                            case FEATHERQBLOCK: //Cape Feather Question Block
                                BlockContext capeFeatherQuestionBlock = new BlockContext(theatre, new Vector2(i * BLOCK, j * BLOCK));
                                capeFeatherQuestionBlock.SetState(new CapeFeatherQuestionBlockState());
                                collisionZones[(i * BLOCK) / 256].Add(capeFeatherQuestionBlock);
                                break;
                            case FLOWERQBLOCK: //Fire Flower Question Block
                                BlockContext fireFlowerQuestionBlock = new BlockContext(theatre, new Vector2(i * BLOCK, j * BLOCK));
                                fireFlowerQuestionBlock.SetState(new FireFlowerQuestionBlockState());
                                collisionZones[(i * BLOCK) / 256].Add(fireFlowerQuestionBlock);
                                break;
                            case BALLOONQBLOCK: //P Balloon Question Block
                                BlockContext pBalloonQuestionBlock = new BlockContext(theatre, new Vector2(i * BLOCK, j * BLOCK));
                                pBalloonQuestionBlock.SetState(new PBalloonQuestionBlockState());
                                collisionZones[(i * BLOCK) / 256].Add(pBalloonQuestionBlock);
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
                            case 3: //Spike block
                                collisionZones[(i * 15 + 32) / 256].Add(new SpikeBlock(theatre, new Vector2(i * 16, j * 16)));
                                break;
                            case 4: //Upside down pipe
                                collisionZones[(i * 15 + 32) / 256].Add(new UpsidedownPipe(theatre, new Vector2(i * 16, j * 15)));
                                break;
                            case 5: //Warpable pipe
                                collisionZones[(i * 15 + 32) / 256].Add(new Pipe(theatre, new Vector2(i * 16, j * 15), true));
                                break;
                            case PIPE: //Pipe
                                collisionZones[(i * 15 + 32) / 256].Add(new Pipe(theatre, new Vector2(i * 16, j * 15), false));
                                break;
                            case 7: //Long pipe
                                collisionZones[(i * 15 + 32) / 256].Add(new LongPipe(theatre, new Vector2(i * 16, j * 15)));
                                break;
                            case 8: //Side Pipe
                                collisionZones[(i * 15 + 32) / 256].Add(new SidePipe(theatre, new Vector2(i * 16, j * 15)));
                                break;
                            case 61:  //Coin
                                entities.entityObjs.Add(new MapCoin(theatre, new Vector2(i * 16, j * COINH)));
                                break;
                            case 111:  //Green Mushroom
                                entities.entityObjs.Add(new GreenMushroom(theatre, new Vector2(i * MUSHROOM, j * MUSHROOM), Mario));
                                break;
                            case 112:  //Red Mushroom
                                entities.entityObjs.Add(new RedMushroom(theatre, new Vector2(i * MUSHROOM, j * MUSHROOM), Mario));
                                break;
                            case 113:  //Cape Feather
                                entities.entityObjs.Add(new CapeFeather(theatre, new Vector2(i * MUSHROOM, j * MUSHROOM), Mario));
                                break;
                            case 133:  //Fire Flower
                                entities.entityObjs.Add(new FireFlower(theatre, new Vector2(i * BLOCK, j * BLOCK)));
                                break;
                            case 134:  //P Balloon
                                entities.entityObjs.Add(new PBalloon(theatre, new Vector2(i * BLOCK, j * BLOCK)));
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
                            case 33: //Parakoopa
                                entities.enemyObjs.Add(new Parakoopa(theatre, new Vector2(i * BLOCK, j * BLOCK)));
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
                            case 54: //Underground Background
                                bgLayerFar.Sprites.Add(new CaveBG(Theatre, new Vector2(i * 15 - 16, j * 16)));
                                break;
                            case 41: //Mario
                                if (!reset)
                                {
                                    spawnPos = new Vector2(i * 10, j * 16 + 1);
                                    mario = new SuperMario(theatre, new Vector2(i * 10, j * 16 + 1), new MarioContext(theatre)) { isAnimated = false };
                                }
                                else if (!inOverworld)
                                    spawnPos = new Vector2(i * 10 + 6, j * 16);
                                break;
                            case 99:
                                GoalGate gg = new GoalGate(theatre, new Vector2(i * BLOCK, j * BLOCK - 99));
                                GoalGateMovingPart mp = new GoalGateMovingPart(theatre, new Vector2(i * BLOCK + 10, j * BLOCK));
                                bgObjects.Add(gg);
                                collisionZones[(i * BLOCK) / 256].Add(mp);
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
            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, camera.GetViewMatrix(new Vector2(0f)));
            menu.Draw(spriteBatch);
            spriteBatch.End();



        }
        public void Update()
        {
            if (!theatre.IsMenuVisible)
            {
                //entities.Update();
                mario.Update();
                //if ((int)mario.Position.X == 1792)
                    //spawnPos = CHECKPOINT;
                if (!mario.context.GetPowerUpState().ToString().Equals("DeadMario"))
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
                        {
                            obj.Update();
                        }
                    }
                    //if (inOverworld)
                        camera.LookAt(mario.position);


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

                    if (sprite.Delete())
                    {
                        entities.entityObjs.Remove(sprite);
                        sprite = null;
                        break;
                    }

                    if (!(sprite is MapCoin))
                    {
                        if (sprite.Position.X > 256)
                        {
                            for (int i1 = 0; i1 < collisionZones[((int)(sprite.Position.X / 256)) - 1].Count; i1++)
                            {
                                ISprite block = collisionZones[((int)(sprite.Position.X / 256)) - 1][i1];

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
                }

                    for (int i = 0; i < entities.enemyObjs.Count; i++)
                    {
                        ISprite sprite = entities.enemyObjs[i];;

                        if (sprite.Delete())
                        {
                            entities.entityObjs.Remove(sprite);
                            sprite = null;
                            continue;
                        }

                        if (sprite is Piranha)
                        {
                            if (Math.Abs(sprite.Position.X - mario.Position.X) < 32)
                            {
                                (sprite as Piranha).hiding = true;
                            }
                            else
                            {
                                (sprite as Piranha).hiding = false;
                            }
                        }
                        if (sprite.Position.X <= 0 && sprite.Position.Y <= 0)
                            entities.enemyObjs.RemoveAt(i);
                        foreach (ISprite fireball in entities.fireBallObjs)
                        {
                            sprite.Collision(fireball);
                        }
                        //World collision
                        if (sprite.Position.X > 256)
                        {
                            foreach (ISprite block in collisionZones[((int)(sprite.Position.X / 256)) - 1])
                            {
                                if (block is BlockContext || block is Pipe || block is SpikeBlock)
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
                            if (block is BlockContext || block is Pipe || block is SpikeBlock)
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
                                if (block is BlockContext || block is Pipe || block is SpikeBlock)
                                {
                                    sprite.Collision(block);
                                    if (mario.context.ShowHitbox)
                                        sprite.ShowHitbox = true;
                                    else sprite.ShowHitbox = false;
                                }
                            }
                        }
                        if (sprite is KoopaShell) sprite.Collision(mario);
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
                if (mario.Position.Y > 400)
                    Reset();
                if (mario.warped)
                {
                    if (mario.overworld)
                    {
                        inOverworld = true;
                        spawnPos = new Vector2(2624, 210);
                        Reset();
                    }
                    else
                    {
                        inOverworld = false;
                        Reset();
                    }
                    
                }
                if (tracker.timeRemaining == 6000)
                {
                    TimeWarning.Play();
                }
                if (tracker.timeRemaining == 0)
                    mario.context.DieInPit();
            }
        }

        public void Reset()
        {
            for (int i = 0; i < collisionZones.Length; i++)
            {
                collisionZones[i].Clear();
            }
            mario.context.isBallooned = false;
            entities.enemyObjs.Clear();
            entities.entityObjs.Clear();
            entities.fireBallObjs.Clear();
            bgObjects.Clear();
            menu.SwitchOverlay(new NoOverlayState(font,menu));
            theatre.IsMenuVisible = false;
            reset = true;
            if (!mario.warped)
            {
                ResetTimeRemainingCommand.Execute();
                mario.context.SetPowerUpState(new StandardMarioState());
            }
            GenerateMap();
            mario.warp = false;
            mario.warped = false;
            mario.isWarpableHorizontal = false;
            mario.Position = spawnPos;
            mario.context.SetActionState(new IdleState(mario.context));
            ResetPointsCommand.Execute();
            mario.balloonTimer = 0;
        }

        public void HardReset()
        {
            for (int i = 0; i < collisionZones.Length; i++)
            {
                collisionZones[i].Clear();
            }
            levelnum = 0;
            mario.context.isBallooned = false;
            mario.balloonTimer = 0;
            entities.enemyObjs.Clear();
            entities.entityObjs.Clear();
            entities.fireBallObjs.Clear();
            bgObjects.Clear();
            menu.SwitchOverlay(new NoOverlayState(font, menu));
            theatre.IsMenuVisible = false;
            inOverworld = true;
            reset = true;
            GenerateMap();
            mario.Position = new Vector2(100, 230);
            mario.context.SetPowerUpState(new StandardMarioState());
            ResetTimeRemainingCommand.Execute();
            ResetLivesCommand.Execute();
            ResetPointsCommand.Execute();
        }
    }
}
