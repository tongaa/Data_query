using HistorydataPolling.Model;
using HistorydataPolling.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HistorydataPolling.View
{
    /// <summary>
    /// MainWindow2.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow2 : Window
    {
        public MainWindow2()
        {
            InitializeComponent();
          
            
            frame.Content = new TestPage1();

        }



        #region 窗口操作
        private void MoveGrid(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }
        private void CloseWindow(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }



        #endregion


        //public class SatInfomations : ObservableCollection<SatInfomation>
        //{
        //    public SatInfomations()
        //    {
        //        this.Add(new SatInfomation {  SatName = "林鸟" });
        //        this.Add(new SatInfomation {  SatName = "小胡" });
        //        this.Add(new SatInfomation {  SatName = "小字" });
             
        //    }
        //}
        /// <summary>
        /// 选择卫星的combox控件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Combox_type_DropDownOpened(object sender, EventArgs e) //单击combox控件的事件
        {
            Console.WriteLine("1213225");
            ObservableCollection<SatinfoName> ResultList = new ObservableCollection<SatinfoName>();
            
            ResultList.Add(new SatinfoName("g1"));
            ResultList.Add(new SatinfoName("g2"));
            ResultList.Add(new SatinfoName("g3"));
            ResultList.Add(new SatinfoName("g4"));
           
            combox_type.ItemsSource = ResultList;
        }

      
    }

}
