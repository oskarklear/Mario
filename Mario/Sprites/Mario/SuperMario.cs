using System;
using System.Collections.Generic;
using System.Text;

namespace Mario.Sprites.Mario
{
    public class SuperMario
    {



        public void moveLeft()
        {
            //currentactionstate
            Console.WriteLine("Move Left");
        }

        public void moveRight()
        {
            Console.WriteLine("Move Right");
        }

        public void jump()
        {
            //CurrentActionState.Jump();
            Console.WriteLine("Jump");
        }

        public void crouch()
        {
            Console.WriteLine("Crouch");
        }
    }
}
