using System;
using System.CodeDom.Compiler;

using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Microsoft.Win32;

using System.Windows;
using System.Windows.Controls;

using System.Windows.Input;
using System.Windows.Media;

using System.Xml.Linq;
using System.Collections.ObjectModel;
using System.Xml.Serialization;
using ActiproSoftware.Text;

namespace WpfApp1
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private string tikupath = "";
        
        private string cscPath;//csc编译器路径

        /// <summary>
        /// 有关加载试题的变量
        /// </summary>
        
        public static List<Problem> problems;
        ObservableCollection<Tiku> tikus = new ObservableCollection<Tiku>();//加载总题库时
        List<InAndOut> testcases = new List<InAndOut>();
        private Solvespace solvespace;//当前的工作空间

        int totalNum;//当前一个题库中含有的总题数
        private string currenttiku;
        List<Detail> details;//用来存储检查结果的
        /// <summary>
        /// 有关加载解决方案的变量
        /// </summary>
        public ObservableCollection<Solvespace> ss = new ObservableCollection<Solvespace>();
        /// <summary>
        /// 有关avalonedit的变量
        /// </summary>
        public static int diynum = 1;//用编写时的标签页编号
        
        public MainWindow()
        {
            InitializeComponent();
            Console.WriteLine("MainWindow");
            cscPath = FindCsc();
            InitialXmlSolved();
            LoadTikuList();//加载题库列表
            if(ss.Count == 1)
            {
                solvespace = ss[0];
            }
            
        }
        
        /// <summary>
        /// 加载题库到泛型tikus
        /// 
        /// </summary>
        private void LoadTikuList()
        {
            try
            {
                XDocument doc = XDocument.Load("resources\\tiku.xml");
                //读取xml文件中所有的Tiku节点
                IEnumerable<XElement> xmltikus = from tiku in doc.Descendants("Tiku") select tiku;
                foreach (XElement e in xmltikus)
                {
                    Tiku tk = new Tiku();
                    //读取tiku节点的属性
                    tk.Name = (string)e.Attribute("Name");
                    tk.Path = (string)e.Attribute("Path");
                    IEnumerable<XElement> xmlproblems = from problem in e.Descendants("Problem") select problem;
                    foreach (XElement eproblem in xmlproblems)
                    {
                        Problem problem = new Problem();
                        problem.Parent = tk;
                        problem.Title = (string)eproblem.Attribute("Title");
                        problem.State = (string)eproblem.Attribute("State");
                        problem.Trytime = (int)eproblem.Attribute("Trytime");
                        problem.Checktime = (int)eproblem.Attribute("Checktime");
                        problem.Method = (int)eproblem.Attribute("Method");
                        tk.problems.Add(problem);
                    }
                    tikus.Add(tk);
                }
            }
            catch
            {
                MessageBox.Show("未找到资源文件tiku.xml");
            }
            
        }
        /// <summary>
        /// 将解决的问题泛型写到xml文件中
        /// </summary>
        private void SolvedClass2Xml()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<Solvespace>));
            TextWriter writer = new StreamWriter("C:\\Users\\yuesh\\Desktop\\123.xml");
            serializer.Serialize(writer, ss);
            writer.Close();
        }
        /// <summary>
        /// 将xml文件中的解决方案加载到泛型中
        /// </summary>
        private void InitialXmlSolved()
        {           
            XDocument doc = XDocument.Load("resources\\123.xml");
            //读取xml文件中所有的Solvespace节点
            IEnumerable<XElement> xmlss = from solvespce in doc.Descendants("Solvespace") select solvespce;
            Console.WriteLine("xmlss的数量为" + xmlss.Count() + "个"); 
            foreach(XElement e in xmlss)
            {
                Solvespace solvespace = new Solvespace();
                //读取Solvespace节点的属性
                solvespace.Path = (String)e.Attribute("Path");
                solvespace.Name = (String)e.Attribute("Name");
                Console.WriteLine("solvespace的Name" + (String)e.Attribute("Name"));
                solvespace.Path = (String)e.Attribute("Path");
                Console.WriteLine("solvespace的Path" + (String)e.Attribute("Path"));
                //读取xml文件中所有的Problem节点
                IEnumerable<XElement> xmlproblem = from problem in e.Descendants("Problem") select problem;
                ObservableCollection<Solvedproblem> solvedproblems = new ObservableCollection<Solvedproblem>();
                foreach (XElement eproblem in xmlproblem)
                {
                    Solvedproblem sp = new Solvedproblem();
                    sp.Name = (String)eproblem.Attribute("Name");
                    sp.Path = (String)eproblem.Attribute("Path");
                    sp.Tiku = (String)eproblem.Attribute("Tiku");
                    sp.Parent = solvespace;
                    ObservableCollection<Solution> solutions = new ObservableCollection<Solution>();
                    IEnumerable<XElement> xmlsolution = from esolution in eproblem.Descendants("Solvelution") select esolution;
                    foreach(XElement esolution in xmlsolution)
                    {
                        Solution solution = new Solution();
                        solution.Parent = sp;
                        solution.Name = (String)esolution.Attribute("Name");
                        solution.Path = (String)esolution.Attribute("Path");
                        IEnumerable<XElement> xmlclassfile = from classfile in esolution.Descendants("Classfile") select classfile;
                        ObservableCollection<Classfile> classfiles = new ObservableCollection<Classfile>();
                        foreach (XElement eclassfile in xmlclassfile)
                        {
                            Classfile cf = new Classfile();
                            cf.Parent = solution;
                            cf.Name = (String)eclassfile.Attribute("Name");
                            cf.Path = (String)eclassfile.Attribute("Path");
                            IEnumerable<XElement> xmlreference = from reference in eclassfile.Descendants("Reference") select reference;
                            ObservableCollection<Reference> references = new ObservableCollection<Reference>();
                            foreach (XElement ereference in xmlreference)
                            {
                                Reference rf = new Reference();
                                rf.Parent = cf;
                                rf.Name = (String)ereference.Attribute("Name");
                                IEnumerable<XElement> xmlref = from refs in ereference.Descendants("Ref") select refs;
                                ObservableCollection<Ref> referencess = new ObservableCollection<Ref>();
                                foreach (XElement eref in xmlref)
                                {
                                    Ref refs = new Ref();
                                    refs.Parent = rf;
                                    refs.Name = (String)eref.Attribute("Name");
                                    refs.Path = (String)eref.Attribute("Path");
                                    referencess.Add(refs);
                                }
                                rf.References = referencess;
                                Console.WriteLine("收集了" + xmlref.Count()+"个ref");
                                references.Add(rf);
                            }
                            Console.WriteLine("收集了" + xmlreference.Count() + "个reference");
                            cf.Reference = references;
                            classfiles.Add(cf);

                        }
                        solution.Classfile = classfiles;
                        solutions.Add(solution);                 
                    }
                    sp.Solution = solutions;
                    solvedproblems.Add(sp);
                }
                solvespace.Solvedproblem = solvedproblems;
                ss.Add(solvespace);
            }
            treeview.ItemsSource = ss;
        }

        private void InitialXmlProblem(string file)
        {
            currenttiku = System.IO.Path.GetFileNameWithoutExtension(file);
            XDocument doc = XDocument.Load(file);
            //读取xml文件中所有的ProblemArchive节点
            IEnumerable<XElement> xmltikus =from problem in doc.Descendants("ProblemArchive") select problem;
            problems = new List<Problem>();
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
                    testcases.Add(new InAndOut() { Input = (String)test.Element("TestInput"), Output = (String)test.Element("TestOutput"), Parent = pr});
                }
                pr.Tests = testcases;
                problems.Add(pr);
               
            }
            totalNum = problems.Count;
        }

        private void Button_Click_compile(object sender, RoutedEventArgs e)
        {
            if(tabcontrol.HasItems)
            {
                MyTabItem ti = (MyTabItem)tabcontrol.SelectedItem;
                Syntax sy = (Syntax)tabcontrol.SelectedContent;
                if(ti.cf.Name != "")
                {
                    string classpath = ti.cf.Path;

                    sy.codeEditor.Document.SaveFile(classpath, LineTerminator.CarriageReturnNewline);
                    string cspath = ti.cf.Parent.Path;//所有cs文件的路径;
                    Console.WriteLine(ti.Header);

                    //递归查询所有的引用
                    string refpath = "";
                    Solution sl = ti.cf.Parent;
                    bool first = true;
                    List<Classfile> cfs = new List<Classfile>();
                    for (int i = 0; i < cfs.Count; i++)
                    {
                        if (cfs[i].Reference[0].References.Count != 0)
                        {
                            for (int j = 0; j < cfs[i].Reference[0].References.Count; j++)
                            {
                                Ref rf = cfs[i].Reference[0].References[j];
                                if (first)
                                {
                                    refpath = refpath + "/r:";
                                    refpath = refpath + rf.Path;
                                    first = false;
                                }
                                else
                                {
                                    refpath = refpath + ";" + rf.Path;
                                }

                            }
                        }
                    }

                    string outpath = "/out:" + cspath + "\\Main.exe";
                    cspath = cspath + "\\*.cs";
                    Process p = new Process();
                    //设置要启动的应用程序
                    p.StartInfo.FileName = cscPath;
                    Console.WriteLine("refpath"+refpath);
                    Console.WriteLine(outpath);
                    Console.WriteLine(cspath);
                    p.StartInfo.Arguments = refpath + " " + outpath + " " + cspath;
                    //是否使用操作系统shell启动
                    p.StartInfo.UseShellExecute = false;
                    //输出信息
                    p.StartInfo.RedirectStandardOutput = true;
                    // 输出错误
                    p.StartInfo.RedirectStandardError = true;
                    //不显示程序窗口
                    p.StartInfo.CreateNoWindow = true;
                    //启动程序
                    p.Start();
                    //获取输出信息
                    string strOuput = p.StandardOutput.ReadToEnd();
                    string strError = p.StandardError.ReadToEnd();
                    p.Close();
                    tb.Text = strOuput;
                    tb.ScrollToEnd();
                    
                }
                else
                {
                    string cspath = ti.cf.Path;
                    sy.codeEditor.Document.SaveFile(cspath, LineTerminator.CarriageReturnNewline);
                    string outpath = cspath.Replace(".cs",".exe");
                    string errorindex = System.IO.Path.GetFileNameWithoutExtension(ti.cf.Path);
                    Process p = new Process();
                    //设置要启动的应用程序
                    p.StartInfo.FileName = cscPath;
                    p.StartInfo.Arguments =  "/t:exe"+ " /out:" + outpath + " " + cspath;
                    //是否使用操作系统shell启动
                    p.StartInfo.UseShellExecute = false;
                    //输出信息
                    p.StartInfo.RedirectStandardOutput = true;
                    // 输出错误
                    p.StartInfo.RedirectStandardError = true;
                    //不显示程序窗口
                    p.StartInfo.CreateNoWindow = true;
                    //启动程序
                    p.Start();
                    //获取输出信息
                    string strOuput = p.StandardOutput.ReadToEnd();
                    string strError = p.StandardError.ReadToEnd();
                    p.Close();
                    tb.Text = strOuput;
                    tb.ScrollToEnd();
                    
                }
                
            }
            else
            {

            }
            
        }

        private void Button_Click_test(object sender, RoutedEventArgs e)
        {
            if(tabcontrol.HasItems == true)
            {
                MyTabItem mti = (MyTabItem)tabcontrol.SelectedItem;
                Classfile cf = mti.cf;
                if (cf.Name != "")
                {
                    ChooseTestProblem ctp = new ChooseTestProblem(cf, tikus, ss);
                    ctp.ShowDialog();
                    int correctNum = 0;
                    
                    string exepath = ctp.exepath;
                    if(exepath != "")
                    {
                        Console.WriteLine("用于测试的exe路径" + exepath);
                        Problem pr = ctp.choosedproblem;
                        Console.WriteLine(pr.Parent.Name);
                        int testcases = pr.Tests.Count;
                        details = new List<Detail>();
                        for (int j = 0; j < testcases; j++)
                        {
                            Detail dt = new Detail();
                            correctNum += TestOneCase(dt,exepath, pr.Tests[j],pr.Limit);
                        }
                        Result rs = new Result(pr.Title, details, testcases, correctNum);
                        rs.ShowDialog();
                        
                    }
                    else if(ctp.dianjiguanbi == true)
                    {
                        return;
                    }
                    else
                    {
                        MessageBox.Show("选定的可执行文件有问题");
                    }
                }
                else
                {
                    MessageBox.Show("当前选定的自由编写不支持提交,请将代码完成并编译为可执行文件后进行单独提交");
                }
            }
            else
            {
                Classfile cf = null;
                ChooseTestProblem ctp = new ChooseTestProblem(cf, tikus, ss);
                ctp.ShowDialog();
                int correctNum = 0;

                string exepath = ctp.exepath;
                Console.WriteLine("exepath:" + exepath);
                if (exepath != "")
                {
                    Console.WriteLine("用于测试的exe路径" + exepath);
                    Problem pr = ctp.choosedproblem;
                    Console.WriteLine(pr.Parent.Name);
                    int testcases = pr.Tests.Count;
                    details = new List<Detail>();
                    for (int j = 0; j < testcases; j++)
                    {
                        Detail dt = new Detail();
                        correctNum += TestOneCase(dt,exepath, pr.Tests[j],pr.Limit);
                    }
                    Result rs = new Result(pr.Title, details,testcases,correctNum);
                    rs.ShowDialog();
                    
                }
                else if (ctp.dianjiguanbi == true)
                {
                    return;
                }
                else
                {
                    MessageBox.Show("选定的可执行文件有问题");
                }
            }
            
            
        }
        //根据传入的数据进行测试
        private int TestOneCase(Detail dt,string exePath,InAndOut iao,int limit)
        {
            string Input = iao.Input;//测试用例输入
            string Output = iao.Output;//测试用例输出
            try
            {
                Process p = new Process();
                //设置要启动的应用程序
                p.StartInfo.FileName = exePath;
                //是否使用操作系统shell启动
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.RedirectStandardInput = true;
                //输出信息
                p.StartInfo.RedirectStandardOutput = true;
                //输出错误
                p.StartInfo.RedirectStandardError = true;
                //不显示程序窗口
                p.StartInfo.CreateNoWindow = true;
                //启动程序
                p.Start();
                string strOuput = "";
                StreamWriter myStreamWriter = p.StandardInput;
                myStreamWriter.WriteLine(Input);
                myStreamWriter.Close();
                Console.WriteLine("本题题库的输入" + Input);
                Console.WriteLine("本题题库的输出" + Output);
                //获取测试程序的输出信息
                if (p.WaitForExit(limit*1000))
                {
                    //等待题目要求的时间，如果这段时间内程序执行完成则执行下一步
                    strOuput = p.StandardOutput.ReadToEnd();
                }
                else
                {
                    //如果程序在这段时间内没有完成，则超时判负
                    p.Kill();
                    dt.Shuru = Input;
                    dt.CorrectShuchu = Output;
                    dt.ExeShuchu = strOuput;
                    dt.problem = iao.Parent;
                    dt.ErrorCode = 5;//5代表超时
                    Console.WriteLine("错误代码：" + dt.ErrorCode);
                    details.Add(dt);
                    return 0;
                }
                //处理格式：1.处理题库中的输出，处理为无格式输出
                char[] OuputChar = Output.ToCharArray();
                int j = 0;
                for (int i = 0; i < (OuputChar.Length); i++)
                {
                    
                    if((int)OuputChar[i] == 13 || (int)OuputChar[i] == 10)
                    {

                    }
                    else
                    {
                        OuputChar[j] = OuputChar[i];
                        j++;
                    }
                    
                }
                char[] OuputChars = new char[j];
                for(int i = 0; i < j; i ++)
                {
                    OuputChars[i] = OuputChar[i];
                }
                //得到正确答案的无格式版本
                string tikuOutNoFormat = new string(OuputChars);
                //处理格式：2.处理程序的输出
                char[] strOuputChar = strOuput.ToCharArray();
                int jj = 0;
                if (strOuputChar.Length != 0)
                {
                    //2.1 将程序的输出处理为无格式，只检查答案是否正确
                    for (int i = 0; i < (strOuputChar.Length); i++)
                    {
                        if ((int)strOuputChar[i] == 13 || (int)strOuputChar[i] == 10)
                        {

                        }
                        else
                        {
                            strOuputChar[jj] = strOuputChar[i];
                            jj++;
                        }

                    }
                    char[] strOuputChars = new char[jj];
                    for (int i = 0; i < jj; i++)
                    {
                        strOuputChars[i] = strOuputChar[i];
                    }
                    string exeOutNoFormat = new string(strOuputChars);
                    Console.WriteLine("exe无格式输出" + exeOutNoFormat);
                    if (tikuOutNoFormat == exeOutNoFormat)
                    {
                        //如果答案正确，继续下一步
                        jj = 0;
                        strOuputChar = strOuput.ToCharArray();
                        for (int i = 0; i < (strOuputChar.Length); i++)
                        {
                            //2.1 将程序的输出处理为有格式，检查格式是否正确
                            if ((int)strOuputChar[i] == 13)
                            {

                            }
                            else
                            {
                                strOuputChar[jj] = strOuputChar[i];
                                jj++;
                            }
                        }
                        char[] strOuputCharss = new char[jj];
                        for (int i = 0; i < jj; i++)
                        {
                            strOuputCharss[i] = strOuputChar[i];
                        }
                        string exeOutFormat = new string(strOuputCharss);
                        Console.WriteLine("exe处理后有格式输出" + exeOutFormat);
                        if(exeOutFormat == Output)
                        {
                            Console.WriteLine("答案正确");
                            return 1;
                        }
                        else
                        {
                            dt.Shuru = Input;
                            dt.CorrectShuchu = Output;
                            dt.ExeShuchu = strOuput;
                            dt.problem = iao.Parent;
                            dt.ErrorCode = 1;//1代表格式错误但答案正确
                            Console.WriteLine("错误代码：" + dt.ErrorCode);
                            details.Add(dt);
                            return 0;
                        }    
                    }
                    else
                    {
                        dt.Shuru = Input;
                        dt.CorrectShuchu = Output;
                        dt.ExeShuchu = strOuput;
                        dt.problem = iao.Parent;
                        dt.ErrorCode = 2;//2代表答案错误
                        details.Add(dt);
                        Console.WriteLine("错误代码：" + dt.ErrorCode);
                        return 0;
                    }
                }
                else
                {
                    dt.Shuru = Input;
                    dt.CorrectShuchu = Output;
                    dt.ExeShuchu = strOuput;
                    dt.problem = iao.Parent;
                    details.Add(dt);
                    dt.ErrorCode = 3;//代表输出为空
                    Console.WriteLine("错误代码：" + dt.ErrorCode);
                    return 0;
                }
            }
            catch(Exception e)
            {
                dt.Shuru = Input;
                dt.CorrectShuchu = Output;
                dt.ExeShuchu = "";
                dt.problem = iao.Parent;
                dt.ErrorCode = 4;//代表程序运行出错
                Console.WriteLine("错误代码：" + dt.ErrorCode);
                details.Add(dt);
                string cuowu = "简单的错误信息:" + e.Message + "\n" + "详细的错误信息" + e.StackTrace;
                tb.Text = cuowu;
                return 0;
            }    
        }

        //寻找csc可执行文件
        private string FindCsc()
        {
            decimal length = 0;
            string file = "";
            string[] files = System.IO.Directory.GetFiles(@"C:\Windows\Microsoft.NET", "csc.exe", System.IO.SearchOption.AllDirectories);
            for (int i = 0; i < files.Length; i++)
            {
                Console.WriteLine(files[i]);
                System.IO.FileInfo fileInfo = null;
                try
                {
                    fileInfo = new System.IO.FileInfo(files[i]);
                }
                catch (Exception e)
                {
                    Console.WriteLine("出现异常：" + e.Message);
                    // 其他处理异常的代码
                }
                // 如果文件存在
                if (fileInfo != null && fileInfo.Exists)
                {
                    System.Diagnostics.FileVersionInfo info = System.Diagnostics.FileVersionInfo.GetVersionInfo(files[i]);
                    if (fileInfo.Length >= length)
                    {
                        length = fileInfo.Length;
                        file = files[i];
                        Console.WriteLine("目前最大为" + length);
                        Console.WriteLine("该文件路径为" + file);
                    }
                }
            }
            return file;
        }
        //用来隐藏试题浏览界面
        private void Button_Click_qiehuan(object sender, RoutedEventArgs e)
        {
            //hh.Visibility = Visibility.Collapsed;
            if((string)hide.Content == "隐藏")
            {
                c1.Width = new GridLength(0, GridUnitType.Pixel);
                //c2.Width = new GridLength(0, GridUnitType.Pixel);
                c1.MinWidth = 0;
                c1.MaxWidth = 0;
                hide.Content = "显示";
            }
            else
            {
                
                c1.Width = new GridLength(400, GridUnitType.Pixel);
                //c2.Width = new GridLength(5, GridUnitType.Pixel);
                c1.MinWidth = 400;
                c1.MaxWidth = Convert.ToInt32(window.ActualWidth.ToString()) * 0.6;
                //c3.Width = new GridLength((int)(Convert.ToInt32(window.ActualWidth.ToString())*0.6), GridUnitType.Pixel);
                //c3.MinWidth = 400.0;

                hide.Content = "隐藏";
            }
            
        }

        private void Button_Click_tiku(object sender, RoutedEventArgs e)
        {
            ManageTiku choose = new ManageTiku(tikus);
            choose.ShowDialog();
            tikupath = choose.path;
            if(tikupath != null)
            {
                LiulanViewModel llv = (LiulanViewModel)liulantab.DataContext;
                llv.InitialXmlProblem(tikupath);
            }  
        }

        private void Button_Click_solve(object sender, RoutedEventArgs e)
        {
            LiulanViewModel llv = (LiulanViewModel)liulantab.DataContext;
            int pronumshow = llv.currentnum;
            currenttiku = llv.currenttiku;
            problems = llv.prs;
            if(ss.Count == 0)
            {
                MessageBox.Show("请先创建工作空间");
            }
            else if(ss.Count > 1)
            {
                ChooseSolvespace css = new ChooseSolvespace(solvespace, ss, problems, pronumshow, currenttiku);
                css.ShowDialog();
                CreateNewTabItem(css.cff);
            }
            else
            {
                //这种情况是在已解决的问题中查到了想要解决的问题
                string proname = problems[pronumshow].Title;
                Chachong cc = new Chachong();
                if(cc.Chazhao(proname, ss[0].Solvedproblem))
                {
                    ChooseOrCreateNewSolution ccns = new ChooseOrCreateNewSolution(cc.problemfound);
                    ccns.ShowDialog();
                    if(ccns.ClickOrNot)
                    {
                        if(ccns.st == null)
                        {
                            Solution sl = new Solution();
                            sl.Name = ccns.newsolution;
                            sl.Parent = cc.problemfound;
                            sl.Path = cc.problemfound.Path + "\\" + sl.Name;
                            System.IO.Directory.CreateDirectory(sl.Path);
                            cc.problemfound.Solution.Add(sl);
                            Classfile cf = new Classfile();
                            cf.Name = "Main.cs";
                            cf.Parent = cc.problemfound.Solution[0];
                            cf.Path = cc.problemfound.Solution[0].Path + "\\" + cf.Name;
                            using (File.Create(cf.Path))
                            {

                            }
                            cc.problemfound.Solution[0].Classfile.Add(cf);
                            Reference rf = new Reference();
                            rf.Name = "引用";
                            rf.Parent = cf;
                            cf.Reference.Add(rf);
                            CreateNewTabItem(cf);
                        }
                        else
                        {
                            for( int i = 0; i < ccns.st.Classfile.Count; i ++)
                            {
                                if(ccns.st.Classfile[i].Name == "Main")
                                {
                                    Classfile cf = ccns.st.Classfile[i];
                                    CreateNewTabItem(cf);
                                }
                                else
                                {
                                    Classfile cf = ccns.st.Classfile[0];
                                    CreateNewTabItem(cf);
                                }
                            }
                            
                        }
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    string text = "第一个解决方案";
                    CreateNewSolution cns = new CreateNewSolution(cc.problemfound,text);
                    cns.ShowDialog();
                    if (cns.ClickOrNot == true)
                    {
                        Solvedproblem spro = new Solvedproblem();
                        spro.Name = problems[pronumshow].Title;
                        string name = spro.Name.Replace(" ", "_");
                        spro.Parent = ss[0];
                        spro.Tiku = currenttiku;
                        spro.Path = ss[0].Path + "\\" + name + "_" + spro.Tiku;
                        System.IO.Directory.CreateDirectory(spro.Path);
                        ss[0].Solvedproblem.Add(spro);
                        Solution sl = new Solution();
                        sl.Name = cns.solutionname;
                        sl.Parent = spro;
                        sl.Path = spro.Path + "\\" + sl.Name;
                        System.IO.Directory.CreateDirectory(sl.Path);
                        spro.Solution.Add(sl);
                        Classfile cf = new Classfile();
                        cf.Name = "Main.cs";
                        cf.Parent = spro.Solution[0];
                        cf.Path = spro.Solution[0].Path + "\\" + cf.Name;
                        using (File.Create(cf.Path))
                        {

                        }
                        spro.Solution[0].Classfile.Add(cf);
                        Reference rf = new Reference();
                        rf.Name = "引用";
                        rf.Parent = cf;
                        cf.Reference.Add(rf);
                        CreateNewTabItem(cf);
                    }
                }
                
            }
            SolvedClass2Xml();
        }

        private void Button_Click_run(object sender, RoutedEventArgs e)
        {
            if(tabcontrol.HasItems)
            {
                MyTabItem mti = (MyTabItem)tabcontrol.SelectedItem;
                if(mti.cf.Name != "")
                {
                    string exepath = mti.cf.Path.Replace(".cs", ".exe");
                    if (System.IO.File.Exists(exepath))//查看文件是否存在
                    {
                        
                        Process p = new Process();
                        //设置要启动的应用程序
                        p.StartInfo.FileName = exepath;
                        p.Start();
                    }
                    else
                    {
                        MessageBox.Show("当前编辑的文件可能还未编译！");
                    }
                    
                }
                else
                {
                    string exepath = mti.cf.Path.Replace(".cs", ".exe");
                    if (System.IO.File.Exists(exepath))//查看文件是否存在
                    {

                        Process p = new Process();
                        //设置要启动的应用程序
                        p.StartInfo.FileName = exepath;
                        p.Start();
                    }
                    else
                    {
                        MessageBox.Show("当前编辑的文件可能还未编译！");
                    }
                }
            }
            else
            {

            }
            
        }

        private void Button_Click_diy(object sender, RoutedEventArgs e)
        {
            Classfile cf = new Classfile();
            cf.Name = "";
            //cf.Path = "C:\\Users\\yuesh\\Desktop\\cs\\diy\\diy" + diynum + ".cs";
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "选择文件的保存路径";
            sfd.Filter = "C# files (*.cs)| *.cs";
            sfd.ShowDialog();
            string path = sfd.FileName;
            if (path == "")
            {
                return;
            }

            using (FileStream fsWrite = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write))
            {
                byte[] buffer = { };
                fsWrite.Write(buffer, 0, buffer.Length);
            }
            cf.Path = path;
            CreateNewTabItem(cf);
        }

        private void Help_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("代码及详细使用信息见项目地址:https://github.com/sajia23/CSharpPracticeSoftware \n" +
                "问题反馈:yueshaohua23@126.com");
        }

        private void Treeview_PreviewMouseRightDown(object sender, MouseButtonEventArgs e)
        {
            var treeViewItem = VisualUpwardSearch<System.Windows.Controls.TreeViewItem>(e.OriginalSource as DependencyObject) as System.Windows.Controls.TreeViewItem;
            if (treeViewItem != null)
            { 
                treeViewItem.Focus();
                Console.WriteLine("选中的节点的名字" + treeview.SelectedItem.GetType().Name);
                string selecttype = treeview.SelectedItem.GetType().Name;
                if(selecttype == "Solvespace")
                {
                    Console.WriteLine(selecttype);
                    solvespace = (Solvespace)treeview.SelectedItem;
                    
                    //Console.WriteLine("wocaonima" + solvespace.Name);
                    ContextMenu contextmenu = new ContextMenu();
                    MenuItem menuItem = new MenuItem();
                    menuItem.Header = "新增题目";
                    menuItem.Click += new RoutedEventHandler(CreateNewProblem_Click);
                    MenuItem menuItem2 = new MenuItem();
                    menuItem2.Header = "删除工作空间";
                    menuItem2.Click += new RoutedEventHandler(DeleteSpace_Click);
                    contextmenu.Items.Add(menuItem);
                    contextmenu.Items.Add(menuItem2);
                    treeViewItem.ContextMenu = contextmenu;
                }
                else if(selecttype == "Solvedproblem")
                {
                    solvespace = ((Solvedproblem)treeview.SelectedItem).Parent;
                    Console.WriteLine(selecttype);
                    ContextMenu contextmenu = new ContextMenu();
                    MenuItem menuItem = new MenuItem();
                    menuItem.Header = "新增解题方法";
                    menuItem.Click += new RoutedEventHandler(CreateNewSolution_Click);
                    MenuItem menuItem2 = new MenuItem();
                    menuItem2.Header = "删除题目";
                    menuItem2.Click += new RoutedEventHandler(DeleteProblem_Click);
                    contextmenu.Items.Add(menuItem);
                    contextmenu.Items.Add(menuItem2);
                    treeViewItem.ContextMenu = contextmenu;
                }
                else if(selecttype == "Solution")
                {
                    solvespace = ((Solution)treeview.SelectedItem).Parent.Parent;
                    Console.WriteLine(selecttype);
                    ContextMenu contextmenu = new ContextMenu();
                    MenuItem menuItem1 = new MenuItem();
                    menuItem1.Header = "新建类";
                    menuItem1.Click += new RoutedEventHandler(CreateNewClass_Click);
                    contextmenu.Items.Add(menuItem1);
                    MenuItem menuItem2 = new MenuItem();
                    menuItem2.Header = "删除全部类";
                    menuItem2.Click += new RoutedEventHandler(DeleteAllClass_Click);
                    contextmenu.Items.Add(menuItem2);
                    MenuItem menuItem3 = new MenuItem();
                    menuItem3.Header = "删除该解题方法";
                    menuItem3.Click += new RoutedEventHandler(DeleteSolution_Click);
                    contextmenu.Items.Add(menuItem3);
                    treeViewItem.ContextMenu = contextmenu;
                }
                else if(selecttype == "Classfile")
                {
                    solvespace = ((Classfile)treeview.SelectedItem).Parent.Parent.Parent;
                    Classfile cf = (Classfile)treeview.SelectedItem;
                    Console.WriteLine("这个类的父节点为" + cf.Parent.Name);
                    Console.WriteLine(selecttype);
                    ContextMenu contextmenu = new ContextMenu();
                    MenuItem menuItem1 = new MenuItem();
                    menuItem1.Header = "打开";
                    menuItem1.Click += new RoutedEventHandler(OpenClass_Click);
                    contextmenu.Items.Add(menuItem1);
                    
                    MenuItem menuItem2 = new MenuItem();
                    menuItem2.Header = "重命名";
                    menuItem2.Click += new RoutedEventHandler(Rename_Click);
                    contextmenu.Items.Add(menuItem2);
                    treeViewItem.ContextMenu = contextmenu;
                    
                    MenuItem menuItem3 = new MenuItem();
                    menuItem3.Header = "删除";
                    menuItem3.Click += new RoutedEventHandler(Delete_Click);
                    contextmenu.Items.Add(menuItem3);
                    
                    treeViewItem.ContextMenu = contextmenu;
                }
                else if(selecttype == "Reference")
                {
                    solvespace = ((Reference)treeview.SelectedItem).Parent.Parent.Parent.Parent;
                    Console.WriteLine(selecttype);
                    ContextMenu contextmenu = new ContextMenu();
                    MenuItem menuItem = new MenuItem();
                    menuItem.Header = "添加引用";
                    menuItem.Click += new RoutedEventHandler(AddReference_Click);
                    contextmenu.Items.Add(menuItem);
                    treeViewItem.ContextMenu = contextmenu;
                }
                else if(selecttype == "Ref")
                {
                    solvespace = ((Ref)treeview.SelectedItem).Parent.Parent.Parent.Parent.Parent;
                    Console.WriteLine(selecttype);
                    ContextMenu contextmenu = new ContextMenu();
                    MenuItem menuItem = new MenuItem();
                    menuItem.Header = "删除引用";
                    menuItem.Click += new RoutedEventHandler(DeleteReference_Click);
                    contextmenu.Items.Add(menuItem);
                    treeViewItem.ContextMenu = contextmenu;
                }
                else
                {

                }
                e.Handled = true;
            }
        }

        private void DeleteProblem_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult dr = System.Windows.MessageBox.Show("是否删除该题目", "提示", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if (dr == MessageBoxResult.OK)
            {
                Solvedproblem sp = (Solvedproblem)treeview.SelectedItem;
                treeview.IsEnabled = false;
                if (Directory.Exists(sp.Path))
                {
                    DirectoryInfo dir = new DirectoryInfo(sp.Path);
                    dir.Delete(true);
                }
                sp.Parent.Solvedproblem.Remove(sp);
                treeview.IsEnabled = true;
            }
            SolvedClass2Xml();
        }

        private void DeleteSpace_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult dr = System.Windows.MessageBox.Show("是否删除该工作空间，如果内容重要请自行备份", "提示", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if (dr == MessageBoxResult.OK)
            {
                Solvespace sp = (Solvespace)treeview.SelectedItem;
                treeview.IsEnabled = false;
                if (Directory.Exists(sp.Path))
                {
                    DirectoryInfo dir = new DirectoryInfo(sp.Path);
                    dir.Delete(true);
                }
                //Directory.Delete(sl.Path);

                ss.Remove(sp);
                treeview.IsEnabled = true;
            }
            SolvedClass2Xml();
        }

        private void DeleteSolution_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult dr = System.Windows.MessageBox.Show("是否删除该解决方案", "提示", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if (dr == MessageBoxResult.OK)
            {
                Solution sl = (Solution)treeview.SelectedItem;
                treeview.IsEnabled = false;
                if (Directory.Exists(sl.Path))
                {
                    DirectoryInfo dir = new DirectoryInfo(sl.Path);
                    dir.Delete(true);
                }
                sl.Parent.Solution.Remove(sl);
                treeview.IsEnabled = true;
            }
            SolvedClass2Xml();
        }

        private void DeleteReference_Click(object sender, RoutedEventArgs e)
        {
            Ref rff = (Ref)treeview.SelectedItem;
            Reference rf = rff.Parent;
            rf.References.Remove(rff);
            SolvedClass2Xml();
        }

        private void AddReference_Click(object sender, RoutedEventArgs e)
        {
            Reference rf = (Reference)treeview.SelectedItem;
            Ref rff = new Ref();
            rff.Parent = rf;
            System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog();
            openFileDialog.Title = "请选择需要的引用";
            openFileDialog.Filter = "DLL files (*.dll)| *.dll";
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                rff.Path = openFileDialog.FileName;
                rff.Name = Path.GetFileNameWithoutExtension(rff.Path);
                rf.References.Add(rff);
                SolvedClass2Xml();
            }
            if(rf.Parent.Zaiti != null)
            {
                Syntax sy = (Syntax)rf.Parent.Zaiti.Content;
                
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            Classfile cf = (Classfile)treeview.SelectedItem;
            
            MessageBoxResult dr = MessageBox.Show("是否删除这个类", "提示", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if (dr == MessageBoxResult.OK)
            {

                Solution sl = cf.Parent;
                sl.Classfile.Remove(cf);
                if(cf.Zaiti != null)
                {
                    cf.Zaiti.btn_Close_Click(cf.Zaiti, e);
                }
                
                if (File.Exists(cf.Path))
                {
                    File.Delete(cf.Path);
                }
            }
            SolvedClass2Xml();
        }

        private void Rename_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("重命名了");
            Classfile cf = (Classfile)treeview.SelectedItem;
            Rename rename = new Rename(cf);
            
            rename.ShowDialog();
            Console.WriteLine("主窗口中的名字为:" + cf.Name);
            SolvedClass2Xml();
        }

        private void OpenClass_Click(object sender, RoutedEventArgs e)
        {
            Classfile cf = (Classfile)treeview.SelectedItem;
            CreateNewTabItem(cf);
        }

        private void DeleteAllClass_Click(object sender, RoutedEventArgs e)
        {
            Solution sl = (Solution)treeview.SelectedItem;
            Console.WriteLine("删除全部类");
            MessageBoxResult dr = MessageBox.Show("是否删除全部类", "提示", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if (dr == MessageBoxResult.OK)
            {
                for(int i = 0; i < sl.Classfile.Count; i ++)
                {
                    sl.Classfile[i].Zaiti.btn_Close_Click(sl.Classfile[i].Zaiti, e);
                }
                sl.Classfile.Clear();
                DirectoryInfo dir = new DirectoryInfo(sl.Path);
                FileSystemInfo[] fileinfo = dir.GetFileSystemInfos();  //返回目录中所有文件和子目录
                foreach (FileSystemInfo i in fileinfo)
                {
                    if (i is DirectoryInfo)            //判断是否文件夹
                    {
                        DirectoryInfo subdir = new DirectoryInfo(i.FullName);
                        subdir.Delete(true);          //删除子目录和文件
                    }
                    else
                    {
                        File.Delete(i.FullName);      //删除指定文件
                    }
                }
            }
            SolvedClass2Xml();
        }

        private void CreateNewClass_Click(object sender, RoutedEventArgs e)
        {
            Solution sl = (Solution)treeview.SelectedItem;
            Console.WriteLine("新建类");
            CreateNewClass cnc = new CreateNewClass(sl);

            cnc.ShowDialog();
            Console.WriteLine("主窗口中的名字为:" + cnc.Name);
            SolvedClass2Xml();
        }

        private void CreateNewSolution_Click(object sender, RoutedEventArgs e)
        {
            Solvedproblem sp = (Solvedproblem)treeview.SelectedItem;
            Console.WriteLine("新建解题方法名称");
            string text = "第" + (sp.Solution.Count + 1) + "个解决方案";
            CreateNewSolution cns = new CreateNewSolution(sp,text);
            cns.ShowDialog();
            if(cns.ClickOrNot)
            {
                Solution sl = new Solution();
                sl.Name = cns.solutionname;
                sl.Parent = sp;
                sl.Path = sp.Path + "\\" + sl.Name;
                System.IO.Directory.CreateDirectory(sl.Path);
                sp.Solution.Add(sl);
            }
            SolvedClass2Xml();
        }

        private void CreateNewProblem_Click(object sender, RoutedEventArgs e)
        {
            
            Solvespace space = (Solvespace)treeview.SelectedItem;
            CreateNewProblem cnp = new CreateNewProblem(space);
            cnp.ShowDialog();
            SolvedClass2Xml();
        }

        static DependencyObject VisualUpwardSearch<T>(DependencyObject source)
        {
            while (source != null && source.GetType() != typeof(T))
                source = VisualTreeHelper.GetParent(source);

            return source;
        }

        private void AddSpace_Click(Object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            folderBrowserDialog1.Description = "新建的文件夹名字中不得含有空格和其他非法字符";
            string spacepath;
            if (folderBrowserDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                spacepath = folderBrowserDialog1.SelectedPath;
                Console.WriteLine(spacepath); 
                String[] splitStrings = spacepath.Split('\\');
                string space = splitStrings[splitStrings.Length - 1];
                Console.WriteLine(space);
                Solvespace sp = new Solvespace();
                sp.Name = space;
                sp.Path = spacepath;
                ss.Add(sp);
                SolvedClass2Xml();
            }
            
        }

        private void SaveSpace_Click(Object sender, RoutedEventArgs e)
        {
            if(tabcontrol.HasItems == true)
            {
                ItemCollection ic = tabcontrol.Items;
                for( int i = 0; i < ic.Count; i ++)
                {
                    MyTabItem item = (MyTabItem)ic[i];
                    Syntax sy = (Syntax)item.Content;
                    sy.codeEditor.Document.SaveFile(item.cf.Path, ActiproSoftware.Text.LineTerminator.CarriageReturnNewline);
                }
            }
            else
            {
                MessageBox.Show("当前编辑区未打开类文件");
            }
        }

        private void OpenShezhi_Click(Object sender, RoutedEventArgs e)
        {
            Shezhi sz = new Shezhi(tikus);
            sz.ShowDialog();
        }

        private void Treeview_PreviewMouseLeftDown(object sender, MouseButtonEventArgs e)
        {
            var treeViewItem = VisualUpwardSearch<System.Windows.Controls.TreeViewItem>(e.OriginalSource as DependencyObject) as System.Windows.Controls.TreeViewItem;
            if (treeViewItem != null)
            {
                treeViewItem.Focus();
                Console.WriteLine("选中的节点的名字" + treeview.SelectedItem.GetType().Name);
                string selecttype = treeview.SelectedItem.GetType().Name;
                if (selecttype == "Solvespace")
                {
                    Console.WriteLine(selecttype);
                    solvespace = (Solvespace)treeview.SelectedItem;

                }
                else if (selecttype == "Solvedproblem")
                {
                    solvespace = ((Solvedproblem)treeview.SelectedItem).Parent;
                    Console.WriteLine(selecttype);

                }
                else if (selecttype == "Solution")
                {
                    solvespace = ((Solution)treeview.SelectedItem).Parent.Parent;
                    Console.WriteLine(selecttype);

                }
                else if (selecttype == "Classfile")
                {
                    solvespace = ((Classfile)treeview.SelectedItem).Parent.Parent.Parent;

                }
                else if (selecttype == "Reference")
                {
                    solvespace = ((Reference)treeview.SelectedItem).Parent.Parent.Parent.Parent;

                }
                else if (selecttype == "Ref")
                {
                    solvespace = ((Ref)treeview.SelectedItem).Parent.Parent.Parent.Parent.Parent;

                }
                else
                {

                }
                e.Handled = false;
            }
            
        }

        private void CreateNewTabItem(Classfile cf)
        {
            MyTabItem item = new MyTabItem(tabcontrol,cf);
            cf.Zaiti = item;
            item.Margin = new Thickness(0, 0, 0, 0);
            item.Height = 24;
            Syntax sy = new Syntax(cf);
            if (cf.Name != "")
            {
                item.Header = string.Format("{0}", cf.Name);//Header作为类的名字
                item.ToolTip = string.Format("{0}-{1}", cf.Parent.Parent.Name, cf.Parent.Name);
                sy.codeEditor.Document.LoadFile(cf.Path);
                
               
            }
            else
            {
                item.Header = "自由编写" + diynum;
                item.ToolTip = "自由编写" + diynum;
                diynum++;
                
            }
            item.Content = sy;
            item.IsSelected = true;
            tabcontrol.Items.Add(item);
        }  
    }
}
