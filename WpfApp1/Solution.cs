using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace WpfApp1
{
    public class Solution : INotifyPropertyChanged
    {
        
        private string name;
        private Solvedproblem parent;
        private string path;
        public Solution()
        {
            Classfile = new ObservableCollection<WpfApp1.Classfile>();
        }
        [XmlIgnore]
        public Solvedproblem Parent
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
        [XmlAttribute("Path")]
        public String Path
        {
            set { path = value; OnPropertyChanged("Path"); }
            get { return path; }
        }
        [XmlElementAttribute("Classfile")]
        public ObservableCollection<Classfile> Classfile
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
