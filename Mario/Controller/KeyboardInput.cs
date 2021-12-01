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
using Microsoft.Xna.Framework.Media;

namespace Mario
{
    class KeyboardInput : IController
    {
        private KeyboardState previousKeyboardState;
        int count;
        private ActivateIdle ActivateIdle { get; set; }
        public Game1 GameObj { get; set; }
        public ICommand MoveLeftCommand { get; set; }
        public ICommand MoveRightCommand { get; set; }
        public ICommand IdleCommand { get; set; }
        public ICommand JumpCommand { get; set; }
        public ICommand CrouchCommand { get; set; }
        public ICommand FireCommand { get; set; }
        public ICommand ExitCommand { get; set; }
        public ICommand QuestionBumpCommand { get; set; }
        public ICommand HiddenBumpCommand { get; set; }
        public ICommand BrickBumpCommand { get; set; }

        public ICommand Pause { get; set; }

        Level map;
        private MarioContext context;

        public KeyboardInput(Level map)
        {
            MoveLeftCommand = new MoveLeftCommand(map.Mario);
            MoveRightCommand = new MoveRightCommand(map.Mario);
            JumpCommand = new JumpCommand(map.Mario);
            CrouchCommand = new CrouchCommand(map.Mario);
            IdleCommand = new IdleCommand(map.Mario);
            ActivateIdle = new ActivateIdle(map.Mario);
            FireCommand = new FireCommand(map.Mario);
            Pause = new Pause(map.menu);
            context = map.Mario.context;
            this.map = map;
            count = 0;
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
            count++;
            if (previousKeyboardState.IsKeyDown(Keys.LeftShift) || previousKeyboardState.IsKeyDown(Keys.Space))
                context.dashing = true;
            else
                context.dashing = false;
            foreach (Input input in inputs) {

                switch (input.Key)
                {
                    //Leftward Movement (A key)
                    case (int)Keys.A:
                        if (GameObj.IsMenuVisible|| context.GetPowerUpState().ToString().Equals("DeadMario"))
                        {
                            // Do nothing
                        }
                        else
                        {
                            MoveLeftCommand.Execute();
                        }
                        break;

                    // Leftward Movement (Left Arrow)
                    case (int)Keys.Left:
                        if (GameObj.IsMenuVisible|| context.GetPowerUpState().ToString().Equals("DeadMario"))
                        {
                            // Do nothing
                        }
                        else
                        {
                            MoveLeftCommand.Execute();
                        }
                        break;

                    //Rightward Movement(D key)
                    case (int)Keys.D:
                        if (GameObj.IsMenuVisible|| context.GetPowerUpState().ToString().Equals("DeadMario"))
                        {
                            // Do nothing
                        }
                        else
                        {
                            MoveRightCommand.Execute();
                        }
                        break;

                    // Rightward Movement (Right Arrow)
                    case (int)Keys.Right:
                        if (GameObj.IsMenuVisible|| context.GetPowerUpState().ToString().Equals("DeadMario"))
                        {
                            // Do nothing
                        }
                        else
                        {
                            MoveRightCommand.Execute();
                        }
                        break;

                    // Jump (W key)
                    case (int)Keys.W:
                        if (GameObj.IsMenuVisible)
                        {
                            // Do nothing
                        }
                        else
                        {
                            if (!context.GetPowerUpState().ToString().Equals("DeadMario"))
                                JumpCommand.Execute();
                        }
                        break;

                    // Jump (Up Arrow)
                    case (int)Keys.Up:
                        if (GameObj.IsMenuVisible)
                        {
                        }
                        else
                        {
                            if (!context.GetPowerUpState().ToString().Equals("DeadMario"))
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
                            if (!context.GetPowerUpState().ToString().Equals("DeadMario"))
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
                            if (!context.GetPowerUpState().ToString().Equals("DeadMario"))
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
                            FireCommand.Execute();
                        }
                        break;

                    // Game Exit (Q key)
                    case (int)Keys.Q:
                        GameObj.Exit();
                        break;

                    // Game Exit (Escape)
                    case (int)Keys.Escape:
                        GameObj.Exit();
                        break;

                    // Pause
                    case (int)Keys.P:
                        if (count > 10)
                        {
                            if (map.menu.toString().Equals("Pause") || map.menu.toString().Equals("NoOverlay"))
                                GameObj.IsMenuVisible = !GameObj.IsMenuVisible;
                            Pause.Execute();
                            count = 0;
                        }
                        break;

                    // Mute
                    case (int)Keys.M:
                        if (count > 10)
                        {
                            MediaPlayer.IsMuted = !MediaPlayer.IsMuted;
                            count = 0;
                        }
                        break;

                    // Standard state
                    case (int)Keys.Y:
                        if (GameObj.IsMenuVisible)
                        {
                            // Do nothing
                        }
                        else
                        {
                            context.SetPowerUpState(new StandardMarioState());
                        }
                        break;

                    // Super state
                    case (int)Keys.U:
                        if (GameObj.IsMenuVisible)
                        {
                            // Do nothing
                        }
                        else
                        {
                            context.SetPowerUpState(new SuperMarioState());
                        }
                        break;

                    // Fire state
                    case (int)Keys.I:
                        if (GameObj.IsMenuVisible)
                        {
                            // Do nothing
                        }
                        else
                        {
                            context.SetPowerUpState(new FireMarioState());
                        }
                        break;

                    // Damage avatar
                    case (int)Keys.O:
                        if (count > 10)
                        {
                            if (GameObj.IsMenuVisible)
                            {
                                // Do nothing
                            }
                            else
                            {
                                context.TakeDamage();
                                count = 0;
                            }
                        }
                        break;

                    //Show Hitboxes
                    case (int)Keys.C:
                        if (GameObj.IsMenuVisible)
                        {
                            // Do nothing
                        }
                        else
                        {
                            context.ToggleHitbox();
                        }
                        break;

                    case (int)Keys.Enter:
                        if (count > 10)
                        {
                            if (map.Levelnum < 2)
                                map.Levelnum = map.Levelnum + 1;
                            else
                                map.Levelnum = 0;
                            map.Reset();
                            count = 0;
                        }
                        
                        break;
                    //Reset
                    case (int)Keys.R:
                        map.HardReset();
                        break;
                }
            }
        }
    }
}
