using HistorydataPolling.IServe;
using MongoDB.Bson;
using RedisplayHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HistorydataPolling.Server.MainHandle
{
    class MainForButtonHandle : ISearchBase
    {
        public List<BsonDocument> SearchDataForNeed( DateTime startTime, DateTime stopTime, string param,string whichPara)
        {
            List<BsonDocument> finallyData = new List<BsonDocument>();

            int t1 = UTCHelper.ConvertDateTimeToInt(startTime);
            int t2 = UTCHelper.ConvertDateTimeToInt(stopTime);
            //   Console.WriteLine(t1);
            // Console.WriteLine(t2);
            //ObservableCollection<ParaListory> paraResultList = ShowValues.ShowProjectValueAndSourceValue();
            //DisplayValues.ItemsSource = paraResultList;  //显示查询结果

            finallyData = TimeServer.GetBaseData(t1, t2, param, whichPara, 1000);

            return finallyData;

        }
    }
}
