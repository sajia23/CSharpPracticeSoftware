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
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace WpfApp1
{
    /// <summary>
    /// MyTabItem.xaml 的交互逻辑
    /// </summary>
    public partial class MyTabItem : TabItem
    {
        /// <summary>
        /// 父级TabControl
        /// </summary>
        public Classfile cf;
        private TabControl m_Parent;
        
        public MyTabItem(Object ob,Object oc)
        {
            cf = (Classfile)oc;
            m_Parent = (TabControl)ob;
            InitializeComponent();
            
        }
        
        /// <summary>
        /// 约定的宽度
        /// </summary>
        private double m_ConventionWidth = 100;

        
        #region 事件
        #region loaded
        /// <summary>
        /// loaded
        /// </summary>
        private void TabItem_Loaded(object sender, RoutedEventArgs e)
        {
            
            
            if (m_Parent != null)
                Load();
        }
        #endregion
        #region 关闭按钮
        /// <summary>
        /// 关闭按钮
        /// </summary>
        public void btn_Close_Click(object sender, RoutedEventArgs e)
        {
            Syntax sy = (Syntax)this.Content;
            if (cf.Name != "")
            {
                if (m_Parent == null)
                    return;
                //移除自身
                m_Parent.Items.Remove(this);
                //移除事件
                m_Parent.SizeChanged -= m_Parent_SizeChanged;

                //调整剩余项大小
                //保持约定宽度item的临界个数
                int criticalCount = (int)((m_Parent.ActualWidth - 5) / m_ConventionWidth);
                //平均宽度
                double perWidth = (m_Parent.ActualWidth - 5) / m_Parent.Items.Count;
                foreach (MyTabItem item in m_Parent.Items)
                {
                    if (m_Parent.Items.Count <= criticalCount)
                    {
                        item.Width = m_ConventionWidth;
                    }
                    else
                    {
                        item.Width = perWidth;
                    }
                }
                cf.Zaiti = null;
                sy.codeEditor.Document.SaveFile(cf.Path,ActiproSoftware.Text.LineTerminator.CarriageReturnNewline);
            }
            else
            {
                MessageBoxResult dr = System.Windows.MessageBox.Show("是否保存", "提示", MessageBoxButton.OKCancel, MessageBoxImage.Question);
                if (dr == MessageBoxResult.OK)
                {
                    sy.codeEditor.Document.SaveFile(cf.Path, ActiproSoftware.Text.LineTerminator.CarriageReturnNewline);    
                }
                
                
                if (m_Parent == null)
                    return;
                //移除自身
                m_Parent.Items.Remove(this);
                //移除事件
                if (m_Parent.HasItems)
                {

                }
                else
                {
                    MainWindow.diynum = 1;
                }
                m_Parent.SizeChanged -= m_Parent_SizeChanged;

                //调整剩余项大小
                //保持约定宽度item的临界个数
                int criticalCount = (int)((m_Parent.ActualWidth - 5) / m_ConventionWidth);
                //平均宽度
                double perWidth = (m_Parent.ActualWidth - 5) / m_Parent.Items.Count;
                foreach (MyTabItem item in m_Parent.Items)
                {
                    if (m_Parent.Items.Count <= criticalCount)
                    {
                        item.Width = m_ConventionWidth;
                    }
                    else
                    {
                        item.Width = perWidth;
                    }
                }
            }
            
        }
        #endregion
        #region 父级TabControl尺寸发生变化
        /// <summary>
        /// 父级TabControl尺寸发生变化
        /// </summary>
        private void m_Parent_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            //调整自身大小
            //保持约定宽度item的临界个数
            int criticalCount = (int)((m_Parent.ActualWidth - 5) / m_ConventionWidth);
            if (m_Parent.Items.Count <= criticalCount)
            {
                //小于等于临界个数 等于约定宽度
                this.Width = m_ConventionWidth;
            }
            else
            {
                //大于临界个数 等于平均宽度
                double perWidth = (m_Parent.ActualWidth - 5) / m_Parent.Items.Count;
                this.Width = perWidth;
            }
        }
        #endregion
        #endregion

        #region 方法
        #region Load
        /// <summary>
        /// Load
        /// </summary>
        private void Load()
        {
            //约定的宽度
            double.TryParse(m_Parent.Tag.ToString(), out m_ConventionWidth);
            //注册父级TabControl尺寸发生变化事件
            m_Parent.SizeChanged += m_Parent_SizeChanged;

            //自适应
            //保持约定宽度item的临界个数
            int criticalCount = (int)((m_Parent.ActualWidth - 5) / m_ConventionWidth);
            if (m_Parent.Items.Count <= criticalCount)
            {
                //小于等于临界个数 等于约定宽度
                this.Width = m_ConventionWidth;
            }
            else
            {
                //大于临界个数 每项都设成平均宽度
                double perWidth = (m_Parent.ActualWidth - 5) / m_Parent.Items.Count;
                foreach (MyTabItem item in m_Parent.Items)
                {
                    item.Width = perWidth;
                }
                this.Width = perWidth;
            }
        }
        #endregion
        #endregion
    }
}
