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
    /// CreateNewSolution.xaml 的交互逻辑
    /// </summary>
    public partial class CreateNewSolution : Window
    {
        private Solvedproblem sp;
        public bool ClickOrNot = false;
        public string solutionname = "";
        public CreateNewSolution(Object ob,string text)
        {
            sp = (Solvedproblem)ob;
            InitializeComponent();
            nametextbox.Text = text;
            solutionname = nametextbox.Text;
            nametextbox.SelectAll();
        }
        private void CreateNewSolution_Click(object sender, RoutedEventArgs e)
        {
            ClickOrNot = true;
            solutionname = nametextbox.Text;
            
            this.Close();
        }
    }
}
