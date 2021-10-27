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
        public ICommand FaceLeftDiscontinueCommand { get; set; }
        public ICommand FaceRightDiscontinueCommand { get; set; }
        public ICommand CrouchingDiscontinueCommand { get; set; }
        public ICommand JumpingDiscontinueCommand { get; set; }

        public ActivateIdle(SuperMario mario)
        {
            IdleCommand = new IdleCommand(mario);
            FaceLeftDiscontinueCommand = new FaceLeftDiscontinueCommand(mario);
            FaceRightDiscontinueCommand = new FaceRightDiscontinueCommand(mario);
            CrouchingDiscontinueCommand = new CrouchingDiscontinueCommand(mario);
            JumpingDiscontinueCommand = new JumpingDiscontinueCommand(mario);
        }

        public void ActivateIdleCommand()
        {
            if (!Keyboard.GetState().IsKeyDown(Keys.D) && !Keyboard.GetState().IsKeyDown(Keys.Right)
                && !GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.DPadRight))
            {
                FaceRightDiscontinueCommand.Execute();
            }

            if (!Keyboard.GetState().IsKeyDown(Keys.A) && !Keyboard.GetState().IsKeyDown(Keys.Left)
                && !GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.DPadLeft))
            {
                FaceLeftDiscontinueCommand.Execute();
            }

            if (!Keyboard.GetState().IsKeyDown(Keys.S) && !Keyboard.GetState().IsKeyDown(Keys.Down)
                && !GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.DPadDown))
            {
                CrouchingDiscontinueCommand.Execute();
            }

            if (!Keyboard.GetState().IsKeyDown(Keys.W) && !Keyboard.GetState().IsKeyDown(Keys.Up)
                && !GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.A))
            {
                JumpingDiscontinueCommand.Execute();
            }
        }
    }
}
