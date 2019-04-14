using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Shadowrun.Model {
	public class SkillGroup {
		#region fields and Properties #############################################################
		[Key]
		public int ID { get; set; }
		[MaxLength(50)]
		public string Name { get; set; }

		//Navigation Property
		public ICollection<Skill> Skills {get;set;}

		
		#endregion

		#region Methods ###########################################################################

		#endregion

		#region Ctors #############################################################################

		public SkillGroup() {
			Skills = new List<Skill>();
		}

		public SkillGroup(string groupname) {
			Name = groupname;
		}
		#endregion
	}
}
