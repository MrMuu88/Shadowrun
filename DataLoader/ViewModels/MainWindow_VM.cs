using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Shadowrun.DataLoader.ViewModels {
	public class MainWindow_VM : INotifyPropertyChanged {
		public event PropertyChangedEventHandler PropertyChanged;

		#region fields and Properties #############################################################

		private ObservableCollection<Skill> _Skills;
		public ObservableCollection<Skill> Skills {
			get { return _Skills; }
			set {
				_Skills = value;
				RaisePropertyChanged(nameof(Skills));
			}
		}

		#endregion

		#region Methods ###########################################################################

		private void RaisePropertyChanged(string PropertyName) {
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
		}

		#endregion

		#region Ctors #############################################################################

		public MainWindow_VM() {
			try {
				App.DB.Database.OpenConnection();
				App.DB.Skills.Load();
			} catch (Exception ex) {
				MessageBox.Show($"An exception occured when Connecting to DB at First load:\n{ex.Message}", $"ERROR: {ex.GetType().Name}", MessageBoxButton.OK, MessageBoxImage.Error);
			} finally { 
				App.DB.Database.CloseConnection();
				Skills = new ObservableCollection<Skill>(App.DB.Skills.Local);
			}
		}

		#endregion
	}
}
