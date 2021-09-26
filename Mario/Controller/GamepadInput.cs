using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Mario.Controller;
using Mario.Movement;

namespace Mario
{
    class GamepadInput : IController
    {
        private GamePadState previousGamePadState;
        public Game1 GameObj { get; set; }
        public MovementCommand MoveLeft { get; set; }
        public MovementCommand MoveRight { get; set; }
        public MovementCommand Jump { get; set; }
        public MovementCommand Crouch { get; set; }
        //public MovementCommand Fireball { get; set; }

        private List<Input> GetInput()
        {
            List<Input> inputs = new List<Input>();

            GamePadState emptyInput = new GamePadState(); //(Vector2.Zero, Vector2.Zero, 0, 0);

            // Get the current GamePad state.
            GamePadState currentGamePadState = GamePad.GetState(PlayerIndex.One);

            // Process input only if connected.
            if (currentGamePadState.IsConnected)
            {
                if (currentGamePadState != emptyInput) // Button Pressed
                {
                    var buttonList = (Buttons[])Enum.GetValues(typeof(Buttons));

                    foreach (var button in buttonList)
                    {
                        if (currentGamePadState.IsButtonDown(button) &&
                            !previousGamePadState.IsButtonDown(button))
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
            List<Input> inputs = GetInput();
            foreach (Input input in inputs)
                switch (input.Key)
                {
                    // Leftward Movement
                    case (int)Buttons.DPadLeft:
                        MoveLeft.Execute();
                        break;

                    // Rightward Movement
                    case (int)Buttons.DPadRight:
                        MoveRight.Execute();
                        break;

                    // Jump
                    case (int)Buttons.A:
                        Jump.Execute();
                        break;

                    // Crouch
                    case (int)Buttons.DPadDown:
                        Crouch.Execute();
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
