using System;
using Mario.Sprites.Mario;
using Mario.States;
using Mario.Sprites;
using Microsoft.Xna.Framework;
using Mario;
using Microsoft.Xna.Framework.Audio;

public class MarioContext
{
	public MarioActionState idleState;
	public MarioActionState runningState;
	public MarioActionState jumpingState;
	public MarioActionState fallingState;
	public MarioActionState crouchingState;
	MarioActionState ActionState;
	MarioPowerupState PowerupState;
	public Mario.Game1 Theatre;
	public Vector2 Velocity;
	public bool facingLeft;
	public bool isTouchingLeft { get; set; }
	public bool isTouchingRight { get; set; }
	public bool isTouchingTop { get; set; }
	public bool isTouchingBottom { get; set; }
	public bool isFalling { get; set; }
	public bool jumped { get; set; }
	public float jumpHeight;
	bool showHitbox;
	public bool ShowHitbox
	{
		get { return showHitbox; }
		set { showHitbox = value; }
	}
	Game1 Theatre;
	public SoundEffect jump { get; }
	public SoundEffect powerup { get; }
	public SoundEffect stomp { get; }
	public SoundEffect coin { get; }
	public SoundEffect death { get; }
	public SoundEffect oneup { get; }

	public MarioContext(Game1 theatre)
	{
		PowerupState = new StandardMarioState();
		ActionState = new IdleState(this);
		facingLeft = true;
		idleState = new IdleState(this);
		runningState = new RunningState(this);
		jumpingState = new JumpingState(this);
		fallingState = new FallingState(this);
		crouchingState = new CrouchingState(this);
		jumpHeight = 0;
		showHitbox = false;
		this.Theatre = theatre;
		jump = theatre.Content.Load<SoundEffect>("SoundEffects/jump");
		powerup = theatre.Content.Load<SoundEffect>("SoundEffects/powerup");
		stomp = theatre.Content.Load<SoundEffect>("SoundEffects/stomp");
		coin = theatre.Content.Load<SoundEffect>("SoundEffects/coin");
		death = theatre.Content.Load<SoundEffect>("SoundEffects/death");
		oneup = theatre.Content.Load<SoundEffect>("SoundEffects/1up");

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

	public void ToggleHitbox()
    {
		showHitbox = !showHitbox;
    }

	public void DieInPit()
    {
		PowerupState.DieInPit(this);
    }
}
