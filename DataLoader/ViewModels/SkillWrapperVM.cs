using GalaSoft.MvvmLight.Command;
using Shadowrun.Model;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;

namespace Shadowrun.DataLoader.ViewModels {
	public class SkillWrapperVM : ViewModelBase {
		#region fields and Properties #############################################################

		private Skill _Skill;
		public Skill Skill {
			get { return _Skill; }
			set {
				_Skill = value;
				RaisePropertyChanged(nameof(Name));
				RaisePropertyChanged(nameof(Group));
				RaisePropertyChanged(nameof(Description));
				RaisePropertyChanged(nameof(LinkedTo));
				RaisePropertyChanged(nameof(Type));
				RaisePropertyChanged(nameof(CanDefault));
			}
		}


		private bool _IsModified;
		public bool IsModified {
			get { return _IsModified; }
			set {
				_IsModified = value;
				RaisePropertyChanged();
				RaisePropertyChanged(nameof(DisplayName));
			}
		}

		public string DisplayName { get => $"{Name}{((IsModified) ? "*" : "")}"; }

		public string Name {
			get => Skill.Name;
			set { Skill.Name = value;
				IsModified = true;
				RaisePropertyChanged();
				RaisePropertyChanged(nameof(DisplayName));
			}
		}

		public SkillGroup Group {
			get => Skill.Group;
			set { Skill.Group = value;
				IsModified = true;
				RaisePropertyChanged();
			}
		}
		public string Description {
			get => Skill.Description;
			set {
				Skill.Description = value;
				IsModified = true;
				RaisePropertyChanged();
			}
		}
		public Attribute LinkedTo {
			get => Skill.LinkedTo;
			set { Skill.LinkedTo = value;
				IsModified = true;
				RaisePropertyChanged();
			}
		}
		public SkillType Type {
			get => Skill.Type;
			set { Skill.Type = value;
				IsModified = true;
				RaisePropertyChanged();
			}
		}
		public bool CanDefault {
			get => Skill.CanDefault;
			set { Skill.CanDefault = value;
				IsModified = true;
				RaisePropertyChanged();
			}
		}

		public ObservableCollection<Specialization> _Specializations;
		public ObservableCollection<Specialization> Specializations {
			get { return _Specializations; }
			set { _Specializations = value;
				IsModified = true;
				RaisePropertyChanged();
			}
		}

		public static string[] Attributes {
			get => System.Enum.GetNames(typeof(Attribute));
		}

		public static string[] SkillTypes {
			get => System.Enum.GetNames(typeof(SkillType));
		}
		public static ObservableCollection<SkillGroup> Groups;
		public ObservableCollection<SkillGroup> SkillGroups {
			get => Groups;
			set { Groups = value;
				RaisePropertyChanged();
			} 
		}


		public ICommand cmdAddSpec { get; }
		public ICommand cmdRemoveSpec { get; }
		#endregion

		#region Methods ###########################################################################

		private void AddSpecialization() {
			Debug.WriteLine(Specializations.Count);
			Specializations.Add(new Specialization("New Specialization"));
		}

		private void RemoveSpecialization(Specialization spec) {
			Debug.WriteLine($"{spec.Name}");
			//TODO:Remove Spec binding is incorrect
			//throw new NotImplementedException();
		}
		#endregion

		#region Ctors #############################################################################

		public SkillWrapperVM() {
			cmdAddSpec = new RelayCommand(AddSpecialization);
			cmdRemoveSpec = new RelayCommand<Specialization>(RemoveSpecialization);
		}


		public SkillWrapperVM(Skill s):this() {
			_Specializations = new ObservableCollection<Specialization>(s.Specializations);
			Skill = s;
		}

		#endregion
	}
}
