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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Forms = System.Windows.Forms;
using System.Windows.Input.Test;
using System.Windows.Threading;

using MessageBox = System.Windows.MessageBox;
using HistorydataPolling.fileCtrol;
using HistorydataPolling.Server.MainHandle;
using HistorydataPolling.Server.ThreeLevelLinkageDataItem;
using MongoDB.Bson;


using static HistorydataPolling.FileCtrol.Common.ff;
using HistorydataPolling.FileCtrol.Common;

namespace HistorydataPolling
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private void SendToUIThread(UIElement element, string text)
        {
            element.Dispatcher.BeginInvoke(new Action(() => { SendKeys.Send(element, text); }), DispatcherPriority.Input);
        }

        ObservableCollection<RegionInfo> regionInfoList = new ObservableCollection<RegionInfo>();//DataGrid的数据源
        ObservableCollection<RegionInfo> regionInfoSelectList = new ObservableCollection<RegionInfo>();//用于DataGrid的模板加载时提供选项
        public MainWindow()
        {
            InitializeComponent();

            SatinfoAndParaGroup satinfoAndParaGroup = new SatinfoAndParaGroup();
            List<BsonDocument> DisplyData = satinfoAndParaGroup.getSatinfoParaGroup();
            //DataGrid初始绑定数据                          
            if (DisplyData != null)
            {
                regionInfoList.Add(new RegionInfo(DisplyData[0][0].ToString(), (/*data1[0][1].ToString()*/DisplyData[0][5].ToString() + "-" + DisplyData[0][4].ToString()), (DisplyData[0][2].ToString() + "-" + DisplyData[0][3].ToString())));

                //regionInfoList.Add(new RegionInfo("山东省", "烟台市", "高新区"));
                //三级联动数据项
                foreach (var item in DisplyData)
                {
                    //  Console.WriteLine(item);

                    regionInfoSelectList.Add(new RegionInfo(item[0].ToString(), /*item[1].ToString()*/item[5].ToString() + "-" + item[4].ToString(), (item[2].ToString() + "-" + item[3].ToString())));
                  //  regionInfoSelectList.Add(new RegionInfo("yyyyyyy", "", ""));


                }

                //=========================Test================================//
                //regionInfoSelectList.Add(new RegionInfo("山东省", "烟台市", "高新区"));
                //regionInfoSelectList.Add(new RegionInfo("山东省", "烟台市", "莱山区"));
                //regionInfoSelectList.Add(new RegionInfo("山东省", "青岛", "四方区"));
                //regionInfoSelectList.Add(new RegionInfo("山东省", "临沂市", "沂水县"));
                //regionInfoSelectList.Add(new RegionInfo("湖北省", "武汉市", "江夏区"));
                //regionInfoSelectList.Add(new RegionInfo("湖北省", "武汉市", "武昌区"));

                dataGrid.ItemsSource = regionInfoList; //绑定数据源
            }
            else
            {
                MessageBox.Show("没有数据");
            }
        }

        /// <summary>
        /// ProvinceLoaded 省份下拉列表框初始化，绑定数据源
        /// </summary>
        void ProvinceLoaded(object sender, RoutedEventArgs e)
        {
            ComboBox curComboBox = sender as ComboBox;
            //为下拉控件绑定数据源，并选择原选项为默认选项
            string text = curComboBox.Text;
            //去除重复项查找，跟数据库连接时可以让数据库来实现
            var query = regionInfoSelectList.GroupBy(p => p.Province).Select(p => new { Province = p.FirstOrDefault().Province });
            int itemcount = 0;
            curComboBox.SelectedIndex = itemcount;
            foreach (var item in query.ToList())
            {
                if (item.Province == text)
                {
                    curComboBox.SelectedIndex = itemcount;
                    break;
                }
                itemcount++;
            }
            curComboBox.ItemsSource = query;
            curComboBox.IsDropDownOpen = true;//获得焦点后下拉
        }
        /// <summary>
        /// CityLoaded 市下拉列表框初始化，绑定数据源
        /// </summary>
        void CityLoaded(object sender, RoutedEventArgs e)
        {
            //获得当前选中项的省份信息
            string province = (dataGrid.SelectedItem as RegionInfo).Province;
            //查找选中省份下的市作为数据源
            var query = (from l in regionInfoSelectList
                         where (l.Province == province)
                         group l by l.City into grouped
                         select new { City = grouped.Key });
            ComboBox curComboBox = sender as ComboBox;
            //为下拉控件绑定数据源，并选择原选项为默认选项  
            string text = curComboBox.Text;
            //去除重复项查找，跟数据库连接时可以让数据库来实现
            int itemcount = 0;
            curComboBox.SelectedIndex = itemcount;
            foreach (var item in query.ToList())
            {
                if (item.City == text)
                {
                    curComboBox.SelectedIndex = itemcount;
                    break;
                }
                itemcount++;
            }
            curComboBox.ItemsSource = query;
            curComboBox.IsDropDownOpen = true;//获得焦点后下拉
        }
        /// <summary>
        /// AreaLoaded 区下拉列表框初始化，绑定数据源
        /// </summary>
        void AreaLoaded(object sender, RoutedEventArgs e)
        {
            string province = (dataGrid.SelectedItem as RegionInfo).Province;
            string city = (dataGrid.SelectedItem as RegionInfo).City;
            //查找选中省份下的市作为数据源
            var query = (from l in regionInfoSelectList
                         where (l.Province == province && l.City == city)
                         group l by l.Area into grouped
                         select new { Area = grouped.Key });

            ComboBox curComboBox = sender as ComboBox;
            //为下拉控件绑定数据源，并选择原选项为默认选项
            string text = curComboBox.Text;
            //去除重复项查找，跟数据库连接时可以让数据库来实现
            int itemcount = 0;
            curComboBox.SelectedIndex = itemcount;
            foreach (var item in query.ToList())
            {
                if (item.Area == text)
                {
                    curComboBox.SelectedIndex = itemcount;
                    break;
                }
                itemcount++;
            }
            curComboBox.ItemsSource = query;
            curComboBox.IsDropDownOpen = true;//获得焦点后下拉
        }

        /// <summary>
        /// ProvinceDropDownClosed 省份下拉列表框选择改变刷新
        /// </summary>
        private void ProvinceDropDownClosed(object sender, EventArgs e)
        {
            DataGridHelper.SetRealTimeCommit(dataGrid, true);//dataGrid为控件名称
            SendToUIThread(dataGrid, "{TAB}!");
        }
        /// <summary>
        /// CityDropDownClosed 市下拉列表框选择改变刷新
        /// </summary>
        private void CityDropDownClosed(object sender, EventArgs e)
        {
            DataGridHelper.SetRealTimeCommit(dataGrid, true);//dataGrid为控件名称
            SendToUIThread(dataGrid, "{TAB}!");
        }
        /// <summary>
        /// AreaDropDownClosed 区下拉列表框选择改变刷新
        /// </summary>
        private void AreaDropDownClosed(object sender, EventArgs e)
        {
            DataGridHelper.SetRealTimeCommit(dataGrid, true);//dataGrid为控件名称


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

        private void BtnSeek_Click(object sender, RoutedEventArgs e) //查询按钮处理 
        {

            // Wait wait = new Wait();
          
            ObservableCollection<ParaListory> paraResultList = new ObservableCollection<ParaListory>();
            /* ((this.FindName("DATA_GRIDpp")) as DataGrid)*/

            MainHandle handle = new MainHandle();
            DateTime t1 = this.SelectedStartTime.SelectDateTime;
            DateTime t2 = this.SelectedStopTime.SelectDateTime;


            TextBlock textBlock = DataGridHelper.GetVisualChild<TextBlock>(dataGrid, v => v.Name == "tb_ParaCode");
            string tbParaCode = textBlock.Text;
            if (null != textBlock)
            {
                //   MessageBox.Show(textBlock.Text);
            }
            paraResultList = handle.Handle(t1, t2, tbParaCode);
           
          //  wait.ShowDialog();
            if (paraResultList.Count >0)
            {
                DisplayValues.ItemsSource = paraResultList;  //显示查询结果

            }
            else
            {
                MessageBox.Show("没有查询到数据......");
            }







        }

        private void BtnExportExcel_Click(object sender, RoutedEventArgs e)
        {
            //string fullFileName = "";
            ////创建一个保存文件式的对话框
            //Forms.SaveFileDialog sfd = new Forms.SaveFileDialog();

            ////设置这个对话框的起始保存路径
            //sfd.InitialDirectory = @"C:\";

            ////设置保存的文件的类型，注意过滤器的语法
            //sfd.Filter = "所有文件(*xls*)|*.xls*";

            ////调用ShowDialog()方法显示该对话框，该方法的返回值代表用户是否点击了确定按钮
            //if (sfd.ShowDialog() == Forms.DialogResult.OK)
            //{
            //    //做一些工作 　
            //    string extensionName = System.IO.Path.GetExtension(sfd.FileName); //获取文件后缀名字
            //    if (extensionName == "")
            //    {
            //         fullFileName = sfd.FileName + ".xls";
            //    }
            //    else
            //    {
            //         fullFileName = sfd.FileName;
            //    }
            //  SaveFile save= new SaveFile();
            //   save.saveFile(fullFileName);
            //  ObservableCollection<ParaListory> DD = ShowValues.ShowProjectValueAndSourceValue();
            if (DisplayValues.Items.Count >0   )
            {
                ExportExcel.ExportDataGridSaveAs(true, this.DisplayValues);
               
            }
            else
            {
                MessageBox.Show("无法保存");
            }
            //ExportToExcel2 testExcel = new ExportToExcel2();
            //testExcel.Export(this.DATA_GRIDpp, "rrr");
         
        }

        //public static implicit operator Uri(TestPage1 v)
        //{
        //    throw new NotImplementedException();
        //}
        //  else
        // {
        //   MessageBox.Show("取消保存");
    }

    // }
    // }
}