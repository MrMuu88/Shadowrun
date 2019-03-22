using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Data;
using System.Xml;
using System.Xml.Serialization;
using Shadowrun;

namespace Tester {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    //[XmlRoot(ElementName ="ListOfSkills")]
    public partial class MainWindow : Window, INotifyPropertyChanged {
        private Skill _WorkingSkill;
        public Skill WorkingSkill { get { return _WorkingSkill; } set { _WorkingSkill = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("WorkingSkill")); } }

        private ObservableCollection<Skill> _Skills;
        public ObservableCollection<Skill> Skills {
            get { return _Skills; }
            set { _Skills = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Skills")); }
        }

        private string _xml;
        public string xml { get { return _xml; } set { _xml = value; PropertyChanged(this, new PropertyChangedEventArgs("xml")); } }
        public event PropertyChangedEventHandler PropertyChanged;

        public MainWindow() {
            InitializeComponent();
            xml = "";
            Skills = new ObservableCollection<Skill>();
            WorkingSkill = new Skill();
            WorkingSkill.Name = "Archery";
            WorkingSkill.Group = "None";
            WorkingSkill.LinkedTo = (int)Shadowrun.Attribute.Body;
            WorkingSkill.Description = "how to use a bow";
            WorkingSkill.Specializations.Add(new Specialization("Bow",WorkingSkill));
            WorkingSkill.Specializations.Add(new Specialization("CrossBow",WorkingSkill));
            WorkingSkill.Specializations.Add(new Specialization("Special Ammunition",WorkingSkill));


        }

        private void btnSerialize_Click(object sender, RoutedEventArgs e) {
            xml = ToXML();
        }

        private void btnDeSerialize_Clicked(object sender, RoutedEventArgs e) {
            Skills = new ObservableCollection<Skill>();
            foreach(Skill s in LoadFromXML(xml)) {
                Skills.Add(s);
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e) {
            Skill sk = (Skill)WorkingSkill.Clone();
            sk.Name = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(sk.Name.ToLower());
            sk.Description = sk.Description.Replace("\r\n", " ");
            Skills.Add(sk);
            WorkingSkill = new Skill();
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e) {
            if (lstSkills.SelectedIndex != -1) {
                Skills.RemoveAt(lstSkills.SelectedIndex);
            }
            WorkingSkill = new Skill();
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e) {
            if (lstSkills.SelectedItem != null) {
                WorkingSkill = (Skill)((Skill)lstSkills.SelectedItem).Clone();
            }
        }

        public string ToXML() {
            StringWriter sw = new StringWriter();
            XmlTextWriter tw = null;
            try {
                XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<Skill>), new XmlRootAttribute("Skills"));
                tw = new XmlTextWriter(sw);
                XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                ns.Add("", "");
                tw.Formatting = Formatting.Indented;
                tw.IndentChar = '\t';
                tw.Indentation = 1;
                serializer.Serialize(tw, Skills,ns);
            } catch (Exception ex) {
                Console.WriteLine("ERROR: " + ex.GetType().Name);
                Console.WriteLine(ex.Message);
            } finally {
                sw.Close();
                if (tw != null) {
                    tw.Close();
                }
            }
            return sw.ToString();
        }

        public static Skill[] LoadFromXML(string xml) {
            StringReader strReader = null;
            XmlSerializer serializer = null;
            XmlTextReader xmlReader = null;
            Skill[] skills= Array.Empty<Skill>();
            try {
                strReader = new StringReader(xml);
                serializer = new XmlSerializer(typeof(Skill[]),new XmlRootAttribute("Skills"));
                xmlReader = new XmlTextReader(strReader);
                skills = (Skill[])serializer.Deserialize(xmlReader);
            } catch (Exception ex) {
                Console.WriteLine("ERROR: " + ex.GetType().Name);
                Console.WriteLine(ex.Message);
            } finally {
                if (xmlReader != null) {
                    xmlReader.Close();
                }
                if (strReader != null) {
                    strReader.Close();
                }
            }
            return skills;
        }

		private void OnbtnSave2DB_Click(object sender, RoutedEventArgs e) {
		
		}
	}//clss

	class StringToSpecsConverter:IValueConverter{  
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture){
            List<string> ret = new List<string>();

        
            ObservableCollection<Specialization> specs = (ObservableCollection<Specialization>)value;
            foreach (Specialization s in specs) {
                ret.Add(s.Name);
            }
            return String.Join(",",ret.ToArray());  
        }  
  
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)  {
            string[] text = ((string)value).Split(',');
            ObservableCollection<Specialization> ret = new ObservableCollection<Specialization>();
            foreach(string s in text) {
                ret.Add(new Specialization(s.Trim()));
            }
            return ret;
        }   
    }//clss

}//ns
