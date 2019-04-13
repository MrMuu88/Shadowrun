using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Shadowrun.DataLoader.ViewModels {
	public abstract class ViewModelBase:INotifyPropertyChanged {
		public event PropertyChangedEventHandler PropertyChanged;
		
		#region fields and Properties #############################################################

		#endregion

		#region Methods ###########################################################################

		public virtual void RaisePropertyChanged ([CallerMemberName]string propertyName = null){
			PropertyChanged?.Invoke(this,new PropertyChangedEventArgs(propertyName));
		}

		#endregion

	}
}
