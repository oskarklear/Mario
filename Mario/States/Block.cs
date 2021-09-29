using System;
using Mario.Sprites;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Mario;

namespace Mario.States
{
	public class BlockContext : ISprite
	{
		BlockState state;
		BlockSprite sprite;
		Vector2 Location;
		Game1 Theatre;
		BrokenBlockSprite rubble1;
		BrokenBlockSprite rubble2;
		BrokenBlockSprite rubble3;
		BrokenBlockSprite rubble4;
		Boolean rubbleActive;

		public BlockContext(Game1 theatre,Vector2 location)
		{
			Theatre = theatre;
			state = new BrickBlockState();
			Location = location;
			sprite = new BrickBlockSprite(Theatre, Location, this);
			Vector2 rubbleLocation1 = sprite.GetLocation();
			rubbleLocation1.X -= 8;
			Vector2 rubbleLocation2 = sprite.GetLocation();
			rubbleLocation2.X -= 4;
			Vector2 rubbleLocation3 = sprite.GetLocation();
			rubbleLocation3.X += 4;
			Vector2 rubbleLocation4 = sprite.GetLocation();
			rubbleLocation4.X += 8;
			rubble1 = new BrokenBlockSprite(Theatre, rubbleLocation1, this);
			rubble2 = new BrokenBlockSprite(Theatre, rubbleLocation2, this);
			rubble3 = new BrokenBlockSprite(Theatre, rubbleLocation3, this);
			rubble4 = new BrokenBlockSprite(Theatre, rubbleLocation4, this);
			rubbleActive = false;
			

		}
		public Game1 GetGame()
		{
			return Theatre;
		}

		public void SetState(BlockState NewState)
		{
			state = NewState;
			switch (state.ToString())
			{
				case "BrickBlock":
					sprite = new BrickBlockSprite(Theatre, Location, this);
					break;
				case "UsedBlock":
					sprite = new UsedBlockSprite(Theatre, Location, this);
					break;
				case "QuestionBlock":
					sprite = new QuestionBlockSprite(Theatre, Location, this);
					break;
				case "HiddenBlock":
					sprite = new HiddenBlockSprite(Theatre, Location, this);
					break;
			}
		}
		public BlockState GetState()
		{
			return state;
		}
		public void Bump(MarioContext Mario)
		{
			state.Bump(this, Mario, sprite);
		}

		public void Update()
		{
            if(!rubbleActive){
				sprite.Update();
			}
			rubble1.Update();
			rubble2.Update();
			rubble3.Update();
			rubble4.Update();
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			if (!rubbleActive)
			{
				sprite.Draw(spriteBatch);
			}
			rubble1.Draw(spriteBatch);
			rubble2.Draw(spriteBatch);
			rubble3.Draw(spriteBatch);
			rubble4.Draw(spriteBatch);

		}
		public void ToggleRubble()
		{
			rubbleActive = true;
			rubble1.ToggleRubble();
			rubble2.ToggleRubble();
			rubble3.ToggleRubble();
			rubble4.ToggleRubble();

		}
	}
	public abstract class BlockState
	{
		public abstract void Bump(BlockContext context, MarioContext Mario, BlockSprite sprite);
		protected void Movement(BlockSprite sprite)
		{
			sprite.setMovement(10);
		}
	}
	class QuestionBlockState : BlockState
	{
		public override void Bump(BlockContext context, MarioContext Mario, BlockSprite sprite)
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
		public override void Bump(BlockContext context, MarioContext Mario, BlockSprite sprite)
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
		void Destroy(BlockContext context)
		{
			context.ToggleRubble();
			
		}
		public override void Bump(BlockContext context, MarioContext Mario, BlockSprite sprite)
		{
			this.Movement(sprite);
			if (Mario.GetPowerUpState().ToString().Equals("SuperMario") || Mario.GetPowerUpState().ToString().Equals("FireMario"))
			{
				Destroy(context);
			}
		}
		public override string ToString()
		{
			return "BrickBlock";
		}
	}
	class UsedBlockState : BlockState
	{
		public override void Bump(BlockContext context, MarioContext Mario, BlockSprite sprite)
		{
			//does nothing
		}
		public override string ToString()
		{
			return "UsedBlock";
		}
	}
}
