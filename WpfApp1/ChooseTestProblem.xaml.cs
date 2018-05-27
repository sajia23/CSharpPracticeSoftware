using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace WpfApp1
{
    /// <summary>
    /// ChooseTestProblem.xaml 的交互逻辑
    /// </summary>
    public partial class ChooseTestProblem : Window
    {
        public string cspath;
        public string exepath = "";//用来决定最终测试的exe文件
        public Problem choosedproblem;//用来承载测试用例和关联他的父级tiku，但是这个problem和tiku中的problem不一样，只是可以查到父级
        public Classfile cff;//当前选定的类文件
        public Solution sll;//当前选定的解决方案
        public List<Problem> prs = new List<Problem>();//用来作为combox的原
        public ObservableCollection<Tiku> tk;
        public ObservableCollection<Solvespace> ss;
        public ObservableCollection<Solution> sls;//用来作为combox的原
        public bool dianjiguanbi;//用来标示是否是点击关闭按钮关闭的
        public ChooseTestProblem(Object ob, Object Oc, Object Od)
        {
            tk = (ObservableCollection<Tiku>)Oc;
            cff = (Classfile)ob;
            ss = (ObservableCollection<Solvespace>)Od;
            InitializeComponent();
            if (cff == null)
            {
                current.Content = "当前编辑的不是题目";
                current.IsEnabled = false;
                other.Content = "选择其他题目";

                if (tk.Count == 0)
                {
                    other.IsEnabled = false;
                }
                else
                {
                    other.IsEnabled = true;
                    
                }

                pr.IsEnabled = false;
                sl.IsEnabled = false;

            }
            else
            {
                current.Content = cff.Parent.Parent.Name + "(当前的解题方法)";
                current.IsEnabled = true;
                other.Content = "选择其他题目";

                if (tk.Count == 0)
                {
                    other.IsEnabled = false;
                }
                else
                {
                    other.IsEnabled = true;
                    
                }
                pr.IsEnabled = false;
                sl.IsEnabled = false;
            }
            solve1.IsEnabled = false;
            solve2.IsEnabled = false;
        }
        private void LoadProblems(ObservableCollection<Tiku> tk)
        {
            for (int i = 0; i < tk.Count; i++)
            {
                for (int j = 0; j < tk[i].problems.Count; j++)
                {
                    
                    prs.Add(tk[i].problems[j]);
                }
            }
            pr.ItemsSource = prs;
        }

        private void Other_Checked(object sender, RoutedEventArgs e)
        {
            LoadProblems(tk);
            pr.IsEnabled = true;
            
        }

        private void Current_Checked(object sender, RoutedEventArgs e)
        {
            LoadTest(cff);

            Solution sl = cff.Parent;

            for (int i = 0; i < sl.Classfile.Count; i++)
            {
                Classfile cf = sl.Classfile[i];
                if (cf.Name == "Main.cs")
                {
                    exepath = cf.Path.Replace(".cs", ".exe");
                    break;
                }
            }
            solve1.IsEnabled = false;
            solve2.IsEnabled = false;
        }
        /// <summary>
        /// 通过类文件加载测试用例
        /// </summary>
        /// <param name="cf"></param>
        private void LoadTest(Classfile cf)
        {
            Solvedproblem problem = cf.Parent.Parent;
            for (int i = 0; i < tk.Count; i++)
            {
                for (int j = 0; j < tk[i].problems.Count; j++)
                {
                    if (problem.Name == tk[i].problems[j].Title && problem.Tiku == tk[i].Name)
                    {
                        LoadTest(tk[i].problems[j]);
                    }
                }
            }
        }
        /// <summary>
        /// 通过路径和名字加载测试用例
        /// </summary>
        /// <param name="path"></param>
        /// <param name="name"></param>
        private void LoadTest(Problem prr)
        {
            String currenttiku = System.IO.Path.GetFileNameWithoutExtension(prr.Parent.Path);
            XDocument doc = XDocument.Load(prr.Parent.Path);
            IEnumerable<XElement> xmltikus = from problem in doc.Descendants("ProblemArchive") select problem;
            foreach (XElement e in xmltikus)
            {
                Console.WriteLine("yige");
                Problem pr = new Problem();
                pr.Title = (String)e.Element("Title");
                pr.Parent = prr.Parent;
                if (pr.Title == prr.Title)
                {
                    
                    pr.Author = (String)e.Element("Author");
                    XElement content = e.Element("Problem");
                    pr.Desc = (String)content.Element("Description");
                    pr.Input = (String)content.Element("InputSpec");
                    pr.Output = (String)content.Element("OutputSpec");
                    XElement Limit = e.Element("TestData");
                    pr.Limit = int.Parse((string)Limit.Element("TimeLimit"));
                    IEnumerable<XElement> testxml = from test in e.Descendants("TestCase") select test;
                    Console.WriteLine("shuchu test");
                    List<InAndOut> testcases = new List<InAndOut>();
                    foreach (XElement test in testxml)
                    {
                        Console.WriteLine("加入一个testcase");
                        testcases.Add(new InAndOut() { Input = (String)test.Element("TestInput"), Output = (String)test.Element("TestOutput") ,Parent = pr});
                    }
                    pr.Tests = testcases;
                    choosedproblem = pr;
                }
                else
                {

                }

            }

        }



        private void Pr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Problem prr = (Problem)pr.SelectedItem;
            LoadTest(prr);
            //将该题目下的解决方案加载到右边的listbox
            for(int i = 0; i < ss.Count; i ++)
            {
                for(int j = 0; j < ss[0].Solvedproblem.Count; j ++)
                {
                    if(ss[0].Solvedproblem[j].Name == prr.Title && ss[0].Solvedproblem[j].Tiku == prr.Parent.Name)
                    {
                        sls = ss[0].Solvedproblem[j].Solution;
                        sl.ItemsSource = sls;
                        sl.IsEnabled = true;
                    }
                }
            }
            
        }

        private void Sl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            sll = (Solution)sl.SelectedItem;
            solve1.IsEnabled = true;
            solve2.IsEnabled = true;
        }

        private void Solve1_Click(object sender, RoutedEventArgs e)
        {
            if(cff != null)
            {
                sll = cff.Parent;
            }
            for (int i = 0; i < sll.Classfile.Count; i++)
            {
                Classfile cf = sll.Classfile[i];
                if (cf.Name == "Main.cs")
                {
                    exepath = cf.Path.Replace(".cs", ".exe");
                    break;
                }
            }
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            dianjiguanbi = true;
        }

        private void Solve2_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog sfd = new OpenFileDialog();
            sfd.Title = "选择可执行文件路径";
            sfd.Filter = "EXE files (*.exe)| *.exe";
            sfd.ShowDialog();
            string path = sfd.FileName;
            if (path == "")
            {
                return;
            }
            exepath = path;
            this.Close();
        }
    }
}
