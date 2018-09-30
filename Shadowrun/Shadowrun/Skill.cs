using System;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Shadowrun{
    [XmlRoot(ElementName = "", Namespace = "")]
    public class Skill : INotifyPropertyChanged,ICloneable {
        public event PropertyChangedEventHandler PropertyChanged;

		private string _Name;
        private string _Group;
        private string _Description;
        private Attribute _LinkedTo;
        private SkillType _Type;
        private ObservableCollection<Specialization> _Specializations;
        private bool _CanDefault;

        [XmlAttribute]
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
            Type = SkillType.Active;
            LinkedTo = Attribute.Agility;
            Group = "None";
            Description = "";
            CanDefault = true;
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
                tw = new XmlTextWriter(sw);
                tw.Formatting = Formatting.Indented;
                tw.IndentChar = '\t';
                tw.Indentation = 1;
                serializer.Serialize(tw, this);
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
            Skill skill = new Skill();
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

        public object Clone() {
            Skill sk = new Skill();
            sk.Name = Name;
            sk.Type = Type;
            sk.LinkedTo = LinkedTo;
            sk.Group = Group;
            sk.Description=Description;
            sk.CanDefault = CanDefault;
            sk.Specializations = Specializations;
            return sk;
        }
    }
	
	public class Specialization: INotifyPropertyChanged{
		public event PropertyChangedEventHandler PropertyChanged;
		private string _Name;
        private Skill _Skill;

        [XmlText]
        public string Name {
            get { return _Name; }
            set { _Name = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Name")); }
        }

        [XmlAttribute]
        public Skill Skill {
            get { return _Skill; }
            set { _Skill = value;PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Skill")); }
        }

        public Specialization() {
            Name = "";
            Skill = null;
        }
		
		public Specialization(string name="",Skill skill=null){
            Name = name;
            Skill = skill;
		}
			
	}
	
	public enum Attribute{Body,Agility,Reaction,Strength,Willpower,Logic,Intuition,Charisma,Essence,Magic,Resonance,Edge}
	public enum SkillType{Active,Knowledge}
}