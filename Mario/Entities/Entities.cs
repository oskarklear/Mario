using System;
using System.Collections.Generic;
using System.Text;
using Mario.Sprites;
using Microsoft.Xna.Framework.Graphics;

namespace Mario.Entities
{
    public class DynamicEntities
    {
        public List<ISprite> entityObjs = new List<ISprite>();


        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (ISprite obj in entityObjs)
                obj.Draw(spriteBatch);
        }
    }
}
