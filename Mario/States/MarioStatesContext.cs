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
	public MarioActionState glidingState;
	MarioActionState ActionState;
	MarioPowerupState PowerupState;
	public Vector2 Velocity;
	public bool facingLeft;
	public bool isBallooned;
	public bool isCape;
	public bool firstJump;
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
	public Game1 Theatre;
	public int topDeathHeight;
	public SoundEffect jump { get; }
	public SoundEffect powerup { get; }
	public SoundEffect stomp { get; }
	public SoundEffect kick { get; }
	public SoundEffect coin { get; }
	public SoundEffect death { get; }
	public SoundEffect oneup { get; }
	public SoundEffect powerdown { get; }
	public SoundEffect fireball { get; }
	public SoundEffect capeglide { get; }
	public SoundEffect getPBalloon { get; }
	public SoundEffect getCapeFeather { get; }
	public bool dashing { get; set; }

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
		glidingState = new GlidingState(this);
		jumpHeight = 0;
		showHitbox = false;
		this.Theatre = theatre;
		dashing = false;
		isCape = false;
		firstJump = false;
		jump = theatre.Content.Load<SoundEffect>("SoundEffects/jump");
		powerup = theatre.Content.Load<SoundEffect>("SoundEffects/powerup");
		stomp = theatre.Content.Load<SoundEffect>("SoundEffects/stomp");
		coin = theatre.Content.Load<SoundEffect>("SoundEffects/coin");
		kick = theatre.Content.Load<SoundEffect>("SoundEffects/kick");
		death = theatre.Content.Load<SoundEffect>("SoundEffects/death");
		oneup = theatre.Content.Load<SoundEffect>("SoundEffects/1up");
		powerdown = theatre.Content.Load<SoundEffect>("SoundEffects/pipe");
		fireball = theatre.Content.Load<SoundEffect>("SoundEffects/fireball");
		capeglide = theatre.Content.Load<SoundEffect>("SoundEffects/smw_cape_rise");
		getPBalloon = theatre.Content.Load<SoundEffect>("SoundEffects/smw_balloon_get");
		getCapeFeather = theatre.Content.Load<SoundEffect>("SoundEffects/smw_feather_get");
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
		isBallooned = false;
		isCape = false;
	}
    public void GetMushroom()
    {
		PowerupState.GetMushroom(this);
    }
	public void GetCape()
    {
		PowerupState.GetCape(this);
		isCape = true;
    }

	public void GetPBalloon()
    {
		PowerupState.GetPBalloon(this);
		isBallooned = true;
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
