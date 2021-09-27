using System;
using System.Collections.Generic;
using System.Text;

namespace Mario.Sprites.Mario
{
    public class SuperMario
    {



        public void MoveLeftCommand()
        {
            //currentactionstate
            Console.WriteLine("Move Left");
        }

        public void MoveRightCommand()
        {
            Console.WriteLine("Move Right");
        }

        public void JumpCommand()
        {
            //CurrentActionState.Jump();
            Console.WriteLine("Jump");
        }

        public void CrouchCommand()
        {
            Console.WriteLine("Crouch");
        }
    }
}
