using System.Collections.Generic;

namespace Shadowrun {
	public abstract class Weapon : Gear {
		public int Accuracy { get; set; }
		public int Damage { get; set; }
		public DamageType DamageType { get; set; }
		public int ArmorPenetration { get; set; }
	}

	public class Meele : Weapon {
		public int Reach { get; set; }
	}

	public class Projectile : Weapon { }

	public class FireArm : Weapon {
		public List<FiringMode> FiringModes { get; set; }
		public int RecoilCompensation { get; set; }
		public int Ammunition { get; set; }
		public ReloadingMethod ReloadingMethod { get; set; }
	}

	public class Explosive : Weapon {
		//TODO this has to be read throught to understand
		//adding some commits t o test git repo
		public string Blast { get; set; }
	}

	public enum DamageType { Physical, Stun }
	public enum FiringMode { SS, SA, BF, LB, FA }

	public enum ReloadingMethod { Clip, Belt, BreakAction, Cylinder, Drum, Magazie, Muzzle }
	
}
