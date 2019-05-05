using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Shadowrun.Model {

	//TODO Implement a skill group class wich has a navigation collection to te skills included

	public class Skill {

		#region Fields and Properties ###################################################################
		[Key]
		public int ID { get; set; }

		[MaxLength(50)]
		public string Name { get; set; }

		public SkillGroup Group { get; set; }

		public string Description { get; set; }
		
		public Attribute LinkedTo { get; set; }

		public SkillType Type { get; set; }

		public bool CanDefault { get; set; }

        public ICollection<Specialization> Specializations { get; set; } = new List<Specialization>();
		
		
		#endregion

		#region Methods #################################################################################


		#endregion

		#region Ctors ###################################################################################
			

		public Skill() {
			
		}
		
		#endregion

	}

	public class Specialization{

		#region Fields and Properties ###################################################################
		[Key]
		public int ID { get; set; }
		[MaxLength(50)]
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

		public Specialization(string name) {
			Name = name;
		}

		#endregion
	}

}