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

		#region Fields and Properties ###################################################################

		private string _Name;
        private string _Group;
        private string _Description;
        private Attribute _LinkedTo;
        private SkillType _Type;
        private ObservableCollection<Specialization> _Specializations;
        private bool _CanDefault;

        [XmlElement]
        public string Name {
			get { return _Name; }
			set { _Name = value; 
				  RaisePropertyChanged(nameof(Name)); } }
		[XmlElement]
		public string Group {
		get { return _Group; } 
		set { _Group = value;
			  RaisePropertyChanged(nameof(Group)); } }
		[XmlElement]
		public string Description { 
		get { return _Description; } 
		set { _Description = value;
			  RaisePropertyChanged(nameof(Description)); } }

        [XmlAttribute]
        public Attribute LinkedTo { 
		get { return _LinkedTo; } 
		set { _LinkedTo = value;
			  RaisePropertyChanged(nameof(Attribute)); } }
        [XmlAttribute]
        public SkillType Type { 
		get { return _Type; } 
		set { _Type = value;
			  RaisePropertyChanged(nameof(Type)); } }
        [XmlAttribute]
        public bool CanDefault { 
		get { return _CanDefault; } 
		set { _CanDefault = value;
			  RaisePropertyChanged(nameof(CanDefault)); } }

        [XmlArrayItem]
        public ObservableCollection<Specialization> Specializations { 
		get { return _Specializations; } 
		set { _Specializations = value;
			  RaisePropertyChanged(nameof(Specializations)); } }

		#endregion

		#region Methods #################################################################################
		private void RaisePropertyChanged(string PropertyName) {
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
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

		[XmlElement]
		public string Name {
			get { return _Name; }
			set { _Name = value; RaisePropertyChanged(nameof(Name)); }
		}

		[XmlElement]
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