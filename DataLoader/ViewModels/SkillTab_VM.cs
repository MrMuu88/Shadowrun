using GalaSoft.MvvmLight.Messaging;
using Shadowrun.DataAccess;
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

		private SkillWrapperVM _SelectedSkill;
		public SkillWrapperVM SelectedSkill {
			get => _SelectedSkill;
			set {
				_SelectedSkill = value;
				RaisePropertyChanged();
			}
		}

		public ICommand cmdNewSkill { get; }
		public ICommand cmdDeleteSkill { get; }
		public ICommand cmdNewSpec { get; }
		public ICommand cmdDeleteSpec { get; }
		#endregion

		#region Methods ###########################################################################

		#region manage Skills ---------------------------------------

		

		#endregion

		#region manage Specializations ------------------------------

		

		

		#endregion

		#endregion

		#region Ctors #############################################################################

		public SkillTab_VM() {}

		public SkillTab_VM(ISkillDataService dataService,IMessenger messenger) {
			DataService = dataService;
			Messenger = messenger;
			Skills = new ObservableCollection<SkillWrapperVM>();
			foreach (var skill in DataService.LoadAllSkills()) {
				Skills.Add(new SkillWrapperVM(skill));
			}
		}
		#endregion
	}
}
