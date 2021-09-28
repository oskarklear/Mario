using System;
using Mario.Sprites;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Mario;

public class BlockContext : ISprite
{
	BlockState state;
	BlockSprite sprite;
	Vector2 Location;
	Game1 Theatre;

	public BlockContext(Game1 theatre)
	{
		Theatre = theatre;
		state = new BrickBlockState();
		sprite = new BrickBlockSprite(Theatre,Location);
	}
	public void SetState(BlockState NewState)
    {
		state = NewState;
        switch (state.ToString())
        {
			case "BrickBlock":
				sprite = new BrickBlockSprite(Theatre, Location);
				break;
			case "UsedBlock":
				sprite = new UsedBlockSprite(Theatre, Location);
				break;
			case "QuestionBlock":
				sprite = new QuestionBlockSprite(Theatre, Location);
				break;
			case "HiddenBlock":
				sprite = new HiddenBlockSprite(Theatre, Location);
				break;
		}
    }
	public BlockState GetState()
    {
		return state;
    }
	public void Bump(MarioContext Mario)
    {
		state.Bump(this,Mario,sprite);
    }

    public void Update()
    {
        sprite.Update();
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        sprite.Draw(spriteBatch);
    }
}
public abstract class BlockState
{
	public abstract void Bump(BlockContext context,MarioContext Mario,BlockSprite sprite);
	protected void Movement(BlockSprite sprite)
    {
		sprite.setMovement(10);
    }
}
class QuestionBlockState : BlockState
{
	public override void Bump(BlockContext context, MarioContext Mario,BlockSprite sprite)
    {
		this.Movement(sprite);
		context.SetState(new UsedBlockState());

    }
    public override string ToString()
    {
        return "QuestionBlock";
    }
}
class HiddenBlockState : BlockState
{
	public override void Bump(BlockContext context, MarioContext Mario,BlockSprite sprite)
	{
		context.SetState(new BrickBlockState());
	}
	public override string ToString()
	{
		return "HiddenBlock";
	}
}

class BrickBlockState : BlockState
{
	void Destroy()
    {
		//TODO
    }
	public override void Bump(BlockContext context, MarioContext Mario,BlockSprite sprite)
	{
		this.Movement(sprite);
		if(Mario.GetPowerUpState().ToString().Equals("SuperMario")|| Mario.GetPowerUpState().ToString().Equals("FireMario"))
        {
			Destroy();
        }
	}
	public override string ToString()
	{
		return "BrickBlock";
	}
}
class UsedBlockState : BlockState
{
	public override void Bump(BlockContext context, MarioContext Mario,BlockSprite sprite)
	{
		//does nothing
	}
	public override string ToString()
	{
		return "UsedBlock";
	}
}
