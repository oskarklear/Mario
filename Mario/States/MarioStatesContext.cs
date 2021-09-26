using System;
using Mario.States;

public class MarioContext
{
	MarioActionState ActionState;
	MarioPowerupState PowerupState;
	public MarioContext()
	{
		ActionState = new IdleState();
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
	public void SetActionState(MarioActionStates NewActionState)
    {
		ActionState = NewActionState;
    }
	public void SetPowerUpState(MarioPowerupState NewPowerUpState)
    {
		PowerupState = NewPowerUpState;
    }

	public void Handle()
    {
		ActionState.Handle();
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
