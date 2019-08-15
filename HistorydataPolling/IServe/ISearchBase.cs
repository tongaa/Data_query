using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HistorydataPolling.IServe
{

    //数据查询接口
    interface ISearchBase
    {
     List<BsonDocument> SearchDataForNeed(DateTime startTime, DateTime stopTime, string parameter, string whichPara);

    }
}
