using HistorydataPolling.Server.MainHandle;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HistorydataPolling.View
{
    /// <summary>
    /// ZLPage.xaml 的交互逻辑
    /// </summary>
    public partial class ZLPage : Page
    {

        MainWindow2 parentWindow = new MainWindow2();

        public MainWindow2 ParentWindow
        {
            get { return parentWindow; }
            set { parentWindow = value; }
        }
        public ZLPage()
        {
            InitializeComponent();
        }
        public void GetInstructionData(string  whichPara) //for 遥测数据 page显示
        {
            ObservableCollection<InstructionParaListory> paraResultList = new ObservableCollection<InstructionParaListory>();
           
            MainForButtonHandle handle = new MainForButtonHandle();

            // MainWindow2 parentWindow = new MainWindow2();

          //  string ParaCode = ParentWindow.tBPara.Text;
            string ParaCode = ParentWindow.ComboxPara.Text.Split('-')[0];
            DateTime t1 = ParentWindow.SelectedStartTime.SelectDateTime;
            DateTime t2 = this.parentWindow.SelectedStopTime.SelectDateTime;
            paraResultList = handle.SearchDataForNeed(t1, t2, ParaCode, whichPara);
            if (paraResultList.Count > 0)
            {
                DisplayInstructValues.ItemsSource = paraResultList;  //显示查询结果
            }
            else
            {
                MessageBox.Show("查询不到数据...");
            }

        }

    }
}
