using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace WpfApp1
{
    public class Problem
    {
        public Problem()
        {
            Tests = new List<InAndOut>();
        }
        [XmlAttribute("Method")]
        public int Method
        {
            get;
            set;
        }
        [XmlAttribute("State")]
        public string State
        {
            get;
            set;
        }
        [XmlAttribute("Trytime")]
        public int Trytime
        {
            get;
            set;
        }
        [XmlAttribute("Checktime")]
        public int Checktime
        {
            get;
            set;
        }
        [XmlAttribute("Title")]
        public string Title
        {
            get;
            set;
        }
        [XmlIgnore]
        public int Limit
        {
            get;
            set;
        }
        [XmlIgnore]
        public Tiku Parent
        {
            get;
            set;
        }
        
        [XmlIgnore]
        public string Author
        {
            get;
            set;
        }
        [XmlIgnore]
        public string Desc
        {
            get;
            set;
        }
        [XmlIgnore]
        public string Input
        {
            get;
            set;
        }
        [XmlIgnore]
        public string Output
        {
            get;
            set;
        }
        [XmlIgnore]
        public string Timelimit
        {
            get;
            set;
        }
        [XmlIgnore]
        public List<InAndOut> Tests
        {
            get;
            set;
        }
       
    }
}
