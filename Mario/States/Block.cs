using System;
using Mario.Sprites;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Mario;
using System.Collections.Generic;
using Mario.Sprites.Mario;

namespace Mario.States
{
	public class BlockContext : ISprite
	{
		BlockState state;
		BlockState oldState;
		BlockSprite sprite;
		Vector2 Location;
		Game1 Theatre;
		List<BrokenBlockSprite> rubbleList;
		
		Boolean rubbleActive;
		public Rectangle DestinationRectangle { get; set; }

		public BlockContext(Game1 theatre,Vector2 location)
		{
			Theatre = theatre;
			state = new BrickBlockState();
			oldState = state;
			Location = location;
			sprite = new BrickBlockSprite(Theatre, Location, this);
			Vector2 rubbleLocation1 = sprite.GetLocation();
			rubbleLocation1.X -= 10;
			Vector2 rubbleLocation2 = sprite.GetLocation();
			rubbleLocation2.X -= 5;
			Vector2 rubbleLocation3 = sprite.GetLocation();
			rubbleLocation3.X += 5;
			Vector2 rubbleLocation4 = sprite.GetLocation();
			rubbleLocation4.X += 10;
			rubbleList = new List<BrokenBlockSprite>();
			rubbleActive = false;
			DestinationRectangle = new Rectangle((int)Location.X, (int)Location.Y,10,10);
			
			

		}
		public Game1 GetGame()
		{
			return Theatre;
		}

		public void SetState(BlockState NewState)
		{
			oldState = state;
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
				case "GroundBlock":
					sprite = new GroundBlockSprite(Theatre, Location, this);
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
			System.Diagnostics.Debug.WriteLine("Bump");
		}

		public void Update()
		{
            if(!rubbleActive){
				sprite.Update();
			}
			foreach(BrokenBlockSprite rubble in rubbleList)
            {
				rubble.Update();
            }
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			if (!rubbleActive)
			{
				sprite.Draw(spriteBatch);
			}
			foreach(BrokenBlockSprite rubble in rubbleList)
            {
				rubble.Draw(spriteBatch);
            }

		}
		public void ToggleRubble()
		{
			if (!rubbleActive)
			{
				rubbleActive = true;
				Vector2 rubbleLocation1 = sprite.GetLocation();
				rubbleLocation1.X -= 10;
				Vector2 rubbleLocation2 = sprite.GetLocation();
				rubbleLocation2.X -= 5;
				Vector2 rubbleLocation3 = sprite.GetLocation();
				rubbleLocation3.X += 5;
				Vector2 rubbleLocation4 = sprite.GetLocation();
				rubbleLocation4.X += 10;
				rubbleList.Add(new BrokenBlockSprite(Theatre, rubbleLocation1, this));
				rubbleList.Add(new BrokenBlockSprite(Theatre, rubbleLocation2, this));
				rubbleList.Add(new BrokenBlockSprite(Theatre, rubbleLocation3, this));
				rubbleList.Add(new BrokenBlockSprite(Theatre, rubbleLocation4, this));
				foreach (BrokenBlockSprite rubble in rubbleList)
				{
					rubble.ToggleRubble();
				}
				System.Diagnostics.Debug.WriteLine("rubble");
			}

		}

        public void Collision(ISprite collider, int xOffset, int yOffset)
        {
			
			if (DestinationRectangle.TouchTopOf(collider.DestinationRectangle))
            {
				//System.Diagnostics.Debug.WriteLine("collision");
				if (collider is SuperMario)
                {
					//System.Diagnostics.Debug.WriteLine("collision with mario");
					SuperMario mario=collider as SuperMario;
					System.Diagnostics.Debug.WriteLine("collision with mario");
					//System.Diagnostics.Debug.WriteLine(this.ToString());
					System.Diagnostics.Debug.WriteLine(sprite.ToString());
					System.Diagnostics.Debug.WriteLine(mario.ToString());
					state.Bump(this, mario.context, sprite);
				}					
				
            }
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
			System.Diagnostics.Debug.WriteLine("Bump");
			context.SetState(new UsedBlockState());
			this.Movement(sprite);
			

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
			System.Diagnostics.Debug.WriteLine("Bump");
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
			System.Diagnostics.Debug.WriteLine("Bump");
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
			System.Diagnostics.Debug.WriteLine("Bump");
			//does nothing
		}
		public override string ToString()
		{
			return "UsedBlock";
		}
	}
	class GroundBlockState : BlockState
	{
		public override void Bump(BlockContext context, MarioContext Mario, BlockSprite sprite)
		{
			//does nothing
		}
		public override string ToString()
		{
			return "GroundBlock";
		}
	}
}
