using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace WpfApp1
{
    public class Tiku : INotifyPropertyChanged
    {
        public Tiku()
        {
            problems = new List<Problem>();
        }
        private string name;
        [XmlAttribute("Name")]
        public String Name
        {
            get { return name; }
            set { name = value; OnPropertyChanged("Name"); }
        }
        private string path;
        [XmlAttribute("Path")]
        public string Path
        {
            get;
            set;
        }
        [XmlElementAttribute("Problem")]
        public List<Problem> problems;
        protected internal virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
