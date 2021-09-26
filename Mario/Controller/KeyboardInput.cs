using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using Mario.Controller;

namespace Mario
{
    class KeyboardInput : IController
    {
        private KeyboardState previousKeyboardState;
        public Game1 GameObj { get; set; }
        public ICommand MoveLeft { get; set; }
        public ICommand MoveRight { get; set; }

        private List<Input> GetInput()
        {
            List<Input> inputs = new List<Input>();

            // Get the current Keyboard state.
            KeyboardState currentKeyboardState = Keyboard.GetState();

            Keys[] keysPressed = currentKeyboardState.GetPressedKeys();
            foreach (Keys key in keysPressed)
                if (!previousKeyboardState.IsKeyDown(key))
                {
                    Input input = new Input();
                    input.Controller = Input.ControllerType.Keyboard;
                    input.Key = (int)key;
                    inputs.Add(input);
                }

            // Update previous Keyboard state.
            previousKeyboardState = currentKeyboardState;

            return inputs;
        }

        public void UpdateInput()
        {
            List<Input> inputs = GetInput();
            foreach (Input input in inputs)
                switch (input.Key)
                {
                    // Leftward Movement (A key)
                    case (int)Keys.A:
                        MoveLeft.Execute();
                        break;
                    
                    // Leftward Movement (Left Arrow)
                    case (int)Keys.Left:
                        MoveLeft.Execute();
                        break;

                    // Rightward Movement (D key)
                    case (int)Keys.D:
                        MoveRight.Execute();
                        break;

                    // Rightward Movement (Right Arrow)
                    case (int)Keys.Right:
                        MoveRight.Execute();
                        break;

                    // Jump (W key)
                    case (int)Keys.W:
                        break;

                    // Jump (Up Arrow)
                    case (int)Keys.Up:
                        break;

                    // Crouch (S key)
                    case (int)Keys.S:
                        break;

                    // Crouch (Down Arrow)
                    case (int)Keys.Down:
                        break;

                    // Dash/throw Fireball
                    case (int)Keys.Space:
                        break;

                    // Game Exit
                    case (int)Keys.Q:
                        GameObj.Exit();
                        break;

                    // Pause
                    case (int)Keys.P:
                        break;

                    // Mute
                    case (int)Keys.M:
                        break;
                }
            }
        }
    }
