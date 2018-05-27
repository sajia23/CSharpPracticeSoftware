using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace WpfApp1
{
    public class Solvedproblem : INotifyPropertyChanged
    {
        
        private string name;
        private Solvespace parent;
        private string tiku;
        
        private string path;

        public Solvedproblem()
        {
            Solution = new ObservableCollection<WpfApp1.Solution>();
        }
        [XmlIgnore]
        public Solvespace Parent
        {
            set;
            get;
        }
        [XmlAttribute("Name")]
        public string Name
        {
            set { name = value; OnPropertyChanged("Name"); }
            get { return name; }
        }
        [XmlAttribute("Tiku")]
        public string Tiku
        {
            set { tiku = value; OnPropertyChanged("Tiku"); }
            get { return tiku; }
        }
        [XmlAttribute("Path")]
        public string Path
        {
            set { path = value; OnPropertyChanged("Path"); }
            get { return path; }
        }
        [XmlElementAttribute("Solvelution")]
        public ObservableCollection<Solution> Solution
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
