using GalaSoft.MvvmLight.Command;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

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

		public ICommand cmdNewSkill{ get; }
		public ICommand cmdDeleteSkill{ get; }
		public ICommand cmdNewSpec{ get; }
		public ICommand cmdDeleteSpec{ get; }
		#endregion

		#region Methods ###########################################################################

		#region manage Skills ---------------------------------------

		private void CreateNewSkill() {
			Skills.Add(new Skill());
		}

		private void DeleteSkill(Skill skill) {
			if (skill == null) { return; }
			Skills.Remove(skill);
		}
		
		#endregion

		#region manage Specializations ------------------------------
		
		private void CreateNewSpecialization(Skill skill) {
			if (skill == null) { return; }
			skill.Specializations.Add(new Specialization());
		}

		private void DeleteSpecialization(Specialization spec) {
		//TODO this Needs Multi binding 
			if (spec == null) { return; }

		}

		#endregion

		private void RaisePropertyChanged(string PropertyName) {
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
		}

		#endregion

		#region Ctors #############################################################################

		public MainWindow_VM() {
			cmdNewSkill = new RelayCommand(CreateNewSkill);
			cmdDeleteSkill = new RelayCommand<Skill>(DeleteSkill);
			cmdNewSpec = new RelayCommand<Skill>(CreateNewSpecialization);

			//TODO This needs Multibinding
			cmdDeleteSpec = new RelayCommand<Specialization>(DeleteSpecialization);
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
