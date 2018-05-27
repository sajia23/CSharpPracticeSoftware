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
    /// Rename.xaml 的交互逻辑
    /// </summary>
    public partial class Rename : Window
    {
        private Classfile cf;
        
        public Rename(object df)
        {
            cf = (Classfile)df;
            
            InitializeComponent();
        }

        private void OnWindowStateChanged(object sender, EventArgs e)
        {
            if (WindowState != WindowState.Normal)
            {
                WindowState = WindowState.Normal;
            }
        }

        private void Rename_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("执行命名任务");
            cf.Path = cf.Path.Replace(cf.Name, renametextbox.Text);
            cf.Name = renametextbox.Text+".cs";
            Console.WriteLine(cf.Name);
            this.Close();
        }
    }
}
