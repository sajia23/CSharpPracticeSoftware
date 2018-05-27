using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace WpfApp1
{
    
    public class Solvespace : INotifyPropertyChanged
    {
        
        private string name;
        
        private string path;
        
        public Solvespace()
        {
            Solvedproblem = new ObservableCollection<WpfApp1.Solvedproblem>();
        }
        [XmlAttribute("Name")]
        public string Name
        {
            set { name = value; OnPropertyChanged("Name"); }
            get { return name; }
        }
        [XmlAttribute("Path")]
        public string Path
        {
            set { path = value; OnPropertyChanged("Path"); }
            get { return path; }
        }
        [XmlElementAttribute("Problem")]
        public ObservableCollection<Solvedproblem> Solvedproblem
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
