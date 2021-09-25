using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Mario.Controller;

namespace Mario
{
    class GamepadInput : IController
    {
        private GamePadState previousGamePadState;
        public Game1 GameObj { get; set; }

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
                        break;

                    // Rightward Movement
                    case (int)Buttons.DPadRight:
                        break;

                    // Jump
                    case (int)Buttons.A:
                        break;

                    // Crouch
                    case (int)Buttons.DPadDown:
                        break;

                    // Dash/throw Fireball
                    case (int)Buttons.B:
                        break;

                    // Pause
                    case (int)Buttons.Start:
                        break;
                }
        }
    }
}
