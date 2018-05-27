using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// ChooseSolvespace.xaml 的交互逻辑
    /// </summary>
    public partial class ChooseSolvespace : Window
    {
        public Solvespace sp;
        public ObservableCollection<Solvespace> ss;
        public List<Problem> problems;
        public int currentproblem;//当前浏览试题编号
        public string currenttiku;
        public Classfile cff;
        public ChooseSolvespace(Object ob,Object od,Object oc,Object num,Object tiku)
        {
            sp = (Solvespace)ob;
            ss = (ObservableCollection<Solvespace>)od;
            problems = (List<Problem>)oc;
            currentproblem = (int)num;
            currenttiku = (string)tiku;
            InitializeComponent();
            if(sp == null)
            {
                rc.Content = "当前未选定工作空间";
                rc.IsEnabled = false;
            }
            else
            {
                rc.Content = sp.Name;
            }
            
            cb.ItemsSource = ss;
            cb.IsEnabled = false;
        }

        private void Checked_rc(object sender, RoutedEventArgs e)
        {
            
        }

        private void Unckecked_rc(object sender, RoutedEventArgs e)
        {
            
        }

        private void Checked_rcc(object sender, RoutedEventArgs e)
        {
            cb.IsEnabled = true;
        }

        private void Click_ok(object sender, RoutedEventArgs e)
        {
            if (rc.IsChecked == true)
            {
                string proname = problems[currentproblem].Title;
                Chachong cc = new Chachong();
                if (cc.Chazhao(proname, sp.Solvedproblem))
                {
                    MessageBox.Show("此题已在解决空间中创立");
                    return;
                }
                Solvedproblem spro = new Solvedproblem();
                spro.Name = proname;
                string name = spro.Name.Replace(" ", "_");
                spro.Parent = sp;
                spro.Tiku = currenttiku;
                spro.Path = sp.Path + "\\" + name + "_" + spro.Tiku;
                System.IO.Directory.CreateDirectory(spro.Path);
                sp.Solvedproblem.Add(spro);
                string text = "第1个解决方案";
                CreateNewSolution cns = new CreateNewSolution(spro,text);
                cns.ShowDialog();
                Classfile cf = new Classfile();
                cf.Name = "Main.cs";
                cf.Parent = spro.Solution[0];
                cf.Path = spro.Solution[0].Path + "\\" + cf.Name;
                File.Create(cf.Path);
                spro.Solution[0].Classfile.Add(cf);
                Reference rf = new Reference();
                rf.Name = "引用";
                rf.Parent = cf;
                cf.Reference.Add(rf);
                cff = cf;
                this.Close();
            }
            else if (rcc.IsChecked == true)
            {

                Solvespace spp = (Solvespace)cb.SelectedItem;
                string proname = problems[currentproblem].Title;
                Chachong cc = new Chachong();
                if (cc.Chazhao(proname, spp.Solvedproblem))
                {
                    MessageBox.Show("此题已在解决空间中创立");
                    return;
                }
                Solvedproblem spro = new Solvedproblem();
                spro.Name = proname;
                string name = spro.Name.Replace(" ", "_");
                spro.Parent = spp;
                spro.Tiku = currenttiku;
                spro.Path = spp.Path + "\\" + name + "_" + spro.Tiku;
                System.IO.Directory.CreateDirectory(spro.Path);
                spp.Solvedproblem.Add(spro);
                string text = "第1个解决方案";
                CreateNewSolution cns = new CreateNewSolution(spro,text);
                cns.ShowDialog();
                Classfile cf = new Classfile();
                cf.Name = "Main.cs";
                cf.Parent = spro.Solution[0];
                cf.Path = spro.Solution[0].Path + "\\" + cf.Name;
                File.Create(cf.Path);
                spro.Solution[0].Classfile.Add(cf);
                Reference rf = new Reference();
                rf.Name = "引用";
                rf.Parent = cf;
                cf.Reference.Add(rf);
                cff = cf;
                this.Close();
            }
            else
            {
                MessageBox.Show("请选择解题空间");
            }
        }
    }
}
