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
    /// RenameForTiku.xaml 的交互逻辑
    /// </summary>
    public partial class RenameForTiku : Window
    {
        private Tiku tk;
        public RenameForTiku(Object ob)
        {
            tk = (Tiku)ob;
            InitializeComponent();
        }
        private void Rename_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("执行命名任务");
            tk.Name = renametextbox.Text;
            Console.WriteLine(tk.Name);
            this.Close();
        }
    }
}
