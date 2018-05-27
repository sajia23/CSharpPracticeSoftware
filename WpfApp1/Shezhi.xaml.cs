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
    /// Shezhi.xaml 的交互逻辑
    /// </summary>
    public partial class Shezhi : Window
    {
        ObservableCollection<Tiku> tikus;
        List<Problem> prs = new List<Problem>();
        int i = 1;
        public Shezhi(Object b)
        {
            tikus = (ObservableCollection<Tiku>)b;
            InitializeComponent();
            foreach(Tiku tk in tikus)
            {
                var tkd = tk;
                foreach(Problem pr in tkd.problems)
                {
                    prs.Add(pr);
                }
            }
            showtiku.ItemsSource = prs;
            foreach (FontFamily font in Fonts.SystemFontFamilies)
            {
                if(i <= 20)
                {
                    daxiao_bianji.Items.Add(i);
                    daxiao_liulan.Items.Add(i);
                    i++;
                }
                
                ziti_bianji.Items.Add(font.Source);
                ziti_liulan.Items.Add(font.Source);
                
            }
            ziti_liulan.SelectedItem = Properties.Settings.Default.liulanziti;
            ziti_bianji.SelectedItem = Properties.Settings.Default.bianjiziti;
            daxiao_bianji.SelectedItem = Properties.Settings.Default.bianjidaxiao;
            daxiao_liulan.SelectedItem = Properties.Settings.Default.liulandaxiao;
        }

        private void Ziti_liulan_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Properties.Settings.Default.liulanziti = (string)ziti_liulan.SelectedValue;
            
            Properties.Settings.Default.Save();
            Console.WriteLine("ff" + Properties.Settings.Default.liulanziti);
        }
        private void Ziti_bianji_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Properties.Settings.Default.bianjiziti = (string)ziti_bianji.SelectedValue;

            Properties.Settings.Default.Save();
            Console.WriteLine("ff" + Properties.Settings.Default.bianjiziti);
        }
        private void Daxiao_liulan_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Properties.Settings.Default.liulandaxiao = (int)daxiao_liulan.SelectedValue;

            Properties.Settings.Default.Save();
            Console.WriteLine("ff" + Properties.Settings.Default.liulandaxiao);
        }
        private void Daxiao_bianji_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Properties.Settings.Default.bianjidaxiao = (int)daxiao_bianji.SelectedValue;

            Properties.Settings.Default.Save();
            Console.WriteLine("ff" + Properties.Settings.Default.bianjidaxiao);
        }
    }
}
