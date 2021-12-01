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
using Mario.Trackers;
using Microsoft.Xna.Framework.Audio;

namespace Mario.States
{
	public class BlockContext : ISprite
	{
		BlockState state;
		BlockState oldState;
		BlockSprite sprite;
		Vector2 Location;
		public bool deleted;
		private bool showHitbox;
		public bool ShowHitbox
		{
			get { return showHitbox; }
			set { showHitbox = value; }
		}
		public Vector2 Position
		{
			get { return Location; }
		}
		public bool isShell { get; set; }
		public bool spinning;
		public int timer;
		Game1 Theatre;
		List<BrokenBlockSprite> rubbleList;
		DynamicEntities entities;
		Boolean rubbleActive;
		public Rectangle Hitbox { get; set; }
		public SoundEffect powerup_appears { get; }
		public SoundEffect break_block { get; }
		public SoundEffect coin { get; }
		public SoundEffect bump { get; }

		public BlockContext(Game1 theatre,Vector2 location)
		{
			Theatre = theatre;
			state = new BrickBlockState();
			oldState = state;
			Location = location;
			sprite = new BrickBlockSprite(Theatre, Location, this);
			rubbleList = new List<BrokenBlockSprite>();
			rubbleActive = false;
			Hitbox = new Rectangle((int)Location.X, (int)Location.Y,16,16);
			showHitbox = false;
			entities = theatre.map.entities;
			powerup_appears = theatre.Content.Load<SoundEffect>("SoundEffects/powerup_appears");
			break_block = theatre.Content.Load<SoundEffect>("SoundEffects/break_block");
			coin = theatre.Content.Load<SoundEffect>("SoundEffects/coin");
			bump = theatre.Content.Load<SoundEffect>("SoundEffects/bump");
		}

		public Game1 GetGame()
		{
			return Theatre;
		}

		public bool Delete()
		{
			if (deleted) return true;
			else return false;
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
				case "UBrickBlock":
					sprite = new UBrickBlockSprite(Theatre, Location, this);
					break;
				case "HardBlock":
					sprite = new HardBlockSprite(Theatre, Location, this);
					break;
				case "UGroundBlock":
					sprite = new UGroundBlockSprite(Theatre, Location, this);
					break;
			}
		}

		public BlockState GetState()
		{
			return state;
		}

		public void Bump(MarioContext Mario, Game1 theatre, SuperMario superMario)
		{
			entities = theatre.map.entities;
			state.Bump(this, Mario, sprite, entities, superMario);
		}

		public void Update()
		{
			timer--;
			if (timer < 0)
			{
				timer = 350;
				spinning = false;
				this.Hitbox = new Rectangle((int)Location.X, (int)Location.Y, 16, 16);
			}
			if (!rubbleActive){
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
				deleted = true;
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
				Hitbox = new Rectangle(-1, -1, 0, 0);
			}
		}

        public void Collision(ISprite collider)
        {
			
			if (Hitbox.TouchTopOf(collider.Hitbox))
            {
				
				if (collider is SuperMario)
                {
					SuperMario mario = collider as SuperMario;

					if (mario.context.Velocity.Y > 0)
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
			//sprite.Spin();
		}
	}

	class RedMushroomQuestionBlockState : BlockState
	{
		public override void Bump(BlockContext context, MarioContext Mario, BlockSprite sprite, DynamicEntities dynamicEntities, SuperMario superMario)
		{
			context.powerup_appears.Play();
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
			context.powerup_appears.Play();
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
			context.powerup_appears.Play();
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

	class PBalloonQuestionBlockState : BlockState
	{
		public override void Bump(BlockContext context, MarioContext Mario, BlockSprite sprite, DynamicEntities dynamicEntities, SuperMario superMario)
		{
			context.powerup_appears.Play();
			context.SetState(new UsedBlockState());
			Vector2 balloonLocation = sprite.GetLocation();
			balloonLocation.Y -= 3;
			dynamicEntities.entityObjs.Add(new PBalloon(context.GetGame(), balloonLocation));
			this.Movement(sprite);

		}

		public override string ToString()
		{
			return "QuestionBlock";
		}
	}

	class CapeFeatherQuestionBlockState : BlockState
	{
		public override void Bump(BlockContext context, MarioContext Mario, BlockSprite sprite, DynamicEntities dynamicEntities, SuperMario superMario)
		{
			context.powerup_appears.Play();
			context.SetState(new UsedBlockState());
			Vector2 featherLocation = sprite.GetLocation();
			featherLocation.Y -= 3;
			dynamicEntities.entityObjs.Add(new CapeFeather(context.GetGame(), featherLocation, superMario));
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
			context.powerup_appears.Play();
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
			context.coin.Play();
			context.SetState(new UsedBlockState());
			Vector2 coinLocation = sprite.GetLocation();
			coinLocation.Y -= 3;
			dynamicEntities.entityObjs.Add(new BlockCoin(context.GetGame(), coinLocation));
			this.Movement(sprite);
			context.GetGame().tracker.AddCoinCommand();
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
			context.SetState(new BrickBlockState());
		}
		public override string ToString()
		{
			return "HiddenBlock";
		}
	}
	class UGroundBlockState : BlockState
	{
		public override void Bump(BlockContext context, MarioContext Mario, BlockSprite sprite, DynamicEntities dynamicEntities, SuperMario superMario)
		{
			//context.SetState(new BrickBlockState());
		}
		public override string ToString()
		{
			return "UGroundBlock";
		}
	}
	class HardBlockState : BlockState
	{
		public override void Bump(BlockContext context, MarioContext Mario, BlockSprite sprite, DynamicEntities dynamicEntities, SuperMario superMario)
		{
			//context.SetState(new BrickBlockState());
		}
		public override string ToString()
		{
			return "HardBlock";
		}
	}
	class UBrickBlockState : BlockState
	{
		public override void Bump(BlockContext context, MarioContext Mario, BlockSprite sprite, DynamicEntities dynamicEntities, SuperMario superMario)
		{
			context.SetState(new UBrickBlockState());
		}
		public override string ToString()
		{
			return "UBrickBlock";
		}
	}

	class BrickBlockState : BlockState
	{
		void Destroy(BlockContext context)
		{
			context.break_block.Play();
			context.ToggleRubble();
		}

		public override void Bump(BlockContext context, MarioContext Mario, BlockSprite sprite, DynamicEntities dynamicEntities, SuperMario superMario)
		{
			//context.bump.Play();
			//this.Movement(sprite);
			sprite.Spin();
			context.Hitbox = Rectangle.Empty;
			//context.
			//if (Mario.GetPowerUpState().ToString().Equals("SuperMario") || Mario.GetPowerUpState().ToString().Equals("FireMario"))
			//{
			//	Destroy(context);
			//}
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
			context.coin.Play();
			this.Movement(sprite);
			context.SetState(new UsedBlockState());
			Vector2 coinLocation = sprite.GetLocation();
			coinLocation.Y -= 3;
			dynamicEntities.entityObjs.Add(new BlockCoin(context.GetGame(), coinLocation));
			//this.Movement(sprite);
			context.GetGame().tracker.AddCoinCommand();
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
			if (coins < 10)
			{
				context.coin.Play();
				Vector2 coinLocation = sprite.GetLocation();
				coinLocation.Y -= 3;
				dynamicEntities.entityObjs.Add(new BlockCoin(context.GetGame(), coinLocation));
				coins++;
				if (coins == 10) context.SetState(new UsedBlockState());
				context.GetGame().tracker.AddCoinCommand();
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
