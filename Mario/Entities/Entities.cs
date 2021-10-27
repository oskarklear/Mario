using System;
using System.Collections.Generic;
using System.Text;
using Mario.Sprites;
using Microsoft.Xna.Framework.Graphics;
using Mario.Sprites.Items;
using Mario.Sprites.Projectiles;

namespace Mario.Entities
{
    public class DynamicEntities
    {
        public List<ISprite> entityObjs = new List<ISprite>();
        public List<ISprite> fireBallObjs = new List<ISprite>();
        public List<ISprite> enemyObjs = new List<ISprite>();


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
    }
}
