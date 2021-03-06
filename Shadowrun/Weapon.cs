﻿using System.Collections.Generic;

namespace Shadowrun.Model {
	public abstract class Weapon : Gear {
		public int ID{ get; set; }
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
		public ICollection<FiringMode> FiringModes { get; set; }
		public int RecoilCompensation { get; set; }
		public int Ammunition { get; set; }
		public ReloadingMethod ReloadingMethod { get; set; }
	}

	public class Explosive : Weapon {
		//TODO this has to be read throught to understand
		public string Blast { get; set; }
	}



}
