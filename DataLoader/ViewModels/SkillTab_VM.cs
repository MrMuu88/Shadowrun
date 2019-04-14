using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Shadowrun.DataAccess;
using Shadowrun.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace Shadowrun.DataLoader.ViewModels {
	public class SkillTab_VM:ViewModelBase {
		#region fields and Properties #############################################################
		public ISkillDataService DataService { get; set; }
		public IMessenger Messenger { get; set; }

		private ObservableCollection<SkillWrapperVM> _Skills;
		public ObservableCollection<SkillWrapperVM> Skills {
			get { return _Skills; }
			set {
				_Skills = value;
				RaisePropertyChanged();
			}
		}


		private ObservableCollection<SkillGroup> _Groups;
		public ObservableCollection<SkillGroup> Groups {
			get { return _Groups; }
			set {
				_Groups = value;
				RaisePropertyChanged();
			}
		}

		public ICommand cmdNewSkill { get; }
		public ICommand cmdDeleteSkill { get; }
		public ICommand cmdNewSpec { get; }
		public ICommand cmdDeleteSpec { get; }
		public ICommand cmdSaveChanges { get; }
		#endregion

		#region Methods ###########################################################################

		public void Load() {
			Skills = new ObservableCollection<SkillWrapperVM>();
			foreach (var skill in DataService.LoadAllSkills()) {
				Skills.Add(new SkillWrapperVM(skill));
			}
			//as the underling pfield is static, this will be set on all Skill wrappers
			SkillWrapperVM.Groups = new ObservableCollection<SkillGroup>(DataService.LoadAllSkillGroups());
		}


		private void NewSkill() {
			var ns = new Skill() {
				Name = "new Skill",
				Type = SkillType.Active,
				LinkedTo = Model.Attribute.Agility,
				Group = null,
				Description = "created with Default values",
				CanDefault = true,
				Specializations = new List<Specialization>()
			};
			Skills.Add(new SkillWrapperVM(ns));
		}

		private void DeleteSkill(SkillWrapperVM skill) {
		}

		private void NewSpec(Skill skill) {
			
		}

		private void DeleteSpec(Specialization spec) {
			
		}
				
		private void SaveChanges() {
			var Modified = Skills.Where(sw => sw.IsModified == true).Select(sw => sw.Skill).ToList();
			DataService.SaveSkills(Modified);
		}

		#endregion

		#region Ctors #############################################################################

		public SkillTab_VM() {}

		public SkillTab_VM(ISkillDataService dataService,IMessenger messenger) {
			DataService = dataService;
			Messenger = messenger;

			cmdNewSkill = new RelayCommand(NewSkill);
			cmdDeleteSkill = new RelayCommand<SkillWrapperVM>(DeleteSkill);
			cmdNewSpec = new RelayCommand<Skill>(NewSpec);
			cmdDeleteSpec = new RelayCommand<Specialization>(DeleteSpec);
			cmdSaveChanges = new RelayCommand(SaveChanges);

			Load();
		}

		#endregion
	}
}
