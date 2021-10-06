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

        public Game1 GameObj { get; set; }
        public ICommand MoveLeftCommand { get; set; }
        public ICommand MoveRightCommand { get; set; }
        public ICommand IdleCommand { get; set; }
        public ICommand JumpCommand { get; set; }
        public ICommand CrouchCommand { get; set; }
        public ICommand ExitCommand { get; set; }

        private MarioContext context;

        public GamepadInput(SuperMario mario)
        {
            MoveLeftCommand = new MoveLeftCommand(mario);
            MoveRightCommand = new MoveRightCommand(mario);
            JumpCommand = new JumpCommand(mario);
            CrouchCommand = new CrouchCommand(mario);
            IdleCommand = new IdleCommand(mario);
            context = mario.context;
        }

        private List<Input> GetInput()
        {
            List<Input> inputs = new List<Input>();
            
            GamePadState emptyInput = new GamePadState(); //(Vector2.Zero, Vector2.Zero, 0, 0);

            // Get the current GamePad state.
            GamePadState currentGamePadState = GamePad.GetState(PlayerIndex.One);

            if (currentGamePadState.IsConnected) // Process input only if connected.
            {
                if (currentGamePadState != emptyInput) // Button Pressed
                {
                    //var buttonList = (Buttons[])Enum.GetValues(typeof(Buttons));
                    var buttonList = new Buttons[] { Buttons.DPadDown, Buttons.DPadUp, Buttons.DPadLeft, Buttons.DPadRight };
                    foreach (var button in buttonList)
                    {
                        if (currentGamePadState.IsButtonDown(button) /*&& !previousGamePadState1.IsButtonDown(button)*/)
                        {
                            Input input = new Input();
                            input.Controller = Input.ControllerType.Gamepad;
                            input.Key = (int)button;
                            inputs.Add(input);
                        }
                    }
                }
            }
            previousGamePadState = currentGamePadState;

            return inputs;
        }

        public void UpdateInput()
        {
            if (!GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.DPadRight) && !GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.DPadLeft))
            {
                IdleCommand.Execute();
            }

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
                        // Fireball.Execute();
                        break;

                    // Pause
                    case (int)Buttons.Start:
                        // Pause.Execute();
                        break;
                }
        }
    }
}
