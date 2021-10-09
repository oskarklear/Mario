using System;
using Mario.Sprites.Mario;
using Mario.States;
using Mario.Sprites;
using Microsoft.Xna.Framework;

public class MarioContext
{
	public MarioActionState idleState;
	public MarioActionState runningState;
	public MarioActionState jumpingState;
	public MarioActionState fallingState;
	public MarioActionState crouchingState;
	MarioActionState ActionState;
	MarioPowerupState PowerupState;
	public bool facingLeft;

	public Vector2 Velocity;
	public bool jumped;
	//public bool isFalling;

	public MarioContext()
	{		
		PowerupState = new StandardMarioState();
		ActionState = new IdleState(this);
		facingLeft = true;

		idleState = new IdleState(this);
		runningState = new RunningState(this);
		jumpingState = new JumpingState(this);
		fallingState = new FallingState(this);
		crouchingState = new CrouchingState(this);
	}
	public MarioActionState GetActionState()
    {
		return ActionState;
    }
	public MarioPowerupState GetPowerUpState()
    {
		return PowerupState;
    }
	public void SetActionState(MarioActionState NewActionState)
    {
		ActionState = NewActionState;
		//if (GetActionState().ToString().Equals(""));
    }
	public void SetPowerUpState(MarioPowerupState NewPowerUpState)
    {
		PowerupState = NewPowerUpState;
    }

	public void PressUp()
    {
		ActionState.JumpingTransition();
    }
	public void PressDown()
    {
		ActionState.FallingTransition();
    }
	public void PressRight()
	{
		ActionState.FaceRightTransition();
	}
	public void PressLeft()
    {
		ActionState.FaceLeftTransition();
    }
	public void TakeDamage()
    {
		PowerupState.TakeDamage(this);
    }
	public void GetMushroom()
    {
		PowerupState.GetMushroom(this);
    }

	public void GetFireFlower()
    {
		PowerupState.GetFireFlower(this);
    }
}
