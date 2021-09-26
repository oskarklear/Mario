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
        private GamePadState previousGamePadState1;
        private GamePadState previousGamePadState2;
        private GamePadState previousGamePadState3;
        private GamePadState previousGamePadState4;
       
        public Game1 GameObj { get; set; }
        public MovementCommand MoveLeft { get; set; }
        public MovementCommand MoveRight { get; set; }
        public MovementCommand Jump { get; set; }
        public MovementCommand Crouch { get; set; }
        //public MovementCommand Fireball { get; set; }

        private List<Input> GetInput()
        {
            List<Input> inputs = new List<Input>();
            List<GamePadState> previousGamePadStates = new List<GamePadState>();
            List<GamePadState> currentGamePadStates = new List<GamePadState>();
            GamePadState emptyInput = new GamePadState(); //(Vector2.Zero, Vector2.Zero, 0, 0);

            // Get the current GamePad state.
            GamePadState P1currentGamePadState = GamePad.GetState(PlayerIndex.One);
            GamePadState P2currentGamePadState = GamePad.GetState(PlayerIndex.Two);
            GamePadState P3currentGamePadState = GamePad.GetState(PlayerIndex.Three);
            GamePadState P4currentGamePadState = GamePad.GetState(PlayerIndex.Four);
            
            currentGamePadStates.Add(P1currentGamePadState);
            currentGamePadStates.Add(P2currentGamePadState);
            currentGamePadStates.Add(P3currentGamePadState);
            currentGamePadStates.Add(P4currentGamePadState);

            previousGamePadStates.Add(previousGamePadState1);
            previousGamePadStates.Add(previousGamePadState2);
            previousGamePadStates.Add(previousGamePadState3);
            previousGamePadStates.Add(previousGamePadState4);

            for (int i = 0; i < currentGamePadStates.Count; i++)
            {
                if (currentGamePadStates[i].IsConnected) // Process input only if connected.
                {
                    if (currentGamePadStates[i] != emptyInput) // Button Pressed
                    {
                        var buttonList = (Buttons[])Enum.GetValues(typeof(Buttons));

                        foreach (var button in buttonList)
                        {
                            if (currentGamePadStates[i].IsButtonDown(button) && !previousGamePadStates[i].IsButtonDown(button))
                            {
                                Input input = new Input();
                                input.Controller = Input.ControllerType.Gamepad;
                                input.Key = (int)button;
                                inputs.Add(input);
                            }
                        }
                    }
                }
                previousGamePadStates[i] = currentGamePadStates[i];
            }

            

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
