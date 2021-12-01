using System;

namespace Mario.States
{
	public abstract class MarioPowerupState
	{
		public abstract void TakeDamage(MarioContext context);
		public abstract void GetMushroom(MarioContext context);
		public abstract void GetFireFlower(MarioContext context);
        public abstract void DieInPit(MarioContext context);
        public abstract void GetCape(MarioContext context);
        public abstract void GetPBalloon(MarioContext context);
    }
       

    class PBalloonMarioState : MarioPowerupState
    {
        public override void GetPBalloon(MarioContext context)
        {
            context.getPBalloon.Play();
        }

        public override void GetFireFlower(MarioContext context)
        {
            
            context.powerup.Play();
            context.SetPowerUpState(new FireMarioState());
            context.isBallooned = false;
        }

        public override void GetMushroom(MarioContext context)
        {
            context.powerup.Play();
        }

        public override void TakeDamage(MarioContext context)
        {
            context.powerdown.Play();
            context.isBallooned = false;
            context.SetPowerUpState(new SuperMarioState());
        }

        public override void DieInPit(MarioContext context)
        {
            context.isBallooned = false;
            context.death.Play();
            context.Theatre.tracker.RemoveLifeCommand();
            context.SetPowerUpState(new DeadMarioState());
        }
        public override void GetCape(MarioContext context)
        {
            context.isBallooned = false;
            context.getCapeFeather.Play();
            context.SetPowerUpState(new CapeMarioState());
        }
        public override string ToString()
        {
            return "PBalloonMario";
        }
    }

    class StandardMarioState : MarioPowerupState
    {
        public override void GetPBalloon(MarioContext context)
        {
            context.isBallooned = true;
            context.getPBalloon.Play();
            context.SetPowerUpState(new PBalloonMarioState());
        }

        public override void GetFireFlower(MarioContext context)
        {
            context.powerup.Play();
            context.SetPowerUpState(new FireMarioState());
        }

        public override void GetMushroom(MarioContext context)
        {
            context.powerup.Play();
            context.SetPowerUpState(new SuperMarioState());
        }

        public override void TakeDamage(MarioContext context)
        {
            context.death.Play();
            context.SetPowerUpState(new DeadMarioState());
            context.Theatre.tracker.RemoveLifeCommand();
        }

        public override void DieInPit(MarioContext context)
        {
            context.death.Play();
            context.SetPowerUpState(new DeadMarioState());
            context.Theatre.tracker.RemoveLifeCommand();
        }

        public override void GetCape(MarioContext context)
        {
            context.getCapeFeather.Play();
            context.SetPowerUpState(new CapeMarioState());
        }

        public override string ToString()
        {
            return "StandardMario";
        }
    }

    class SuperMarioState : MarioPowerupState
    {
        public override void GetPBalloon(MarioContext context)
        {
            context.isBallooned = true;
            context.getPBalloon.Play();
            context.SetPowerUpState(new PBalloonMarioState());
        }
        public override void GetFireFlower(MarioContext context)
        {
            context.powerup.Play();
            context.SetPowerUpState(new FireMarioState());
        }

        public override void GetMushroom(MarioContext context)
        {
            context.powerup.Play();
        }

        public override void TakeDamage(MarioContext context)
        {
            context.powerdown.Play();
            context.SetPowerUpState(new StandardMarioState());
        }

        public override void DieInPit(MarioContext context)
        {
            context.death.Play();
            context.SetPowerUpState(new DeadMarioState());
            context.Theatre.tracker.RemoveLifeCommand();
        }

        public override void GetCape(MarioContext context)
        {
            context.getCapeFeather.Play();
            context.SetPowerUpState(new CapeMarioState());
        }

        public override string ToString()
        {
            return "SuperMario";
        }
    }

    class FireMarioState : MarioPowerupState
    {
        public override void GetPBalloon(MarioContext context)
        {
            context.isBallooned = true;
            context.getPBalloon.Play();
            context.SetPowerUpState(new PBalloonMarioState());
        }
        public override void GetFireFlower(MarioContext context)
        {
            context.powerup.Play();
        }

        public override void GetMushroom(MarioContext context)
        {
            context.powerup.Play();
        }

        public override void TakeDamage(MarioContext context)
        {
            context.powerdown.Play();
            context.SetPowerUpState(new SuperMarioState());
        }

        public override void DieInPit(MarioContext context)
        {
            context.death.Play();
            context.SetPowerUpState(new DeadMarioState());
            context.Theatre.tracker.RemoveLifeCommand();
        }

        public override void GetCape(MarioContext context)
        {
            context.getCapeFeather.Play();
            context.SetPowerUpState(new CapeMarioState());
        }

        public override string ToString()
        {
            return "FireMario";
        }
    }

    class DeadMarioState : MarioPowerupState
    {
        public override void GetPBalloon(MarioContext context)
        {
            //ur dead idiot
        }
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

        public override void GetCape(MarioContext context)
        {
            //do nothing
        }

        public override string ToString()
        {
            return "DeadMario";
        }
    }

    class CapeMarioState : MarioPowerupState
    {
        public override void GetFireFlower(MarioContext context)
        {
            context.powerup.Play();
            context.SetPowerUpState(new FireMarioState());
        }

        public override void GetMushroom(MarioContext context)
        {
            context.powerup.Play();
        }

        public override void TakeDamage(MarioContext context)
        {
            context.powerdown.Play();
            context.SetPowerUpState(new SuperMarioState());
        }

        public override void DieInPit(MarioContext context)
        {
            context.Theatre.tracker.RemoveLifeCommand();
            context.death.Play();
            context.SetPowerUpState(new DeadMarioState());
        }

        public override void GetCape(MarioContext context)
        {
            context.getCapeFeather.Play();
        }
        public override void GetPBalloon(MarioContext context)
        {
            context.getPBalloon.Play();
            context.SetPowerUpState(new PBalloonMarioState());
        }
        public override string ToString()
        {
            return "CapeMario";
        }
    }
}

