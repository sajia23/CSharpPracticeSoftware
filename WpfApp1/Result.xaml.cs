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
    /// Result.xaml 的交互逻辑
    /// </summary>
    public partial class Result : Window
    {
        List<Detail> details;
        public Result(string name,Object ob,int num1,int num2)
        {
            details = (List<Detail>)ob;
            InitializeComponent();
            Name.Text = name;
            Results.Text = "测试用例共" + num1 + "个,你答对了" + num2 + "个";
            if(details.Count != 0)
            {
                chde.IsEnabled = true;
            }
            else
            {
                chde.IsEnabled = false;
            }
        }

        private void Check_Click(object sender, RoutedEventArgs e)
        {
            
            CheckDetail cd = new CheckDetail(details);
            cd.ShowDialog();
            this.Close();
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
