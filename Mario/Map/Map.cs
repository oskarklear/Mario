using Mario.Sprites;
using Mario.Sprites.Enemies;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mario.Map
{
    class Level
    {
        private List<ISprite> collisionTiles = new List<ISprite>();
        public List<ISprite> CollisionTiles
        {
            get { return collisionTiles; }
        }
        private Game1 theatre;
        public Game1 Theatre
        {
            get { return theatre; }
            set { theatre = value; }
        }
        private const int KOOPAH = 26;
        private const int KOOPAW = 16;
        private const int GOOMBAH = 16;
        private const int GOOMBAW = 16;
        public Level()
        {

        }

        public void GenerateMap(string[][] map, int size)
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
                            case 1:
                                collisionTiles.Add(new GroundBlock(theatre, new Vector2(i * size, j * size), null));
                                break;
                            case 2:
                                collisionTiles.Add(new BrickBlockSprite(theatre, new Vector2(i * size, j * size), null));
                                break;
                            case 3:
                                collisionTiles.Add(new QuestionBlockSprite(theatre, new Vector2(i * size, j * size), null));
                                break;
                            case 30:
                                collisionTiles.Add(new Goomba(theatre, new Vector2(i * GOOMBAH, j * GOOMBAW - size + 1)));
                                break;
                            case 31:
                                collisionTiles.Add(new Koopa(theatre, new Vector2(i * KOOPAH, j * KOOPAW - size)));
                                break;
                        }
                    }
                        //collisionTiles.Add(new CollisionTiles(number, new Rectangle(i * size, j * size, size, size)));

                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (ISprite tile in collisionTiles)
                tile.Draw(spriteBatch);
        }
        public void Update()
        {
            foreach (ISprite tile in collisionTiles)
                tile.Update();
        }
    }
}
