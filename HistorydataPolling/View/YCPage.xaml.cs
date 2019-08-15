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



//备用

namespace HistorydataPolling.View
{
    /// <summary>
    /// TestPage1.xaml 的交互逻辑
    /// </summary>
    public partial class YCPage : Page
    {
        MainWindow2 parentWindow = new MainWindow2();

        public MainWindow2 ParentWindow
        {
            get { return parentWindow; }
            set { parentWindow = value; }
        }


        public YCPage()
        {
            InitializeComponent();

         

        }

        public void test()
        {
            ObservableCollection<ParaListory> paraResultList = new ObservableCollection<ParaListory>();
            /* ((this.FindName("DATA_GRIDpp")) as DataGrid)*/

            MainHandle handle = new MainHandle();

            // MainWindow2 parentWindow = new MainWindow2();

            string ParaCode = ParentWindow.tBPara.Text;
            DateTime t1 = ParentWindow.SelectedStartTime.SelectDateTime;
            DateTime t2 = this.parentWindow.SelectedStopTime.SelectDateTime;
            paraResultList = handle.Handle(t1, t2, ParaCode);
            if (paraResultList.Count > 0)
            {
                DisplayValues.ItemsSource = paraResultList;  //显示查询结果
            }
            else
            {
                MessageBox.Show("没有查询到数据......");
            }

        }




    }





#if false
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
            //DateTime t1 = this.SelectedStartTime.SelectDateTime;
            //DateTime t2 = this.SelectedStopTime.SelectDateTime;


            TextBlock textBlock = DataGridHelper.GetVisualChild<TextBlock>(dataGrid, v => v.Name == "tb_ParaCode");
            string tbParaCode = textBlock.Text;
            if (null != textBlock)
            {
                //   MessageBox.Show(textBlock.Text);
            }
            //paraResultList = handle.Handle(t1, t2, tbParaCode);
           
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

#endif
}



