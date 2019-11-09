using GalaSoft.MvvmLight.Messaging;
using Shadowrun.DataAccess;
using Shadowrun.Model;

namespace Shadowrun.DataLoader.ViewModels {
    public class Tab_VM<T> : ViewModelBase,ITabVM {
        #region fields and Properties #############################################################
        public IDataService<T> DataService { get; set; }
        public IMessenger Messenger { get; set; }

        public INavigationVM NavVM { get; set; }

        public IDetailVM DetailVM {get;set;}

        #endregion

        #region Methods ###########################################################################

        public void Load() {
            NavVM.Load();
		}
               
		#endregion

		#region Ctors #############################################################################

		public Tab_VM() {}

		public Tab_VM(IDataService<T> dataService,IMessenger messenger) {
			DataService = dataService;
			Messenger = messenger;            
		}
            

        #endregion
    }
}
