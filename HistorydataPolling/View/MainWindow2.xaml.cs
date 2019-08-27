using HistorydataPolling.Common.FileCtrol.Common;
using HistorydataPolling.Model;
using HistorydataPolling.Server;
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
using static HistorydataPolling.FileCtrol.Common.ff;

namespace HistorydataPolling.View
{
    /// <summary>
    /// MainWindow2.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow2 : Window
    {
        public static ObservableCollection<RegisterDisplayParaInfo> tempCache = new ObservableCollection<RegisterDisplayParaInfo>();
        bool ready = false;
        bool ItemsClearFlag = false;
        bool ? RadioButtonYCState = false;
        bool ? RadioButtonZLState = false;
      public  string previousSatInfo = null;
        public MainWindow2()
        {
            // ComboxSearchHelp.ToComboxDisplay("","" );
           
            InitializeComponent();
            {
                List<BsonDocument> temp = new List<BsonDocument>();

                ObservableCollection<SatinfoName> ResultList = new ObservableCollection<SatinfoName>();
                SatinfoAndParaGroup satinfo = new SatinfoAndParaGroup();
                temp = satinfo.getSatinfo();

                foreach (var item in temp)
                {
                    ResultList.Add(new SatinfoName(item[0].ToString()));
                }
                
                this.ComboxSatInfo.ItemsSource = ResultList;

            }
            RegisterDisplayParaInfo test = new RegisterDisplayParaInfo("在此选择要查询的参数");
            this.ComboxPara.Items.Add(test);
            ItemsClearFlag = true; 
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
            //+++转移到窗口初始化中去完成 MainWindow2()+++
            //List<BsonDocument> temp = new List<BsonDocument>();

            //ObservableCollection<SatinfoName> ResultList = new ObservableCollection<SatinfoName>();
            //SatinfoAndParaGroup satinfo = new SatinfoAndParaGroup();
            //temp = satinfo.getSatinfo();

            //foreach (var item in temp)
            //{
            //    ResultList.Add(new SatinfoName(item[0].ToString()));
            //}
            //ComboxSatInfo.Text = ResultList[0].ToString();
            //ComboxSatInfo.ItemsSource = ResultList;
        }
       
        private void ComboxPara_DropDownOpened_1(object sender, EventArgs e)
        {
             string currentSatInfo = this.ComboxSatInfo.Text;
             

            if ((ready == true) && (previousSatInfo == currentSatInfo) )
            {
                if ((RadioButtonYC.IsChecked == true) == RadioButtonYCState )
                {

                    ComboxPara.ItemsSource = MainWindow2.tempCache;
                    return;
                }
                else if ((RadioButtonZL.IsChecked == true) == RadioButtonZLState)
                {
                    ComboxPara.ItemsSource = MainWindow2.tempCache;
                    return;
                }
                else
                {
                  
                }
            }
            List<BsonDocument> temp = new List<BsonDocument>();
          
            ObservableCollection<RegisterDisplayParaInfo> ResultList = new ObservableCollection<RegisterDisplayParaInfo>();
            ObservableCollection<RegisterDisplayParaInfo> tempCache = new ObservableCollection<RegisterDisplayParaInfo>();
            //SatinfoAndParaGroup satinfo = new SatinfoAndParaGroup();
            // temp = satinfo.getSatinfo();

            if (RadioButtonYC.IsChecked == true)
            {
                RadioButtonZLState = false;
                RadioButtonYCState = true;
               
                temp = ComboxSearchHelp.GetAllRemote(currentSatInfo, true);
                foreach (var item in temp)
                {
                    ResultList.Add(new RegisterDisplayParaInfo(item[0].ToString() + "-"+ item[1].ToString() + "-" + item[2].ToString() + "-" + item[3].ToString() ));


                }

            }
            else if(RadioButtonZL.IsChecked == true)
            {
                RadioButtonZLState = true;
                RadioButtonYCState = false;
                temp = ComboxSearchHelp.GetAllInstruct(currentSatInfo, true);
                foreach (var item in temp)
                {
                    ResultList.Add(new RegisterDisplayParaInfo(item[0].ToString() +"-"+ item[1].ToString()));


                }
            }

            if (ResultList.Count == 0)
            {
                MessageBox.Show("没有数据！");
                return;
            }
            if (ItemsClearFlag == true) //此时  ComboxPara还没有数据 仅是构造函数加载的提示数据
            {
                ComboxPara.Items.Clear(); //清空一下防止报错
                ItemsClearFlag = false;
            }
            MainWindow2.tempCache = ResultList;
            ready = true;
            previousSatInfo = currentSatInfo;
           
        
           ComboxPara.ItemsSource = MainWindow2.tempCache;
            
           
            


        }




        private void BtnSeek_Click(object sender, RoutedEventArgs e)
        {
            string whichPara= null;
            List<BsonDocument> temp = new List<BsonDocument>();
            //if (string.IsNullOrEmpty(tBPara.Text))
            if (string.IsNullOrEmpty(ComboxPara.Text))
            {
                //MessageBox.Show("请输入查询信息！");
                MessageBox.Show("请选择查询信息！");
                return;
            }
            else if (RadioButtonZL.IsChecked == false && RadioButtonYC.IsChecked == false)
            {
               // MessageBox.Show("请选择参数信息！");
                MessageBox.Show("请选择查询信息！");
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

        private void Btn_export2_Click(object sender, RoutedEventArgs e)
        {
           
            if (RadioButtonZL.IsChecked == true)
            {
                ZLPage zlPage = new ZLPage();
                zlPage = (ZLPage)frame.Content;
                if (zlPage.DisplayInstructValues.Items.Count > 0)
                {
                     //ExportExcel.ExportDataGridSaveAs(true, zlPage.DisplayInstructValues);  //此方法打开会报异常
                    DataGridRow rowContainer = (DataGridRow)zlPage.DisplayInstructValues.ItemContainerGenerator.ContainerFromIndex(0);
                    ExportToExcel2.EE(zlPage.DisplayInstructValues, "指令查询数据结果", "制表时间:"+ DateTime.Now.ToString());


                }
                else
                {
                    MessageBox.Show("没有数据！");
                }
            }
      
            else if (RadioButtonYC.IsChecked == true)
            {
                YCPage ycPage = new YCPage();
                ycPage = (YCPage)frame.Content;
                if (ycPage.DisplayValues.Items.Count > 0)
                {
                      // ExportExcel.ExportDataGridSaveAs(true, ycPage.DisplayValues);//此方法打开会报异常  
                    DataGridRow rowContainer = (DataGridRow)ycPage.DisplayValues.ItemContainerGenerator.ContainerFromIndex(0);
                    ExportToExcel2.EE(ycPage.DisplayValues, "遥测查询数据结果", "制表时间:" + DateTime.Now.ToString());


                }
                else
                {
                    MessageBox.Show("没有数据！");
                }
            }
            else
            {
                MessageBox.Show("无法保存");
            }
            //ExportToExcel2 testExcel = new ExportToExcel2();
            //testExcel.Export(this.DATA_GRIDpp, "rrr");

        }

       
    }
}


