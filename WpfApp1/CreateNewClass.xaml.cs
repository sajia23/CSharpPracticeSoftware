using System;
using System.Collections.Generic;
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
    /// CreateNewClass.xaml 的交互逻辑
    /// </summary>
    public partial class CreateNewClass : Window
    {
        private Solution sl;
        public CreateNewClass(Object ss)
        {
            sl = (Solution)ss;
            InitializeComponent();
        }

        private void CreateNewClass_Click(object sender, RoutedEventArgs e)
        {
            Classfile cf = new Classfile();
            Reference rf = new Reference();
            Chachong cc = new Chachong();
            if(cc.Chazhao(createclasstextbox.Text,sl.Classfile))
            {
                MessageBox.Show("当前解题方法中已有此类");
                return;
            }
            rf.Name = "引用";
            cf.Parent = sl;  
            cf.Reference.Add(rf);
            cf.Name = createclasstextbox.Text + ".cs";
            cf.Path = sl.Path + "\\" + cf.Name;
            using (File.Create(cf.Path))
            {

            }
            sl.Classfile.Add(cf);
            this.Close();
        }
    }
}
