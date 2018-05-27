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
    /// CheckDetail.xaml 的交互逻辑
    /// </summary>
    public partial class CheckDetail : Window
    {
        public List<Detail> details;
        public int shownum = 0;
        public Detail detail;
        public CheckDetail(Object ob)
        {

            details = (List<Detail>)ob;
            detail = details[shownum];
            Console.WriteLine("detail中的str" + detail.Shuru);
            Console.WriteLine("detail中的str" + detail.CorrectShuchu);
            Console.WriteLine("detail中的str" + detail.ExeShuchu);
            InitializeComponent();
            shurushuchu.DataContext = detail;
            ProblemTitle.Text = detail.problem.Title;
            TimeLimit.Text = "要求用时:"+(detail.problem.Limit).ToString() + "秒";
            Errorcode.Text = Choose(detail.ErrorCode);
            Num.Text = "第" + (shownum + 1) + "个";
        }

        private void Button_Click_Next(object sender, RoutedEventArgs e)
        {
            if(shownum == (details.Count - 1))
            {

            }
            else
            {
                shownum++;
                detail = details[shownum];
                shurushuchu.DataContext = detail;
                ProblemTitle.Text = detail.problem.Title;
                TimeLimit.Text = "要求用时:"+(detail.problem.Limit).ToString() + "秒";
                Errorcode.Text = Choose(detail.ErrorCode);
                Num.Text = "第" + (shownum + 1) + "个";
            }
            
        }

        private string Choose(int errorCode)
        {
            switch(errorCode)
            {
                case 1:
                    return "答案正确格式错误";
                    
                case 2:
                    return "答案错误";
                    
                case 3:
                    return "答案错误（输出为空）";
                    
                case 4:
                    return "程序运行出错";
                    
                case 5:
                    return "超时";
                    
                default:
                    return null;
            }
            
        }

        private void Button_Click_Last(object sender, RoutedEventArgs e)
        {
            if (shownum == 0)
            {

            }
            else
            {
                shownum--;
                detail = details[shownum];
                shurushuchu.DataContext = detail;
                ProblemTitle.Text = "要求用时:"+detail.problem.Title;
                TimeLimit.Text = (detail.problem.Limit).ToString() + "秒";
                Errorcode.Text = Choose(detail.ErrorCode);
                Num.Text = "第" + (shownum + 1) + "个";
            }
        }
    }
}
