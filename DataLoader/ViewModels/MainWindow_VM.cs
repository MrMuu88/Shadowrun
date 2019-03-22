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
			App.DB.Skills.Add(ns);
		}

		private void DeleteSkill(Skill skill) {
			if (skill == null) { return; }
			Skills.Remove(skill);
			App.DB.Skills.Remove(skill);
			App.DB.Specializations.RemoveRange(skill.Specializations);
		}
		
		#endregion

		#region manage Specializations ------------------------------
		
		private void CreateNewSpecialization(Skill skill) {
			if (skill == null) { return; }
			var ns = new Specialization("New Spec");
			skill.Specializations.Add(ns);
			App.DB.Specializations.Add(ns);
		}

		private void DeleteSpecialization(Specialization spec) {
			if (spec == null) { return; }
			spec.Skill.Specializations.Remove(spec);
			App.DB.Specializations.Remove(spec);
		}

		#endregion

		private void RaisePropertyChanged(string PropertyName) {
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
		}

		private void Save2DB() {
			try {
				App.DB.Database.OpenConnection();
				App.DB.SaveChanges();
			} catch (Exception ex) {
				MessageBox.Show($"An exception occured when trying to save modifications to DB:\n{ex.Message}", $"ERROR: {ex.GetType().Name}", MessageBoxButton.OK, MessageBoxImage.Error);
			} finally {
				App.DB.Database.CloseConnection();
			}
		}
		#endregion

		#region Ctors #############################################################################

		public MainWindow_VM() {
			#region set Commands ------------------------------------

			cmdSave = new RelayCommand(Save2DB);
			cmdNewSkill = new RelayCommand(CreateNewSkill);
			cmdDeleteSkill = new RelayCommand<Skill>(DeleteSkill);
			cmdNewSpec = new RelayCommand<Skill>(CreateNewSpecialization);
			cmdDeleteSpec = new RelayCommand<Specialization>(DeleteSpecialization);
			#endregion

			#region Load Data from DB -------------------------------

			try {
				App.DB.Database.OpenConnection();
				App.DB.Skills.Load();
				App.DB.Specializations.Load();
			} catch (Exception ex) {
				MessageBox.Show($"An exception occured when Connecting to DB at First load:\n{ex.Message}", $"ERROR: {ex.GetType().Name}", MessageBoxButton.OK, MessageBoxImage.Error);
			} finally { 
				App.DB.Database.CloseConnection();
				Skills = new ObservableCollection<Skill>(App.DB.Skills.Local);
			}
			#endregion
		}


		#endregion
	}
}
