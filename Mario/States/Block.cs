using System;
using Mario.Sprites;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Mario;
using System.Collections.Generic;
using Mario.Sprites.Mario;
using Mario.Sprites.Items;
using Mario.Entities;
using Mario.Map;

namespace Mario.States
{
	public class BlockContext : ISprite
	{
		BlockState state;
		BlockState oldState;
		BlockSprite sprite;
		Vector2 Location;
		private bool showHitbox;
		public bool ShowHitbox
		{
			get { return showHitbox; }
			set { showHitbox = value; }
		}
		Game1 Theatre;
		//Level level;
		List<BrokenBlockSprite> rubbleList;
		//List<ISprite> powerups;
		//String powerup;
		DynamicEntities entities;
		Boolean rubbleActive;
		public Rectangle Hitbox { get; set; }

		public BlockContext(Game1 theatre,Vector2 location)
		{
			Theatre = theatre;
			state = new BrickBlockState();
			oldState = state;
			Location = location;
			sprite = new BrickBlockSprite(Theatre, Location, this);
/*			Vector2 rubbleLocation1 = sprite.GetLocation();
			rubbleLocation1.X -= 10;
			Vector2 rubbleLocation2 = sprite.GetLocation();
			rubbleLocation2.X -= 5;
			Vector2 rubbleLocation3 = sprite.GetLocation();
			rubbleLocation3.X += 5;
			Vector2 rubbleLocation4 = sprite.GetLocation();
			rubbleLocation4.X += 10;*/
			rubbleList = new List<BrokenBlockSprite>();
			rubbleActive = false;
			Hitbox = new Rectangle((int)Location.X, (int)Location.Y,16,16);
			showHitbox = false;
			entities = theatre.entities;
			

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
		public void Bump(MarioContext Mario, Game1 theatre, SuperMario superMario)
		{
			entities = theatre.entities;
			state.Bump(this, Mario, sprite, entities, superMario);
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
				if (showHitbox)
				{
					Texture2D hitboxTextureW = new Texture2D(spriteBatch.GraphicsDevice, Hitbox.Width, 1);
					Texture2D hitboxTextureH = new Texture2D(spriteBatch.GraphicsDevice, 1, Hitbox.Height);
					Color[] dataW = new Color[Hitbox.Width];
					for (int i = 0; i < dataW.Length; i++) dataW[i] = Color.Blue;
					Color[] dataH = new Color[Hitbox.Height];
					for (int i = 0; i < dataH.Length; i++) dataH[i] = Color.Blue;
					hitboxTextureW.SetData(dataW);
					hitboxTextureH.SetData(dataH);
					spriteBatch.Draw(hitboxTextureW, new Vector2((int)Hitbox.X, (int)Hitbox.Y), Color.White);
					spriteBatch.Draw(hitboxTextureW, new Vector2((int)Hitbox.X, (int)Hitbox.Y + (int)Hitbox.Height), Color.White);
					spriteBatch.Draw(hitboxTextureH, new Vector2((int)Hitbox.X, (int)Hitbox.Y), Color.White);
					spriteBatch.Draw(hitboxTextureH, new Vector2((int)Hitbox.X + (int)Hitbox.Width, (int)Hitbox.Y), Color.White);
				}
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
				Hitbox = new Rectangle(-1, -1, 1, 1);
			}

		}

        public void Collision(ISprite collider)
        {
			
			if (Hitbox.TouchTopOf(collider.Hitbox))
            {
				
				if (collider is SuperMario)
                {
					
					SuperMario mario=collider as SuperMario;
					System.Diagnostics.Debug.WriteLine("collision with mario");
					
					System.Diagnostics.Debug.WriteLine(sprite.ToString());
					System.Diagnostics.Debug.WriteLine(mario.ToString());
					state.Bump(this, mario.context, sprite, entities, mario);
				}					
            }
        }
	}

	public abstract class BlockState
	{
		public abstract void Bump(BlockContext context, MarioContext Mario, BlockSprite sprite, DynamicEntities dynamicEntities, SuperMario superMario);
		protected void Movement(BlockSprite sprite)
		{
			sprite.setMovement(10);
		}
	}

	class RedMushroomQuestionBlockState : BlockState
	{
		public override void Bump(BlockContext context, MarioContext Mario, BlockSprite sprite, DynamicEntities dynamicEntities, SuperMario superMario)
		{
			System.Diagnostics.Debug.WriteLine("Bump");
			context.SetState(new UsedBlockState());

			Vector2 mushroomLocation = sprite.GetLocation();
			mushroomLocation.Y -= 3;
			dynamicEntities.entityObjs.Add(new RedMushroom(context.GetGame(), mushroomLocation, superMario));

			this.Movement(sprite);
		}
		public override string ToString()
		{
			return "QuestionBlock";
		}
	}

