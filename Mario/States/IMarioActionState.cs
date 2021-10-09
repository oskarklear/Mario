﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Mario.States
{
    public interface IMarioActionState
    {
        IMarioActionState PreviousActionState { get; }

        void Enter(IMarioActionState previousActionState);
        void Exit();
        void StandingTransition();
        void CrouchingTransition();
        void WalkingTransition();
        void RunningTransition();
        void JumpingTransition();
        void FallingTransition();
        void FaceLeftTransition();
        void FaceRightTransition();
        void CrouchingDiscontinueTransition();
        //void FaceLeftDiscontinueTransition();
        //void FaceRightDiscontinueTransition();
        //void WalkingDiscontinueTransition();
        void RunningDiscontinueTransition();
        void JumpingDiscontinueTransition();

    }
}
