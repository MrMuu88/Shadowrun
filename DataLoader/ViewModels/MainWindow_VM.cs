using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Shadowrun.DataAccess;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Shadowrun.DataLoader.ViewModels {
	public class MainWindow_VM : ViewModelBase {

		#region fields and Properties #############################################################

		public IDataService DataService {get;set;}

		public IMessenger Messenger{ get; set; }

		private ObservableCollection<Skill> _Skills;

		public ObservableCollection<Skill> Skills {
			get { return _Skills; }
			set {
				_Skills = value;
				RaisePropertyChanged();
			}
		}

		#region Commands --------------------------------------------

		public ICommand cmdSave{ get; }
		public ICommand cmdNewSkill{ get; }
		public ICommand cmdDeleteSkill{ get; }
		public ICommand cmdNewSpec{ get; }
		public ICommand cmdDeleteSpec{ get; }

		#endregion

		#endregion

		#region Methods ###########################################################################

		#region manage Skills ---------------------------------------

		private void CreateNewSkill() {
			var ns = new Skill(Attribute.Agility,SkillType.Active,true,"new Skill");
			Skills.Add(ns);
		}

		private void DeleteSkill(Skill skill) {
			if (skill == null) { return; }
			Skills.Remove(skill);
		}
		
		#endregion

		#region manage Specializations ------------------------------
		
		private void CreateNewSpecialization(Skill skill) {
			if (skill == null) { return; }
			var ns = new Specialization("New Spec");
			skill.Specializations.Add(ns);
			
		}

		private void DeleteSpecialization(Specialization spec) {
			if (spec == null) { return; }
			spec.Skill.Specializations.Remove(spec);
			
		}

		#endregion

		private void Save2DB() {
			
		}
		#endregion

		#region Ctors #############################################################################

		public MainWindow_VM(IDataService dataService,IMessenger messenger):this() {
			DataService = dataService;
			Messenger = messenger;

			#region set Commands ------------------------------------

			cmdSave = new RelayCommand(Save2DB);
			cmdNewSkill = new RelayCommand(CreateNewSkill);
			cmdDeleteSkill = new RelayCommand<Skill>(DeleteSkill);
			cmdNewSpec = new RelayCommand<Skill>(CreateNewSpecialization);
			cmdDeleteSpec = new RelayCommand<Specialization>(DeleteSpecialization);
			#endregion

			#region Load Data ---------------------------------------
			Skills = new ObservableCollection<Skill>(DataService.LoadAllSkills());
			#endregion
		}

		public MainWindow_VM() {
			Skills = new ObservableCollection<Skill>();
		}

		#endregion
	}
}
