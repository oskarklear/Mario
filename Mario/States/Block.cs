using System;

public class BlockContext
{
	BlockState state;
	public BlockContext()
	{
		state = new BrickBlockState();
	}
	public void SetState(BlockState NewState)
    {
		state = NewState;
    }
	public BlockState GetState()
    {
		return state;
    }
	public void Bump(MarioContext Mario)
    {
		state.Bump(this, Mario);
    }
}
public abstract class BlockState
{
	public abstract void Bump(BlockContext context, MarioContext Mario);
	protected void Movement()
    {
		//TODO
    }
}
class QuestionBlockState : BlockState
{
	public override void Bump(BlockContext context, MarioContext Mario)
    {
		this.Movement();
		context.SetState(new UsedBlockState());

    }	
}
class HiddenBlockState : BlockState
{
	public override void Bump(BlockContext context, MarioContext Mario)
	{
		context.SetState(new BrickBlockState());
	}
}

class BrickBlockState : BlockState
{
	void Destroy()
    {
		//TODO
    }
	public override void Bump(BlockContext context, MarioContext Mario)
	{
		this.Movement();
		if(Mario.GetPowerUpState().ToString().Equals("SuperMario")|| Mario.GetPowerUpState().ToString().Equals("FireMario"))
        {
			Destroy();
        }
	}
}
class UsedBlockState : BlockState
{
	public override void Bump(BlockContext context, MarioContext Mario)
	{
		//does nothing
	}
}
