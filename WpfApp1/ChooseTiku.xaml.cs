using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// ChooseTiku.xaml 的交互逻辑
    /// </summary>
    public partial class ManageTiku : Window
    {
        public string path;
        ObservableCollection<Tiku> tikus = new ObservableCollection<Tiku>();
        
        public ManageTiku(Object ob)
        {
            tikus = (ObservableCollection<Tiku>)ob;
            InitializeComponent();
            listview.ItemsSource = tikus;
            Console.WriteLine("tikus里的tiku的数目:" + tikus.Count);
            Console.WriteLine("tikus里的tiku的数目:" + tikus[0].problems.Count);
        }
        private void Showtiku_Click(object sender, RoutedEventArgs e)
        {
            Tiku tiku = (Tiku)listview.SelectedItem;
            path = tiku.Path;
            this.Close();
        }

        private void Selected(object sender, SelectionChangedEventArgs e)
        {
            showtiku.ItemsSource = null;
            
            Tiku tiku = (Tiku)listview.SelectedItem;
            Console.WriteLine("tiku中含有的题目数量:" + tiku.problems.Count);
            showtiku.ItemsSource = tiku.problems;
        }
    }
}
