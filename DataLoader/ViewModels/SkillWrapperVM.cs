using Shadowrun.Model;

namespace Shadowrun.DataLoader.ViewModels {
	public class SkillWrapperVM : ViewModelBase {
		#region fields and Properties #############################################################
		public Skill Skill { get; set; }
		public bool IsModified { get; private set; }
		public string Name { get => Skill.Name; set => Skill.Name = value; }

		public SkillGroup Group{
			get => Skill.Group;
			set { Skill.Group = value;
			      IsModified = true;}
		}
		public string Description {
			get => Skill.Description;
			set {Skill.Description = value;
				 IsModified = true;}
		}
		public Attribute LinkedTo {
			get => Skill.LinkedTo;
			set {Skill.LinkedTo = value;
				 IsModified = true;}
		}
		public SkillType Type {
			get => Skill.Type;
			set {Skill.Type = value;
				 IsModified = true;}
		}
		public bool CanDefault {
			get => Skill.CanDefault;
			set {Skill.CanDefault = value;
				 IsModified = true;}
		}
		#endregion

		#region Methods ###########################################################################

		#endregion

		#region Ctors #############################################################################

		public SkillWrapperVM() {
			Skill = Skill.Default;
		}

		public SkillWrapperVM(Skill s) {
			Skill = s;
		}
		#endregion
	}
}
