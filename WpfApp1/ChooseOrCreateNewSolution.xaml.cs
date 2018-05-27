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
    /// ChooseOrCreateNewSolution.xaml 的交互逻辑
    /// </summary>
    public partial class ChooseOrCreateNewSolution : Window
    {
        public bool ClickOrNot = false;
        public string newsolution = "";
        public Solvedproblem sp;
        public Solution st;
        public ChooseOrCreateNewSolution(Object ob)
        {
            sp = (Solvedproblem)ob;
            InitializeComponent();
            newsolutiontext.IsReadOnly = true;
            oldSolution.IsEnabled = false;
            oldSolution.ItemsSource = sp.Solution;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(st == null && newsolution == "")
            {
                MessageBox.Show("请选择");
            }
            else
            {
                ClickOrNot = true;
                if (st == null)
                {
                    if (newsolution != "")
                    {
                        newsolution = newsolutiontext.Text;
                        Chachong cc = new Chachong();
                        if (cc.Chazhao(newsolution, sp.Solution))
                        {
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("该解题方法已存在");
                        }
                    }
                    else
                    {

                        this.Close();
                    }
                }
                else
                {
                    Console.WriteLine("打开解决方案");
                    this.Close();
                }
            }
                        
        }
        private void NewSolution_Checked(object sender, RoutedEventArgs e)
        {
            newsolutiontext.IsReadOnly = false;
            newsolutiontext.Text = "第" + (sp.Solution.Count + 1) + "个解决方案";
            st = null;
            oldSolution.IsEnabled = false;
        }

        private void OldSolution_Checked(object sender, RoutedEventArgs e)
        {
            oldSolution.IsEnabled = true;
            newsolution = "";
            newsolutiontext.Clear();
            newsolutiontext.IsEnabled = false;
        }

        private void OldSolution_SelectionChanged(object sender, RoutedEventArgs e)
        {
            st = (Solution)oldSolution.SelectedItem;
            Console.WriteLine(st.Name);
        }
    }
}
