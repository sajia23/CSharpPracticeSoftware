using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace WpfApp1
{
    public class Reference
    {
        private Classfile parent;
        private String name;
        public Reference()
        {
            References = new ObservableCollection<Ref>();
        }
        [XmlIgnore]
        public Classfile Parent
        {
            set;
            get;
        }
        [XmlAttribute("Name")]
        public String Name
        {
            set;
            get;
        }
        [XmlElementAttribute("Ref")]
        public ObservableCollection<Ref> References
        {
            set;
            get;
        }
    }
}
