using System;
using System.Collections.Generic;
using System.Text;
using Mario.States;
using Mario.Movement;

namespace Mario.States
{
    public abstract class MarioActionState : IMarioActionState
    {
        public MarioContext marioContext;
        public MarioPowerupState PowerUpState;
        public IMarioActionState PreviousActionState;
        public IMarioActionState CurrentActionState;
        public Kinematics kinematics;

        public override abstract string ToString();
        public abstract void Enter(IMarioActionState previousActionState);
        public abstract void Exit();
        public abstract void StandingTransition();
        public abstract void CrouchingTransition();
        public abstract void WalkingTransition();
        public abstract void RunningTransition();
        public abstract void JumpingTransition();
        public abstract void FallingTransition();
        public abstract void FaceLeftTransition();
        public abstract void FaceRightTransition();
        public abstract void CrouchingDiscontinueTransition();
        public abstract void FaceLeftDiscontinueTransition();
        public abstract void FaceRightDiscontinueTransition();
        public abstract void RunningDiscontinueTransition();
        public abstract void JumpingDiscontinueTransition();
    }
}

