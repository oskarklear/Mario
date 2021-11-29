using System;
using System.Collections.Generic;
using System.Text;
using Mario.Sprites;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Mario.Sprites.Items;
using Mario.Sprites.Projectiles;
using Mario.Sprites.Enemies;

namespace Mario.Entities
{
    public class DynamicEntities
    {
        public List<ISprite> entityObjs = new List<ISprite>();
        public List<ISprite> fireBallObjs = new List<ISprite>();
        public List<ISprite> enemyObjs = new List<ISprite>();

        private Game1 gameObj;

        public DynamicEntities(Game1 theatre)
        {
            gameObj = theatre;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (ISprite obj in entityObjs)
                obj.Draw(spriteBatch);
            foreach (ISprite fireball in fireBallObjs)
                fireball.Draw(spriteBatch);
            foreach (ISprite enemy in enemyObjs)
                enemy.Draw(spriteBatch);
        }

        public void Update()
        {
            foreach (ISprite obj in entityObjs)
            {
                obj.Update();
            }
            foreach (ISprite fireball in fireBallObjs)
            {
                fireball.Update();
            }
            foreach (ISprite enemy in enemyObjs)
            {
                enemy.Update();
            }
        }

        public void NewKoopa(Vector2 location)
        {
            enemyObjs.Add(new Koopa(gameObj, new Vector2(location.X, location.Y)));
        }

        public void NewKoopaShell(Vector2 location)
        {
            enemyObjs.Add(new KoopaShell(gameObj, new Vector2(location.X, location.Y)));
        }

        public void NewParakoopa(Vector2 location)
        {
            enemyObjs.Add(new Parakoopa(gameObj, new Vector2(location.X, location.Y)));
        }
    }
}
