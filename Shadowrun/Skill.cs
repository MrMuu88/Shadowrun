using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Collections.Generic;

namespace Shadowrun {

	//TODO Implement a skill group class wich has a navigation collection to te skills included

	public class Skill {

		#region Fields and Properties ###################################################################

		public int ID { get; set; }


		public string Name { get; set; }

		public string Group { get; set; }

		public string Description { get; set; }

		public Attribute LinkedTo { get; set; }

		public SkillType Type { get; set; }

		public bool CanDefault { get; set; }

		public ICollection<Specialization> Specializations { get; set; }
		
		
		#endregion

		#region Methods #################################################################################


		#endregion

		#region Ctors ###################################################################################

		public static readonly Skill Default = new Skill() {
			Name = "",
			Type = SkillType.Active,
			LinkedTo = Attribute.Agility,
			Group = "None",
			Description = "",
			CanDefault = true,
			Specializations = new List<Specialization>()
		};

		public Skill() {
			
		}
		
		#endregion

	}

	public class Specialization{

		#region Fields and Properties ###################################################################

		public int ID { get; set; }
		public string Name { get; set; }
		public Skill Skill { get; set; }

		public static readonly Specialization Default = new Specialization() {
			Name = "Default Spec",
			Skill = null
		};
		#endregion

		#region Methods #################################################################################


		#endregion

		#region Ctors####################################################################################

		public Specialization() {}

		#endregion
	}

}