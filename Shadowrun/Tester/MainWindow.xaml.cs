using System.ComponentModel;
using System.Windows;
using Shadowrun;

namespace Tester {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window,INotifyPropertyChanged {
        private Skill _TestSkill;
        public Skill TestSkill { get { return _TestSkill; } set { _TestSkill = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TestSkill")); } }

        private string _xml;
        public string xml { get { return _xml; } set { _xml = value;PropertyChanged(this,new PropertyChangedEventArgs("xml")); } }
        public event PropertyChangedEventHandler PropertyChanged;

        public MainWindow() {
            InitializeComponent();
            xml = "";
            TestSkill = new Skill(Attribute.Agility,SkillType.Active,false,"Archery","None","how to use a bow");
            
            TestSkill.Specializations.Add(new Specialization("Bow"));
            TestSkill.Specializations.Add(new Specialization("Crossbow"));
            TestSkill.Specializations.Add(new Specialization("Non - Standard Ammunition"));
            TestSkill.Specializations.Add(new Specialization("Slingshot"));
        }

        private void btnSerialize_Click(object sender, RoutedEventArgs e) {
            xml = TestSkill.ToXML();
        }

        private void btnDeSerialize_Clicked(object sender, RoutedEventArgs e) {
            TestSkill =Skill.LoadFromXML(xml);
        }
    }
}
