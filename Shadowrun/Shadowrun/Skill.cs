using System;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Shadowrun{
    public class Skill : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;

        private int _Rating;
        private string _Name;
        private string _Group;
        private string _Description;
        private Attribute _LinkedTo;
        private SkillType _Type;
        private ObservableCollection<Specialization> _Specializations;
        private bool _CanDefault;

        [XmlAttribute]
        public int Rating { get { return _Rating; } set { _Rating = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Rating")); } }
        public string Name { get { return _Name; } set { _Name = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Name")); } }
        public string Group { get { return _Group; } set { _Group = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Group")); } }
        public string Description { get { return _Description; } set { _Description = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Description")); } }

        [XmlAttribute]
        public Attribute LinkedTo { get { return _LinkedTo; } set { _LinkedTo = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Attribute")); } }
        [XmlAttribute]
        public SkillType Type { get { return _Type; } set { _Type = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Type")); } }
        [XmlArrayItem(ElementName = "Spec")]
        public ObservableCollection<Specialization> Specializations { get { return _Specializations; } set { _Specializations = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Specializations")); } }
        [XmlAttribute]
        public bool CanDefault { get { return _CanDefault; } set { _CanDefault = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CanDefault")); } }

        public Skill() {
            Name = "";
            Description = "";
            Group = "";
            CanDefault = false;
            LinkedTo = Attribute.Agility;
            Specializations = new ObservableCollection<Specialization>();
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

        public string ToXML() {
            StringWriter sw = new StringWriter();
            XmlTextWriter tw = null;
            try {
                XmlSerializer serializer = new XmlSerializer(typeof(Skill));
                XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                ns.Add("", "");
                tw = new XmlTextWriter(sw);
                tw.Formatting = Formatting.Indented;
                tw.IndentChar = '\t';
                tw.Indentation = 1;
                serializer.Serialize(tw, this, ns);
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

        public static Skill LoadFromXML(string xml) {
            StringReader strReader = null;
            XmlSerializer serializer = null;
            XmlTextReader xmlReader = null;
            Skill skill = null;
            try {
                strReader = new StringReader(xml);
                serializer = new XmlSerializer(typeof(Skill));
                xmlReader = new XmlTextReader(strReader);
                skill = (Skill)serializer.Deserialize(xmlReader);
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
            return skill;
        }
    }
	
	public class Specialization: INotifyPropertyChanged{
		public event PropertyChangedEventHandler PropertyChanged;
		private string _Name;
        private int _Rating;

        [XmlAttribute]
        public int Rating {get { return _Rating; }set { _Rating = value;PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Rating")); }}

        [XmlText]
		public string Name{
			get{return _Name;}
			set{_Name = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Name")); }
        }
		
		

		public Specialization(){
			Name = "";
		}
		
		public Specialization(string name=""){
			Name=name;
		}
			
	}
	
	public enum Attribute{Body,Agility,Reaction,Strength,Willpower,Logic,Intuition,Charisma,Essence,Magic,Resonance,Edge}
	public enum SkillType{Active,Knowledge}
}