using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Mario.Controller;
using Mario.Movement;
using Mario.Sprites.Mario;

namespace Mario
{
    class GamepadInput : IController
    {
        private GamePadState previousGamePadState1;
        private GamePadState previousGamePadState2;
        private GamePadState previousGamePadState3;
        private GamePadState previousGamePadState4;
        public Game1 GameObj { get; set; }
        public ICommand MoveLeftCommand { get; set; }
        public ICommand MoveRightCommand { get; set; }
        public ICommand JumpCommand { get; set; }
        public ICommand CrouchCommand { get; set; }
        public ICommand ExitCommand { get; set; }
        private MarioContext context;
        //public MovementCommand Fireball { get; set; }
        public GamepadInput(SuperMario mario)
        {
            MoveLeftCommand = new MoveLeftCommand(mario);
            MoveRightCommand = new MoveRightCommand(mario);
            JumpCommand = new JumpCommand(mario);
            CrouchCommand = new CrouchCommand(mario);
            context = mario.context;
        }
        private List<Input> GetInput()
        {
            List<Input> inputs = new List<Input>();
            
            GamePadState emptyInput = new GamePadState(); //(Vector2.Zero, Vector2.Zero, 0, 0);

            // Get the current GamePad state.
            GamePadState P1currentGamePadState = GamePad.GetState(PlayerIndex.One);
            GamePadState P2currentGamePadState = GamePad.GetState(PlayerIndex.Two);
            GamePadState P3currentGamePadState = GamePad.GetState(PlayerIndex.Three);
            GamePadState P4currentGamePadState = GamePad.GetState(PlayerIndex.Four);

            if (P1currentGamePadState.IsConnected) // Process input only if connected.
            {
                if (P1currentGamePadState != emptyInput) // Button Pressed
                {
                    var buttonList = (Buttons[])Enum.GetValues(typeof(Buttons));

                    foreach (var button in buttonList)
                    {
                        if (P1currentGamePadState.IsButtonDown(button) && !previousGamePadState1.IsButtonDown(button))
                        {
                            Input input = new Input();
                            input.Controller = Input.ControllerType.Gamepad;
                            input.Key = (int)button;
                            inputs.Add(input);
                        }
                    }
                }
            }
            previousGamePadState1 = P1currentGamePadState;

            if (P2currentGamePadState.IsConnected) // Process input only if connected.
            {
                if (P2currentGamePadState != emptyInput) // Button Pressed
                {
                    var buttonList = (Buttons[])Enum.GetValues(typeof(Buttons));

                    foreach (var button in buttonList)
                    {
                        if (P2currentGamePadState.IsButtonDown(button) && !previousGamePadState2.IsButtonDown(button))
                        {
                            Input input = new Input();
                            input.Controller = Input.ControllerType.Gamepad;
                            input.Key = (int)button;
                            inputs.Add(input);
                        }
                    }
                }
            }
            previousGamePadState2 = P2currentGamePadState;

            if (P3currentGamePadState.IsConnected) // Process input only if connected.
            {
                if (P3currentGamePadState != emptyInput) // Button Pressed
                {
                    var buttonList = (Buttons[])Enum.GetValues(typeof(Buttons));

                    foreach (var button in buttonList)
                    {
                        if (P3currentGamePadState.IsButtonDown(button) && !previousGamePadState3.IsButtonDown(button))
                        {
                            Input input = new Input();
                            input.Controller = Input.ControllerType.Gamepad;
                            input.Key = (int)button;
                            inputs.Add(input);
                        }
                    }
                }
            }
            previousGamePadState3 = P3currentGamePadState;

            if (P4currentGamePadState.IsConnected) // Process input only if connected.
            {
                if (P4currentGamePadState != emptyInput) // Button Pressed
                {
                    var buttonList = (Buttons[])Enum.GetValues(typeof(Buttons));

                    foreach (var button in buttonList)
                    {
                        if (P4currentGamePadState.IsButtonDown(button) && !previousGamePadState4.IsButtonDown(button))
                        {
                            Input input = new Input();
                            input.Controller = Input.ControllerType.Gamepad;
                            input.Key = (int)button;
                            inputs.Add(input);
                        }
                    }
                }
            }
            previousGamePadState4 = P4currentGamePadState;

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
