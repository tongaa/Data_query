using HistorydataPolling.IServe;
using HistorydataPolling.ViewModel;
using MongoDB.Bson;
using RedisplayHelper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HistorydataPolling.Server.MainHandle
{
    class MainForButtonHandle : ISearchBase <InstructionParaListory>
    {
        public ObservableCollection<InstructionParaListory> SearchDataForNeed( DateTime startTime, DateTime stopTime, string param,string whichPara)
        {
            List<BsonDocument> finallyData = new List<BsonDocument>();
            ObservableCollection<InstructionParaListory> paraResultList = new ObservableCollection<InstructionParaListory>();

            int t1 = UTCHelper.ConvertDateTimeToInt(startTime);
            int t2 = UTCHelper.ConvertDateTimeToInt(stopTime);
            //   Console.WriteLine(t1);
            // Console.WriteLine(t2);
            //ObservableCollection<ParaListory> paraResultList = ShowValues.ShowProjectValueAndSourceValue();
            //DisplayValues.ItemsSource = paraResultList;  //显示查询结果

            finallyData = TimeServer.GetBaseData(t1, t2, param, whichPara, 1000);
            foreach (var item in finallyData)
            {
                paraResultList.Add(new InstructionParaListory(UTCHelper.ConvertIntToDateTime(item[1].ToDouble()), item[2].ToString(), item[3].ToString(), item[4].ToString()));  
            }


           

            return paraResultList;

        }
    }
}
