using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Shadowrun.DataAccess;
using Shadowrun.DataLoader.Messages;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Shadowrun.DataLoader.ViewModels {

    public class NavigationVM<T> : ViewModelBase,INavigationVM {
        #region Fields,Properties,Events ##############################################################

        public IMessenger Messenger { get; set; }
        public IDataService<T> DataService { get; set; }

        private ObservableCollection<LookupItem> items = new ObservableCollection<LookupItem>();

        public ObservableCollection<LookupItem> Items {
            get => items;
            set { items = value; RaisePropertyChanged(); }
        }

        private LookupItem selectedItem;
        public LookupItem SelectedItem {
            get => selectedItem;
            set { selectedItem = value;
                if (selectedItem != null) {
                    Messenger.Send(new SkillSelected(selectedItem.ID));
                }
            }
        }

        public ICommand cmdAdd { get; set; }
        #endregion

        #region Methods, Tasks, commands ##############################################################

        public void Load() {
            Items = new ObservableCollection<LookupItem>(DataService.LookupItems());
        }

        private void AddItem() {
            Items.Add(new LookupItem(-1, "new Item*"));
        }

        private void OnChanged(ListChanged<T> obj) {
            Load();
        }
        #endregion

        #region Ctors #################################################################################

        public NavigationVM() {
            cmdAdd = new RelayCommand(AddItem);
        }


        public NavigationVM(IDataService<T> dataService, IMessenger messenger) :this(){
            Messenger = messenger;
            DataService = dataService;
            Messenger.Register<ListChanged<T>>(this, OnChanged);
        }


        #endregion
    }//clss

}//ns
