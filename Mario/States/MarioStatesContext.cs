using System;
using Mario.Sprites.Mario;
using Mario.States;
using Mario.Sprites;
using Microsoft.Xna.Framework;

public class MarioContext
{
	MarioActionState ActionState;
	MarioPowerupState PowerupState;
	public Vector2 Velocity;


	public MarioContext()
	{
		ActionState = new IdleStateLeft();
		PowerupState = new StandardMarioState();
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
		ActionState.PressUp(this);
    }
	public void PressDown()
    {
		ActionState.PressDown(this);
    }
	public void PressRight()
	{
		ActionState.PressRight(this);
	}
	public void PressLeft()
    {
		ActionState.PressLeft(this);
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
