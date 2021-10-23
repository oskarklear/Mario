using System;
using System.Collections.Generic;
using System.Text;
using Mario.Sprites;
using Microsoft.Xna.Framework.Graphics;
using Mario.Sprites.Items;

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

        public void Update()
        {
            foreach (ISprite obj in entityObjs)
            {
                obj.Update();
                if (obj is RedMushroom)
                {
                    System.Diagnostics.Debug.WriteLine(obj.ToString());
                    System.Diagnostics.Debug.WriteLine("Hitbox: " + obj.Hitbox);
                    //System.Diagnostics.Debug.WriteLine("Obtained: " + (RedMushroom)obj.Obtained);
                }
            }
        }
    }
}
