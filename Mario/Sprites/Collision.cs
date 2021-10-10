using System;
using System.Collections.Generic;
using Mario.Map;
using Mario.Sprites;
using Microsoft.Xna.Framework.Graphics;

namespace Mario.Sprites
{
    public interface ICollider : ISprite
    {
        public void Collision(ICollider collider, int xOffset, int yOffset);
    }

    public class Collision
    {
        List<ICollider>[,] CollisionGrid;
        
        GraphicsDevice Graphics;

        public Collision(List<ISprite> map,GraphicsDevice graphics)
        {
            CollisionGrid = new List<ICollider>[10,10];
            for(int x = 0; x < 10; x++)
            {
                for(int y = 0; y < 10; y++)
                {
                    
                    CollisionGrid[x, y] = new List<ICollider>();
                }
            }
            
            
            Graphics = graphics;

            foreach (ISprite sprite in map)
            {
                
                if (sprite is ICollider)
                {
                    ICollider collider = sprite as ICollider;
                    int x = collider.DestinationRectangle.X;
                    int y = collider.DestinationRectangle.Y;
                    //This probably doesn't work right. If something's wrong, its this
                    System.Diagnostics.Debug.WriteLine("item added to x bin:", x / graphics.Viewport.Width * 10);
                    System.Diagnostics.Debug.WriteLine(x / graphics.Viewport.Width * 10);
                    System.Diagnostics.Debug.WriteLine("item added to y bin:", y / graphics.Viewport.Height * 10);
                    System.Diagnostics.Debug.WriteLine(y / graphics.Viewport.Height * 10);
                    CollisionGrid[x / graphics.Viewport.Width * 10, y / graphics.Viewport.Height * 10].Add(collider);
                }
            }
        }
        public void Update(List<ICollider> dynamics)
        {
            
            foreach (ICollider dynamic in dynamics)
            {
                //System.Diagnostics.Debug.WriteLine(dynamic.GetType());
                int x = dynamic.DestinationRectangle.X;
                int y = dynamic.DestinationRectangle.Y;
                foreach(ICollider target in CollisionGrid[x / Graphics.Viewport.Width * 10, y / Graphics.Viewport.Height * 10])
                {
                    target.Collision(dynamic, 800, 608);
                    dynamic.Collision(target, 800, 608);
                    
                }
            }
        }
    }
}
