
using System.ComponentModel;
namespace Shadowrun.DataLoader.ViewModels {
	public class SkillWrapperVM : ViewModelBase {
		#region fields and Properties #############################################################
		public Skill Skill { get; set; }
		public string Name { get => Skill.Name; set => Skill.Name = value; }

		public string Group		  { get => Skill.Group; set => Skill.Group = value; }
		public string Description { get => Skill.Description; set => Skill.Description = value; }
		public Attribute LinkedTo { get => Skill.LinkedTo; set => Skill.LinkedTo = value; }
		public SkillType Type	  { get => Skill.Type; set => Skill.Type = value; }
		public bool CanDefault	  { get => Skill.CanDefault; set => Skill.CanDefault = value; }
		#endregion

		#region Methods ###########################################################################

		#endregion

		#region Ctors #############################################################################

		public SkillWrapperVM(Skill s) {
			Skill = s;
		}
		#endregion
	}
}
