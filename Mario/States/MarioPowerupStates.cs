using System;

namespace Mario.States
{
	public abstract class MarioPowerupState
	{
		public abstract void TakeDamage(MarioContext context);
		public abstract void GetMushroom(MarioContext context);
		public abstract void GetFireFlower(MarioContext context);

        public abstract void DieInPit(MarioContext context);

	}

    class StandardMarioState : MarioPowerupState
    {
        public override void GetFireFlower(MarioContext context)
        {
            context.SetPowerUpState(new FireMarioState());
        }

        public override void GetMushroom(MarioContext context)
        {
            context.SetPowerUpState(new SuperMarioState());
        }

        public override void TakeDamage(MarioContext context)
        {
            context.SetPowerUpState(new DeadMarioState());
        }
        public override void DieInPit(MarioContext context)
        {
            context.SetPowerUpState(new DeadMarioState());
        }
        public override string ToString()
        {
            return "StandardMario";
        }
    }
    class SuperMarioState : MarioPowerupState
    {
        public override void GetFireFlower(MarioContext context)
        {
            context.SetPowerUpState(new FireMarioState());
        }

        public override void GetMushroom(MarioContext context)
        {
            //does nothing
        }

        public override void TakeDamage(MarioContext context)
        {
            context.SetPowerUpState(new StandardMarioState());
        }
        public override void DieInPit(MarioContext context)
        {
            context.SetPowerUpState(new DeadMarioState());
        }
        public override string ToString()
        {
            return "SuperMario";
        }
    }

    class FireMarioState : MarioPowerupState
    {
        public override void GetFireFlower(MarioContext context)
        {
            //does nothing
        }

        public override void GetMushroom(MarioContext context)
        {
            //does nothing 
        }

        public override void TakeDamage(MarioContext context)
        {
            context.SetPowerUpState(new SuperMarioState());
        }
        public override void DieInPit(MarioContext context)
        {
            context.SetPowerUpState(new DeadMarioState());
        }
        public override string ToString()
        {
            return "FireMario";
        }
    }
    class DeadMarioState : MarioPowerupState
    {
        public override void GetFireFlower(MarioContext context)
        {
            //does nothing
        }

        public override void GetMushroom(MarioContext context)
        {
            //does nothing
        }

        public override void TakeDamage(MarioContext context)
        {
            //does nothing
        }
        public override void DieInPit(MarioContext context)
        {
            context.SetPowerUpState(new DeadMarioState());
        }
        public override string ToString()
        {
            return "DeadMario";
        }
    }

}

