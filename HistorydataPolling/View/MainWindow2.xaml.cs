using HistorydataPolling.Model;
using HistorydataPolling.Server.MainHandle;
using HistorydataPolling.Server.ThreeLevelLinkageDataItem;
using HistorydataPolling.ViewModel;
using MongoDB.Bson;
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
         
            //  frame.Content = new TestPage1();

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
        /// <summary>
        /// 选择卫星的combox控件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Combox_type_DropDownOpened(object sender, EventArgs e) //单击combox控件的事件
        {

            List<BsonDocument> temp = new List<BsonDocument>();

            ObservableCollection<SatinfoName> ResultList = new ObservableCollection<SatinfoName>();
            SatinfoAndParaGroup satinfo = new SatinfoAndParaGroup();
            temp = satinfo.getSatinfo();

            foreach (var item in temp)
            {
                ResultList.Add(new SatinfoName(item[0].ToString()));
            }
           
            combox_type.ItemsSource = ResultList;
        }


     

            //string d= combox_type.Text; //获得combox当前显示的数据








        private void BtnSeek_Click(object sender, RoutedEventArgs e)
        {
            string whichPara = null;
            List<BsonDocument> temp = new List<BsonDocument>();
            if (RadioButtonYC.IsChecked == true)
            {
                whichPara = "RadioButtonYC";
              
              //  frame.Content =new  YCPage();
                YCPage yCPage = new YCPage();
                yCPage.test();
                frame.Content = yCPage;
                yCPage.ParentWindow = this;
                
            }
            else if(RadioButtonZL.IsChecked == true)
            {
                whichPara = "RadioButtonZL";
            }
            //DateTime t1 = this.SelectedStartTime.SelectDateTime;
            //DateTime t2 = this.SelectedStopTime.SelectDateTime;

            //MainForButtonHandle buttonHandle = new MainForButtonHandle();
            //temp = buttonHandle.SearchDataForNeed(t1,t2,tBPara.Text, whichPara);

           
        }
    }

}
