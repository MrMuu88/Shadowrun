using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Shadowrun {

	//TODO Implement a skill group class wich has a navigation collection to te skills included

	public class Skill : INotifyPropertyChanged {
		public event PropertyChangedEventHandler PropertyChanged;
		#region Fields and Properties ###################################################################

		private string _Name;
		private string _Group;
		private string _Description;
		private Attribute _LinkedTo;
		private SkillType _Type;
		private ObservableCollection<Specialization> _Specializations;
		private bool _CanDefault;

		public int ID { get; set; }


		public string Name {
			get { return _Name; }
			set {
				_Name = value;
				RaisePropertyChanged(nameof(Name));
			}
		}

		public string Group {
			get { return _Group; }
			set {
				_Group = value;
				RaisePropertyChanged(nameof(Group));
			}
		}

		public string Description {
			get { return _Description; }
			set {
				_Description = value;
				RaisePropertyChanged(nameof(Description));
			}
		}

		public Attribute LinkedTo {
			get { return _LinkedTo; }
			set {
				_LinkedTo = value;
				RaisePropertyChanged(nameof(Attribute));
			}
		}

		public SkillType Type {
			get { return _Type; }
			set {
				_Type = value;
				RaisePropertyChanged(nameof(Type));
			}
		}

		public bool CanDefault {
			get { return _CanDefault; }
			set {
				_CanDefault = value;
				RaisePropertyChanged(nameof(CanDefault));
			}
		}

		public ObservableCollection<Specialization> Specializations {
			get { return _Specializations; }
			set {
				_Specializations = value;
				RaisePropertyChanged(nameof(Specializations));
			}
		}

		#endregion

		#region Methods #################################################################################
		
		private void Specializations_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e) {
			if (e.Action == NotifyCollectionChangedAction.Remove ||
			    e.Action == NotifyCollectionChangedAction.Replace ||
			    e.Action == NotifyCollectionChangedAction.Reset) {
				foreach (Specialization i in e.OldItems) {
					i.Skill = null;
				}
			}
			if (e.Action == NotifyCollectionChangedAction.Add) {
				foreach (Specialization i in e.NewItems) {
					i.Skill = this;
				}
			}
		}

		private void RaisePropertyChanged(string PropertyName) {
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
		}

		#endregion

		#region Ctors ###################################################################################

		public Skill() {
			Name = "";
			Type = SkillType.Active;
			LinkedTo = Attribute.Agility;
			Group = "None";
			Description = "";
			CanDefault = true;
			Specializations = new ObservableCollection<Specialization>();
			Specializations.CollectionChanged += Specializations_CollectionChanged;

		}


		public Skill(Attribute attribute, SkillType skilltype, bool candefault, string name = "", string group = "", string description = "") {
			Name = name;
			Type = skilltype;
			LinkedTo = attribute;
			Group = group;
			Description = description;
			CanDefault = candefault;
			Specializations = new ObservableCollection<Specialization>();
		}

		#endregion

	}

	public class Specialization : INotifyPropertyChanged {
		public event PropertyChangedEventHandler PropertyChanged;

		#region Fields and Properties ###################################################################

		private string _Name;
		private Skill _Skill;

		public int ID { get; set; }
		public string Name {
			get { return _Name; }
			set { _Name = value; RaisePropertyChanged(nameof(Name)); }
		}

		public Skill Skill {
			get { return _Skill; }
			set { _Skill = value; RaisePropertyChanged(nameof(Skill)); }
		}
		#endregion

		#region Methods #################################################################################

		private void RaisePropertyChanged(string PropertyName) {
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
		}

		#endregion

		#region Ctors####################################################################################

		public Specialization() {
			Name = "";
			Skill = null;
		}

		public Specialization(string name = "", Skill skill = null) {
			Name = name;
			Skill = skill;
		}

		#endregion
	}

}