	class GreenMushroomQuestionBlockState : BlockState
	{
		public override void Bump(BlockContext context, MarioContext Mario, BlockSprite sprite, DynamicEntities dynamicEntities, SuperMario superMario)
		{
			System.Diagnostics.Debug.WriteLine("Bump");
			context.SetState(new UsedBlockState());

			Vector2 mushroomLocation = sprite.GetLocation();
			mushroomLocation.Y -= 3;
			dynamicEntities.entityObjs.Add(new GreenMushroom(context.GetGame(), mushroomLocation, superMario));

			this.Movement(sprite);
		}
		public override string ToString()
		{
			return "QuestionBlock";
		}
	}

	class FireFlowerQuestionBlockState : BlockState
	{
		public override void Bump(BlockContext context, MarioContext Mario, BlockSprite sprite, DynamicEntities dynamicEntities, SuperMario superMario)
		{
			System.Diagnostics.Debug.WriteLine("Bump");
			context.SetState(new UsedBlockState());

			Vector2 flowerLocation = sprite.GetLocation();
			flowerLocation.Y -= 3;
			dynamicEntities.entityObjs.Add(new FireFlower(context.GetGame(), flowerLocation));

			this.Movement(sprite);
		}
		public override string ToString()
		{
			return "QuestionBlock";
		}
	}

	class StarQuestionBlockState : BlockState
	{
		public override void Bump(BlockContext context, MarioContext Mario, BlockSprite sprite, DynamicEntities dynamicEntities, SuperMario superMario)
		{
			System.Diagnostics.Debug.WriteLine("Bump");
			context.SetState(new UsedBlockState());

			Vector2 starLocation = sprite.GetLocation();
			starLocation.Y -= 3;
			dynamicEntities.entityObjs.Add(new Star(context.GetGame(), starLocation, superMario));

			this.Movement(sprite);
		}
		public override string ToString()
		{
			return "QuestionBlock";
		}
	}

	class CoinQuestionBlockState : BlockState
	{
		public override void Bump(BlockContext context, MarioContext Mario, BlockSprite sprite, DynamicEntities dynamicEntities, SuperMario superMario)
		{
			System.Diagnostics.Debug.WriteLine("Bump");
			context.SetState(new UsedBlockState());

			Vector2 coinLocation = sprite.GetLocation();
			coinLocation.Y -= 3;
			dynamicEntities.entityObjs.Add(new BlockCoin(context.GetGame(), coinLocation));

			this.Movement(sprite);
		}
		public override string ToString()
		{
			return "QuestionBlock";
		}
	}

	class HiddenBlockState : BlockState
	{
		public override void Bump(BlockContext context, MarioContext Mario, BlockSprite sprite, DynamicEntities dynamicEntities, SuperMario superMario)
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
		public override void Bump(BlockContext context, MarioContext Mario, BlockSprite sprite, DynamicEntities dynamicEntities, SuperMario superMario)
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

	class OneCoinBrickBlockState : BlockState
	{
		public override void Bump(BlockContext context, MarioContext Mario, BlockSprite sprite, DynamicEntities dynamicEntities, SuperMario superMario)
		{
			System.Diagnostics.Debug.WriteLine("Bump");
			this.Movement(sprite);
			context.SetState(new UsedBlockState());

			Vector2 coinLocation = sprite.GetLocation();
			coinLocation.Y -= 3;
			dynamicEntities.entityObjs.Add(new BlockCoin(context.GetGame(), coinLocation));

			this.Movement(sprite);
		}
		public override string ToString()
		{
			return "BrickBlock";
		}
	}

	class TenCoinBrickBlockState : BlockState
	{
		int coins = 0;
		public override void Bump(BlockContext context, MarioContext Mario, BlockSprite sprite, DynamicEntities dynamicEntities, SuperMario superMario)
		{
			System.Diagnostics.Debug.WriteLine("Bump");

			if (coins <= 10)
			{
				Vector2 coinLocation = sprite.GetLocation();
				coinLocation.Y -= 3;
				dynamicEntities.entityObjs.Add(new BlockCoin(context.GetGame(), coinLocation));
				coins++;
			}
            else
            {
				context.SetState(new UsedBlockState());
			}

			this.Movement(sprite);
		}
		public override string ToString()
		{
			return "BrickBlock";
		}
	}

	class UsedBlockState : BlockState
	{
		public override void Bump(BlockContext context, MarioContext Mario, BlockSprite sprite, DynamicEntities dynamicEntities, SuperMario superMario)
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
		public override void Bump(BlockContext context, MarioContext Mario, BlockSprite sprite, DynamicEntities dynamicEntities, SuperMario superMario)
		{
			//does nothing
		}
		public override string ToString()
		{
			return "GroundBlock";
		}
	}
}
