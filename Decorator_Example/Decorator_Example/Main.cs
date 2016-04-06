using System;

namespace Decorator_Example
{
	public interface IWeapon {
		decimal price { get; }
	}

	public abstract class Weapon : IWeapon {
		public abstract decimal price { get; }
	}

	public class Dagger : Weapon {
		public override decimal price {
			get {
				return 1000;
			}
		}
	}

	public class Bow : Weapon {
		public override decimal price {
			get {
				return 2000;
			}
		}
	}

	public class Axe : Weapon {
		public override decimal price {
			get {
				return 3000;
			}
		}
	}

	public abstract class WeaponDecorator : Weapon {
		public IWeapon WeaponExtra;
		public override decimal price {
			get {
				return WeaponExtra.price;
			}
		}
		public WeaponDecorator (IWeapon extra) {
			this.WeaponExtra = extra;
		}
	}


	public class WithMagic : WeaponDecorator {
		public WithMagic (IWeapon weapon) : base(weapon) {}
		public override decimal price {
			get {
				return base.price + 2000;
			}
		}
	}
	 
	public class WithResistances : WeaponDecorator {
		public WithResistances (IWeapon weapon) : base(weapon) {}
		public override decimal price {
			get {
				return base.price + 3000;
			}
		}
	}



	class MainClass
	{
		public static void Main (string[] args)
		{
			Console.WriteLine ("Good Evening Sir/Madam,\nToday we have on offer:\n Dagger 1000\n Bow \t2000\n Axe \t3000\nYou can get all of these upgraded with:\n " +
				"Magic Damage \t  +2000\n Magic Resistance +3000\n");
			IWeapon axe = new Axe ();
			WithMagic magicAxe = new WithMagic (axe);
			WithResistances res_magicAxe = new WithResistances (magicAxe);
			Console.ReadLine ();
			Console.WriteLine ("Axe with both? That will be " + res_magicAxe.price + ", lol.");
		}
	}
}
