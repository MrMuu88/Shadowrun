using GalaSoft.MvvmLight.Messaging;
using Shadowrun.DataAccess;
using Shadowrun.Model;

namespace Shadowrun.DataLoader.ViewModels {
	public class MainWindow_VM : ViewModelBase {

		#region fields and Properties #############################################################

		public IDataService<Skill> DataService {get;set;}

		public IMessenger Messenger{ get; set; }

		public ITabVM SkillsTabVM{ get; set; }

		#endregion

		#region Methods ###########################################################################
		#endregion

		#region Ctors #############################################################################

		public MainWindow_VM(IDataService<Skill> dataService,IMessenger messenger):this() {
			DataService = dataService;
			Messenger = messenger;

			SkillsTabVM = new Tab_VM<Skill>(DataService,Messenger);
            SkillsTabVM.NavVM = new NavigationVM<Skill>(DataService, Messenger);
            SkillsTabVM.DetailVM = new SkillDetailVM(DataService, Messenger);

            SkillsTabVM.Load();		
            		
		}

		public MainWindow_VM() {
		}

		#endregion
	}
}
