using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HistorydataPolling.IServe
{

    //数据查询接口
    interface ISearchBase <T> where T :class
    {
      ObservableCollection <T> SearchDataForNeed(DateTime startTime, DateTime stopTime, string parameter, string whichPara);

    }
}
