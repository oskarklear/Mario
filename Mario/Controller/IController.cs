using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Mario
{
    interface IController
    {
        void UpdateInput();
    }

    class KeyboardInput : IController
    {
        private KeyboardState previousKeyboardState;
        public Game1 GameObj { get; set; }

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
                    case (int)Keys.Q:
                        GameObj.Exit();
                        break;

                    case (int)Keys.W:
                        break;

                    case (int)Keys.E:
                        break;

                    case (int)Keys.R:
                        break;

                    case (int)Keys.T:
                        break;
                }
        }
    }

    class GamePadInput : IController
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
                    case (int)Buttons.Start:
                        GameObj.Exit();
                        break;

                    case (int)Buttons.A:
                        break;

                    case (int)Buttons.B:
                        break;

                    case (int)Buttons.X:
                        break;

                    case (int)Buttons.Y:
                        break;
                }
        }
    }

    class Input
    {
        public enum ControllerType
        {
            [Description("Undefined")] Undefined,
            [Description("Keyboard")] Keyboard,
            [Description("Gamepad")] Gamepad
        }

        public ControllerType Controller { get; set; }

        public int Key { get; set; }

        public Input()
        {
            Controller = ControllerType.Undefined;
        }

        public override string ToString()
        {
            string key = (Controller == ControllerType.Keyboard) ? ((Keys)Key).ToString() : ((Buttons)Key).ToString();
            return base.ToString() + ": " + Controller.GetDescription() + " - " + key;
        }

    }

    // Reflection Helper - For getting String from enumeration
    public static class StringHelper
    {
        public static string GetDescription(this Enum value)
        {
            Type type = value.GetType();
            string name = Enum.GetName(type, value);
            if (name != null)
            {
                FieldInfo field = type.GetField(name);
                if (field != null)
                {
                    DescriptionAttribute attr =
                        Attribute.GetCustomAttribute(field,
                            typeof(DescriptionAttribute)) as DescriptionAttribute;
                    if (attr != null)
                    {
                        return attr.Description;
                    }
                }
            }
            return null;
        }
    }
}


