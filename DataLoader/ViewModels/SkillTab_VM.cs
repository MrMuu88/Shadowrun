using GalaSoft.MvvmLight.Messaging;
using Shadowrun.DataAccess;

namespace Shadowrun.DataLoader.ViewModels {
    public class SkillTab_VM : ViewModelBase {
        #region fields and Properties #############################################################
        public ISkillDataService DataService { get; set; }
        public IMessenger Messenger { get; set; }

        public NavigationVM NavVM { get; set; }

        public SkillDetailVM SkillVM {get;set;}

        #endregion

        #region Methods ###########################################################################

        public void Load() {
            NavVM.Load();
		}
               
		#endregion

		#region Ctors #############################################################################

		public SkillTab_VM() {}

		public SkillTab_VM(ISkillDataService dataService,IMessenger messenger) {
			DataService = dataService;
			Messenger = messenger;

            NavVM = new NavigationVM(Messenger, DataService);
            SkillVM = new SkillDetailVM(Messenger,DataService);

            Load();
		}
            

        #endregion
    }
}
