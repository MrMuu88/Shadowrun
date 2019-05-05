using GalaSoft.MvvmLight.Messaging;
using Shadowrun.DataAccess;
using Shadowrun.Model;

namespace Shadowrun.DataLoader.ViewModels {
	public class MainWindow_VM : ViewModelBase {

		#region fields and Properties #############################################################

		public IDataService<Skill> DataService {get;set;}

		public IMessenger Messenger{ get; set; }

		public SkillTab_VM SkillsVM{ get; set; }

		#endregion

		#region Methods ###########################################################################
		#endregion

		#region Ctors #############################################################################

		public MainWindow_VM(IDataService<Skill> dataService,IMessenger messenger):this() {
			DataService = dataService;
			Messenger = messenger;

			SkillsVM = new SkillTab_VM(DataService,Messenger);

			#region set Commands ------------------------------------

			
			#endregion

			#region Load Data ---------------------------------------
			
			#endregion
		}

		public MainWindow_VM() {
		}

		#endregion
	}
}
