using System;
using System.Collections.Generic;
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
    /// CreateNewProblem.xaml 的交互逻辑
    /// </summary>
    public partial class CreateNewProblem : Window
    {
        
        private Solvespace sp;
        public CreateNewProblem(Object ob)
        {
            sp = (Solvespace)ob;
            InitializeComponent();
        }
        private void CreateNewProblem_Click(object sender, RoutedEventArgs e)
        {
            Solvedproblem sproblem = new Solvedproblem();
            Chachong cc = new Chachong();
            if(cc.Chazhao(nametextbox.Text,sp.Solvedproblem))
            {
                MessageBox.Show("此工作空间下已有此题");
                return;
            }
            sproblem.Name = nametextbox.Text;
            sproblem.Tiku = tikutextbox.Text;
            sproblem.Parent = sp;
            sproblem.Path = sp.Path + "\\" + sproblem.Name;
            System.IO.Directory.CreateDirectory(sproblem.Path);
            sp.Solvedproblem.Add(sproblem);
            this.Close();
        }
    }
}
