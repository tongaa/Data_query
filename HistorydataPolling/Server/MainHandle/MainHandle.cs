using MongoDB.Bson;
using MongoDB.Driver;
using TimeTypeHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HistorydataPolling.Model;
using HistorydataPolling.Server;
using System.Globalization;
using HistorydataPolling.Server.ThreeLevelLinkageDataItem;
using RedisplayHelper;
using System.Collections.ObjectModel;


namespace HistorydataPolling.Server.MainHandle
{
    class MainHandle
    {
        public MainHandle()
        {

          //  MongoDBHelper<ParaStorage>.Init();

        }
        public ObservableCollection<ParaListory> Handle(DateTime startTime, DateTime stopTime, string tbParaCode)
        {
            List<BsonDocument> finallyData = new List<BsonDocument>();
           // ObservableCollection<ParaListory> paraList = new ObservableCollection<ParaListory>();
            ObservableCollection<ParaListory> paraResultList = new ObservableCollection<ParaListory>();

            int t1  = UTCHelper.ConvertDateTimeToInt(startTime);
            int t2  = UTCHelper.ConvertDateTimeToInt(stopTime);
         //   Console.WriteLine(t1);
           // Console.WriteLine(t2);
            //ObservableCollection<ParaListory> paraResultList = ShowValues.ShowProjectValueAndSourceValue();
            //DisplayValues.ItemsSource = paraResultList;  //显示查询结果

            finallyData =  TimeServer.TimeRangeJudgment(t1, t2, tbParaCode, 1000);


            foreach (var item in finallyData)
            {

                paraResultList.Add(new ParaListory(UTCHelper.ConvertIntToDateTime(item[4].ToDouble()), item[2].ToString(), item[3].ToString()));  //item[4] 时间
            }
           

            return paraResultList;




















            ///////////////////////////////////////////////////////////////////////////////////

        }
    }
}
