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
            App.Current.Shutdown(); //强制退出调试模式
            
        }



        #endregion
        /// <summary>
        /// 选择卫星的combox控件
        ///string d= combox_type.Text; //获得combox当前显示的数据
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




        private void BtnSeek_Click(object sender, RoutedEventArgs e)
        {
            string whichPara= null;
            List<BsonDocument> temp = new List<BsonDocument>();
            if (string.IsNullOrEmpty(tBPara.Text))
            {
                MessageBox.Show("请输入查询信息！");
                return;
            }
            else if (RadioButtonZL.IsChecked == false || RadioButtonYC.IsChecked == false)
            {
                MessageBox.Show("请选择参数信息！");
                return;
            }
            if (RadioButtonYC.IsChecked == true)
            {
                whichPara = "RadioButtonYC".ToString();
                YCPage yCPage = new YCPage();
              
                frame.Content = yCPage;
                yCPage.ParentWindow = this;
                yCPage.GetTelemetryData(); //为遥测数据page赋值
            }
            else if(RadioButtonZL.IsChecked == true)
            {
                whichPara = "RadioButtonZL".ToString();
                ZLPage zlPage = new ZLPage();
                frame.Content = zlPage;
                zlPage.ParentWindow = this; //指定父级窗口
                zlPage.GetInstructionData(whichPara); //为遥测数据page赋值

            }
          

           
        }
    }

}
