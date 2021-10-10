using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Mario.Movement;
using Mario.Sprites.Mario;
using Mario.Controller;
using Microsoft.Xna.Framework;

namespace Mario.Controller
{
    class ActivateIdle
    {
        public ICommand IdleCommand { get; set; }

        public ActivateIdle(SuperMario mario)
        {
            IdleCommand = new IdleCommand(mario);
        }
        public void ActivateIdleCommand()
        {
            if (!Keyboard.GetState().IsKeyDown(Keys.D) && !Keyboard.GetState().IsKeyDown(Keys.A)
                && !Keyboard.GetState().IsKeyDown(Keys.W) && !Keyboard.GetState().IsKeyDown(Keys.S)
                && !Keyboard.GetState().IsKeyDown(Keys.Right) && !Keyboard.GetState().IsKeyDown(Keys.Left)
                && !Keyboard.GetState().IsKeyDown(Keys.Up) && !Keyboard.GetState().IsKeyDown(Keys.Down)
                && !GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.DPadLeft)
                && !GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.DPadRight)
                && !GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.A)
                && !GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.DPadDown))
            {
                IdleCommand.Execute();
            }
        }
    }
}
