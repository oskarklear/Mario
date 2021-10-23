using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using Mario.Controller;
using Mario.Movement;
using Mario.Sprites.Mario;
using Mario.States;
using Mario.Sprites;
using System.Windows.Input;
using Mario.Map;

namespace Mario
{
    class KeyboardInput : IController
    {
        private KeyboardState previousKeyboardState;
        private ActivateIdle ActivateIdle { get; set; }
        public Game1 GameObj { get; set; }
        public ICommand MoveLeftCommand { get; set; }
        public ICommand MoveRightCommand { get; set; }
        public ICommand IdleCommand { get; set; }
        public ICommand JumpCommand { get; set; }
        public ICommand CrouchCommand { get; set; }
        public ICommand ExitCommand { get; set; }
        public ICommand QuestionBumpCommand { get; set; }
        public ICommand HiddenBumpCommand { get; set; }
        public ICommand BrickBumpCommand { get; set; }

        Level map;
        private MarioContext context;
        private BlockContext questionBlockContext;
        private BlockContext hiddenBlockContext;
        private BlockContext brickBlockContext;
        //public MovementCommand Fireball { get; set; }
        public KeyboardInput(Level map)
        {
            MoveLeftCommand = new MoveLeftCommand(map.Mario);
            MoveRightCommand = new MoveRightCommand(map.Mario);
            JumpCommand = new JumpCommand(map.Mario);
            CrouchCommand = new CrouchCommand(map.Mario);
            IdleCommand = new IdleCommand(map.Mario);
            ActivateIdle = new ActivateIdle(map.Mario);
            context = map.Mario.context;
            QuestionBumpCommand = new BumpCommand(questionBlockContext, context);
            HiddenBumpCommand = new BumpCommand(hiddenBlockContext, context);
            BrickBumpCommand = new BumpCommand(brickBlockContext, context);
            this.map = map;
        }
        private List<Input> GetInput()
        {
            List<Input> inputs = new List<Input>();

            // Get the current Keyboard state.
            KeyboardState currentKeyboardState = Keyboard.GetState();

            Keys[] keysPressed = currentKeyboardState.GetPressedKeys();

            foreach (Keys key in keysPressed)
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
            ActivateIdle.ActivateIdleCommand();

            List<Input> inputs = GetInput();

            foreach (Input input in inputs) {

                switch (input.Key)
                {
                    //Leftward Movement (A key)

                    case (int)Keys.A:
                        MoveLeftCommand.Execute();
                        break;

                    // Leftward Movement (Left Arrow)
                    case (int)Keys.Left:
                        MoveLeftCommand.Execute();
                        break;

                    //Rightward Movement(D key)
                    case (int)Keys.D:
                        MoveRightCommand.Execute();
                        break;

                    // Rightward Movement (Right Arrow)
                    case (int)Keys.Right:
                        MoveRightCommand.Execute();
                        break;

                    // Jump (W key)
                    case (int)Keys.W:
                        if (GameObj.IsMenuVisible)
                        {
                            // Do nothing
                        }
                        else
                        {
                            JumpCommand.Execute();
                        }
                        break;

                    // Jump (Up Arrow)
                    case (int)Keys.Up:
                        if (GameObj.IsMenuVisible)
                        {
                            // Do nothing
                        }
                        else
                        {
                            JumpCommand.Execute();
                        }
                        break;

                    // Crouch (S key)
                    case (int)Keys.S:
                        if (GameObj.IsMenuVisible)
                        {
                            // Do nothing
                        }
                        else
                        {
                            CrouchCommand.Execute();
                        }
                        break;

                    // Crouch (Down Arrow)
                    case (int)Keys.Down:
                        if (GameObj.IsMenuVisible)
                        {
                            // Do nothing
                        }
                        else
                        {
                            CrouchCommand.Execute();
                        }
                        break;

                    // Dash/throw Fireball
                    case (int)Keys.Space:
                        if (GameObj.IsMenuVisible)
                        {
                            // Do nothing
                        }
                        else
                        {
                            // FireballCommand.Execute();
                        }
                        break;

                    case (int)Keys.OemQuestion:
                        QuestionBumpCommand.Execute();
                        break;

                    case (int)Keys.H:
                        HiddenBumpCommand.Execute();
                        break;

                    case (int)Keys.B:
                        BrickBumpCommand.Execute();
                        break;

                    // Game Exit
                    case (int)Keys.Q:
                        //ExitCommand.Execute();
                        GameObj.Exit();
                        break;

                    // Pause
                    case (int)Keys.P:
                        GameObj.IsMenuVisible = !GameObj.IsMenuVisible;
                        //Pause.Execute();
                        break;

                    // Mute
                    case (int)Keys.M:
                        //Mute.Execute();
                        break;

                    // Standard state
                    case (int)Keys.Y:
                        context.SetPowerUpState(new StandardMarioState());
                        break;

                    // Super state
                    case (int)Keys.U:
                        context.SetPowerUpState(new SuperMarioState());
                        break;

                    // Fire state
                    case (int)Keys.I:
                        context.SetPowerUpState(new FireMarioState());
                        break;

                    // Damage avatar
                    case (int)Keys.O:
                        context.TakeDamage();
                        break;

                    //Show Hitboxes
                    case (int)Keys.C:
                        context.ToggleHitbox();
                        break;

                    //Reset
                    case (int)Keys.R:
                        map.Reset();
                        break;
                }
            }
        }
    }
}
