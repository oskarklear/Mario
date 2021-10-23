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

namespace Mario.Map
{
    class Level
    {
        private List<ISprite> collisionObjs = new List<ISprite>();
        public List<ISprite> bgObjects = new List<ISprite>();
        //public DynamicEntities entities = new DynamicEntities();

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
/*        bool direction;
        bool Direction
        {
            get
            {
                if (Mario.position.X)
            }
        }*/
        private const int KOOPAH = 26;
        private const int KOOPAW = 16;
        private const int GOOMBAH = 16;
        private const int GOOMBAW = 16;
        private const int MUSHROOM = 18;
        private const int FLOWER = 16;
        private const int COINH = 16;
        private const int COINW = 12;
        private const int BLOCK = 16;
        public Level()
        {

        }

        public void GenerateMap(string[][] map)
        {
            for (int i = 0; i < 50; i++)
            {
                for (int j = 0; j < 38; j++)
                {
                    int number = Int32.Parse(map[j][i]);

                    if (number > 0)
                    {
                        switch (number)
                        {
                            case 1: //Ground Block
                                BlockContext groundBlock = new BlockContext(theatre, new Vector2(i * BLOCK, j * BLOCK));
                                groundBlock.SetState(new GroundBlockState());
                                collisionObjs.Add(groundBlock);
                                break;
                            case 2: //Brick Block
                                BlockContext brickBlock = new BlockContext(theatre, new Vector2(i * BLOCK, j * BLOCK));
                                brickBlock.SetState(new BrickBlockState());
                                collisionObjs.Add(brickBlock);
                                break;
                            case 3:  //Red Mushroom Question Block
                                BlockContext redMushroomQuestionBlock = new BlockContext(theatre, new Vector2(i * BLOCK, j * BLOCK));
                                redMushroomQuestionBlock.SetState(new RedMushroomQuestionBlockState());
                                collisionObjs.Add(redMushroomQuestionBlock);
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
                                collisionObjs.Add(new Pipe(theatre, new Vector2(i * 15, j * 15 + 20)));
                                break;
                            case 7: //Green Mushroom Question Block
                                BlockContext greenMushroomQuestionBlock = new BlockContext(theatre, new Vector2(i * BLOCK, j * BLOCK));
                                greenMushroomQuestionBlock.SetState(new GreenMushroomQuestionBlockState());
                                collisionObjs.Add(greenMushroomQuestionBlock);
                                break;
                            case 8: //Fire Flower Question Block
                                BlockContext fireFlowerQuestionBlock = new BlockContext(theatre, new Vector2(i * BLOCK, j * BLOCK));
                                fireFlowerQuestionBlock.SetState(new FireFlowerQuestionBlockState());
                                collisionObjs.Add(fireFlowerQuestionBlock);
                                break;
                            case 9: //Coin Question Block
                                BlockContext coinQuestionBlock = new BlockContext(theatre, new Vector2(i * BLOCK, j * BLOCK));
                                coinQuestionBlock.SetState(new CoinQuestionBlockState());
                                collisionObjs.Add(coinQuestionBlock);
                                break;
                            case 10:  //Coin
                                collisionObjs.Add(new Coin(theatre, new Vector2(i * COINW, j * COINH)));
                                break;
                            case 11:  //Green Mushroom
                                collisionObjs.Add(new GreenMushroom(theatre, new Vector2(i * MUSHROOM, j * MUSHROOM), Mario));
                                break;
                            case 12:  //Red Mushroom
                                collisionObjs.Add(new RedMushroom(theatre, new Vector2(i * MUSHROOM, j * MUSHROOM), Mario));
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
                            case 51:
                                bgObjects.Add(new Cloud(Theatre, new Vector2(i * 16, j * 7)));
                                break;
                            case 52:
                                bgObjects.Add(new Bush(Theatre, new Vector2(i * 13, (j * 5) + 350)));
                                break;
                            case 41: //Mario
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
            foreach (ISprite obj in collisionObjs)
                obj.Draw(spriteBatch);
            //entities.Draw(spriteBatch);
            
        }
        public void Update()
        {
            //SuperMario marioTemp;
            foreach (ISprite obj in collisionObjs)
            {
                //if (obj is SuperMario) marioTemp = (SuperMario)obj;
                //if (obj is RedMushroom)
                //{
                //    ((RedMushroom)obj).position.X.CompareTo(marioTemp.position.X);
                //}
                obj.Update();
            }
                
        }
    }
}
