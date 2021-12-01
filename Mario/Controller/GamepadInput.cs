using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Mario.Controller;
using Mario.Movement;
using Mario.Sprites.Mario;
using Mario.Sprites;

namespace Mario
{
    class GamepadInput : IController
    {
        private GamePadState previousGamePadState;
        private ActivateIdle ActivateIdle { get; set; }
        public Game1 GameObj { get; set; }
        public ICommand MoveLeftCommand { get; set; }
        public ICommand MoveRightCommand { get; set; }
        public ICommand IdleCommand { get; set; }
        public ICommand JumpCommand { get; set; }
        public ICommand CrouchCommand { get; set; }
        public ICommand FireCommand { get; set; }
        public ICommand ExitCommand { get; set; }

        private MarioContext context;

        public GamepadInput(SuperMario mario)
        {
            MoveLeftCommand = new MoveLeftCommand(mario);
            MoveRightCommand = new MoveRightCommand(mario);
            JumpCommand = new JumpCommand(mario);
            CrouchCommand = new CrouchCommand(mario);
            FireCommand = new FireCommand(mario);
            IdleCommand = new IdleCommand(mario);
            ActivateIdle = new ActivateIdle(mario);
            context = mario.context;
        }

        private List<Input> GetInput()
        {
            List<Input> inputs = new List<Input>();
            
            GamePadState currentGamePadState = GamePad.GetState(PlayerIndex.One);

                Buttons[] buttonList = (Buttons[])Enum.GetValues(typeof(Buttons));
                    
                foreach (Buttons button in buttonList)
                {
                    if (currentGamePadState.IsButtonDown(button))
                    {
                        Input input = new Input();
                        input.Controller = Input.ControllerType.Gamepad;
                        input.Key = (int)button;
                        inputs.Add(input);
                    }
                }

            previousGamePadState = currentGamePadState;

            return inputs;
        }

        public void UpdateInput()
        {
            ActivateIdle.ActivateIdleCommand();
            if (previousGamePadState.IsButtonDown(Buttons.B))
                context.dashing = true;
            else
                context.dashing = false;
            List<Input> inputs = GetInput();
            foreach (Input input in inputs)
                switch (input.Key)
                {
                    // Leftward Movement
                    case (int)Buttons.DPadLeft:
                        MoveLeftCommand.Execute();
                        break;

                    // Rightward Movement
                    case (int)Buttons.DPadRight:
                        MoveRightCommand.Execute();
                        break;

                    // Jump
                    case (int)Buttons.A:
                        JumpCommand.Execute();
                        break;

                    // Crouch
                    case (int)Buttons.DPadDown:
                        CrouchCommand.Execute();
                        break;

                    // Dash/throw Fireball
                    case (int)Buttons.B:
                        FireCommand.Execute();
                        break;

                    // Pause
                    case (int)Buttons.Start:
                        // Pause.Execute();
                        break;
                }
        }
    }
}
