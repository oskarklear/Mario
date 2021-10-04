using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Mario.Movement
{
    class Position
    {
        private double x;
        private double y;

        public Position(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public int intX()
        {
            return (int)Math.Round(x);
        }

        public int intY()
        {
            return (int)Math.Round(y);
        }

        public double getX()
        {
            return x;
        }
        
        public double getY()
        {
            return y;
        }

/*        public void move(Move movement)
        {
            Vector2 vector = movement.;
            x += vector.X;
            y += vector.Y;
        }*/
    }
}
