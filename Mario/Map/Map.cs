using Mario.Sprites;
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
                        }
                    }
                        //collisionTiles.Add(new CollisionTiles(number, new Rectangle(i * size, j * size, size, size)));

                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (BlockSprite tile in collisionTiles)
                tile.Draw(spriteBatch);
        }
        public void Update()
        {
            foreach (BlockSprite tile in collisionTiles)
                tile.Update();
        }
    }
}
