using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Xml.Linq;

namespace WpfApp1
{
    public class LiulanViewModel : INotifyPropertyChanged
    {
        private Problem pr;
        private int totalnum;
        private LastRecord lr = new LastRecord();
        public Problem Pr
        {
            get { return pr; }
            set
            {  
                pr = value;
                RaisePropertyChanged("Pr");
            }
        }
        
        public List<Problem> prs;
        public int currentnum = 0;
        public string currenttiku = "";
        
        public LiulanViewModel()
        {
            Console.WriteLine("ViewModel");
            InitialLast();
            InitialXmlProblem(lr.lastpath);
            
            Console.WriteLine(Pr.Title);
            totalnum = prs.Count;
            Console.WriteLine("ViewModle");
        }

        private void InitialLast()
        {
            string path = "resources\\last.xml";
            XDocument doc = XDocument.Load(path);
            IEnumerable<XElement> lasttiku = from LastTiku in doc.Descendants("LastTiku") select LastTiku;
            foreach (XElement lt in lasttiku)
            {
                lr.lastname = (string)lt.Attribute("Name");
                Console.WriteLine("jgkgkfgkf"+lr.lastname);
                Console.WriteLine("asdfasfsa"+(string)lt.Attribute("Path"));
                lr.lastpath = (string)lt.Attribute("Path");
                lr.lastpronum = (int)lt.Element("LastProblem");
            }
        }

        public void InitialXmlProblem(string file)
        {
            currenttiku = System.IO.Path.GetFileNameWithoutExtension(file);
            XDocument doc = XDocument.Load(file);
            //读取xml文件中所有的ProblemArchive节点
            IEnumerable<XElement> xmltikus = from problem in doc.Descendants("ProblemArchive") select problem;
            prs = new List<Problem>();
            foreach (XElement e in xmltikus)
            {
                Problem pr = new Problem();
                //读取problem节点下的节点
                pr.Title = (String)e.Element("Title");
                pr.Author = (String)e.Element("Author");
                XElement content = e.Element("Problem");
                pr.Desc = (String)content.Element("Description");
                pr.Input = (String)content.Element("InputSpec");
                pr.Output = (String)content.Element("OutputSpec");
                XElement Limit = e.Element("TestData");
                pr.Limit = int.Parse((string)Limit.Element("TimeLimit"));
                Console.WriteLine("XML limit" + (string)Limit.Element("TimeLimit"));
                Console.WriteLine("pr.Limit" + pr.Limit);
                IEnumerable<XElement> testxml = from test in e.Descendants("TestCase") select test;
                List<InAndOut> testcases = new List<InAndOut>();
                foreach (XElement test in testxml)
                {
                    testcases.Add(new InAndOut() { Input = (String)test.Element("TestInput"), Output = (String)test.Element("TestOutput"), Parent = pr });
                }
                pr.Tests = testcases;
                prs.Add(pr);

            }
            Pr = prs[lr.lastpronum];
        }

        public ICommand NextCommand 
         { 
             get { return new NextCommand(NextProblem); } 
         }

        public ICommand LastCommand
        {
            get { return new LastCommand(LastProblem); }
        }

        private void LastProblem()
        {
            currentnum--;
            if (currentnum <= 0)
            {
                currentnum = 0;
            }
            Pr = prs[currentnum];
        }

        public void NextProblem()
        {
            Console.WriteLine("下一题");
            currentnum++;
            
            if (currentnum > (totalnum - 1))
            {
                currentnum = totalnum - 1;
                
            }
            Pr = prs[currentnum];
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string propertyName)
        {
             // take a copy to prevent thread issues
             PropertyChangedEventHandler handler = PropertyChanged;
             if (handler != null)
             {
                 handler(this, new PropertyChangedEventArgs(propertyName));
             }
        }


    }
}
