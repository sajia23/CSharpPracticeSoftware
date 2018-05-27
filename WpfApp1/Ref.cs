using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace WpfApp1
{
    public class Ref
    {
        private string path;
        private Reference parent;
        private string name;
        [XmlIgnore]
        public Reference Parent
        {
            set;
            get;
        }
        [XmlAttribute("Name")]
        public string Name
        {
            set;
            get;
        }
        [XmlAttribute("Path")]
        public string Path
        {
            set;
            get;
        }
    }
}
