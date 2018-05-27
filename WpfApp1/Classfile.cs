using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace WpfApp1
{
    public class Classfile : INotifyPropertyChanged
    {
        
        private Solution parent;
        private string path;
        private string name;
        public Classfile()
        {
            Reference = new ObservableCollection<WpfApp1.Reference>();
        }
        [XmlAttribute("Path")]
        public String Path
        {
            set;
            get;
        }
        [XmlIgnore]
        public Solution Parent
        {
            set;
            get;
        }
        [XmlIgnore]
        public MyTabItem Zaiti
        {
            set;
            get;
        }
        [XmlAttribute("Name")]
        public String Name
        {
            set { name = value; OnPropertyChanged("Name"); }
            get { return name; }
        }
        [XmlElementAttribute("Reference")]
        public ObservableCollection<Reference> Reference
        {
            set;
            get;
        }
        protected internal virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
