using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Shadowrun.DataAccess;
using Shadowrun.DataLoader.Messages;
using Shadowrun.Model;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace Shadowrun.DataLoader.ViewModels {
    public class SkillDetailVM : ViewModelBase {
        #region fields and Properties #############################################################

        #region fields --------------------------

        private Skill _Skill;
        private bool _IsModified = false;
        private string _Name;
        private SkillGroup _Group;
        private string _Description;
        private Attribute _LinkedTo;
        private SkillType _Type;
        private bool _CanDefault;
        public ObservableCollection<Specialization> _Specializations = new ObservableCollection<Specialization>();
        public static ObservableCollection<SkillGroup> _SkillGroups = new ObservableCollection<SkillGroup>();
        private IMessenger _Messenger;

        #endregion

        #region Properties ----------------------
        public IMessenger Messenger {
            get => _Messenger;
            set { _Messenger = value;
                Messenger.Register<SkillSelected>(this, OnSkillSelected);
            }
        }
        public IDataService<Skill> DataService { get; set; }

        private Skill Skill {
            get => _Skill;
            set {
                _Skill = value;
                if (_Skill != null) {
                    _Name = _Skill.Name;
                    _LinkedTo = _Skill.LinkedTo;
                    _Type = _Skill.Type;
                    _Group = _Skill.Group;
                    _CanDefault = _Skill.CanDefault;
                    _Description = _Skill.Description;
                    _Specializations = new ObservableCollection<Specialization>(_Skill.Specializations);
                }
                RaisePropertyChanged(nameof(Name));
                RaisePropertyChanged(nameof(LinkedTo));
                RaisePropertyChanged(nameof(Type));
                RaisePropertyChanged(nameof(Group));
                RaisePropertyChanged(nameof(CanDefault));
                RaisePropertyChanged(nameof(Description));
                RaisePropertyChanged(nameof(Specializations));
            }
        }

        public bool IsModified {
            get { return _IsModified; }
            set {
                _IsModified = value;
                RaisePropertyChanged();
            }
        }

        public string Name {
            get { return _Name; }
            set {
                _Name = value;
                IsModified = true;
                RaisePropertyChanged();
            }
        }

        public SkillGroup Group {
            get => _Group;
            set {
                _Group = value;
                IsModified = true;
                RaisePropertyChanged();
            }
        }

        public string Description {
            get => _Description;
            set {
                _Description = value;
                IsModified = true;
                RaisePropertyChanged();
            }
        }

        public Attribute LinkedTo {
            get => _LinkedTo;
            set {
                _LinkedTo = value;
                IsModified = true;
                RaisePropertyChanged();
            }
        }

        public SkillType Type {
            get => _Type;
            set {
                _Type = value;
                IsModified = true;
                RaisePropertyChanged();
            }
        }

        public bool CanDefault {
            get => _CanDefault;
            set {
                _CanDefault = value;
                IsModified = true;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<Specialization> Specializations {
            get { return _Specializations; }
            set {
                _Specializations = value;
                IsModified = true;
                RaisePropertyChanged();
            }
        }

        public static string[] Attributes {
            get => System.Enum.GetNames(typeof(Attribute));
        }
        public static string[] SkillTypes {
            get => System.Enum.GetNames(typeof(SkillType));
        }


        public ObservableCollection<SkillGroup> SkillGroups {
            get => _SkillGroups;
            set {
                _SkillGroups = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region Commands ------------------------
        public ICommand cmdAddSpec { get; }
        public ICommand cmdRemoveSpec { get; }
        public ICommand cmdSave { get; }
        public ICommand cmdDelete { get; }

        #endregion

        #endregion


        #region Methods ###########################################################################

        private void OnSkillSelected(SkillSelected e) {

            //SkillGroups = new ObservableCollection<SkillGroup>(DataService.LoadSkillGroups());

            if (IsModified) {
                var result = MessageBox.Show("Unsaved Changes", "save?", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
                if (result == MessageBoxResult.Yes) {
                    SaveSkill();
                }
            }
            if (e.SkillID == -1) {
                Skill = new Skill();
                IsModified = true;
            } else {
                Skill = DataService.LoadByID(e.SkillID);
                IsModified = false;
            }
        }

        private void AddSpecialization(string SpecName) {
            if (!string.IsNullOrWhiteSpace(SpecName)) {
                Specializations.Add(new Specialization(SpecName));
                IsModified = true;
            }
        }

        private void RemoveSpecialization(Specialization spec) {
            Specializations.Remove(spec);
            IsModified = true;
        }
        private void DeleteSkill() {
            DataService.Delete(_Skill);
            Messenger.Send(new SkillsChanged());
            Skill = null;
            IsModified = false;
        }

        private void SaveSkill() {
            _Skill.Name = Name;
            _Skill.LinkedTo = LinkedTo;
            _Skill.Type = Type;
            _Skill.Group = Group;
            _Skill.CanDefault = CanDefault;
            _Skill.Description = Description;
            _Skill.Specializations = Specializations;
            DataService.Save(_Skill);
            Messenger.Send(new SkillsChanged());
            IsModified = false;
        }

        #endregion

        #region Ctors #############################################################################

        private SkillDetailVM() {
            cmdAddSpec = new RelayCommand<string>(AddSpecialization);
            cmdRemoveSpec = new RelayCommand<Specialization>(RemoveSpecialization);
            cmdSave = new RelayCommand(SaveSkill);
            cmdDelete = new RelayCommand(DeleteSkill);
        }


        public SkillDetailVM(IMessenger messenger, IDataService<Skill> dataService) : this() {
            Messenger = messenger;
            DataService = dataService;
        }

        #endregion
    }
}
